using System;
using System.Collections.Generic;
using UnityEngine;

namespace BananaParty.Arch
{
    public abstract class ReferenceListAsset<T> : AbstractReferenceAsset where T : class
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

        private void Register(T entry)
        {
            if (entry == null)
                throw new ArgumentNullException(nameof(entry));

            if (_entriesIndexLookupTable.TryAdd(entry, _entries.Count))
                _entries.Add(entry);
            else
                throw new InvalidOperationException($"Attempt to {nameof(Register)} {nameof(T)} that already exists in {nameof(ReferenceListAsset<T>)}.");
        }

        private void Unregister(T entry)
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
                throw new InvalidOperationException($"Attempt to {nameof(Unregister)} {nameof(T)} that doesn't exist in {nameof(ReferenceListAsset<T>)}.");
            }
        }

        public override void Register(object entry)
        {
            if (entry is T typedEntry)
                Register(typedEntry);
            else
                throw new ArgumentException($"Entry must be of type {typeof(T).Name}", nameof(entry));
        }

        public override void Unregister(object entry)
        {
            if (entry is T typedEntry)
                Unregister(typedEntry);
            else
                throw new ArgumentException($"Entry must be of type {typeof(T).Name}", nameof(entry));
        }

        public List<T> GetAllEntries()
        {
            return _entries;
        }
    }
}
