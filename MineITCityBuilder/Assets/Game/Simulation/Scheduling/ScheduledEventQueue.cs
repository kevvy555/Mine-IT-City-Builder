using System;
using MineIT.CityBuilder.Core.Determinism;
using MineIT.CityBuilder.Simulation.Contracts;

namespace MineIT.CityBuilder.Simulation.Scheduling
{
    public sealed class ScheduledEventQueue : IDeterministicChecksumContributor
    {
        private readonly StableMinHeap<ScheduledEvent> _heap = new StableMinHeap<ScheduledEvent>();
        private ulong _nextSequence;

        public int Count => _heap.Count;
        public ulong NextSequence => _nextSequence;

        public ScheduledEvent Schedule(
            long timestampMinute,
            int priorityClass,
            uint eventTypeId,
            long valueA = 0,
            long valueB = 0)
        {
            var scheduledEvent = new ScheduledEvent(
                timestampMinute,
                priorityClass,
                _nextSequence++,
                eventTypeId,
                valueA,
                valueB);
            _heap.Push(scheduledEvent);
            return scheduledEvent;
        }

        public bool TryDequeueDue(long simulationMinute, out ScheduledEvent scheduledEvent)
        {
            if (!_heap.TryPeek(out scheduledEvent) || scheduledEvent.TimestampMinute > simulationMinute)
            {
                scheduledEvent = default;
                return false;
            }

            return _heap.TryPop(out scheduledEvent);
        }

        public ScheduledEvent[] ExportSorted() => _heap.ExportSorted();

        public void Restore(ScheduledEvent[] events, ulong nextSequence)
        {
            _heap.Clear();
            ulong highest = 0;

            if (events != null)
            {
                for (var i = 0; i < events.Length; i++)
                {
                    _heap.Push(events[i]);
                    if (events[i].StableSequence >= highest)
                    {
                        highest = events[i].StableSequence + 1UL;
                    }
                }
            }

            if (nextSequence < highest)
            {
                throw new InvalidOperationException("Event queue next sequence would reuse an existing sequence.");
            }

            _nextSequence = nextSequence;
        }

        public void AddToChecksum(ref DeterministicChecksum checksum)
        {
            checksum.Add(_nextSequence);
            var events = ExportSorted();
            checksum.Add(events.Length);
            for (var i = 0; i < events.Length; i++)
            {
                events[i].AddToChecksum(ref checksum);
            }
        }
    }
}
