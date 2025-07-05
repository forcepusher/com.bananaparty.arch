using System;
using UnityEngine;

namespace BananaParty.Arch
{
    [DefaultExecutionOrder(-110000)]
    public abstract class ReferenceSource<T> : MonoBehaviour where T : class
    {
        [SerializeField]
        private AbstractReferenceAsset _referenceAsset;

        private T _entryComponent;

        private void Awake()
        {
            _entryComponent = GetComponent<T>();
            if (_entryComponent == null)
                throw new NullReferenceException($"Component {nameof(T)} not found.");
        }

        private void OnEnable()
        {
            _referenceAsset.Register(_entryComponent);
        }

        private void OnDisable()
        {
            _referenceAsset.Unregister(_entryComponent);
        }
    }
}
