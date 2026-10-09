using MineIT.CityBuilder.Content;
using MineIT.CityBuilder.World.Atlas;
using MineIT.CityBuilder.World.Chunks;
using NUnit.Framework;

namespace MineIT.CityBuilder.Tests
{
    public sealed class RuntimeCanonCatalogTests
    {
        [Test]
        public void LockedCanon_LoadsKoplin3ConcordiaAndFederalForum()
        {
            var canon = RuntimeCanonCatalog.LoadRequired();

            Assert.That(
                canon.UniverseCommit,
                Is.EqualTo("2a3251ba439a2108f0bf03e46eda02343e518925"));
            Assert.That(canon.PlanetId.ToString(), Is.EqualTo("planet-koplin-prime"));
            Assert.That(canon.PlanetName, Is.EqualTo("Koplin 3"));
            Assert.That(canon.SettlementId.ToString(), Is.EqualTo("settlement-concordia"));
            Assert.That(canon.SettlementName, Is.EqualTo("Concordia"));
            Assert.That(canon.Atlas.Id.ToString(), Is.EqualTo("world-atlas-koplin-3"));
            Assert.That(canon.Atlas.Tiles, Has.Length.EqualTo(100));

            var origin = canon.Atlas.GetRequiredTile(new AtlasCoordinate(0, 0));
            Assert.That(origin.Id.ToString(), Is.EqualTo("atlas-tile-koplin-3-x0-y0"));
            Assert.That(origin.DistrictName, Is.EqualTo("Federal Forum"));
            Assert.That(origin.Zone, Is.EqualTo(AtlasZone.Core));
            Assert.That(origin.NeighborIds, Has.Length.EqualTo(4));

            var chunks = new ChunkRegistry().MaterialiseTile(origin);
            Assert.That(chunks, Has.Length.EqualTo(16));
        }

        [Test]
        public void LockedCanon_ExposesDeterministicProvenanceHash()
        {
            var first = RuntimeCanonCatalog.LoadRequired();
            var second = RuntimeCanonCatalog.LoadRequired();

            Assert.That(first.ContentHashSha256, Has.Length.EqualTo(64));
            Assert.That(second.ContentHashSha256, Is.EqualTo(first.ContentHashSha256));
            Assert.That(first.GeneratedAtlasImages + first.PendingAtlasImages, Is.EqualTo(100));
        }

        [Test]
        public void AtlasCoordinates_AreUniqueAtRuntime()
        {
            var canon = RuntimeCanonCatalog.LoadRequired();
            var seen = new System.Collections.Generic.HashSet<AtlasCoordinate>();

            foreach (var tile in canon.Atlas.Tiles)
            {
                Assert.That(seen.Add(tile.Coordinate), Is.True, $"Duplicate coordinate {tile.Coordinate}");
            }

            Assert.That(seen.Count, Is.EqualTo(100));
        }
    }
}
