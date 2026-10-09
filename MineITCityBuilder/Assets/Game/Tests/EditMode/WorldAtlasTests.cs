using System.Collections.Generic;
using MineIT.CityBuilder.World.Atlas;
using MineIT.CityBuilder.World.Chunks;
using NUnit.Framework;

namespace MineIT.CityBuilder.Tests
{
    public sealed class WorldAtlasTests
    {
        [Test]
        public void ChunkKey_GeneratesExactlySixteenUniqueChunksPerTile()
        {
            var keys = ChunkKey.ForTile(new AtlasCoordinate(0, 0));
            Assert.That(keys, Has.Length.EqualTo(16));

            var unique = new HashSet<ChunkKey>(keys);
            Assert.That(unique.Count, Is.EqualTo(16));

            for (var i = 0; i < keys.Length; i++)
            {
                Assert.That(keys[i].LocalIndex, Is.InRange(0, 15));
            }
        }

        [Test]
        public void ChunkWorldOrigins_AreStableAcrossNegativeAndPositiveTiles()
        {
            var westEdge = new ChunkKey(-1, 0, 3, 0).OriginMillimetres;
            var eastEdge = new ChunkKey(0, 0, 0, 0).OriginMillimetres;
            var north = new ChunkKey(0, 1, 0, 0).OriginMillimetres;

            Assert.That(westEdge.X, Is.EqualTo(-250_000L));
            Assert.That(eastEdge.X, Is.EqualTo(0L));
            Assert.That(north.Z, Is.EqualTo(1_000_000L));
        }

        [Test]
        public void BasicTerrain_IsDeterministicForSameCanonicalChunk()
        {
            var tile = MakeTile();
            var key = new ChunkKey(0, 0, 2, 3);

            var first = BasicTerrainGenerator.ForChunk(tile, key);
            var second = BasicTerrainGenerator.ForChunk(tile, key);

            Assert.That(second.BaseElevationCentimetres, Is.EqualTo(first.BaseElevationCentimetres));
            Assert.That(second.ReliefCentimetres, Is.EqualTo(first.ReliefCentimetres));
            Assert.That(second.SemanticZone, Is.EqualTo(AtlasZone.Core));
        }

        [Test]
        public void ChunkRegistry_MaterialisesOriginTileWithoutChangingIdentity()
        {
            var tile = MakeTile();
            var registry = new ChunkRegistry();

            var chunks = registry.MaterialiseTile(tile);
            var second = registry.MaterialiseTile(tile);

            Assert.That(chunks, Has.Length.EqualTo(16));
            Assert.That(registry.Count, Is.EqualTo(16));
            Assert.That(second[0], Is.SameAs(chunks[0]));

            registry.SetRenderState(chunks[0].Key, ChunkRenderState.Detailed);
            registry.SetSimulationState(chunks[0].Key, ChunkSimulationState.Active);

            Assert.That(chunks[0].RenderState, Is.EqualTo(ChunkRenderState.Detailed));
            Assert.That(chunks[0].SimulationState, Is.EqualTo(ChunkSimulationState.Active));
        }

        private static CanonicalAtlasTile MakeTile()
        {
            return new CanonicalAtlasTile(
                new MineIT.CityBuilder.Core.Ids.StableId("atlas-tile-koplin-3-x0-y0"),
                "Federal Forum",
                new AtlasCoordinate(0, 0),
                1,
                1,
                0,
                "Federal Forum",
                AtlasZone.Core,
                "centre",
                "test",
                new EdgeContinuity("", "", "", ""),
                new MineIT.CityBuilder.Core.Ids.StableId[0],
                "",
                false,
                "not-generated");
        }
    }
}
