using System;
using UnityEngine;

namespace BananaParty.Arch
{
    public abstract class ReferenceAsset<T> : AbstractReferenceAsset where T : class
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

        private void Register(T entry)
        {
            if (_entry != null)
                throw new InvalidOperationException($"Attempt to {nameof(Register)} {nameof(T)} when it's already set in {nameof(ReferenceAsset<T>)}.");

            _entry = entry;
        }

        private void Unregister(T entry)
        {
            if (_entry == null)
                throw new InvalidOperationException($"Attempt to {nameof(Unregister)} {nameof(T)} when it's not set in {nameof(ReferenceAsset<T>)}.");

            if (_entry != entry)
                throw new InvalidOperationException($"Attempt to {nameof(Unregister)} {nameof(T)} when it doesn't match the current one in {nameof(ReferenceAsset<T>)}.");

            _entry = null;
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

        public T GetEntry()
        {
            if (_entry == null)
                throw new InvalidOperationException($"Attempt to {nameof(GetEntry)} {nameof(T)} when it's not set in {nameof(ReferenceAsset<T>)}.");

            return _entry;
        }
    }
}
