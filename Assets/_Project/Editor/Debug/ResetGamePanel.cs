using System.Collections.Generic;
using System.Linq;
using SnekTech.Grid;
using SnekTech.UI;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace SnekTech.Editor.Debug
{
    public class ResetGamePanel : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<ResetGamePanel>{}

        private readonly List<GridData> _gridDataList;
        private readonly Dictionary<GridData, Button> _gridDataToButton = new Dictionary<GridData, Button>();

        public ResetGamePanel()
        {
            _gridDataList = FindGridData();
            AddResetButtons();
        }

        private static List<GridData> FindGridData()
        {
            string dirPath = Application.dataPath + C.DirPath.AssetTypeToDir[typeof(GridData)];
            var gridDataPaths = FileUtils.GetFileAssetPaths(dirPath);
            var gridDataList = gridDataPaths.Select(AssetDatabase.LoadAssetAtPath<GridData>)
                .Where(gridData => gridData != null)
                .ToList();
            return gridDataList;
        }

        private void AddResetButtons()
        {
            foreach (var gridData in _gridDataList)
            {
                var button = new Button
                {
                    text = $"Reset with {gridData.name}",
                };
                _gridDataToButton[gridData] = button;
                hierarchy.Add(button);
            }
        }

        public void Init(UIEventManager uiEventManager)
        {
            if (!EditorApplication.isPlaying)
            {
                return;
            }
            
            foreach (var (gridData, button) in _gridDataToButton)
            {
                button.clickable = new Clickable(() => uiEventManager.InvokeResetButtonClicked(gridData));
            }
        }
    }
}
