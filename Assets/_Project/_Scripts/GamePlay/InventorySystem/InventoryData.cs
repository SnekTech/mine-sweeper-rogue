using System.Collections.Generic;

namespace SnekTech.GamePlay.InventorySystem
{
    public class InventoryData
    {
        public SortedDictionary<ItemData, InventoryItem> itemDict = new SortedDictionary<ItemData, InventoryItem>();
    }
}
