using System.Linq;
using SnekTech.Grid;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace SnekTech.Editor.Pools
{
    [CustomEditor(typeof(GridDataPool))]
    public class GridDataPoolEditor : UnityEditor.Editor
    {
        private GridDataPool Pool => serializedObject.targetObject as GridDataPool;

        private const string GridDataDir = "/_Project/MyScriptableObjects/Static";
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
            var gridDataPaths = FileUtils.GetFilePaths(Application.dataPath + GridDataDir);
            var gridDataList = gridDataPaths.Select(AssetDatabase.LoadAssetAtPath<GridData>)
                .Where(gridData => gridData != null).ToList();

            Pool!.Populate(gridDataList);
        }
    }
}