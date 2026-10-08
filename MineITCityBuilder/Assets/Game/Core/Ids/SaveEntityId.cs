using System;

namespace MineIT.CityBuilder.Core.Ids
{
    /// <summary>
    /// Deterministic 128-bit save-created identity.
    /// High 64 bits are the world instance. Low 64 bits contain a 16-bit entity type
    /// and a 48-bit monotonic sequence. IDs are never reused inside a save.
    /// </summary>
    public readonly struct SaveEntityId : IEquatable<SaveEntityId>, IComparable<SaveEntityId>
    {
        public const ulong MaxSequence = 0x0000FFFFFFFFFFFFUL;

        public SaveEntityId(ulong worldInstanceId, ushort entityType, ulong sequence)
        {
            if (sequence > MaxSequence)
            {
                throw new ArgumentOutOfRangeException(nameof(sequence), sequence, "Save entity sequence exceeds 48-bit capacity.");
            }

            High = worldInstanceId;
            Low = ((ulong)entityType << 48) | sequence;
        }

        private SaveEntityId(ulong high, ulong low, bool raw)
        {
            High = high;
            Low = low;
        }

        public ulong High { get; }
        public ulong Low { get; }
        public ulong WorldInstanceId => High;
        public ushort EntityType => (ushort)(Low >> 48);
        public ulong Sequence => Low & MaxSequence;

        public static SaveEntityId FromRaw(ulong high, ulong low) => new SaveEntityId(high, low, true);

        public bool Equals(SaveEntityId other) => High == other.High && Low == other.Low;
        public override bool Equals(object obj) => obj is SaveEntityId other && Equals(other);
        public override int GetHashCode() => unchecked((High.GetHashCode() * 397) ^ Low.GetHashCode());

        public int CompareTo(SaveEntityId other)
        {
            var high = High.CompareTo(other.High);
            return high != 0 ? high : Low.CompareTo(other.Low);
        }

        public override string ToString() => $"{High:x16}-{EntityType:x4}-{Sequence:x12}";

        public static bool operator ==(SaveEntityId left, SaveEntityId right) => left.Equals(right);
        public static bool operator !=(SaveEntityId left, SaveEntityId right) => !left.Equals(right);
    }
}
