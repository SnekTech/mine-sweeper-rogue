using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace SnekTech.InventorySystem
{
    [CreateAssetMenu(fileName = nameof(ItemPool), menuName = Utils.MyInventoryMenuName + "/" + nameof(ItemPool))]
    public class ItemPool : ScriptableObject
    {
        [SerializeField]
        private List<ItemData> items;

        private readonly Random _random = new Random();

        public ItemData GetRandom()
        {
            int index = _random.Next() % items.Count;
            return items[index];
        }
    }
}
