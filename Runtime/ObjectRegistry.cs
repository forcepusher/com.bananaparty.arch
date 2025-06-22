using System;
using UnityEngine;

namespace BananaParty.Registry
{
    public abstract class ObjectRegistry<T> : ScriptableObject where T : class
    {
        [SerializeField]
        private bool _neverUnload = false;

        private T _entry = null;

        private void OnEnable()
        {
            _entry = null;

            if (_neverUnload)
                hideFlags |= HideFlags.DontUnloadUnusedAsset;
            else
                hideFlags &= ~HideFlags.DontUnloadUnusedAsset;
        }

        public void SetEntry(T entry)
        {
            if (_entry != null)
                throw new InvalidOperationException($"Attempt to {nameof(SetEntry)} {nameof(T)} when it's already set in {nameof(ObjectRegistry<T>)}.");

            _entry = entry;
        }

        public void ReleaseEntry(T entry)
        {
            if (_entry == null)
                throw new InvalidOperationException($"Attempt to {nameof(ReleaseEntry)} {nameof(T)} when it's not set in {nameof(ObjectRegistry<T>)}.");

            if (_entry != entry)
                throw new InvalidOperationException($"Attempt to {nameof(ReleaseEntry)} {nameof(T)} when it doesn't match the current one in {nameof(ObjectRegistry<T>)}.");

            _entry = null;
        }

        public T GetEntry()
        {
            if (_entry == null)
                throw new InvalidOperationException($"Attempt to {nameof(GetEntry)} {nameof(T)} when it's not set in {nameof(ObjectRegistry<T>)}.");

            return _entry;
        }
    }
}
