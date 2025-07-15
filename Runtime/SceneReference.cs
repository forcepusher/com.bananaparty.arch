using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace BananaParty.Arch
{
    [Serializable]
    public class SceneReference
    {
#if UNITY_EDITOR
        [SerializeField]
        internal SceneAsset _sceneAsset;
#endif
        [SerializeField]
        internal string _scenePath;

        public string ScenePath => _scenePath;
        public string SceneName => System.IO.Path.GetFileNameWithoutExtension(ScenePath);
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(SceneReference))]
    public class SceneReferencePropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty sceneAssetProperty = property.FindPropertyRelative(nameof(SceneReference._sceneAsset));
            SerializedProperty scenePathProperty = property.FindPropertyRelative(nameof(SceneReference._scenePath));

            SceneAsset sceneAsset = sceneAssetProperty.objectReferenceValue as SceneAsset;
            if (sceneAsset != null)
            {
                string fullPath = AssetDatabase.GetAssetPath(sceneAsset);
                const string prefixToRemove = "Assets/";
                const string suffixToRemove = ".unity";
                scenePathProperty.stringValue = fullPath.Substring(prefixToRemove.Length, fullPath.Length - prefixToRemove.Length - suffixToRemove.Length);
            }

            EditorGUI.PropertyField(position, property, label, true);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }
    }
#endif
}
