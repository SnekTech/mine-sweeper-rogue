using System.Linq;
using SnekTech.Core.CustomAttributes;
using SnekTech.Roguelike;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace SnekTech.Editor.Pools
{
    public abstract class RandomPoolEditor<TAsset> : UnityEditor.Editor where TAsset : ScriptableObject
    {
        private const string PopulateButtonText = "Populate Pool";
        private const string ClearButtonText = "Clear Pool";
        
        protected abstract string AssetDirPath { get; }

        public override VisualElement CreateInspectorGUI()
        {
            // see: https://docs.unity3d.com/ScriptReference/SerializedObject.html
            serializedObject.Update(); // essential for correct save in editor
            
            var root = new VisualElement();
            InspectorElement.FillDefaultInspector(root, serializedObject, this);

            var populateButton = new Button(Populate)
            {
                text = PopulateButtonText,
            };
            root.Add(populateButton);

            var clearButton = new Button(Clear)
            {
                text = ClearButtonText,
            };
            root.Add(clearButton);

            return root;
        }

        private SerializedProperty GetElementsProperty()
        {
            var elementFields = typeof(RandomPool<TAsset>).GetInstanceFieldsWithAttributeOfType<PoolElementsFieldAttribute>();
            if (elementFields.Count != 1)
            {
                UnityEngine.Debug.LogWarning($"Invalid elements field count : {elementFields.Count}, should be only 1");
            }

            string fieldName = elementFields[0].Name;

            return serializedObject.FindProperty(fieldName);
        }
        
        private void Populate()
        {
            Clear();
            
            var assetPaths = FileUtils.GetFileAssetPaths(Application.dataPath + AssetDirPath);
            var assets = assetPaths.Select(AssetDatabase.LoadAssetAtPath<TAsset>)
                .Where(asset => asset != null)
                .ToList();

            var elementsProperty = GetElementsProperty();

            for (int i = 0; i < assets.Count; i++)
            {
                elementsProperty.InsertArrayElementAtIndex(i);
                elementsProperty.GetArrayElementAtIndex(i).objectReferenceValue = assets[i];
            }

            serializedObject.ApplyModifiedProperties();
        }

        private void Clear()
        {
            var property = GetElementsProperty();
            property.ClearArray();

            serializedObject.ApplyModifiedProperties();
        }
    }
}