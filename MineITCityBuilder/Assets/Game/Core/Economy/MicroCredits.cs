using System;

namespace MineIT.CityBuilder.Core.Economy
{
    /// <summary>
    /// Exact Commonwealth Credit value stored in one-millionth-credit units.
    /// No authoritative money calculation uses floating point.
    /// </summary>
    public readonly struct MicroCredits : IEquatable<MicroCredits>, IComparable<MicroCredits>
    {
        public const long UnitsPerCredit = 1_000_000L;

        public MicroCredits(long value) => Value = value;

        public long Value { get; }

        public static MicroCredits FromWholeCredits(long credits) =>
            new MicroCredits(checked(credits * UnitsPerCredit));

        public bool Equals(MicroCredits other) => Value == other.Value;
        public override bool Equals(object obj) => obj is MicroCredits other && Equals(other);
        public override int GetHashCode() => Value.GetHashCode();
        public int CompareTo(MicroCredits other) => Value.CompareTo(other.Value);
        public override string ToString() => $"{Value / (decimal)UnitsPerCredit:0.######} CC";

        public static MicroCredits operator +(MicroCredits left, MicroCredits right) =>
            new MicroCredits(checked(left.Value + right.Value));

        public static MicroCredits operator -(MicroCredits left, MicroCredits right) =>
            new MicroCredits(checked(left.Value - right.Value));

        public static MicroCredits operator -(MicroCredits value) =>
            new MicroCredits(checked(-value.Value));

        public static MicroCredits operator *(MicroCredits value, long multiplier) =>
            new MicroCredits(checked(value.Value * multiplier));
    }
}
