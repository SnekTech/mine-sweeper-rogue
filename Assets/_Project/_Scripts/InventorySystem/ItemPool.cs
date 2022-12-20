using System.Collections.Generic;
using SnekTech.Constants;
using SnekTech.Roguelike;
using UnityEngine;
using Random = System.Random;

namespace SnekTech.InventorySystem
{
    [CreateAssetMenu(fileName = nameof(ItemPool), menuName = MenuName.Inventory + MenuName.Slash + nameof(ItemPool))]
    public class ItemPool : ScriptableObject
    {
        [SerializeField]
        private List<ItemData> items;

        public ItemData GetRandom()
        {
            return items.GetRandom();
        }
    }
}
