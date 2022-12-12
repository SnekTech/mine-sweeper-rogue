using System;
using SnekTech.InventorySystem;
using SnekTech.UI.Tooltip;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SnekTech.UI
{
    [RequireComponent(typeof(TooltipTrigger))]
    public class ItemSlot : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text stackSize;

        [SerializeField]
        private Image icon;

        private TooltipTrigger _tooltipTrigger;

        private void Awake()
        {
            _tooltipTrigger = GetComponent<TooltipTrigger>();
        }

        public void SetContent(InventoryItem item)
        {
            ItemData itemData = item.ItemData;
            
            stackSize.text = item.StackSize.ToString();
            icon.sprite = itemData.Icon;
            
            SetTooltipContent(itemData.Label, itemData.Description);
        }

        private void SetTooltipContent(string header, string body)
        {
            var tooltipContent = new TooltipContent(header, body);
            _tooltipTrigger.SetContent(tooltipContent);
        }
    }
}
