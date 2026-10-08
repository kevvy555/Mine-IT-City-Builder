using System;
using System.Collections.Generic;
using MineIT.CityBuilder.Core.Determinism;
using MineIT.CityBuilder.Simulation.Contracts;
using MineIT.CityBuilder.Simulation.Time;

namespace MineIT.CityBuilder.Simulation.Scheduling
{
    public sealed class DeterministicScheduler
    {
        private sealed class NoOpCommandSink : ISimulationCommandSink
        {
            public void Apply(in SimulationCommand command, in SimulationStepContext context) { }
        }

        private sealed class NoOpEventSink : IScheduledEventSink
        {
            public void Handle(in ScheduledEvent scheduledEvent, in SimulationStepContext context) { }
        }

        private readonly List<ISimulationSystem> _systems = new List<ISimulationSystem>();
        private readonly ISimulationCommandSink _commandSink;
        private readonly IScheduledEventSink _eventSink;
        private readonly IPresentationSnapshotSink _presentationSink;

        public DeterministicScheduler(
            ulong rootSeed,
            long startingMinute = 0,
            ISimulationCommandSink commandSink = null,
            IScheduledEventSink eventSink = null,
            IPresentationSnapshotSink presentationSink = null)
        {
            RootSeed = rootSeed;
            Clock = new SimulationClock(startingMinute);
            Commands = new SimulationCommandQueue();
            Events = new ScheduledEventQueue();
            DomainEvents = new DomainEventStream();
            _commandSink = commandSink ?? new NoOpCommandSink();
            _eventSink = eventSink ?? new NoOpEventSink();
            _presentationSink = presentationSink;
        }

        public ulong RootSeed { get; }
        public SimulationClock Clock { get; }
        public SimulationCommandQueue Commands { get; }
        public ScheduledEventQueue Events { get; }
        public DomainEventStream DomainEvents { get; }

        public void Register(ISimulationSystem system)
        {
            if (system == null)
            {
                throw new ArgumentNullException(nameof(system));
            }

            for (var i = 0; i < _systems.Count; i++)
            {
                if (_systems[i].Phase == system.Phase && _systems[i].StableOrder == system.StableOrder)
                {
                    throw new InvalidOperationException(
                        $"Duplicate simulation system order {system.Phase}/{system.StableOrder}.");
                }
            }

            _systems.Add(system);
            _systems.Sort(CompareSystems);
        }

        public bool AdvanceOneMinute()
        {
            if (!Clock.TryAdvanceOneMinute())
            {
                return false;
            }

            var context = new SimulationStepContext(Clock.Minute, RootSeed, DomainEvents);

            while (Commands.TryDequeueDue(Clock.Minute, out var command))
            {
                _commandSink.Apply(in command, in context);
            }

            while (Events.TryDequeueDue(Clock.Minute, out var scheduledEvent))
            {
                _eventSink.Handle(in scheduledEvent, in context);
            }

            for (var i = 0; i < _systems.Count; i++)
            {
                var system = _systems[i];
                if (system.Cadence.RunsAt(Clock.Minute))
                {
                    system.Execute(in context);
                }
            }

            if (_presentationSink != null)
            {
                var snapshot = CreateReadModelSnapshot();
                _presentationSink.Publish(in snapshot);
            }

            return true;
        }

        public SimulationReadModelSnapshot CreateReadModelSnapshot()
        {
            return new SimulationReadModelSnapshot(
                Clock.Minute,
                ComputeChecksum(),
                Commands.Count,
                Events.Count);
        }

        public ulong ComputeChecksum()
        {
            var checksum = new DeterministicChecksum(true);
            checksum.Add(RootSeed);
            checksum.Add(Clock.Minute);
            checksum.Add(Clock.IsPaused);
            Commands.AddToChecksum(ref checksum);
            Events.AddToChecksum(ref checksum);
            DomainEvents.AddToChecksum(ref checksum);

            for (var i = 0; i < _systems.Count; i++)
            {
                checksum.Add((byte)_systems[i].Phase);
                checksum.Add(_systems[i].StableOrder);
                checksum.Add(_systems[i].Cadence.PeriodMinutes);
                checksum.Add(_systems[i].Cadence.OffsetMinutes);

                if (_systems[i] is IDeterministicChecksumContributor contributor)
                {
                    contributor.AddToChecksum(ref checksum);
                }
            }

            if (_commandSink is IDeterministicChecksumContributor commandContributor)
            {
                commandContributor.AddToChecksum(ref checksum);
            }

            if (_eventSink is IDeterministicChecksumContributor eventContributor)
            {
                eventContributor.AddToChecksum(ref checksum);
            }

            return checksum.Value;
        }

        private static int CompareSystems(ISimulationSystem left, ISimulationSystem right)
        {
            var phase = left.Phase.CompareTo(right.Phase);
            return phase != 0 ? phase : left.StableOrder.CompareTo(right.StableOrder);
        }
    }
}
