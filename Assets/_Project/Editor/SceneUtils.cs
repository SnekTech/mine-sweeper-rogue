using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace SnekTech.Editor
{
    public static class SceneUtils
    {
        public static bool IsSceneOpened(string scenePath)
        {
            var activeScene = SceneManager.GetActiveScene();
            string sceneName = Path.GetFileNameWithoutExtension(scenePath);
            return activeScene.name.Equals(sceneName);
        }

        public static void OpenScene(string scenePath)
        {
            if (IsSceneOpened(scenePath) || EditorApplication.isPlaying)
            {
                return;
            }

            EditorSceneManager.OpenScene(scenePath);
        }
    }
}
