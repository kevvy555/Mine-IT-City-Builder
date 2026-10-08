using System;
using System.IO;
using MineIT.CityBuilder.Core.Ids;
using MineIT.CityBuilder.Persistence.Container;
using MineIT.CityBuilder.Persistence.Kernel;
using MineIT.CityBuilder.Simulation.Random;
using MineIT.CityBuilder.Simulation.Scheduling;
using NUnit.Framework;

namespace MineIT.CityBuilder.Tests
{
    public sealed class SaveContainerTests
    {
        [Test]
        public void MicityHeader_RoundTripsRequiredIdentity()
        {
            var header = new SaveHeader(
                SaveHeader.CurrentContainerVersion,
                SaveHeader.CurrentSchemaVersion,
                "0.2.0",
                "content-test",
                "0123456789012345678901234567890123456789",
                5300UL,
                new StableId("scenario.concordia"),
                12345L,
                1L,
                2L,
                3UL,
                0);

            var bytes = MicitySaveContainerCodec.Encode(header, Array.Empty<SaveSection>());
            var decoded = MicitySaveContainerCodec.Decode(bytes).Header;

            Assert.That(decoded.GameVersion, Is.EqualTo("0.2.0"));
            Assert.That(decoded.RootSeed, Is.EqualTo(5300UL));
            Assert.That(decoded.ScenarioId.ToString(), Is.EqualTo("scenario.concordia"));
            Assert.That(decoded.SimulationMinute, Is.EqualTo(12345L));
        }

        [Test]
        public void MicityContainer_DetectsCorruptedPayload()
        {
            var header = CreateHeader();
            var section = new SaveSection(0x11111111u, 1, false, new byte[] { 1, 2, 3, 4, 5 });
            var bytes = MicitySaveContainerCodec.Encode(header, new[] { section });
            bytes[bytes.Length - 1] ^= 0x7F;

            Assert.Throws<InvalidDataException>(() => MicitySaveContainerCodec.Decode(bytes));
        }

        [Test]
        public void KernelSave_MidpointReloadMatchesUninterruptedKernelState()
        {
            var allocator = new SaveEntityIdAllocator(77UL);
            allocator.Allocate(1);
            allocator.Allocate(1);
            allocator.Allocate(2);

            var scheduler = new DeterministicScheduler(5300UL);
            scheduler.Commands.Enqueue(15, 10, 100);
            scheduler.Commands.Enqueue(28, 20, 200);
            scheduler.Events.Schedule(18, 1, 30, 300);
            scheduler.Events.Schedule(18, 1, 31, 301);

            var random = new DeterministicRandomService(5300UL);
            for (var i = 0; i < 7; i++)
            {
                random.NextUInt(MineIT.CityBuilder.Core.Determinism.RandomStreamIds.Migration);
            }

            for (var i = 0; i < 10; i++)
            {
                scheduler.AdvanceOneMinute();
            }

            var bytes = KernelSaveCodec.Capture(
                "0.2.0",
                "content-test",
                string.Empty,
                new StableId("scenario.concordia"),
                allocator,
                scheduler,
                random,
                100L,
                200L);

            var restored = KernelSaveCodec.Restore(bytes);

            for (var i = 0; i < 30; i++)
            {
                scheduler.AdvanceOneMinute();
                restored.Scheduler.AdvanceOneMinute();
            }

            Assert.That(
                restored.Scheduler.ComputeChecksum(),
                Is.EqualTo(scheduler.ComputeChecksum()));

            for (var i = 0; i < 20; i++)
            {
                var expected = random.NextUInt(MineIT.CityBuilder.Core.Determinism.RandomStreamIds.Migration);
                var actual = restored.Random.NextUInt(MineIT.CityBuilder.Core.Determinism.RandomStreamIds.Migration);
                Assert.That(actual, Is.EqualTo(expected));
            }

            Assert.That(restored.Allocator.Allocate(1), Is.EqualTo(allocator.Allocate(1)));
            Assert.That(restored.Header.SimulationMinute, Is.EqualTo(10L));
        }

        [Test]
        public void SchedulerEventOrder_SurvivesSaveReload()
        {
            var allocator = new SaveEntityIdAllocator(1UL);
            var scheduler = new DeterministicScheduler(123UL);
            scheduler.Events.Schedule(50, 5, 500);
            scheduler.Events.Schedule(50, 1, 100);
            scheduler.Events.Schedule(50, 1, 101);
            var random = new DeterministicRandomService(123UL);

            var bytes = KernelSaveCodec.Capture(
                "0.2.0",
                "test",
                string.Empty,
                new StableId("scenario.test"),
                allocator,
                scheduler,
                random,
                1L,
                1L);

            var restored = KernelSaveCodec.Restore(bytes);
            var events = restored.Scheduler.Events.ExportSorted();

            Assert.That(events[0].EventTypeId, Is.EqualTo(100u));
            Assert.That(events[1].EventTypeId, Is.EqualTo(101u));
            Assert.That(events[2].EventTypeId, Is.EqualTo(500u));
            Assert.That(events[0].StableSequence, Is.LessThan(events[1].StableSequence));
        }

        private static SaveHeader CreateHeader()
        {
            return new SaveHeader(
                SaveHeader.CurrentContainerVersion,
                SaveHeader.CurrentSchemaVersion,
                "0.2.0",
                "test",
                string.Empty,
                1UL,
                new StableId("scenario.test"),
                0L,
                1L,
                1L,
                0UL,
                0);
        }
    }
}
