using System;
using System.Collections.Generic;
using System.Linq;
using SnekTech.GamePlay.PlayerSystem;
using UnityEngine;

namespace SnekTech.GamePlay.InventorySystem
{
    [CreateAssetMenu(menuName = C.MenuName.InventorySystem + "/" + nameof(Inventory))]
    public class Inventory : ScriptableObject, IPlayerDataHolder
    {
        public event Action<List<InventoryItem>> ItemsChanged;
        
        [SerializeField]
        private Player player;
        
        private SortedDictionary<ItemData, InventoryItem> _itemDict = new SortedDictionary<ItemData, InventoryItem>();

        public List<InventoryItem> Items => _itemDict.Values.ToList();
        
        public void AddItem(ItemData itemData)
        {
            if (_itemDict.TryGetValue(itemData, out var item))
            {
                item.StackSize++;
            }
            else
            {
                var newItem = new InventoryItem(itemData);
                newItem.StackSize++;
                _itemDict.Add(itemData, newItem);
            }
            
            _itemDict[itemData].OnAdd(player);
            ItemsChanged?.Invoke(Items);
        }

        public void RemoveItem(ItemData itemData)
        {
            if (!_itemDict.ContainsKey(itemData))
            {
                throw new ArgumentException($"item: {itemData.Label} does not exist in the player inventory");
            }

            var item = _itemDict[itemData];
            item.StackSize--;
            if (item.StackSize == 0)
            {
                _itemDict.Remove(itemData);
            }

            item.OnRemove(player);
            ItemsChanged?.Invoke(Items);
        }

        public void LoadData(PlayerData playerData)
        {
            var savedItems = playerData.inventoryData.Items;
            _itemDict.Clear();
            foreach (var inventoryItem in savedItems)
            {
                _itemDict[inventoryItem.ItemData] = inventoryItem;
            }
        }

        public void SaveData(PlayerData playerData)
        {
            playerData.inventoryData.Items = Items;
        }
    }
}
