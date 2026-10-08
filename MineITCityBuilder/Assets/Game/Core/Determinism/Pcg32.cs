using System.Runtime.CompilerServices;
using MineIT.CityBuilder.Core.Ids;

namespace MineIT.CityBuilder.Core.Determinism
{
    /// <summary>
    /// Project-owned PCG-XSH-RR 32 implementation. The algorithm and state shape are
    /// a save/determinism contract; do not replace it without a migration decision.
    /// </summary>
    public struct Pcg32
    {
        private ulong _state;
        private ulong _increment;

        public Pcg32(ulong initialState, ulong sequence)
        {
            _state = 0UL;
            _increment = (sequence << 1) | 1UL;
            NextUInt();
            _state = unchecked(_state + initialState);
            NextUInt();
        }

        private Pcg32(ulong state, ulong increment, bool restored)
        {
            _state = state;
            _increment = increment | 1UL;
        }

        public ulong State => _state;
        public ulong Increment => _increment;

        public static Pcg32 Restore(ulong state, ulong increment) =>
            new Pcg32(state, increment, true);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint NextUInt()
        {
            var oldState = _state;
            _state = unchecked(oldState * 6364136223846793005UL + _increment);
            var xorShifted = (uint)(((oldState >> 18) ^ oldState) >> 27);
            var rotation = (int)(oldState >> 59);
            return (xorShifted >> rotation) | (xorShifted << ((-rotation) & 31));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int NextInt(int exclusiveMax)
        {
            if (exclusiveMax <= 0)
            {
                throw new System.ArgumentOutOfRangeException(nameof(exclusiveMax));
            }

            var bound = (uint)exclusiveMax;
            var threshold = unchecked((uint)(0u - bound)) % bound;
            while (true)
            {
                var value = NextUInt();
                if (value >= threshold)
                {
                    return (int)(value % bound);
                }
            }
        }
    }

    public static class DeterministicSeed
    {
        public static ulong Derive(
            ulong rootSeed,
            ulong streamStableId,
            SaveEntityId entityId,
            long periodKey)
        {
            var hash = Mix(rootSeed);
            hash = Mix(hash ^ streamStableId);
            hash = Mix(hash ^ entityId.High);
            hash = Mix(hash ^ entityId.Low);
            hash = Mix(hash ^ unchecked((ulong)periodKey));
            return hash;
        }

        public static ulong Derive(ulong rootSeed, ulong streamStableId, long periodKey)
        {
            var hash = Mix(rootSeed);
            hash = Mix(hash ^ streamStableId);
            hash = Mix(hash ^ unchecked((ulong)periodKey));
            return hash;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong Mix(ulong value)
        {
            value += 0x9E3779B97F4A7C15UL;
            value = (value ^ (value >> 30)) * 0xBF58476D1CE4E5B9UL;
            value = (value ^ (value >> 27)) * 0x94D049BB133111EBUL;
            return value ^ (value >> 31);
        }
    }

    public static class RandomStreamIds
    {
        public const ulong Migration = 0x6d6967726174696fUL;
        public const ulong HouseholdLifecycle = 0x686f757365686f6cUL;
        public const ulong BusinessCreation = 0x627573696e657373UL;
        public const ulong BuildingGrammar = 0x6275696c64696e67UL;
        public const ulong CitizenNaming = 0x6e616d696e670001UL;
        public const ulong IncidentGeneration = 0x696e636964656e74UL;
    }
}
