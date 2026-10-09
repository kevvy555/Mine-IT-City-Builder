using System;
using MineIT.CityBuilder.Core.Ids;
using MineIT.CityBuilder.World.Atlas;
using UnityEngine;

namespace MineIT.CityBuilder.Content
{
    [Serializable]
    internal sealed class CanonCatalogDto
    {
        public int formatVersion;
        public string universeCommit;
        public PlanetDto planet;
        public SettlementDto settlement;
    }

    [Serializable]
    internal sealed class PlanetDto
    {
        public string id;
        public string name;
        public string capitalSettlementId;
        public string worldAtlasId;
    }

    [Serializable]
    internal sealed class SettlementDto
    {
        public string id;
        public string name;
        public string planetId;
    }

    [Serializable]
    internal sealed class AtlasIndexDto
    {
        public int formatVersion;
        public string universeCommit;
        public AtlasDto atlas;
        public AtlasTileDto[] tiles;
    }

    [Serializable]
    internal sealed class AtlasDto
    {
        public string id;
        public string name;
        public string planetId;
        public string originSettlementId;
        public int tileSizeKm;
        public int phaseOneTileCount;
    }

    [Serializable]
    internal sealed class AtlasTileDto
    {
        public string id;
        public string name;
        public int x;
        public int y;
        public int sequence;
        public int batch;
        public int ring;
        public string districtName;
        public string zone;
        public string sector;
        public string description;
        public string northEdge;
        public string eastEdge;
        public string southEdge;
        public string westEdge;
        public string[] neighborTileIds;
        public string imageKey;
        public bool imageGenerated;
        public string imageStatus;
    }

    [Serializable]
    internal sealed class ProvenanceDto
    {
        public string repository;
        public string universeCommit;
        public int universeSchemaVersion;
        public string universeContentVersion;
        public int importerVersion;
        public string contentHashSha256;
        public int atlasTileCount;
        public int atlasImagesGenerated;
        public int atlasImagesPending;
    }

    public sealed class RuntimeCanonCatalog
    {
        private RuntimeCanonCatalog(
            string universeCommit,
            string contentHash,
            StableId planetId,
            string planetName,
            StableId settlementId,
            string settlementName,
            CanonicalWorldAtlas atlas,
            int generatedAtlasImages,
            int pendingAtlasImages)
        {
            UniverseCommit = universeCommit;
            ContentHashSha256 = contentHash;
            PlanetId = planetId;
            PlanetName = planetName;
            SettlementId = settlementId;
            SettlementName = settlementName;
            Atlas = atlas;
            GeneratedAtlasImages = generatedAtlasImages;
            PendingAtlasImages = pendingAtlasImages;
        }

        public string UniverseCommit { get; }
        public string ContentHashSha256 { get; }
        public StableId PlanetId { get; }
        public string PlanetName { get; }
        public StableId SettlementId { get; }
        public string SettlementName { get; }
        public CanonicalWorldAtlas Atlas { get; }
        public int GeneratedAtlasImages { get; }
        public int PendingAtlasImages { get; }

        public static RuntimeCanonCatalog LoadRequired()
        {
            var catalogText = LoadText("Canon/canon.catalog");
            var atlasText = LoadText("Canon/atlas.index");
            var provenanceText = LoadText("Canon/canon.provenance");

            var catalog = JsonUtility.FromJson<CanonCatalogDto>(catalogText);
            var atlasDto = JsonUtility.FromJson<AtlasIndexDto>(atlasText);
            var provenance = JsonUtility.FromJson<ProvenanceDto>(provenanceText);

            if (catalog == null || atlasDto == null || provenance == null)
            {
                throw new InvalidOperationException("Generated canon assets could not be parsed.");
            }
            if (catalog.formatVersion != 1 || atlasDto.formatVersion != 1)
            {
                throw new InvalidOperationException("Unsupported generated canon format.");
            }
            if (catalog.universeCommit != atlasDto.universeCommit ||
                catalog.universeCommit != provenance.universeCommit)
            {
                throw new InvalidOperationException("Generated canon provenance commit mismatch.");
            }
            if (catalog.planet == null || catalog.settlement == null || atlasDto.atlas == null)
            {
                throw new InvalidOperationException("Generated canon is missing required world records.");
            }
            if (catalog.planet.capitalSettlementId != catalog.settlement.id ||
                catalog.planet.worldAtlasId != atlasDto.atlas.id ||
                atlasDto.atlas.planetId != catalog.planet.id ||
                atlasDto.atlas.originSettlementId != catalog.settlement.id)
            {
                throw new InvalidOperationException("Generated canon world references do not reconcile.");
            }

            var tileDtos = atlasDto.tiles ?? Array.Empty<AtlasTileDto>();
            if (tileDtos.Length != atlasDto.atlas.phaseOneTileCount)
            {
                throw new InvalidOperationException(
                    $"Generated atlas expected {atlasDto.atlas.phaseOneTileCount} tiles but has {tileDtos.Length}.");
            }

            var tiles = new CanonicalAtlasTile[tileDtos.Length];
            for (var i = 0; i < tileDtos.Length; i++)
            {
                var dto = tileDtos[i];
                var neighbors = dto.neighborTileIds ?? Array.Empty<string>();
                var stableNeighbors = new StableId[neighbors.Length];
                for (var n = 0; n < neighbors.Length; n++)
                {
                    stableNeighbors[n] = new StableId(neighbors[n]);
                }

                tiles[i] = new CanonicalAtlasTile(
                    new StableId(dto.id),
                    dto.name,
                    new AtlasCoordinate(dto.x, dto.y),
                    dto.sequence,
                    dto.batch,
                    dto.ring,
                    dto.districtName,
                    ParseZone(dto.zone),
                    dto.sector,
                    dto.description,
                    new EdgeContinuity(dto.northEdge, dto.eastEdge, dto.southEdge, dto.westEdge),
                    stableNeighbors,
                    dto.imageKey,
                    dto.imageGenerated,
                    dto.imageStatus);
            }

            var atlas = new CanonicalWorldAtlas(
                new StableId(atlasDto.atlas.id),
                atlasDto.atlas.name,
                new StableId(atlasDto.atlas.planetId),
                new StableId(atlasDto.atlas.originSettlementId),
                checked(atlasDto.atlas.tileSizeKm * 1000),
                tiles);

            var origin = atlas.GetRequiredTile(new AtlasCoordinate(0, 0));
            if (origin.Id.ToString() != "atlas-tile-koplin-3-x0-y0" ||
                origin.DistrictName != "Federal Forum")
            {
                throw new InvalidOperationException("Canonical atlas origin is not Federal Forum tile (0,0).");
            }

            return new RuntimeCanonCatalog(
                catalog.universeCommit,
                provenance.contentHashSha256,
                new StableId(catalog.planet.id),
                catalog.planet.name,
                new StableId(catalog.settlement.id),
                catalog.settlement.name,
                atlas,
                provenance.atlasImagesGenerated,
                provenance.atlasImagesPending);
        }

        private static string LoadText(string resourcePath)
        {
            var asset = Resources.Load<TextAsset>(resourcePath);
            if (asset == null)
            {
                throw new InvalidOperationException(
                    $"Generated canon resource '{resourcePath}' is missing. Run the Universe importer.");
            }
            return asset.text;
        }

        private static AtlasZone ParseZone(string zone)
        {
            switch (zone)
            {
                case "core": return AtlasZone.Core;
                case "inner": return AtlasZone.Inner;
                case "middle": return AtlasZone.Middle;
                case "outer": return AtlasZone.Outer;
                case "fringe": return AtlasZone.Fringe;
                default: throw new InvalidOperationException($"Unknown atlas zone '{zone}'.");
            }
        }
    }
}
