using System;
using System.Collections.Generic;
using MineIT.CityBuilder.World.Atlas;

namespace MineIT.CityBuilder.World.Chunks
{
    public sealed class ChunkRegistry
    {
        private readonly Dictionary<ChunkKey, RuntimeChunk> _chunks = new Dictionary<ChunkKey, RuntimeChunk>();

        public int Count => _chunks.Count;

        public RuntimeChunk[] MaterialiseTile(CanonicalAtlasTile tile)
        {
            if (tile == null)
            {
                throw new ArgumentNullException(nameof(tile));
            }

            var keys = ChunkKey.ForTile(tile.Coordinate);
            var result = new RuntimeChunk[keys.Length];

            for (var i = 0; i < keys.Length; i++)
            {
                if (!_chunks.TryGetValue(keys[i], out var chunk))
                {
                    chunk = new RuntimeChunk(keys[i], BasicTerrainGenerator.ForChunk(tile, keys[i]));
                    _chunks.Add(keys[i], chunk);
                }
                result[i] = chunk;
            }

            return result;
        }

        public bool TryGet(ChunkKey key, out RuntimeChunk chunk) => _chunks.TryGetValue(key, out chunk);

        public void SetRenderState(ChunkKey key, ChunkRenderState state)
        {
            if (!_chunks.TryGetValue(key, out var chunk))
            {
                throw new KeyNotFoundException($"Chunk {key} is not materialised.");
            }
            chunk.RenderState = state;
        }

        public void SetSimulationState(ChunkKey key, ChunkSimulationState state)
        {
            if (!_chunks.TryGetValue(key, out var chunk))
            {
                throw new KeyNotFoundException($"Chunk {key} is not materialised.");
            }
            chunk.SimulationState = state;
        }
    }

    public static class BasicTerrainGenerator
    {
        public static BasicTerrainProfile ForChunk(CanonicalAtlasTile tile, ChunkKey key)
        {
            var zoneBase = ZoneBaseElevation(tile.Zone);
            var hash = unchecked(
                (uint)(key.AtlasTileX * 73856093) ^
                (uint)(key.AtlasTileY * 19349663) ^
                (uint)(key.LocalX * 83492791) ^
                (uint)(key.LocalY * 2654435761u));
            var signedVariation = (int)(hash % 121u) - 60;
            var relief = 80 + (int)((hash >> 8) % 121u);
            return new BasicTerrainProfile(zoneBase + signedVariation, relief, tile.Zone);
        }

        private static int ZoneBaseElevation(AtlasZone zone)
        {
            switch (zone)
            {
                case AtlasZone.Core: return 1200;
                case AtlasZone.Inner: return 1250;
                case AtlasZone.Middle: return 1300;
                case AtlasZone.Outer: return 1350;
                case AtlasZone.Fringe: return 1400;
                default: return 1200;
            }
        }
    }
}
