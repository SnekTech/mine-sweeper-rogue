using SnekTech.GamePlay.PlayerSystem;
using UnityEngine;

namespace SnekTech.GamePlay.InventorySystem
{
    public class InventoryItem
    {
        private int _stackSize;
        private readonly ItemData _itemData;

        public int StackSize
        {
            get => _stackSize;
            set => _stackSize = Mathf.Max(0, value);
        }
        public ItemData ItemData => _itemData;


        public InventoryItem(ItemData itemData)
        {
            _itemData = itemData;
        }

        public void OnAdd(IPlayer player)
        {
            foreach (var component in _itemData.AffectPlayerComponents)
            {
                component.OnAdd(player);
            }
        }

        public void OnRemove(IPlayer player)
        {
            foreach (var component in _itemData.AffectPlayerComponents)
            {
                component.OnRemove(player);
            }
        }
    }
}
