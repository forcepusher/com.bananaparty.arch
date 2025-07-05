using System;
using UnityEngine;

namespace BananaParty.Arch
{
    [Serializable]
    public abstract class AbstractReferenceAsset : ScriptableObject
    {
        public abstract void Register(object entry);
        public abstract void Unregister(object entry);
    }
} 
