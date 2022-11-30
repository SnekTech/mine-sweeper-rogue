using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SnekTech.SceneManagement
{
    [CreateAssetMenu(menuName = nameof(MySceneManager))]
    public class MySceneManager : ScriptableObject
    {
        public async void HandleLoadGameRequest()
        {
            await LoadSceneAsync(SceneIndex.Game);
        }

        public async void HandleLoadMainMenuRequest()
        {
            await LoadSceneAsync(SceneIndex.MainMenu);
        }

        public static async UniTask LoadSceneAsync(SceneIndex sceneIndex)
        {
            await SceneManager.LoadSceneAsync((int) sceneIndex);
        }
    }
}