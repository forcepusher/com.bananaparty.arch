using System;
using UnityEngine;

namespace BananaParty.Arch
{
    public abstract class ReferenceAsset<T> : AbstractReferenceAsset where T : class
    {
        [SerializeField]
        private bool _neverUnload = false;

        private T _entry = null;

        public T Value
        {
            get
            {
                if (_entry == null)
                    throw new InvalidOperationException($"Attempt to get {typeof(T).Name} when it's not set in {GetType().Name}.");

                return _entry;
            }
        }

        private void OnEnable()
        {
            _entry = null;

            if (_neverUnload)
                hideFlags |= HideFlags.DontUnloadUnusedAsset;
            else
                hideFlags &= ~HideFlags.DontUnloadUnusedAsset;
        }

        public void Set(T entry)
        {
            if (_entry != null)
                throw new InvalidOperationException($"Attempt to {nameof(Set)} {typeof(T).Name} when it's already set in {GetType().Name}.");

            _entry = entry;
        }

        public void Release(T entry)
        {
            if (_entry == null)
                throw new InvalidOperationException($"Attempt to {nameof(Release)} {typeof(T).Name} when it's not set in {GetType().Name}.");

            if (_entry != entry)
                throw new InvalidOperationException($"Attempt to {nameof(Release)} {typeof(T).Name} when it doesn't match the current one in {GetType().Name}.");

            _entry = null;
        }
    }
}
