using MineIT.CityBuilder.World.Atlas;

namespace MineIT.CityBuilder.World.Chunks
{
    public enum ChunkSimulationState : byte
    {
        Dormant = 0,
        Aggregate = 1,
        Warm = 2,
        Active = 3
    }

    public enum ChunkRenderState : byte
    {
        Unloaded = 0,
        Proxy = 1,
        Normal = 2,
        Detailed = 3
    }

    public readonly struct BasicTerrainProfile
    {
        public BasicTerrainProfile(int baseElevationCentimetres, int reliefCentimetres, AtlasZone semanticZone)
        {
            BaseElevationCentimetres = baseElevationCentimetres;
            ReliefCentimetres = reliefCentimetres;
            SemanticZone = semanticZone;
        }

        public int BaseElevationCentimetres { get; }
        public int ReliefCentimetres { get; }
        public AtlasZone SemanticZone { get; }
    }

    public sealed class RuntimeChunk
    {
        public RuntimeChunk(ChunkKey key, BasicTerrainProfile terrain)
        {
            Key = key;
            Terrain = terrain;
            SimulationState = ChunkSimulationState.Dormant;
            RenderState = ChunkRenderState.Unloaded;
        }

        public ChunkKey Key { get; }
        public BasicTerrainProfile Terrain { get; }
        public ChunkSimulationState SimulationState { get; set; }
        public ChunkRenderState RenderState { get; set; }
    }
}
