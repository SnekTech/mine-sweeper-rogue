using SnekTech.InventorySystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SnekTech.UI
{
    public class ItemSlot : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text label;

        [SerializeField]
        private TMP_Text stackSize;

        [SerializeField]
        private Image icon;
        
        public void SetContent(InventoryItem item)
        {
            label.text = item.ItemData.Label;
            stackSize.text = item.StackSize.ToString();
            icon.sprite = item.ItemData.Icon;
        }
    }
}
