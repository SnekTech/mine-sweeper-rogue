using System;
using System.Collections.Generic;
using System.Linq;
using SnekTech.GamePlay.PlayerSystem;

namespace SnekTech.GamePlay.InventorySystem
{
    public class Inventory
    {
        public event Action<List<InventoryItem>> ItemsChanged;

        public Inventory(IPlayer player)
        {
            _player = player;
        }
        
        private IPlayer _player;
        private readonly SortedDictionary<ItemData, InventoryItem> _itemDict = new SortedDictionary<ItemData, InventoryItem>();

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
            
            _itemDict[itemData].OnAdd(_player);
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

            item.OnRemove(_player);
            ItemsChanged?.Invoke(Items);
        }
    }
}
