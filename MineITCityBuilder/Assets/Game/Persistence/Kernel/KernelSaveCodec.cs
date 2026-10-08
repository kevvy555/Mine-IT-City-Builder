using System;
using System.Collections.Generic;
using System.IO;
using MineIT.CityBuilder.Core.Ids;
using MineIT.CityBuilder.Persistence.Container;
using MineIT.CityBuilder.Simulation.Contracts;
using MineIT.CityBuilder.Simulation.Random;
using MineIT.CityBuilder.Simulation.Scheduling;

namespace MineIT.CityBuilder.Persistence.Kernel
{
    public sealed class KernelRestoreResult
    {
        public KernelRestoreResult(
            SaveHeader header,
            SaveEntityIdAllocator allocator,
            DeterministicScheduler scheduler,
            DeterministicRandomService random)
        {
            Header = header;
            Allocator = allocator;
            Scheduler = scheduler;
            Random = random;
        }

        public SaveHeader Header { get; }
        public SaveEntityIdAllocator Allocator { get; }
        public DeterministicScheduler Scheduler { get; }
        public DeterministicRandomService Random { get; }
    }

    public static class KernelSaveCodec
    {
        public static byte[] Capture(
            string gameVersion,
            string contentVersion,
            string universeCommit,
            StableId scenarioId,
            SaveEntityIdAllocator allocator,
            DeterministicScheduler scheduler,
            DeterministicRandomService random,
            long createdUtcTicks,
            long lastSavedUtcTicks,
            ulong featureFlags = 0)
        {
            if (allocator == null) throw new ArgumentNullException(nameof(allocator));
            if (scheduler == null) throw new ArgumentNullException(nameof(scheduler));
            if (random == null) throw new ArgumentNullException(nameof(random));
            if (scheduler.RootSeed != random.RootSeed)
            {
                throw new InvalidOperationException("Scheduler and random service must share the save root seed.");
            }

            var header = new SaveHeader(
                SaveHeader.CurrentContainerVersion,
                SaveHeader.CurrentSchemaVersion,
                gameVersion,
                contentVersion,
                universeCommit,
                scheduler.RootSeed,
                scenarioId,
                scheduler.Clock.Minute,
                createdUtcTicks,
                lastSavedUtcTicks,
                featureFlags,
                0);

            var sections = new[]
            {
                new SaveSection(
                    SaveSectionIds.IdAllocator,
                    IdAllocatorSectionCodec.SchemaVersion,
                    true,
                    IdAllocatorSectionCodec.Encode(allocator)),
                new SaveSection(
                    SaveSectionIds.Scheduler,
                    SchedulerSectionCodec.SchemaVersion,
                    true,
                    SchedulerSectionCodec.Encode(scheduler)),
                new SaveSection(
                    SaveSectionIds.RandomStreams,
                    RandomStreamSectionCodec.SchemaVersion,
                    true,
                    RandomStreamSectionCodec.Encode(random))
            };

            return MicitySaveContainerCodec.Encode(header, sections);
        }

        public static KernelRestoreResult Restore(
            byte[] data,
            ISimulationCommandSink commandSink = null,
            IScheduledEventSink eventSink = null,
            IPresentationSnapshotSink presentationSink = null)
        {
            var container = MicitySaveContainerCodec.Decode(data);
            ValidateKnownRequiredSections(container.Sections);

            var allocatorSection = container.GetRequired(SaveSectionIds.IdAllocator);
            var schedulerSection = container.GetRequired(SaveSectionIds.Scheduler);
            var randomSection = container.GetRequired(SaveSectionIds.RandomStreams);

            RequireVersion(allocatorSection, IdAllocatorSectionCodec.SchemaVersion);
            RequireVersion(schedulerSection, SchedulerSectionCodec.SchemaVersion);
            RequireVersion(randomSection, RandomStreamSectionCodec.SchemaVersion);

            var allocator = IdAllocatorSectionCodec.Decode(allocatorSection.Data);
            var schedulerState = SchedulerSectionCodec.Decode(schedulerSection.Data);
            var random = new DeterministicRandomService(container.Header.RootSeed);
            random.RestoreState(RandomStreamSectionCodec.Decode(randomSection.Data));

            var scheduler = new DeterministicScheduler(
                container.Header.RootSeed,
                container.Header.SimulationMinute,
                commandSink,
                eventSink,
                presentationSink);
            scheduler.RestorePersistenceState(
                container.Header.SimulationMinute,
                schedulerState.Paused,
                schedulerState.Commands,
                schedulerState.CommandNextSequence,
                schedulerState.Events,
                schedulerState.EventNextSequence);

            return new KernelRestoreResult(container.Header, allocator, scheduler, random);
        }

        private static void ValidateKnownRequiredSections(SaveSection[] sections)
        {
            var known = new HashSet<uint>
            {
                SaveSectionIds.IdAllocator,
                SaveSectionIds.Scheduler,
                SaveSectionIds.RandomStreams
            };

            for (var i = 0; i < sections.Length; i++)
            {
                if (sections[i].Required && !known.Contains(sections[i].TypeId))
                {
                    throw new InvalidDataException(
                        $"Save contains unknown required section {sections[i].TypeId:x8}.");
                }
            }
        }

        private static void RequireVersion(SaveSection section, uint expected)
        {
            if (section.SchemaVersion != expected)
            {
                throw new InvalidDataException(
                    $"Unsupported section {section.TypeId:x8} schema {section.SchemaVersion}; expected {expected}.");
            }
        }
    }
}
