using System;
using MineIT.CityBuilder.Core.Determinism;
using MineIT.CityBuilder.Simulation.Contracts;

namespace MineIT.CityBuilder.Simulation.Scheduling
{
    public sealed class SimulationCommandQueue : IDeterministicChecksumContributor
    {
        private readonly StableMinHeap<SimulationCommand> _heap = new StableMinHeap<SimulationCommand>();
        private ulong _nextSequence;

        public int Count => _heap.Count;
        public ulong NextSequence => _nextSequence;

        public SimulationCommand Enqueue(
            long executeMinute,
            uint commandTypeId,
            long valueA = 0,
            long valueB = 0)
        {
            var command = new SimulationCommand(
                executeMinute,
                _nextSequence++,
                commandTypeId,
                valueA,
                valueB);
            _heap.Push(command);
            return command;
        }

        public bool TryDequeueDue(long simulationMinute, out SimulationCommand command)
        {
            if (!_heap.TryPeek(out command) || command.ExecuteMinute > simulationMinute)
            {
                command = default;
                return false;
            }

            return _heap.TryPop(out command);
        }

        public SimulationCommand[] ExportSorted() => _heap.ExportSorted();

        public void Restore(SimulationCommand[] commands, ulong nextSequence)
        {
            _heap.Clear();
            ulong highest = 0;

            if (commands != null)
            {
                for (var i = 0; i < commands.Length; i++)
                {
                    _heap.Push(commands[i]);
                    if (commands[i].StableSequence >= highest)
                    {
                        highest = commands[i].StableSequence + 1UL;
                    }
                }
            }

            if (nextSequence < highest)
            {
                throw new InvalidOperationException("Command queue next sequence would reuse an existing sequence.");
            }

            _nextSequence = nextSequence;
        }

        public void AddToChecksum(ref DeterministicChecksum checksum)
        {
            checksum.Add(_nextSequence);
            var commands = ExportSorted();
            checksum.Add(commands.Length);
            for (var i = 0; i < commands.Length; i++)
            {
                commands[i].AddToChecksum(ref checksum);
            }
        }
    }
}
