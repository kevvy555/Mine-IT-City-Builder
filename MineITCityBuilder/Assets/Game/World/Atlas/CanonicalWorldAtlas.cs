using System;
using System.Collections.Generic;
using MineIT.CityBuilder.Core.Ids;

namespace MineIT.CityBuilder.World.Atlas
{
    public sealed class CanonicalWorldAtlas
    {
        private readonly Dictionary<AtlasCoordinate, CanonicalAtlasTile> _byCoordinate;
        private readonly Dictionary<StableId, CanonicalAtlasTile> _byId;

        public CanonicalWorldAtlas(
            StableId id,
            string name,
            StableId planetId,
            StableId originSettlementId,
            int tileSizeMetres,
            CanonicalAtlasTile[] tiles)
        {
            if (tileSizeMetres != 1000)
            {
                throw new ArgumentOutOfRangeException(nameof(tileSizeMetres), "City Builder atlas tiles must be 1000 m.");
            }

            Id = id;
            Name = name ?? string.Empty;
            PlanetId = planetId;
            OriginSettlementId = originSettlementId;
            TileSizeMetres = tileSizeMetres;
            Tiles = tiles ?? throw new ArgumentNullException(nameof(tiles));
            _byCoordinate = new Dictionary<AtlasCoordinate, CanonicalAtlasTile>(Tiles.Length);
            _byId = new Dictionary<StableId, CanonicalAtlasTile>(Tiles.Length);

            for (var i = 0; i < Tiles.Length; i++)
            {
                var tile = Tiles[i] ?? throw new ArgumentException("Atlas cannot contain null tiles.", nameof(tiles));
                if (_byCoordinate.ContainsKey(tile.Coordinate))
                {
                    throw new InvalidOperationException($"Duplicate atlas coordinate {tile.Coordinate}.");
                }
                if (_byId.ContainsKey(tile.Id))
                {
                    throw new InvalidOperationException($"Duplicate atlas tile id {tile.Id}.");
                }

                _byCoordinate.Add(tile.Coordinate, tile);
                _byId.Add(tile.Id, tile);
            }
        }

        public StableId Id { get; }
        public string Name { get; }
        public StableId PlanetId { get; }
        public StableId OriginSettlementId { get; }
        public int TileSizeMetres { get; }
        public CanonicalAtlasTile[] Tiles { get; }

        public bool TryGetTile(AtlasCoordinate coordinate, out CanonicalAtlasTile tile) =>
            _byCoordinate.TryGetValue(coordinate, out tile);

        public CanonicalAtlasTile GetRequiredTile(AtlasCoordinate coordinate)
        {
            if (!_byCoordinate.TryGetValue(coordinate, out var tile))
            {
                throw new KeyNotFoundException($"Canonical atlas tile {coordinate} is missing.");
            }
            return tile;
        }

        public bool TryGetTile(StableId id, out CanonicalAtlasTile tile) =>
            _byId.TryGetValue(id, out tile);
    }
}
