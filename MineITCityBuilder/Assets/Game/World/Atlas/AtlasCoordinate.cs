using System;

namespace MineIT.CityBuilder.World.Atlas
{
    public readonly struct AtlasCoordinate : IEquatable<AtlasCoordinate>, IComparable<AtlasCoordinate>
    {
        public AtlasCoordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }

        public int CompareTo(AtlasCoordinate other)
        {
            var y = Y.CompareTo(other.Y);
            return y != 0 ? y : X.CompareTo(other.X);
        }

        public bool Equals(AtlasCoordinate other) => X == other.X && Y == other.Y;
        public override bool Equals(object obj) => obj is AtlasCoordinate other && Equals(other);
        public override int GetHashCode() => unchecked((X * 397) ^ Y);
        public override string ToString() => $"({X},{Y})";

        public static bool operator ==(AtlasCoordinate left, AtlasCoordinate right) => left.Equals(right);
        public static bool operator !=(AtlasCoordinate left, AtlasCoordinate right) => !left.Equals(right);
    }

    public readonly struct WorldPositionMillimetres : IEquatable<WorldPositionMillimetres>
    {
        public WorldPositionMillimetres(long x, long y, long z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public long X { get; }
        public long Y { get; }
        public long Z { get; }

        public bool Equals(WorldPositionMillimetres other) =>
            X == other.X && Y == other.Y && Z == other.Z;

        public override bool Equals(object obj) =>
            obj is WorldPositionMillimetres other && Equals(other);

        public override int GetHashCode() =>
            unchecked((((X.GetHashCode() * 397) ^ Y.GetHashCode()) * 397) ^ Z.GetHashCode());
    }
}
