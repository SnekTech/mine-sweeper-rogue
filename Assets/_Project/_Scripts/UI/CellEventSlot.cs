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

        public void SetContent(CellEvent cellEvent)
        {
            CellEventData eventData = cellEvent.CellEventData;
            icon.sprite = eventData.Icon;

            SetTooltipContent(cellEvent);
        }

        private void SetTooltipContent(CellEvent cellEvent)
        {
            CellEventData eventData = cellEvent.CellEventData;
            
            string header = eventData.Label;
            string body =
                $"{eventData.Description}\n" +
                $"Level #{cellEvent.LevelIndex}, Cell [{cellEvent.GridIndex.RowIndex}, {cellEvent.GridIndex.ColumnIndex}]";
            var tooltipContent = new TooltipContent(header, body);
            _tooltipTrigger.SetContent(tooltipContent);
        }
    }
}