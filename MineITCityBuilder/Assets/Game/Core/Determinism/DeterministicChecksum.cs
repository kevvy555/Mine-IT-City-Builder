using MineIT.CityBuilder.Core.Ids;

namespace MineIT.CityBuilder.Core.Determinism
{
    public struct DeterministicChecksum
    {
        private const ulong OffsetBasis = 14695981039346656037UL;
        private const ulong Prime = 1099511628211UL;

        private ulong _value;

        public DeterministicChecksum(bool initialise)
        {
            _value = OffsetBasis;
        }

        public ulong Value => _value == 0UL ? OffsetBasis : _value;

        public void Add(byte value)
        {
            EnsureInitialised();
            _value ^= value;
            _value = unchecked(_value * Prime);
        }

        public void Add(bool value) => Add((byte)(value ? 1 : 0));

        public void Add(ushort value)
        {
            Add((byte)value);
            Add((byte)(value >> 8));
        }

        public void Add(uint value)
        {
            Add((byte)value);
            Add((byte)(value >> 8));
            Add((byte)(value >> 16));
            Add((byte)(value >> 24));
        }

        public void Add(int value) => Add(unchecked((uint)value));

        public void Add(ulong value)
        {
            Add((uint)value);
            Add((uint)(value >> 32));
        }

        public void Add(long value) => Add(unchecked((ulong)value));

        public void Add(SaveEntityId value)
        {
            Add(value.High);
            Add(value.Low);
        }

        private void EnsureInitialised()
        {
            if (_value == 0UL)
            {
                _value = OffsetBasis;
            }
        }
    }

    public interface IDeterministicChecksumContributor
    {
        void AddToChecksum(ref DeterministicChecksum checksum);
    }
}
