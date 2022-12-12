﻿using SnekTech.DataPersistence;
using SnekTech.SceneManagement;
using SnekTech.UI.Tooltip;
using UnityEngine;

namespace SnekTech.Core
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private DataPersistenceManager dataPersistenceManager;

        [Header("Scene")]
        [SerializeField]
        private MySceneManager mySceneManager;

        [SerializeField]
        private GameObject loadingScreen;

        [Header("Tooltip")]
        [SerializeField]
        private TooltipManager tooltipManager;
        [SerializeField]
        private Tooltip tooltip;

        private async void Awake()
        {
            mySceneManager.Init(loadingScreen);
            tooltipManager.Init(tooltip);
            await mySceneManager.LoadSceneAsync(SceneIndex.MainMenu);
        }
        
        private void OnApplicationQuit()
        {
            dataPersistenceManager.SaveGame();
        }
    }
}
