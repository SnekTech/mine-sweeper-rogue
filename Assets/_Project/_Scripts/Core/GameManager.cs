using System;
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

        [SerializeField]
        private GameObject loadingScreen;

        private async void Awake()
        {
            mySceneManager.Init(loadingScreen);
            await mySceneManager.LoadSceneAsync(SceneIndex.MainMenu);
        }
        
        private void OnApplicationQuit()
        {
            dataPersistenceManager.SaveGame();
        }
    }
}
