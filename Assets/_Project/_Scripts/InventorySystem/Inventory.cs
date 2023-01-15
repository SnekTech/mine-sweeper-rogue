using System;
using System.Collections.Generic;
using System.Linq;
using SnekTech.C;
using SnekTech.Player;
using UnityEngine;

namespace SnekTech.InventorySystem
{
    [CreateAssetMenu(fileName = "New " + nameof(Inventory), menuName = MenuName.Inventory + MenuName.Slash + nameof(Inventory))]
    public class Inventory : ScriptableObject
    {
        public event Action<List<InventoryItem>> ItemsChanged;

        [SerializeField]
        private PlayerState playerState;

        private List<InventoryItem> _items = new List<InventoryItem>();

        public List<InventoryItem> Items => _items;
        
        private readonly Dictionary<ItemData, InventoryItem> _dictionary = new Dictionary<ItemData, InventoryItem>();

        private void RemoveItemsWithZeroStackSize()
        {
            foreach (InventoryItem item in _items.Where(item => item.StackSize <= 0))
            {
                _items.Remove(item);
            }
        }
        
        private void RefreshDictionary()
        {
            RemoveItemsWithZeroStackSize();
            
            _dictionary.Clear();
            foreach (InventoryItem item in Items)
            {
                _dictionary.Add(item.ItemData, item);
            }
        }

        private void ActivateItems()
        {
            foreach (InventoryItem item in Items)
            {
                for (int i = 0; i < item.StackSize; i++)
                {
                    item.ItemData.OnAdd(playerState);
                }
            }
        }

        public void Load(List<InventoryItem> savedItems)
        {
            _items = savedItems;
            RefreshDictionary();
            ActivateItems();
        }

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
            
            itemData.OnAdd(playerState);
            
            ItemsChanged?.Invoke(Items);
        }

        public void RemoveItem(ItemData itemData)
        {
            if (!_dictionary.ContainsKey(itemData))
            {
                throw new ArgumentException($"item: {itemData.Label} does not exist in the player inventory");
            }
            
            _dictionary[itemData].RemoveStack();
            
            itemData.OnRemove(playerState);
            
            if (_dictionary[itemData].StackSize <= 0)
            {
                Items.Remove(_dictionary[itemData]);
                _dictionary.Remove(itemData);
            }
            ItemsChanged?.Invoke(Items);
        }
    }
}
