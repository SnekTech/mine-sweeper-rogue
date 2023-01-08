using SnekTech.Core;
using SnekTech.UI.Tooltip;
using TMPro;
using UnityEngine;

namespace SnekTech.UI.Status
{
    public class LevelInfo : MonoBehaviour
    {
        #region Components

        [SerializeField]
        private TMP_Text levelIndexLabel;

        [SerializeField]
        private TMP_Text gameModeText;

        private TooltipTrigger _gameModeTooltipTrigger;

        #endregion

        private void Awake()
        {
            _gameModeTooltipTrigger = gameModeText.GetComponent<TooltipTrigger>();
        }

        public void SetContent(Level level)
        {
            // +1 to make the level index human-readable
            levelIndexLabel.text = $"Level #{level.Index + 1}";
            
            gameModeText.text = $"Mode: {level.GameMode.Info.GameModeName}";
            _gameModeTooltipTrigger.SetContent(new TooltipContent(body: level.GameMode.Info.Description));
        }
    }
}