using System.Collections.Generic;
using MineIT.CityBuilder.Core.Determinism;

namespace MineIT.CityBuilder.Simulation.Contracts
{
    public readonly struct DomainEventRecord
    {
        public DomainEventRecord(
            long simulationMinute,
            ulong stableSequence,
            uint eventTypeId,
            long valueA = 0,
            long valueB = 0)
        {
            SimulationMinute = simulationMinute;
            StableSequence = stableSequence;
            EventTypeId = eventTypeId;
            ValueA = valueA;
            ValueB = valueB;
        }

        public long SimulationMinute { get; }
        public ulong StableSequence { get; }
        public uint EventTypeId { get; }
        public long ValueA { get; }
        public long ValueB { get; }

        public void AddToChecksum(ref DeterministicChecksum checksum)
        {
            checksum.Add(SimulationMinute);
            checksum.Add(StableSequence);
            checksum.Add(EventTypeId);
            checksum.Add(ValueA);
            checksum.Add(ValueB);
        }
    }

    public sealed class DomainEventStream : IDeterministicChecksumContributor
    {
        private readonly List<DomainEventRecord> _records = new List<DomainEventRecord>();
        private ulong _nextSequence;

        public int Count => _records.Count;
        public ulong NextSequence => _nextSequence;

        public DomainEventRecord Publish(long minute, uint eventTypeId, long valueA = 0, long valueB = 0)
        {
            var record = new DomainEventRecord(minute, _nextSequence++, eventTypeId, valueA, valueB);
            _records.Add(record);
            return record;
        }

        public DomainEventRecord[] Snapshot() => _records.ToArray();

        public void ClearConsumed() => _records.Clear();

        public void Restore(DomainEventRecord[] records, ulong nextSequence)
        {
            _records.Clear();
            if (records != null)
            {
                _records.AddRange(records);
            }

            _nextSequence = nextSequence;
        }

        public void AddToChecksum(ref DeterministicChecksum checksum)
        {
            checksum.Add(_nextSequence);
            checksum.Add(_records.Count);
            for (var i = 0; i < _records.Count; i++)
            {
                _records[i].AddToChecksum(ref checksum);
            }
        }
    }
}
