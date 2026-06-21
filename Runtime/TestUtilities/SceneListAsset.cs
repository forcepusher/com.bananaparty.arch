using System.Collections.Generic;
using UnityEngine;

namespace BananaParty.Arch.TestUtilities
{
    [CreateAssetMenu]
    public class SceneListAsset : ScriptableObject
    {
        [field: SerializeField]
        public List<SceneReference> SceneReferences { get; private set; }
    }
}
