using Cysharp.Threading.Tasks;
using SnekTech.DataPersistence;
using SnekTech.SceneManagement;
using UnityEngine;
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

        [SerializeField]
        private Button historyButton;

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
            historyButton.onClick.AddListener(LoadHistorySceneAsync);
        }

        private void OnDisable()
        {
            newGameButton.onClick.RemoveAllListeners();
            continueButton.onClick.RemoveAllListeners();
            historyButton.onClick.RemoveAllListeners();
        }

        private void OnNewGameButtonClicked()
        {
            dataPersistenceManager.NewGame();
            LoadGameSceneAsync();
        }

        private void OnContinueButtonClicked()
        {
            LoadGameSceneAsync();
        }

        private void LoadGameSceneAsync()
        {
            dataPersistenceManager.LoadGame();
            mySceneManager.LoadSceneAsync(SceneIndex.Game).Forget();
        }

        private void LoadHistorySceneAsync()
        {
            mySceneManager.LoadSceneAsync(SceneIndex.History).Forget();
        }
    }
}
