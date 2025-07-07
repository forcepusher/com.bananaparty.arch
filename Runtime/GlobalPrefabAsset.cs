#if UNITY_EDITOR
using System.Linq;
#endif
using UnityEngine;

namespace BananaParty.Arch
{
    [CreateAssetMenu(fileName = "GlobalContextAsset")]
    public class GlobalPrefabAsset : ScriptableObject
    {
        private static GlobalPrefabAsset[] s_instances;

        [SerializeField]
        internal GameObject _prefab;

        [SerializeField]
        private RuntimeInitializeLoadType _instantiationTime = RuntimeInitializeLoadType.SubsystemRegistration;

        private static void InstantiatePrefabIfMatchLoadType(RuntimeInitializeLoadType loadType)
        {
            foreach (GlobalPrefabAsset instance in s_instances)
            {
                if (instance._prefab == null)
                {
                    Debug.LogError($"{nameof(GlobalPrefabAsset)} {instance.name} has no {nameof(_prefab)} assigned!", instance);
                    continue;
                }

                if (instance._instantiationTime == loadType)
                {
                    GameObject prefabInstance = Instantiate(instance._prefab);
                    prefabInstance.name = instance._prefab.name;
                    DontDestroyOnLoad(prefabInstance);
                }
            }
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void AfterSceneLoad()
        {
            InstantiatePrefabIfMatchLoadType(RuntimeInitializeLoadType.AfterSceneLoad);
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void BeforeSceneLoad()
        {
            InstantiatePrefabIfMatchLoadType(RuntimeInitializeLoadType.BeforeSceneLoad);
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void AfterAssembliesLoaded()
        {
            InstantiatePrefabIfMatchLoadType(RuntimeInitializeLoadType.AfterAssembliesLoaded);
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
        private static void BeforeSplashScreen()
        {
            InstantiatePrefabIfMatchLoadType(RuntimeInitializeLoadType.BeforeSplashScreen);
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void SubsystemRegistration()
        {
#if UNITY_EDITOR
            // Preloaded Assets are broken in the Editor, have to be preloaded manually.
            s_instances = UnityEditor.PlayerSettings.GetPreloadedAssets().OfType<GlobalPrefabAsset>().ToArray();
#else
            s_instances = (GlobalPrefabAsset[])Resources.FindObjectsOfTypeAll(typeof(GlobalPrefabAsset));
#endif

            InstantiatePrefabIfMatchLoadType(RuntimeInitializeLoadType.SubsystemRegistration);
        }
    }
}

#if UNITY_EDITOR
namespace BananaParty.Arch.Editor
{
#pragma warning disable IDE0065 // Misplaced using directive
    using UnityEditor;
    using UnityEngine;
#pragma warning restore IDE0065 // Misplaced using directive

    [CustomEditor(typeof(GlobalPrefabAsset))]
    public class GlobalPrefabAssetEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var globalContext = (GlobalPrefabAsset)target;
            Object[] preloadedAssets = PlayerSettings.GetPreloadedAssets();
            bool isInPreloaded = System.Array.IndexOf(preloadedAssets, globalContext) >= 0;

            GUILayout.Space(10);

            if (globalContext._prefab == null)
                EditorGUILayout.HelpBox("No prefab assigned!", MessageType.Error);

            if (!isInPreloaded)
                EditorGUILayout.HelpBox("Not in Preloaded Assets - will not work at runtime!", MessageType.Warning);

            bool shouldBeInPreloaded = EditorGUILayout.Toggle("Add to Preloaded Assets", isInPreloaded);

            if (shouldBeInPreloaded != isInPreloaded)
            {
                var newList = new System.Collections.Generic.List<Object>(preloadedAssets);
                if (shouldBeInPreloaded)
                    newList.Add(globalContext);
                else
                    newList.Remove(globalContext);

                PlayerSettings.SetPreloadedAssets(newList.ToArray());
            }
        }
    }
}
#endif
