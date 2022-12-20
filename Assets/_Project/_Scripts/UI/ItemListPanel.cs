using System;
using System.Collections.Generic;
using SnekTech.InventorySystem;
using UnityEngine;

namespace SnekTech.UI
{
    public class ItemListPanel : MonoBehaviour
    {
        [SerializeField]
        private Inventory inventory;

        [SerializeField]
        private ItemSlot itemSlotPrefab;

        [SerializeField]
        private RectTransform gridParentTransform;

        private void Awake()
        {
            RefreshPanel(inventory.Items);
        }

        private void OnEnable()
        {
            inventory.ItemsChanged += HandleInventoryItemsChanged;
        }

        private void OnDisable()
        {
            inventory.ItemsChanged -= HandleInventoryItemsChanged;
        }

        private void DestroyAllItemSlots()
        {
            gridParentTransform.DestroyAllChildren();
        }
        
        private void RefreshPanel(List<InventoryItem> items)
        {
            DestroyAllItemSlots();
            
            foreach (InventoryItem item in items)
            {
                ItemSlot itemSlot = Instantiate(itemSlotPrefab, gridParentTransform);
                itemSlot.SetContent(item);
            }
        }

        private void HandleInventoryItemsChanged(List<InventoryItem> items)
        {
            RefreshPanel(items);
        }
    }
}
