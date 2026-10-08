using System;
using System.Collections.Generic;
using MineIT.CityBuilder.Core.Diagnostics;

namespace MineIT.CityBuilder.Core.Ids
{
    /// <summary>
    /// Derived runtime lookup. The registry is rebuilt from authoritative records after load
    /// and is never itself persisted as save identity.
    /// </summary>
    public sealed class StableIdRegistry<T>
    {
        private readonly Dictionary<StableId, T> _items = new Dictionary<StableId, T>();

        public int Count => _items.Count;

        public void Add(StableId id, T value, string domain = "core")
        {
            if (_items.ContainsKey(id))
            {
                throw new InvariantViolationException(
                    DiagnosticIds.StableIdCollision,
                    domain,
                    0,
                    $"Duplicate stable ID '{id}'.");
            }

            _items.Add(id, value);
        }

        public bool TryGet(StableId id, out T value) => _items.TryGetValue(id, out value);

        public T GetRequired(StableId id)
        {
            if (!_items.TryGetValue(id, out var value))
            {
                throw new KeyNotFoundException($"Stable ID '{id}' is not registered.");
            }

            return value;
        }

        public void Clear() => _items.Clear();
    }
}
