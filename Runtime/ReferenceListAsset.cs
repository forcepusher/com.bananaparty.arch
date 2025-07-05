using System;
using System.Collections.Generic;
using UnityEngine;

namespace BananaParty.Arch
{
    public abstract class ReferenceListAsset<T> : AbstractReferenceAsset where T : class
    {
        [SerializeField]
        private bool _neverUnload = false;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0032:Use auto property", Justification = "Reserved for changes and consistency")]
        private readonly List<T> _entries = new();
        private readonly Dictionary<T, int> _entriesIndexLookupTable = new();

        public List<T> Values => _entries;

        private void OnEnable()
        {
            _entries.Clear();
            _entriesIndexLookupTable.Clear();

            if (_neverUnload)
                hideFlags |= HideFlags.DontUnloadUnusedAsset;
            else
                hideFlags &= ~HideFlags.DontUnloadUnusedAsset;
        }

        public void Add(T entry)
        {
            if (entry == null)
                throw new ArgumentNullException(nameof(entry));

            if (_entriesIndexLookupTable.TryAdd(entry, _entries.Count))
                _entries.Add(entry);
            else
                throw new InvalidOperationException($"Attempt to {nameof(Add)} {typeof(T).Name} that already exists in {GetType().Name}.");
        }

        public void Remove(T entry)
        {
            if (entry == null)
                throw new ArgumentNullException(nameof(entry));

            if (_entriesIndexLookupTable.Remove(entry, out int entryIndexToRemove))
            {
                int lastEntryIndex = _entries.Count - 1;

                if (entryIndexToRemove < lastEntryIndex)
                {
                    T lastEntry = _entries[lastEntryIndex];
                    _entries[entryIndexToRemove] = lastEntry;
                    _entriesIndexLookupTable[lastEntry] = entryIndexToRemove;
                }

                _entries.RemoveAt(lastEntryIndex);
            }
            else
            {
                throw new InvalidOperationException($"Attempt to {nameof(Remove)} {typeof(T).Name} that doesn't exist in {GetType().Name}.");
            }
        }
    }
}
