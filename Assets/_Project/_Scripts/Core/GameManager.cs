using SnekTech.DataPersistence;
using SnekTech.SceneManagement;
using UnityEngine;

namespace SnekTech.Core
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private DataPersistenceManager dataPersistenceManager;

        [SerializeField]
        private MySceneManager mySceneManager;

        private async void Start()
        {
            mySceneManager.Init();
            await mySceneManager.LoadSceneAsync(SceneIndex.MainMenu);
        }
        
        private void OnApplicationQuit()
        {
            dataPersistenceManager.SaveGame();
        }
    }
}
