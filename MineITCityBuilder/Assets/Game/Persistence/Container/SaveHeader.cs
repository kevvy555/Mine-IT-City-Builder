using System;
using MineIT.CityBuilder.Core.Ids;

namespace MineIT.CityBuilder.Persistence.Container
{
    public readonly struct SaveHeader
    {
        public const uint CurrentContainerVersion = 1;
        public const uint CurrentSchemaVersion = 1;

        public SaveHeader(
            uint containerVersion,
            uint saveSchemaVersion,
            string gameVersion,
            string contentVersion,
            string universeCommit,
            ulong rootSeed,
            StableId scenarioId,
            long simulationMinute,
            long createdUtcTicks,
            long lastSavedUtcTicks,
            ulong featureFlags,
            uint sectionCount)
        {
            if (simulationMinute < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(simulationMinute));
            }

            ContainerVersion = containerVersion;
            SaveSchemaVersion = saveSchemaVersion;
            GameVersion = gameVersion ?? throw new ArgumentNullException(nameof(gameVersion));
            ContentVersion = contentVersion ?? throw new ArgumentNullException(nameof(contentVersion));
            UniverseCommit = universeCommit ?? throw new ArgumentNullException(nameof(universeCommit));
            RootSeed = rootSeed;
            ScenarioId = scenarioId;
            SimulationMinute = simulationMinute;
            CreatedUtcTicks = createdUtcTicks;
            LastSavedUtcTicks = lastSavedUtcTicks;
            FeatureFlags = featureFlags;
            SectionCount = sectionCount;
        }

        public uint ContainerVersion { get; }
        public uint SaveSchemaVersion { get; }
        public string GameVersion { get; }
        public string ContentVersion { get; }
        public string UniverseCommit { get; }
        public ulong RootSeed { get; }
        public StableId ScenarioId { get; }
        public long SimulationMinute { get; }
        public long CreatedUtcTicks { get; }
        public long LastSavedUtcTicks { get; }
        public ulong FeatureFlags { get; }
        public uint SectionCount { get; }

        public SaveHeader WithSectionCount(uint sectionCount) =>
            new SaveHeader(
                ContainerVersion,
                SaveSchemaVersion,
                GameVersion,
                ContentVersion,
                UniverseCommit,
                RootSeed,
                ScenarioId,
                SimulationMinute,
                CreatedUtcTicks,
                LastSavedUtcTicks,
                FeatureFlags,
                sectionCount);
    }
}
