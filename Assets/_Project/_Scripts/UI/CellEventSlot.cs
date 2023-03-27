using SnekTech.Core.GameEvent;
using SnekTech.UI.Tooltip;
using UnityEngine;
using UnityEngine.UI;

namespace SnekTech.UI
{
    [RequireComponent(typeof(TooltipTrigger))]
    public class CellEventSlot : MonoBehaviour
    {
        [SerializeField]
        private Image icon;

        private TooltipTrigger _tooltipTrigger;

        private void Awake()
        {
            _tooltipTrigger = GetComponent<TooltipTrigger>();
        }

        public void SetContent(CellEventInstance cellEventInstance)
        {
            var eventData = cellEventInstance.CellEvent;
            icon.sprite = eventData.Icon;

            SetTooltipContent(cellEventInstance);
        }

        private void SetTooltipContent(CellEventInstance cellEventInstance)
        {
            var eventData = cellEventInstance.CellEvent;
            
            var header = eventData.Label;
            var body =
                $"{eventData.Description}\n" +
                $"Level #{cellEventInstance.LevelIndex}, Cell [{cellEventInstance.GridIndex.RowIndex}, {cellEventInstance.GridIndex.ColumnIndex}]";
            var tooltipContent = new TooltipContent(header, body);
            _tooltipTrigger.SetContent(tooltipContent);
        }
    }
}