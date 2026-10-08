using System.Collections.Generic;
using MineIT.CityBuilder.Core.Determinism;
using MineIT.CityBuilder.Simulation.Contracts;
using MineIT.CityBuilder.Simulation.Lab;
using MineIT.CityBuilder.Simulation.Scheduling;
using MineIT.CityBuilder.Simulation.Time;
using NUnit.Framework;

namespace MineIT.CityBuilder.Tests
{
    public sealed class SimulationKernelTests
    {
        private sealed class RecordingSink : ISimulationCommandSink, IScheduledEventSink
        {
            public readonly List<string> Log = new List<string>();

            public void Apply(in SimulationCommand command, in SimulationStepContext context)
            {
                Log.Add($"C:{command.CommandTypeId}");
            }

            public void Handle(in ScheduledEvent scheduledEvent, in SimulationStepContext context)
            {
                Log.Add($"E:{scheduledEvent.EventTypeId}");
            }
        }

        private sealed class RecordingSystem : ISimulationSystem
        {
            private readonly List<string> _log;
            private readonly string _name;

            public RecordingSystem(
                List<string> log,
                string name,
                SimulationPhase phase,
                int stableOrder,
                SimulationCadence cadence)
            {
                _log = log;
                _name = name;
                Phase = phase;
                StableOrder = stableOrder;
                Cadence = cadence;
            }

            public SimulationPhase Phase { get; }
            public int StableOrder { get; }
            public SimulationCadence Cadence { get; }

            public void Execute(in SimulationStepContext context)
            {
                _log.Add($"S:{_name}");
            }
        }

        private sealed class DeterministicCounterSystem : ISimulationSystem, IDeterministicChecksumContributor
        {
            public SimulationPhase Phase => SimulationPhase.MetricsHistory;
            public int StableOrder => 10;
            public SimulationCadence Cadence => SimulationCadence.EveryMinute;
            public ulong Total { get; private set; }

            public void Execute(in SimulationStepContext context)
            {
                Total = unchecked(Total +
                    DeterministicSeed.Derive(
                        context.RootSeed,
                        RandomStreamIds.Migration,
                        context.SimulationMinute));
            }

            public void AddToChecksum(ref DeterministicChecksum checksum)
            {
                checksum.Add(Total);
            }
        }

        [Test]
        public void Scheduler_AppliesCommandsEventsAndSystemsInStableOrder()
        {
            var sink = new RecordingSink();
            var systemLog = sink.Log;
            var scheduler = new DeterministicScheduler(1UL, commandSink: sink, eventSink: sink);

            scheduler.Register(new RecordingSystem(
                systemLog, "later", SimulationPhase.Mobility, 20, SimulationCadence.EveryMinute));
            scheduler.Register(new RecordingSystem(
                systemLog, "first", SimulationPhase.WorldNetworkMutation, 10, SimulationCadence.EveryMinute));
            scheduler.Register(new RecordingSystem(
                systemLog, "second", SimulationPhase.WorldNetworkMutation, 20, SimulationCadence.EveryMinute));

            scheduler.Commands.Enqueue(1, 100);
            scheduler.Events.Schedule(1, 5, 500);
            scheduler.Events.Schedule(1, 1, 200);
            scheduler.Events.Schedule(1, 1, 201);

            Assert.That(scheduler.AdvanceOneMinute(), Is.True);

            CollectionAssert.AreEqual(
                new[] { "C:100", "E:200", "E:201", "E:500", "S:first", "S:second", "S:later" },
                sink.Log);
        }

        [Test]
        public void Scheduler_PauseAdvancesNoAuthoritativeState()
        {
            var scheduler = new DeterministicScheduler(99UL);
            scheduler.Clock.SetPaused(true);
            var before = scheduler.ComputeChecksum();

            Assert.That(scheduler.AdvanceOneMinute(), Is.False);
            Assert.That(scheduler.Clock.Minute, Is.EqualTo(0));
            Assert.That(scheduler.ComputeChecksum(), Is.EqualTo(before));
        }

        [Test]
        public void HeadlessRunner_ReplaysChecksumForSameSeed()
        {
            var leftScheduler = new DeterministicScheduler(5300UL);
            var leftSystem = new DeterministicCounterSystem();
            leftScheduler.Register(leftSystem);

            var rightScheduler = new DeterministicScheduler(5300UL);
            var rightSystem = new DeterministicCounterSystem();
            rightScheduler.Register(rightSystem);

            var left = new HeadlessSimulationRunner(leftScheduler).RunMinutes(5000);
            var right = new HeadlessSimulationRunner(rightScheduler).RunMinutes(5000);

            Assert.That(right, Is.EqualTo(left));
            Assert.That(rightSystem.Total, Is.EqualTo(leftSystem.Total));
        }

        [Test]
        public void SpeedController_IsIndependentOfRenderFramePartition()
        {
            var fine = new SimulationSpeedController { Speed = SimulationSpeed.Normal };
            var coarse = new SimulationSpeedController { Speed = SimulationSpeed.Normal };

            var fineSteps = 0;
            for (var i = 0; i < 10; i++)
            {
                fineSteps += fine.AccumulateAndTake(100, 100);
            }

            var coarseSteps = 0;
            for (var i = 0; i < 4; i++)
            {
                coarseSteps += coarse.AccumulateAndTake(250, 100);
            }

            Assert.That(fineSteps, Is.EqualTo(5));
            Assert.That(coarseSteps, Is.EqualTo(fineSteps));
        }

        [Test]
        public void Int64SimulationMinute_EasilyRepresentsOneHundredYears()
        {
            const long hundredYears = 100L * 365L * 1440L;
            var clock = new SimulationClock();
            clock.Restore(hundredYears, false);

            Assert.That(clock.Minute, Is.EqualTo(52_560_000L));
            Assert.That(checked(clock.Minute + 1L), Is.EqualTo(52_560_001L));
        }

        [Test]
        public void Simulation_RunsWithoutPresentationSink()
        {
            var scheduler = new DeterministicScheduler(123UL, presentationSink: null);
            var runner = new HeadlessSimulationRunner(scheduler);

            Assert.DoesNotThrow(() => runner.RunMinutes(50));
            Assert.That(scheduler.Clock.Minute, Is.EqualTo(50));
        }
    }
}
