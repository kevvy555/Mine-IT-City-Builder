using System;
using MineIT.CityBuilder.Core.Ids;
using MineIT.CityBuilder.Persistence.Binary;
using MineIT.CityBuilder.Simulation.Contracts;
using MineIT.CityBuilder.Simulation.Random;
using MineIT.CityBuilder.Simulation.Scheduling;

namespace MineIT.CityBuilder.Persistence.Kernel
{
    public readonly struct SchedulerPersistenceState
    {
        public SchedulerPersistenceState(
            bool paused,
            SimulationCommand[] commands,
            ulong commandNextSequence,
            ScheduledEvent[] events,
            ulong eventNextSequence)
        {
            Paused = paused;
            Commands = commands ?? Array.Empty<SimulationCommand>();
            CommandNextSequence = commandNextSequence;
            Events = events ?? Array.Empty<ScheduledEvent>();
            EventNextSequence = eventNextSequence;
        }

        public bool Paused { get; }
        public SimulationCommand[] Commands { get; }
        public ulong CommandNextSequence { get; }
        public ScheduledEvent[] Events { get; }
        public ulong EventNextSequence { get; }
    }

    public static class IdAllocatorSectionCodec
    {
        public const uint SchemaVersion = 1;

        public static byte[] Encode(SaveEntityIdAllocator allocator)
        {
            var writer = new LittleEndianWriter();
            writer.WriteUInt64(allocator.WorldInstanceId);
            var entries = allocator.ExportState();
            writer.WriteUInt32(checked((uint)entries.Length));

            for (var i = 0; i < entries.Length; i++)
            {
                writer.WriteUInt16(entries[i].EntityType);
                writer.WriteUInt64(entries[i].NextSequence);
            }

            return writer.ToArray();
        }

        public static SaveEntityIdAllocator Decode(byte[] data)
        {
            var reader = new LittleEndianReader(data);
            var allocator = new SaveEntityIdAllocator(reader.ReadUInt64());
            var count = reader.ReadUInt32();
            if (count > 65535)
            {
                throw new System.IO.InvalidDataException("Allocator entry count is unreasonable.");
            }

            var entries = new SaveEntityAllocatorEntry[count];
            for (var i = 0; i < entries.Length; i++)
            {
                entries[i] = new SaveEntityAllocatorEntry(
                    reader.ReadUInt16(),
                    reader.ReadUInt64());
            }

            if (reader.Remaining != 0)
            {
                throw new System.IO.InvalidDataException("Allocator section has trailing bytes.");
            }

            allocator.RestoreState(entries);
            return allocator;
        }
    }

    public static class SchedulerSectionCodec
    {
        public const uint SchemaVersion = 1;

        public static byte[] Encode(DeterministicScheduler scheduler)
        {
            var writer = new LittleEndianWriter();
            writer.WriteBool(scheduler.Clock.IsPaused);

            var commands = scheduler.Commands.ExportSorted();
            writer.WriteUInt64(scheduler.Commands.NextSequence);
            writer.WriteUInt32(checked((uint)commands.Length));
            for (var i = 0; i < commands.Length; i++)
            {
                writer.WriteInt64(commands[i].ExecuteMinute);
                writer.WriteUInt64(commands[i].StableSequence);
                writer.WriteUInt32(commands[i].CommandTypeId);
                writer.WriteInt64(commands[i].ValueA);
                writer.WriteInt64(commands[i].ValueB);
            }

            var events = scheduler.Events.ExportSorted();
            writer.WriteUInt64(scheduler.Events.NextSequence);
            writer.WriteUInt32(checked((uint)events.Length));
            for (var i = 0; i < events.Length; i++)
            {
                writer.WriteInt64(events[i].TimestampMinute);
                writer.WriteInt32(events[i].PriorityClass);
                writer.WriteUInt64(events[i].StableSequence);
                writer.WriteUInt32(events[i].EventTypeId);
                writer.WriteInt64(events[i].ValueA);
                writer.WriteInt64(events[i].ValueB);
            }

            return writer.ToArray();
        }

        public static SchedulerPersistenceState Decode(byte[] data)
        {
            var reader = new LittleEndianReader(data);
            var paused = reader.ReadBool();

            var commandNext = reader.ReadUInt64();
            var commandCount = reader.ReadUInt32();
            ValidateCount(commandCount, "commands");
            var commands = new SimulationCommand[commandCount];
            for (var i = 0; i < commands.Length; i++)
            {
                commands[i] = new SimulationCommand(
                    reader.ReadInt64(),
                    reader.ReadUInt64(),
                    reader.ReadUInt32(),
                    reader.ReadInt64(),
                    reader.ReadInt64());
            }

            var eventNext = reader.ReadUInt64();
            var eventCount = reader.ReadUInt32();
            ValidateCount(eventCount, "events");
            var events = new ScheduledEvent[eventCount];
            for (var i = 0; i < events.Length; i++)
            {
                events[i] = new ScheduledEvent(
                    reader.ReadInt64(),
                    reader.ReadInt32(),
                    reader.ReadUInt64(),
                    reader.ReadUInt32(),
                    reader.ReadInt64(),
                    reader.ReadInt64());
            }

            if (reader.Remaining != 0)
            {
                throw new System.IO.InvalidDataException("Scheduler section has trailing bytes.");
            }

            return new SchedulerPersistenceState(
                paused,
                commands,
                commandNext,
                events,
                eventNext);
        }

        private static void ValidateCount(uint count, string name)
        {
            if (count > 10_000_000)
            {
                throw new System.IO.InvalidDataException($"Scheduler {name} count is unreasonable: {count}.");
            }
        }
    }

    public static class RandomStreamSectionCodec
    {
        public const uint SchemaVersion = 1;

        public static byte[] Encode(DeterministicRandomService random)
        {
            var writer = new LittleEndianWriter();
            var states = random.ExportState();
            writer.WriteUInt32(checked((uint)states.Length));
            for (var i = 0; i < states.Length; i++)
            {
                writer.WriteUInt64(states[i].StreamId);
                writer.WriteUInt64(states[i].State);
                writer.WriteUInt64(states[i].Increment);
            }

            return writer.ToArray();
        }

        public static RandomStreamState[] Decode(byte[] data)
        {
            var reader = new LittleEndianReader(data);
            var count = reader.ReadUInt32();
            if (count > 1_000_000)
            {
                throw new System.IO.InvalidDataException("Random stream count is unreasonable.");
            }

            var states = new RandomStreamState[count];
            for (var i = 0; i < states.Length; i++)
            {
                states[i] = new RandomStreamState(
                    reader.ReadUInt64(),
                    reader.ReadUInt64(),
                    reader.ReadUInt64());
            }

            if (reader.Remaining != 0)
            {
                throw new System.IO.InvalidDataException("Random stream section has trailing bytes.");
            }

            return states;
        }
    }
}
