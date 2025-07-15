using System.Collections.Generic;
using UnityEngine;

namespace BananaParty.Arch.TestingUtilities
{
    [CreateAssetMenu]
    public class StringListAsset : ScriptableObject
    {
        [field: SerializeField]
        public List<string> Values { get; private set; }
    }
}
