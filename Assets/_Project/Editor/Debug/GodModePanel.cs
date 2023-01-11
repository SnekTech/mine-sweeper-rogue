using System.Collections.Generic;
using System.Linq;
using SnekTech.Player;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace SnekTech.Editor.Debug
{
    public class GodModePanel : EditorWindow
    {
        [SerializeField]
        private VisualTreeAsset panelUxml;

        [SerializeField]
        private PlayerState playerState;

        [SerializeField]
        private TriggersPanelData triggersPanelData;

        [MenuItem("Tools/" + nameof(SnekTech) + "/GodModePanel")]
        private static void ShowWindow()
        {
            var window = GetWindow<GodModePanel>();
            window.titleContent = new GUIContent("God Mode");
            window.ShowUtility();
        }

        public void CreateGUI()
        {
            panelUxml.CloneTree(rootVisualElement);

            GenerateSceneButtons();

            triggersPanelData.Init(playerState);
            var triggersPanel = rootVisualElement.Q<TriggersPanel>();
            triggersPanel.Init(triggersPanelData);
        }

        #region switch scenes

        private void GenerateSceneButtons()
        {
            var sceneSwitchRootFoldout = rootVisualElement.Q<Foldout>("sceneSwitchFoldout");

            var sceneNames = new List<string>();
            var scenePaths = new List<string>();

            var sceneDirPaths = new List<string>
            {
                Application.dataPath + "/_Project/Scenes",
            };

            var sceneFiles = sceneDirPaths.Select(dirPath => FileUtils.GetFilePaths(dirPath, "*Unity"))
                .Where(filePaths => !filePaths.IsEmpty()).SelectMany(filePaths => filePaths);
            foreach (string filePath in sceneFiles)
            {
                scenePaths.Add(filePath);
                sceneNames.Add(FileUtils.GetFileName(filePath));
            }
            
            for (int i = 0; i < sceneNames.Count; i++)
            {
                string scenePath = scenePaths[i];
                var button = new Button(() => SceneUtils.OpenScene(scenePath))
                {
                    text = sceneNames[i],
                };
                sceneSwitchRootFoldout.Add(button);
            }
        }

        #endregion
    }
}