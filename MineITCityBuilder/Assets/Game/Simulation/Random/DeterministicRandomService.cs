using System;
using System.Collections.Generic;
using MineIT.CityBuilder.Core.Determinism;

namespace MineIT.CityBuilder.Simulation.Random
{
    public readonly struct RandomStreamState
    {
        public RandomStreamState(ulong streamId, ulong state, ulong increment)
        {
            StreamId = streamId;
            State = state;
            Increment = increment;
        }

        public ulong StreamId { get; }
        public ulong State { get; }
        public ulong Increment { get; }
    }

    /// <summary>
    /// Named stateful streams. Independent stream IDs prevent a new random draw in one
    /// subsystem from reordering unrelated domains.
    /// </summary>
    public sealed class DeterministicRandomService : IDeterministicChecksumContributor
    {
        private readonly Dictionary<ulong, Pcg32> _streams = new Dictionary<ulong, Pcg32>();

        public DeterministicRandomService(ulong rootSeed)
        {
            RootSeed = rootSeed;
        }

        public ulong RootSeed { get; }

        public uint NextUInt(ulong streamId)
        {
            var stream = GetStream(streamId);
            var value = stream.NextUInt();
            _streams[streamId] = stream;
            return value;
        }

        public int NextInt(ulong streamId, int exclusiveMax)
        {
            var stream = GetStream(streamId);
            var value = stream.NextInt(exclusiveMax);
            _streams[streamId] = stream;
            return value;
        }

        public RandomStreamState[] ExportState()
        {
            var keys = new List<ulong>(_streams.Keys);
            keys.Sort();
            var states = new RandomStreamState[keys.Count];

            for (var i = 0; i < keys.Count; i++)
            {
                var stream = _streams[keys[i]];
                states[i] = new RandomStreamState(keys[i], stream.State, stream.Increment);
            }

            return states;
        }

        public void RestoreState(RandomStreamState[] states)
        {
            _streams.Clear();
            if (states == null)
            {
                return;
            }

            for (var i = 0; i < states.Length; i++)
            {
                if (_streams.ContainsKey(states[i].StreamId))
                {
                    throw new InvalidOperationException($"Duplicate random stream ID {states[i].StreamId:x16}.");
                }

                _streams.Add(
                    states[i].StreamId,
                    Pcg32.Restore(states[i].State, states[i].Increment));
            }
        }

        public void AddToChecksum(ref DeterministicChecksum checksum)
        {
            checksum.Add(RootSeed);
            var states = ExportState();
            checksum.Add(states.Length);
            for (var i = 0; i < states.Length; i++)
            {
                checksum.Add(states[i].StreamId);
                checksum.Add(states[i].State);
                checksum.Add(states[i].Increment);
            }
        }

        private Pcg32 GetStream(ulong streamId)
        {
            if (_streams.TryGetValue(streamId, out var stream))
            {
                return stream;
            }

            var state = DeterministicSeed.Derive(RootSeed, streamId, 0L);
            var sequence = DeterministicSeed.Derive(RootSeed, streamId, 1L);
            return new Pcg32(state, sequence);
        }
    }
}
