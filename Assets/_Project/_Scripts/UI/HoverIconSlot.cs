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

        /// <summary>
        /// if <paramref name="tooltipContent"/> is null, <paramref name="iconHolder"/>'s label & description
        /// will be used as tooltip content
        /// </summary>
        /// <param name="iconHolder"></param>
        /// <param name="tooltipContent"></param>
        public void SetContent(IHoverableIconHolder iconHolder, TooltipContent tooltipContent = null)
        {
            iconImage.sprite = iconHolder.Icon;

            tooltipContent ??= new TooltipContent(iconHolder.Label, iconHolder.Description);
            _tooltipTrigger.SetContent(tooltipContent);
        }
    }
}
