using System;
using Cysharp.Threading.Tasks;
using SnekTech.DataPersistence;
using SnekTech.SceneManagement;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace SnekTech.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField]
        private DataPersistenceManager dataPersistenceManager;

        [SerializeField]
        private MySceneManager mySceneManager;
        
        [SerializeField]
        private Button newGameButton;

        [SerializeField]
        private Button continueButton;

        private void Awake()
        {
            if (!dataPersistenceManager.HasSavedGameData)
            {
                continueButton.interactable = false;
            }
        }

        private void OnEnable()
        {
            newGameButton.onClick.AddListener(OnNewGameButtonClicked);
            continueButton.onClick.AddListener(OnContinueButtonClicked);
        }

        private void OnDisable()
        {
            newGameButton.onClick.RemoveAllListeners();
            newGameButton.onClick.RemoveAllListeners();
        }

        private async void OnNewGameButtonClicked()
        {
            dataPersistenceManager.NewGame();
            await LoadGameSceneAsync();
        }

        private async void OnContinueButtonClicked()
        {
            await LoadGameSceneAsync();
        }

        private async UniTask LoadGameSceneAsync()
        {
            dataPersistenceManager.LoadGame();
            await mySceneManager.LoadSceneAsync(SceneIndex.Game);
        }
    }
}
