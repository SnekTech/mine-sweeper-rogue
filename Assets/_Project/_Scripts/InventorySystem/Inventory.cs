using System;
using System.Collections.Generic;
using System.Linq;
using SnekTech.Constants;
using SnekTech.Player;
using UnityEngine;

namespace SnekTech.InventorySystem
{
    [CreateAssetMenu(fileName = "New " + nameof(Inventory), menuName = MenuName.Inventory + MenuName.Slash + nameof(Inventory))]
    public class Inventory : ScriptableObject
    {
        public event Action<List<InventoryItem>> ItemsUpdated;

        [SerializeField]
        private PlayerData playerData;

        [SerializeField]
        private List<InventoryItem> items = new List<InventoryItem>();

        public List<InventoryItem> Items => items;
        
        private readonly Dictionary<ItemData, InventoryItem> _dictionary = new Dictionary<ItemData, InventoryItem>();

        private void RemoveItemsWithZeroStackSize()
        {
            foreach (InventoryItem item in items.Where(item => item.StackSize <= 0))
            {
                items.Remove(item);
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
                    item.ItemData.OnAdd(playerData);
                }
            }
        }

        private void DeactivateItems()
        {
            foreach (InventoryItem item in Items)
            {
                for (int i = 0; i < item.StackSize; i++)
                {
                    item.ItemData.OnRemove(playerData);
                }
            }
        }
        
        private void OnEnable()
        {
            RefreshDictionary();
            ActivateItems();
        }

        private void OnDisable()
        {
            DeactivateItems();
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
            
            itemData.OnAdd(playerData);
            
            ItemsUpdated?.Invoke(Items);
        }

        public void RemoveItem(ItemData itemData)
        {
            if (!_dictionary.ContainsKey(itemData))
            {
                throw new ArgumentException($"item: {itemData.Label} does not exist in the player inventory");
            }
            
            _dictionary[itemData].RemoveStack();
            
            itemData.OnRemove(playerData);
            
            if (_dictionary[itemData].StackSize <= 0)
            {
                Items.Remove(_dictionary[itemData]);
                _dictionary.Remove(itemData);
            }
            ItemsUpdated?.Invoke(Items);
        }
    }
}
