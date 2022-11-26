using UnityEngine;
using UnityEngine.SceneManagement;

namespace SnekTech.Core
{
    [CreateAssetMenu(fileName = nameof(MySceneManager), menuName = nameof(MySceneManager))]
    public class MySceneManager : ScriptableObject
    {
        
        public void LoadMainMenu()
        {
            SceneManager.LoadSceneAsync(SceneType.MainMenu.ToString());
        }

        public void LoadGame()
        {
            SceneManager.LoadSceneAsync(SceneType.Game.ToString());
        }
    }
}