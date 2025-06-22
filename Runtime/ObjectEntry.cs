using System;
using UnityEngine;

namespace BananaParty.Registry
{
    [DefaultExecutionOrder(-110000)]
    public abstract class ObjectEntry<T> : MonoBehaviour where T : class
    {
        [SerializeField]
        private ObjectRegistry<T> _objectRegistry;

        private T _entryComponent;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Type Safety", "UNT0014:Invalid type for call to GetComponent", Justification = "Interface is actually a valid type for GetComponent")]
        private void Awake()
        {
            _entryComponent = GetComponent<T>();
            if (_entryComponent == null)
                throw new NullReferenceException($"Component {nameof(T)} not found.");
        }

        private void OnEnable()
        {
            _objectRegistry.SetEntry(_entryComponent);
        }

        private void OnDisable()
        {
            _objectRegistry.ReleaseEntry(_entryComponent);
        }
    }
}
