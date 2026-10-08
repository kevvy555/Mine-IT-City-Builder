using UnityEngine;

namespace MineIT.CityBuilder.Bootstrap
{
    public readonly struct PrimitiveBuilding
    {
        public PrimitiveBuilding(Vector3 position, Vector3 scale, int paletteIndex, bool accent)
        {
            Position = position;
            Scale = scale;
            PaletteIndex = paletteIndex;
            Accent = accent;
        }

        public Vector3 Position { get; }
        public Vector3 Scale { get; }
        public int PaletteIndex { get; }
        public bool Accent { get; }
    }

    public static class BootstrapLayout
    {
        public const int Width = 100;
        public const int Depth = 100;
        public const int BuildingCount = Width * Depth;
        public const float BaseSpacing = 5.5f;
        public const float AvenueGap = 8f;
        public const int AvenueInterval = 10;

        public static PrimitiveBuilding[] Generate(uint seed = 5300u)
        {
            var output = new PrimitiveBuilding[BuildingCount];
            var index = 0;

            for (var z = 0; z < Depth; z++)
            {
                for (var x = 0; x < Width; x++)
                {
                    var hash = Hash((uint)x, (uint)z, seed);
                    var height = 8f + ((hash >> 8) & 0x3Fu) * 0.72f;
                    var width = 3.2f + ((hash >> 16) & 0x7u) * 0.16f;
                    var depth = 3.2f + ((hash >> 20) & 0x7u) * 0.16f;
                    var worldX = x * BaseSpacing + (x / AvenueInterval) * AvenueGap;
                    var worldZ = z * BaseSpacing + (z / AvenueInterval) * AvenueGap;
                    var palette = (int)(hash % 3u);
                    var accent = ((hash >> 4) & 0x7u) == 0u;

                    output[index++] = new PrimitiveBuilding(
                        new Vector3(worldX, height * 0.5f, worldZ),
                        new Vector3(width, height, depth),
                        palette,
                        accent);
                }
            }

            return output;
        }

        public static Vector3 CityCentre
        {
            get
            {
                var x = (Width - 1) * BaseSpacing + ((Width - 1) / AvenueInterval) * AvenueGap;
                var z = (Depth - 1) * BaseSpacing + ((Depth - 1) / AvenueInterval) * AvenueGap;
                return new Vector3(x * 0.5f, 0f, z * 0.5f);
            }
        }

        public static float CityExtent
        {
            get
            {
                var x = (Width - 1) * BaseSpacing + ((Width - 1) / AvenueInterval) * AvenueGap;
                var z = (Depth - 1) * BaseSpacing + ((Depth - 1) / AvenueInterval) * AvenueGap;
                return Mathf.Max(x, z);
            }
        }

        private static uint Hash(uint x, uint z, uint seed)
        {
            unchecked
            {
                var value = seed ^ (x * 0x8DA6B343u) ^ (z * 0xD8163841u);
                value ^= value >> 16;
                value *= 0x7FEB352Du;
                value ^= value >> 15;
                value *= 0x846CA68Bu;
                value ^= value >> 16;
                return value;
            }
        }
    }
}
