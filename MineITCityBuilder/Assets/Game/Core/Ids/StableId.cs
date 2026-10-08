using System;
using Unity.Collections;

namespace MineIT.CityBuilder.Core.Ids
{
    /// <summary>
    /// Stable string identity used for canonical and game-authored definitions.
    /// Never persist Unity ECS Entity handles or display names as identity.
    /// </summary>
    public readonly struct StableId : IEquatable<StableId>, IComparable<StableId>
    {
        public const int CapacityBytes = 128;

        private readonly FixedString128Bytes _value;

        public StableId(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Stable IDs cannot be null, empty or whitespace.", nameof(value));
            }

            _value = new FixedString128Bytes(value);
        }

        public StableId(FixedString128Bytes value)
        {
            if (value.Length == 0)
            {
                throw new ArgumentException("Stable IDs cannot be empty.", nameof(value));
            }

            _value = value;
        }

        public FixedString128Bytes Value => _value;

        public bool Equals(StableId other) => _value.Equals(other._value);

        public override bool Equals(object obj) => obj is StableId other && Equals(other);

        public override int GetHashCode() => _value.GetHashCode();

        public int CompareTo(StableId other) => string.CompareOrdinal(ToString(), other.ToString());

        public override string ToString() => _value.ToString();

        public static bool operator ==(StableId left, StableId right) => left.Equals(right);

        public static bool operator !=(StableId left, StableId right) => !left.Equals(right);
    }

    public readonly struct CanonicalId : IEquatable<CanonicalId>
    {
        public CanonicalId(string value) => Value = new StableId(value);
        public CanonicalId(StableId value) => Value = value;
        public StableId Value { get; }
        public bool Equals(CanonicalId other) => Value.Equals(other.Value);
        public override bool Equals(object obj) => obj is CanonicalId other && Equals(other);
        public override int GetHashCode() => Value.GetHashCode();
        public override string ToString() => Value.ToString();
    }

    public readonly struct ContentId : IEquatable<ContentId>
    {
        public ContentId(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value.IndexOf('.') <= 0)
            {
                throw new ArgumentException(
                    "Game-authored content IDs must be namespaced (for example building.residential.garden-midrise).",
                    nameof(value));
            }

            Value = new StableId(value);
        }

        public ContentId(StableId value) => Value = value;
        public StableId Value { get; }
        public bool Equals(ContentId other) => Value.Equals(other.Value);
        public override bool Equals(object obj) => obj is ContentId other && Equals(other);
        public override int GetHashCode() => Value.GetHashCode();
        public override string ToString() => Value.ToString();
    }
}
