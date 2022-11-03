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

        private void Awake()
        {
            RefreshPanel(inventory.Items);
        }

        private void OnEnable()
        {
            inventory.ItemsUpdated += HandleInventoryItemsUpdated;
        }

        private void OnDisable()
        {
            inventory.ItemsUpdated -= HandleInventoryItemsUpdated;
        }

        private void DestroyAllChildren()
        {
            var children = new List<GameObject>();
            for (int i = 0; i < transform.childCount; i++)
            {
                children.Add(transform.GetChild(i).gameObject);
            }

            foreach (GameObject child in children)
            {
                DestroyImmediate(child);
            }
        }
        
        private void RefreshPanel(List<InventoryItem> items)
        {
            DestroyAllChildren();
            
            foreach (InventoryItem item in items)
            {
                ItemSlot itemSlot = Instantiate(itemSlotPrefab, transform);
                itemSlot.SetContent(item);
            }
        }

        private void HandleInventoryItemsUpdated(List<InventoryItem> items)
        {
            RefreshPanel(items);
        }
    }
}
