using System;
using System.Collections.Generic;

namespace MineIT.CityBuilder.Core.Ids
{
    public readonly struct SaveEntityAllocatorEntry
    {
        public SaveEntityAllocatorEntry(ushort entityType, ulong nextSequence)
        {
            EntityType = entityType;
            NextSequence = nextSequence;
        }

        public ushort EntityType { get; }
        public ulong NextSequence { get; }
    }

    /// <summary>
    /// Persisted monotonic allocator. A sequence advances before an ID can be issued again.
    /// </summary>
    public sealed class SaveEntityIdAllocator
    {
        private readonly Dictionary<ushort, ulong> _nextByType = new Dictionary<ushort, ulong>();

        public SaveEntityIdAllocator(ulong worldInstanceId)
        {
            if (worldInstanceId == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(worldInstanceId), "World instance ID 0 is reserved.");
            }

            WorldInstanceId = worldInstanceId;
        }

        public ulong WorldInstanceId { get; }

        public SaveEntityId Allocate(ushort entityType)
        {
            if (entityType == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(entityType), "Entity type 0 is reserved.");
            }

            _nextByType.TryGetValue(entityType, out var next);
            if (next > SaveEntityId.MaxSequence)
            {
                throw new OverflowException($"Stable ID sequence exhausted for entity type {entityType}.");
            }

            var id = new SaveEntityId(WorldInstanceId, entityType, next);
            _nextByType[entityType] = checked(next + 1UL);
            return id;
        }

        public SaveEntityAllocatorEntry[] ExportState()
        {
            var keys = new List<ushort>(_nextByType.Keys);
            keys.Sort();

            var entries = new SaveEntityAllocatorEntry[keys.Count];
            for (var i = 0; i < keys.Count; i++)
            {
                entries[i] = new SaveEntityAllocatorEntry(keys[i], _nextByType[keys[i]]);
            }

            return entries;
        }

        public void RestoreState(IEnumerable<SaveEntityAllocatorEntry> entries)
        {
            _nextByType.Clear();
            if (entries == null)
            {
                return;
            }

            foreach (var entry in entries)
            {
                if (entry.EntityType == 0 || entry.NextSequence > SaveEntityId.MaxSequence + 1UL)
                {
                    throw new InvalidOperationException("Allocator state contains an invalid entity type or sequence.");
                }

                if (_nextByType.ContainsKey(entry.EntityType))
                {
                    throw new InvalidOperationException($"Duplicate allocator entry for entity type {entry.EntityType}.");
                }

                _nextByType.Add(entry.EntityType, entry.NextSequence);
            }
        }
    }
}
