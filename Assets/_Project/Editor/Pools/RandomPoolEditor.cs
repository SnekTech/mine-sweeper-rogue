using System.Linq;
using SnekTech.Roguelike;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace SnekTech.Editor.Pools
{
    public abstract class RandomPoolEditor<TPool, TAsset> : UnityEditor.Editor
        where TPool : RandomPool<TAsset> where TAsset : ScriptableObject
    {
        private TPool Pool => serializedObject.targetObject as TPool;
        private const string PopulateButtonText = "Populate Pool";
        private const string ClearButtonText = "Clear Pool";
        protected abstract string AssetDirPath { get; }

        public override VisualElement CreateInspectorGUI()
        {
            serializedObject.Update();
            
            var root = new VisualElement();
            InspectorElement.FillDefaultInspector(root, serializedObject, this);

            var populateButton = new Button(Populate)
            {
                text = PopulateButtonText,
            };
            root.Add(populateButton);

            var clearButton = new Button(Pool.Clear)
            {
                text = ClearButtonText,
            };
            root.Add(clearButton);

            serializedObject.ApplyModifiedProperties();

            return root;
        }

        private void Populate()
        {
            var assetPaths = FileUtils.GetFileAssetPaths(Application.dataPath + AssetDirPath);
            var assets = assetPaths.Select(AssetDatabase.LoadAssetAtPath<TAsset>)
                .Where(asset => asset != null)
                .ToList();
            
            Pool.Populate(assets);
            EditorUtility.SetDirty(Pool);
        }
    }
}