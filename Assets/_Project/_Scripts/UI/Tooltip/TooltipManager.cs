using System;
using UnityEngine;

namespace SnekTech.UI.Tooltip
{
    [CreateAssetMenu(fileName = nameof(TooltipManager))]
    public class TooltipManager : ScriptableObject
    {
        private Tooltip _tooltip;

        public void Init(Tooltip tooltip)
        {
            _tooltip = tooltip;
        }

        public void Show(TooltipContent tooltipContent)
        {
            _tooltip.SetContent(tooltipContent);
            _tooltip.MoveToMouse();
            _tooltip.SetActive(true);
        }

        public void Hide()
        {
            _tooltip.SetActive(false);
        }
    }
}
