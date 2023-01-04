using SnekTech.UI.Tooltip;
using UnityEngine;
using UnityEngine.UI;

namespace SnekTech.UI
{
    [RequireComponent(typeof(TooltipTrigger))]
    public class HoverIconSlot : MonoBehaviour
    {
        [SerializeField]
        private Image iconImage;

        private TooltipTrigger _tooltipTrigger;

        private void Awake()
        {
            _tooltipTrigger = GetComponent<TooltipTrigger>();
        }

        public void SetContent(IHoverableIconHolder iconHolder)
        {
            iconImage.sprite = iconHolder.Icon;

            var tooltipContent = new TooltipContent(iconHolder.Label, iconHolder.Description);
            _tooltipTrigger.SetContent(tooltipContent);
        }
    }
}
