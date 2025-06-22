using System;
using System.Collections.Generic;
using UnityEngine;

namespace BananaParty.Registry
{
    public abstract class ListRegistry<T> : ScriptableObject where T : class
    {
        [SerializeField]
        private bool _neverUnload = false;

        private readonly List<T> _entries = new();
        private readonly Dictionary<T, int> _entriesIndexLookupTable = new();

        private void OnEnable()
        {
            _entries.Clear();
            _entriesIndexLookupTable.Clear();

            if (_neverUnload)
                hideFlags |= HideFlags.DontUnloadUnusedAsset;
            else
                hideFlags &= ~HideFlags.DontUnloadUnusedAsset;
        }

        public void AddEntry(T entry)
        {
            if (entry == null)
                throw new ArgumentNullException(nameof(entry));

            if (_entriesIndexLookupTable.TryAdd(entry, _entries.Count))
                _entries.Add(entry);
            else
                throw new InvalidOperationException($"Attempt to {nameof(AddEntry)} {nameof(T)} that already exists in {nameof(ListRegistry<T>)}.");
        }

        public void RemoveEntry(T entry)
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
                throw new InvalidOperationException($"Attempt to {nameof(RemoveEntry)} {nameof(T)} that doesn't exist in {nameof(ListRegistry<T>)}.");
            }
        }

        public List<T> GetAllEntries()
        {
            return _entries;
        }
    }
}
