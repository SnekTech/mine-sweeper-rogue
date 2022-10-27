using System;
using UnityEngine;

namespace SnekTech.InventorySystem
{
    [Serializable]
    public class InventoryItem
    {
        [SerializeField]
        private int stackSize;

        [SerializeField]
        private ItemData itemData;
        public int StackSize => stackSize;
        public ItemData ItemData => itemData;
        
        public InventoryItem(ItemData itemDataIn)
        {
            itemData = itemDataIn;
            stackSize = 0;
        }

        public void AddStack(int size = 1)
        {
            stackSize += size;
        }

        public void RemoveStack(int size = 1)
        {
            if (StackSize < size)
            {
                throw new ArgumentException($"cannot remove {size} items from {StackSize} items");
            }

            stackSize -= size;
        }
    }
}
