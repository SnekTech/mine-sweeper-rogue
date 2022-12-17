using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SnekTech.UI.Tooltip
{
    public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private TooltipManager tooltipManager;

        private TooltipContent _tooltipContent;

        public void SetContent(TooltipContent tooltipContent)
        {
            _tooltipContent = tooltipContent;
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            tooltipManager.ShowAsync(_tooltipContent).Forget();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            tooltipManager.Hide();
        }
    }
}
