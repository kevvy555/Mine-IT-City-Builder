using System;
using MineIT.CityBuilder.Core.Determinism;

namespace MineIT.CityBuilder.Simulation.Contracts
{
    public readonly struct ScheduledEvent : IComparable<ScheduledEvent>
    {
        public ScheduledEvent(
            long timestampMinute,
            int priorityClass,
            ulong stableSequence,
            uint eventTypeId,
            long valueA = 0,
            long valueB = 0)
        {
            if (timestampMinute < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(timestampMinute));
            }

            TimestampMinute = timestampMinute;
            PriorityClass = priorityClass;
            StableSequence = stableSequence;
            EventTypeId = eventTypeId;
            ValueA = valueA;
            ValueB = valueB;
        }

        public long TimestampMinute { get; }
        public int PriorityClass { get; }
        public ulong StableSequence { get; }
        public uint EventTypeId { get; }
        public long ValueA { get; }
        public long ValueB { get; }

        public int CompareTo(ScheduledEvent other)
        {
            var minute = TimestampMinute.CompareTo(other.TimestampMinute);
            if (minute != 0)
            {
                return minute;
            }

            var priority = PriorityClass.CompareTo(other.PriorityClass);
            return priority != 0 ? priority : StableSequence.CompareTo(other.StableSequence);
        }

        public void AddToChecksum(ref DeterministicChecksum checksum)
        {
            checksum.Add(TimestampMinute);
            checksum.Add(PriorityClass);
            checksum.Add(StableSequence);
            checksum.Add(EventTypeId);
            checksum.Add(ValueA);
            checksum.Add(ValueB);
        }
    }
}
