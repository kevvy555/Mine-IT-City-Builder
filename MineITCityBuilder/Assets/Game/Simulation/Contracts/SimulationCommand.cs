using System;
using MineIT.CityBuilder.Core.Determinism;

namespace MineIT.CityBuilder.Simulation.Contracts
{
    public readonly struct SimulationCommand : IComparable<SimulationCommand>
    {
        public SimulationCommand(
            long executeMinute,
            ulong stableSequence,
            uint commandTypeId,
            long valueA = 0,
            long valueB = 0)
        {
            if (executeMinute < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(executeMinute));
            }

            ExecuteMinute = executeMinute;
            StableSequence = stableSequence;
            CommandTypeId = commandTypeId;
            ValueA = valueA;
            ValueB = valueB;
        }

        public long ExecuteMinute { get; }
        public ulong StableSequence { get; }
        public uint CommandTypeId { get; }
        public long ValueA { get; }
        public long ValueB { get; }

        public int CompareTo(SimulationCommand other)
        {
            var minute = ExecuteMinute.CompareTo(other.ExecuteMinute);
            return minute != 0 ? minute : StableSequence.CompareTo(other.StableSequence);
        }

        public void AddToChecksum(ref DeterministicChecksum checksum)
        {
            checksum.Add(ExecuteMinute);
            checksum.Add(StableSequence);
            checksum.Add(CommandTypeId);
            checksum.Add(ValueA);
            checksum.Add(ValueB);
        }
    }
}
