using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SnekTech.SceneManagement
{
    [CreateAssetMenu(menuName = nameof(MySceneManager))]
    public class MySceneManager : ScriptableObject
    {
        private SceneIndex _currentScene = SceneIndex.Root;

        public async UniTask LoadSceneAsync(SceneIndex sceneIndex)
        {
            if (_currentScene != SceneIndex.Root)
            {
                await UnloadSceneAsync(_currentScene);
            }
            await SceneManager.LoadSceneAsync((int) sceneIndex, LoadSceneMode.Additive);
            _currentScene = sceneIndex;
        }

        private async UniTask UnloadSceneAsync(SceneIndex sceneIndex)
        {
            await SceneManager.UnloadSceneAsync((int) sceneIndex);
        }

        public void Init()
        {
            _currentScene = SceneIndex.Root;
        }
    }
}