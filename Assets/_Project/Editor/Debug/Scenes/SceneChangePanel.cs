using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace SnekTech.Editor.Debug.Scenes
{
    public class SceneChangePanel : VisualElement
    {
        private const string SceneSearchPattern = "*.unity";

        private readonly List<string> sceneDirPaths = new List<string>
        {
            Application.dataPath + C.DirPath.ScenesDir,
        };
        
        private readonly Dictionary<string, string> _scenePathToName = new Dictionary<string, string>();

        public new class UxmlFactory : UxmlFactory<SceneChangePanel>
        {
        }

        public SceneChangePanel()
        {
            FindScenes();
            AddSceneButtons();
        }

        private void FindScenes()
        {
            var scenePaths = sceneDirPaths.Select(dirPath => FileUtils.GetFileAssetPaths(dirPath, SceneSearchPattern))
                .Where(filePaths => !filePaths.IsEmpty())
                .SelectMany(filePaths => filePaths);

            foreach (string scenePath in scenePaths)
            {
                _scenePathToName[scenePath] = FileUtils.GetFileName(scenePath);
            }
        }

        private void AddSceneButtons()
        {
            foreach ((string scenePath, string sceneName) in _scenePathToName)
            {
                var button = new Button(() => SceneUtils.OpenScene(scenePath))
                {
                    text = sceneName,
                };
                
                hierarchy.Add(button);
            }
        }
    }
}