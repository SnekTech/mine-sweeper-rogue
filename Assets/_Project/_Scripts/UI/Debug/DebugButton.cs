using SnekTech.UI.Tooltip;
using UnityEngine;
using UnityEngine.UI;

namespace SnekTech.UI.Debug
{
    [RequireComponent(typeof(Button), typeof(TooltipTrigger))]
    public class DebugButton : MonoBehaviour
    {
        private TooltipTrigger _tooltipTrigger;

        private void Awake()
        {
            _tooltipTrigger = GetComponent<TooltipTrigger>();
            _tooltipTrigger.SetContent(new TooltipContent(name));
        }
    }
}
