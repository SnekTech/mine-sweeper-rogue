using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SnekTech.SceneManagement
{
    [CreateAssetMenu(menuName = C.MenuName.Managers + "/" + nameof(MySceneManager))]
    public class MySceneManager : ScriptableObject
    {
        public event Action GameSceneLoaded;
        
        private SceneIndex _currentScene = SceneIndex.Root;
        private GameObject _loadingScreen;

        public SceneIndex CurrentScene => _currentScene;

        public async UniTask LoadSceneAsync(SceneIndex sceneIndex)
        {
            ShowLoading();
            
            if (_currentScene != SceneIndex.Root)
            {
                await UnloadSceneAsync(_currentScene);
            }
            await SceneManager.LoadSceneAsync((int) sceneIndex, LoadSceneMode.Additive);

            if (sceneIndex == SceneIndex.Game)
            {
                // main game entry point
                GameSceneLoaded?.Invoke();
            }
            
            _currentScene = sceneIndex;
            HideLoading();
        }

        private async UniTask UnloadSceneAsync(SceneIndex sceneIndex)
        {
            await SceneManager.UnloadSceneAsync((int) sceneIndex);
        }

        public void Init(GameObject loadingScreen)
        {
            _currentScene = SceneIndex.Root;
            _loadingScreen = loadingScreen;
        }

        private void ShowLoading()
        {
            _loadingScreen.SetActive(true);
        }

        private void HideLoading()
        {
            _loadingScreen.SetActive(false);
        }
    }
}