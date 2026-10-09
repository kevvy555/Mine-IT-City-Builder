using System;
using MineIT.CityBuilder.World.Atlas;

namespace MineIT.CityBuilder.World.Chunks
{
    public readonly struct ChunkKey : IEquatable<ChunkKey>, IComparable<ChunkKey>
    {
        public const int ChunksPerAxis = 4;
        public const int ChunkSizeMetres = 250;
        public const int TileSizeMetres = 1000;

        public ChunkKey(int atlasTileX, int atlasTileY, byte localX, byte localY)
        {
            if (localX >= ChunksPerAxis)
            {
                throw new ArgumentOutOfRangeException(nameof(localX));
            }
            if (localY >= ChunksPerAxis)
            {
                throw new ArgumentOutOfRangeException(nameof(localY));
            }

            AtlasTileX = atlasTileX;
            AtlasTileY = atlasTileY;
            LocalX = localX;
            LocalY = localY;
        }

        public int AtlasTileX { get; }
        public int AtlasTileY { get; }
        public byte LocalX { get; }
        public byte LocalY { get; }
        public int LocalIndex => LocalY * ChunksPerAxis + LocalX;
        public AtlasCoordinate TileCoordinate => new AtlasCoordinate(AtlasTileX, AtlasTileY);

        public WorldPositionMillimetres OriginMillimetres =>
            new WorldPositionMillimetres(
                ((long)AtlasTileX * TileSizeMetres + LocalX * ChunkSizeMetres) * 1000L,
                0L,
                ((long)AtlasTileY * TileSizeMetres + LocalY * ChunkSizeMetres) * 1000L);

        public int CompareTo(ChunkKey other)
        {
            var tileY = AtlasTileY.CompareTo(other.AtlasTileY);
            if (tileY != 0) return tileY;
            var tileX = AtlasTileX.CompareTo(other.AtlasTileX);
            if (tileX != 0) return tileX;
            return LocalIndex.CompareTo(other.LocalIndex);
        }

        public bool Equals(ChunkKey other) =>
            AtlasTileX == other.AtlasTileX &&
            AtlasTileY == other.AtlasTileY &&
            LocalX == other.LocalX &&
            LocalY == other.LocalY;

        public override bool Equals(object obj) => obj is ChunkKey other && Equals(other);
        public override int GetHashCode() =>
            unchecked(((((AtlasTileX * 397) ^ AtlasTileY) * 397) ^ LocalX) * 397 ^ LocalY);
        public override string ToString() =>
            $"{AtlasTileX},{AtlasTileY}:{LocalX},{LocalY}";

        public static ChunkKey[] ForTile(AtlasCoordinate coordinate)
        {
            var result = new ChunkKey[ChunksPerAxis * ChunksPerAxis];
            var index = 0;
            for (byte y = 0; y < ChunksPerAxis; y++)
            {
                for (byte x = 0; x < ChunksPerAxis; x++)
                {
                    result[index++] = new ChunkKey(coordinate.X, coordinate.Y, x, y);
                }
            }
            return result;
        }
    }
}
