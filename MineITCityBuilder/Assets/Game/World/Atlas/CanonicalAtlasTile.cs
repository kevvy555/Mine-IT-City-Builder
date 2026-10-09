using System;
using MineIT.CityBuilder.Core.Ids;

namespace MineIT.CityBuilder.World.Atlas
{
    public enum AtlasZone : byte
    {
        Core = 0,
        Inner = 1,
        Middle = 2,
        Outer = 3,
        Fringe = 4
    }

    public readonly struct EdgeContinuity
    {
        public EdgeContinuity(string north, string east, string south, string west)
        {
            North = north ?? string.Empty;
            East = east ?? string.Empty;
            South = south ?? string.Empty;
            West = west ?? string.Empty;
        }

        public string North { get; }
        public string East { get; }
        public string South { get; }
        public string West { get; }
    }

    public sealed class CanonicalAtlasTile
    {
        public CanonicalAtlasTile(
            StableId id,
            string name,
            AtlasCoordinate coordinate,
            int sequence,
            int batch,
            int ring,
            string districtName,
            AtlasZone zone,
            string sector,
            string description,
            EdgeContinuity edges,
            StableId[] neighborIds,
            string imageKey,
            bool imageGenerated,
            string imageStatus)
        {
            Id = id;
            Name = name ?? string.Empty;
            Coordinate = coordinate;
            Sequence = sequence;
            Batch = batch;
            Ring = ring;
            DistrictName = districtName ?? string.Empty;
            Zone = zone;
            Sector = sector ?? string.Empty;
            Description = description ?? string.Empty;
            Edges = edges;
            NeighborIds = neighborIds ?? Array.Empty<StableId>();
            ImageKey = imageKey ?? string.Empty;
            ImageGenerated = imageGenerated;
            ImageStatus = imageStatus ?? string.Empty;
        }

        public StableId Id { get; }
        public string Name { get; }
        public AtlasCoordinate Coordinate { get; }
        public int Sequence { get; }
        public int Batch { get; }
        public int Ring { get; }
        public string DistrictName { get; }
        public AtlasZone Zone { get; }
        public string Sector { get; }
        public string Description { get; }
        public EdgeContinuity Edges { get; }
        public StableId[] NeighborIds { get; }
        public string ImageKey { get; }
        public bool ImageGenerated { get; }
        public string ImageStatus { get; }
    }
}
