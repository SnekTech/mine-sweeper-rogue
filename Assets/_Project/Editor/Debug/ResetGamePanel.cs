﻿using System.Collections.Generic;
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
        private readonly Foldout _rootFoldout;

        public ResetGamePanel()
        {
            _rootFoldout = new Foldout
            {
                text = "Reset Game Buttons",
                viewDataKey = nameof(ResetGamePanel),
            };
            hierarchy.Add(_rootFoldout);

            _gridDataList = FindGridData();
            AddResetButtons();
        }

        private static List<GridData> FindGridData()
        {
            var gridDataPaths = FileUtils.GetFileAssetPaths(Application.dataPath + C.DirPath.GridDataDir);
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
                _rootFoldout.Add(button);
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
