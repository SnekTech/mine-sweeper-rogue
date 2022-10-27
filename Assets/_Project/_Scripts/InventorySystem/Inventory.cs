using System;
using System.Collections.Generic;
using UnityEngine;

namespace SnekTech.InventorySystem
{
    [Serializable]
    public class Inventory
    {
        public event Action<List<InventoryItem>> ItemsUpdated;

        private Dictionary<ItemData, InventoryItem> _dictionary = new Dictionary<ItemData, InventoryItem>();

        [SerializeField]
        private List<InventoryItem> items = new List<InventoryItem>();

        public List<InventoryItem> Items => items;

        public void AddItem(ItemData itemData)
        {
            if (_dictionary.TryGetValue(itemData, out InventoryItem item))
            {
                item.AddStack();
            }
            else
            {
                var newItem = new InventoryItem(itemData);
                newItem.AddStack();
                _dictionary.Add(itemData, newItem);
                Items.Add(newItem);
            }
            ItemsUpdated?.Invoke(Items);
        }

        public void RemoveItem(ItemData itemData)
        {
            if (!_dictionary.ContainsKey(itemData))
            {
                throw new ArgumentException($"item: {itemData.label} does not exist in the player inventory");
            }
            
            _dictionary[itemData].RemoveStack();
            if (_dictionary[itemData].StackSize <= 0)
            {
                Items.Remove(_dictionary[itemData]);
                _dictionary.Remove(itemData);
            }
            ItemsUpdated?.Invoke(Items);
        }
    }
}
