using System;
using Cysharp.Threading.Tasks;
using SnekTech.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace SnekTech.UI
{
    public class GameOverMenu : MonoBehaviour
    {
        [Header("DI")]
        [SerializeField]
        private MySceneManager mySceneManager;
        
        [Header("Components")]
        [SerializeField]
        private Button backToMainButton;

        private void OnEnable()
        {
            backToMainButton.onClick.AddListener(OnBackToMainButtonClicked);
        }

        private void OnDisable()
        {
            backToMainButton.onClick.RemoveAllListeners();
        }

        private void OnBackToMainButtonClicked()
        {
            mySceneManager.LoadSceneAsync(SceneIndex.MainMenu).Forget();
        }
    }
}
