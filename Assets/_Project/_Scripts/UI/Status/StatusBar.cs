using SnekTech.Core;
using UnityEngine;

namespace SnekTech.UI.Status
{
    public class StatusBar : MonoBehaviour
    {
        [SerializeField]
        private Judge judge;

        #region Components

        private LevelInfo _levelInfo;

        #endregion

        #region Unity callbacks

        private void Awake()
        {
            _levelInfo = GetComponentInChildren<LevelInfo>();
        }

        private void OnEnable()
        {
            judge.OnLevelLoad += HandleLevelLoad;
        }

        private void OnDisable()
        {
            judge.OnLevelLoad -= HandleLevelLoad;
        }

        #endregion

        private void HandleLevelLoad()
        {
            _levelInfo.SetContent(judge.CurrentLevel);
        }
    }
}
