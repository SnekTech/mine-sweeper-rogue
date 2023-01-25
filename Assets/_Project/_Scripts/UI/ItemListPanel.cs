using System.Collections.Generic;
using SnekTech.GamePlay.InventorySystem;
using SnekTech.GamePlay.PlayerSystem;
using UnityEngine;

namespace SnekTech.UI
{
    public class ItemListPanel : MonoBehaviour
    {
        [SerializeField]
        private PlayerEventChannel playerEventChannel;

        [SerializeField]
        private Inventory playerInventory;
        
        [SerializeField]
        private ItemSlot itemSlotPrefab;

        [SerializeField]
        private RectTransform gridParentTransform;

        private void OnEnable()
        {
            RefreshPanel(playerInventory.Items);
            playerEventChannel.InventoryItemChanged += HandleInventoryItemsChanged;
        }

        private void OnDisable()
        {
            playerEventChannel.InventoryItemChanged -= HandleInventoryItemsChanged;
        }

        private void DestroyAllItemSlots()
        {
            gridParentTransform.DestroyAllChildren();
        }
        
        private void RefreshPanel(List<InventoryItem> items)
        {
            DestroyAllItemSlots();
            
            foreach (var item in items)
            {
                var itemSlot = Instantiate(itemSlotPrefab, gridParentTransform);
                itemSlot.SetContent(item);
            }
        }

        private void HandleInventoryItemsChanged(List<InventoryItem> items)
        {
            RefreshPanel(items);
        }
    }
}
