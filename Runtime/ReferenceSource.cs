using System;
using UnityEngine;

namespace BananaParty.Arch
{
    [DefaultExecutionOrder(-110000)]
    public abstract class ReferenceSource<T> : MonoBehaviour where T : class
    {
        [SerializeField]
        private AbstractReferenceAsset _reference;

        [SerializeField, HideInInspector]
        private ReferenceAsset<T> _referenceAsset;

        [SerializeField, HideInInspector]
        private ReferenceListAsset<T> _referenceListAsset;

        private T _entryComponent;

        private void Awake()
        {
            _entryComponent = GetComponent<T>();
            if (_entryComponent == null)
                throw new NullReferenceException($"Component {typeof(T).Name} not found.");
        }

        private void OnEnable()
        {
            if (_referenceListAsset != null)
                _referenceListAsset.Add(_entryComponent);
            else if (_referenceAsset != null)
                _referenceAsset.Set(_entryComponent);
        }

        private void OnDisable()
        {
            if (_referenceListAsset != null)
                _referenceListAsset.Remove(_entryComponent);
            else if (_referenceAsset != null)
                _referenceAsset.Release(_entryComponent);
        }

        private void OnValidate()
        {
            if (_reference == null)
            {
                _referenceAsset = null;
                _referenceListAsset = null;
                return;
            }

            if (_reference is ReferenceAsset<T> referenceAsset)
            {
                _referenceAsset = referenceAsset;
                _referenceListAsset = null;
            }
            else if (_reference is ReferenceListAsset<T> referenceListAsset)
            {
                _referenceListAsset = referenceListAsset;
                _referenceAsset = null;
            }
            else
            {
                Debug.LogError($"Reference is of incompatible type. Expected ReferenceAsset<{typeof(T).Name}> or ReferenceListAsset<{typeof(T).Name}>, but got {_reference.GetType().Name}.");
                _reference = null;
                _referenceAsset = null;
                _referenceListAsset = null;
            }
        }
    }
}
