using Cysharp.Threading.Tasks;
using SnekTech.SceneManagement;
using SnekTech.UI.Modal;
using SnekTech.UI.Tooltip;
using UnityEngine;

namespace SnekTech.Core
{
    public class GameManager : MonoBehaviour
    {
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

        [Header("Modal")]
        [SerializeField]
        private ModalManager modalManager;
        [SerializeField]
        private Modal modal;

        private void Awake()
        {
            InitGame().Forget();
        }

        private async UniTaskVoid InitGame()
        {
            mySceneManager.Init(loadingScreen);
            tooltipManager.Init(tooltip);
            modalManager.Init(modal);
            await mySceneManager.LoadSceneAsync(SceneIndex.MainMenu);
        }
    }
}