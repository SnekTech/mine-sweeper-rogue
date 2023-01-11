using System.Linq;
using SnekTech.Roguelike;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace SnekTech.Editor.Pools
{
    public abstract class RandomPoolEditor<TPool, TElement> : UnityEditor.Editor
        where TPool : RandomPool<TElement> where TElement : ScriptableObject
    {
        private TPool Pool => serializedObject.targetObject as TPool;
        private const string PopulateButtonText = "Populate Pool";
        private const string ClearButtonText = "Clear Pool";

        public override VisualElement CreateInspectorGUI()
        {
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

            return root;
        }

        private void Populate()
        {
            var elementPaths = FileUtils.GetFilePaths(Pool.AssetDirPath);
            var elements = elementPaths.Select(AssetDatabase.LoadAssetAtPath<TElement>)
                .Where(element => element != null)
                .ToList();
            
            Pool.Populate(elements);
        }
    }
}