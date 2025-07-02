using UnityEngine;

namespace BananaParty.Registry
{
    [CreateAssetMenu(fileName = "GlobalContextAsset")]
    public class GlobalContext : ScriptableObject
    {
        private static GlobalContext[] s_instances;

        [SerializeField]
        internal GameObject _prefab;

        [SerializeField]
        private RuntimeInitializeLoadType _instantiationTime = RuntimeInitializeLoadType.SubsystemRegistration;

        private static void InstantiatePrefabIfMatchLoadType(RuntimeInitializeLoadType loadType)
        {
            foreach (GlobalContext instance in s_instances)
            {
                if (instance._prefab == null)
                {
                    Debug.LogError($"{nameof(GlobalContext)} {instance.name} has no {nameof(_prefab)} assigned!", instance);
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
            s_instances = (GlobalContext[])Resources.FindObjectsOfTypeAll(typeof(GlobalContext));

            InstantiatePrefabIfMatchLoadType(RuntimeInitializeLoadType.SubsystemRegistration);
        }
    }
}

#if UNITY_EDITOR
namespace BananaParty.Registry.Editor
{
#pragma warning disable IDE0065 // Misplaced using directive
    using UnityEditor;
    using UnityEngine;
#pragma warning restore IDE0065 // Misplaced using directive

    [CustomEditor(typeof(GlobalContext))]
    public class GlobalContextEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var globalContext = (GlobalContext)target;
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
