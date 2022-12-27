using System.Collections.Generic;
using SnekTech.InventorySystem;
using UnityEngine;

namespace SnekTech.UI.History
{
    public class ItemsPanel : MonoBehaviour
    {
        [SerializeField]
        private ItemSlot itemSlotPrefab;
        
        public void SetContent(List<InventoryItem> items)
        {
            transform.DestroyAllChildren();
            foreach (InventoryItem item in items)
            {
                ItemSlot slot = Instantiate(itemSlotPrefab, transform);
                slot.SetContent(item);
            }
        }
    }
}
