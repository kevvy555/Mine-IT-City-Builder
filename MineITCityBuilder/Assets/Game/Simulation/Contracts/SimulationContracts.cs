using MineIT.CityBuilder.Core.Determinism;
using MineIT.CityBuilder.Simulation.Time;

namespace MineIT.CityBuilder.Simulation.Contracts
{
    public enum SimulationPhase : byte
    {
        WorldNetworkMutation = 2,
        RouteInvalidation = 3,
        Utilities = 4,
        Mobility = 5,
        Services = 6,
        Production = 7,
        Population = 8,
        Development = 9,
        Environment = 10,
        Governance = 11,
        MetricsHistory = 12,
        Invariants = 13
    }

    public readonly struct SimulationStepContext
    {
        public SimulationStepContext(long simulationMinute, ulong rootSeed, DomainEventStream events)
        {
            SimulationMinute = simulationMinute;
            RootSeed = rootSeed;
            Events = events;
        }

        public long SimulationMinute { get; }
        public ulong RootSeed { get; }
        public DomainEventStream Events { get; }
    }

    public interface ISimulationSystem
    {
        SimulationPhase Phase { get; }
        int StableOrder { get; }
        SimulationCadence Cadence { get; }
        void Execute(in SimulationStepContext context);
    }

    public interface ISimulationCommandSink
    {
        void Apply(in SimulationCommand command, in SimulationStepContext context);
    }

    public interface IScheduledEventSink
    {
        void Handle(in ScheduledEvent scheduledEvent, in SimulationStepContext context);
    }

    public readonly struct SimulationReadModelSnapshot
    {
        public SimulationReadModelSnapshot(
            long simulationMinute,
            ulong checksum,
            int pendingCommands,
            int pendingEvents)
        {
            SimulationMinute = simulationMinute;
            Checksum = checksum;
            PendingCommands = pendingCommands;
            PendingEvents = pendingEvents;
        }

        public long SimulationMinute { get; }
        public ulong Checksum { get; }
        public int PendingCommands { get; }
        public int PendingEvents { get; }
    }

    public interface IPresentationSnapshotSink
    {
        void Publish(in SimulationReadModelSnapshot snapshot);
    }
}
