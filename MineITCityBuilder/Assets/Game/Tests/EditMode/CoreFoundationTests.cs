using System;
using MineIT.CityBuilder.Core.Determinism;
using MineIT.CityBuilder.Core.Diagnostics;
using MineIT.CityBuilder.Core.Economy;
using MineIT.CityBuilder.Core.Ids;
using MineIT.CityBuilder.Simulation.Random;
using NUnit.Framework;

namespace MineIT.CityBuilder.Tests
{
    public sealed class CoreFoundationTests
    {
        [Test]
        public void SaveEntityAllocator_RestoresWithoutReusingIds()
        {
            var allocator = new SaveEntityIdAllocator(42UL);
            var first = allocator.Allocate(7);
            var second = allocator.Allocate(7);

            Assert.That(first.Sequence, Is.EqualTo(0UL));
            Assert.That(second.Sequence, Is.EqualTo(1UL));

            var restored = new SaveEntityIdAllocator(42UL);
            restored.RestoreState(allocator.ExportState());
            var third = restored.Allocate(7);

            Assert.That(third.Sequence, Is.EqualTo(2UL));
            Assert.That(third, Is.Not.EqualTo(first));
            Assert.That(third, Is.Not.EqualTo(second));
        }

        [Test]
        public void StableIdRegistry_RejectsCollisions()
        {
            var registry = new StableIdRegistry<int>();
            var id = new StableId("settlement-concordia");
            registry.Add(id, 1, "test");

            var error = Assert.Throws<InvariantViolationException>(
                () => registry.Add(id, 2, "test"));

            Assert.That(error.DiagnosticId.ToString(), Is.EqualTo("CORE.ID.COLLISION"));
        }

        [Test]
        public void ContentId_RequiresNamespace()
        {
            Assert.Throws<ArgumentException>(() => new ContentId("unnamespaced"));
            Assert.That(
                new ContentId("building.residential.garden-midrise").ToString(),
                Is.EqualTo("building.residential.garden-midrise"));
        }

        [Test]
        public void Pcg32_ReplaysExactly()
        {
            var left = new Pcg32(1234UL, 5678UL);
            var right = new Pcg32(1234UL, 5678UL);

            for (var i = 0; i < 1000; i++)
            {
                Assert.That(left.NextUInt(), Is.EqualTo(right.NextUInt()));
            }
        }

        [Test]
        public void NamedRandomStreams_AreIndependent()
        {
            var first = new DeterministicRandomService(5300UL);
            var expectedIncident = first.NextUInt(RandomStreamIds.IncidentGeneration);

            var second = new DeterministicRandomService(5300UL);
            for (var i = 0; i < 50; i++)
            {
                second.NextUInt(RandomStreamIds.Migration);
            }

            var actualIncident = second.NextUInt(RandomStreamIds.IncidentGeneration);
            Assert.That(actualIncident, Is.EqualTo(expectedIncident));
        }

        [Test]
        public void MicroCredits_UsesCheckedExactArithmetic()
        {
            var ten = MicroCredits.FromWholeCredits(10);
            var five = MicroCredits.FromWholeCredits(5);

            Assert.That((ten + five).Value, Is.EqualTo(15L * MicroCredits.UnitsPerCredit));
            Assert.Throws<OverflowException>(
                () => { var ignored = new MicroCredits(long.MaxValue) + new MicroCredits(1); });
        }
    }
}
