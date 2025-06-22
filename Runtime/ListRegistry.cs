using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace BananaParty.Registry
{
    public abstract class ListRegistry<T> : ScriptableObject where T : class
    {
        [SerializeField]
        private bool _neverUnload = false;

        private readonly List<T> _entries = new();
        private readonly Dictionary<T, int> _entriesLookup = new();

        private void OnEnable()
        {
            _entries.Clear();
            _entriesLookup.Clear();

            if (_neverUnload)
                hideFlags |= HideFlags.DontUnloadUnusedAsset;
            else
                hideFlags &= ~HideFlags.DontUnloadUnusedAsset;
        }

        public void AddEntry(T entry)
        {
            if (entry == null)
                throw new ArgumentNullException(nameof(entry));

            if (_entriesLookup.TryAdd(entry, _entries.Count))
                _entries.Add(entry);
            else
                throw new InvalidOperationException($"Attempt to {nameof(AddEntry)} {nameof(T)} that already exists in {nameof(ListRegistry<T>)}.");
        }

        public void RemoveEntry(T entry)
        {
            if (entry == null)
                throw new ArgumentNullException(nameof(entry));

            if (_entriesHashSet.Remove(entry, out var index))
            {
                _entries[index] = _entries[^1];
                _entriesLookup[_entries[^1]] = index;
                _entries.RemoveAt(_entries.Count - 1);
            }
            else
                throw new InvalidOperationException($"Attempt to {nameof(RemoveEntry)} {nameof(T)} that doesn't exist in {nameof(ListRegistry<T>)}.");
        }

        public List<T> GetAllEntries()
        {
            return _entries;
        }
    }
}
