using Newtonsoft.Json;
using SnekTech.GamePlay.PlayerSystem;
using UnityEngine;

namespace SnekTech.GamePlay.InventorySystem
{
    public class InventoryItem
    {
        private int _stackSize;
        private ItemData _itemData;

        public int StackSize
        {
            get => _stackSize;
            set => _stackSize = Mathf.Max(0, value);
        }
        
        [JsonIgnore]
        public ItemData ItemData => _itemData;

        public string ItemName
        {
            get => _itemData.name;
            set => _itemData = ItemDataAssetRepo.Instance.Get(value);
        }


        public InventoryItem(ItemData itemData)
        {
            _itemData = itemData;
        }

        public void OnAdd(IPlayer player)
        {
            foreach (var component in _itemData.Components)
            {
                component.OnAdd(player);
            }
        }

        public void OnRemove(IPlayer player)
        {
            foreach (var component in _itemData.Components)
            {
                component.OnRemove(player);
            }
        }
    }
}
