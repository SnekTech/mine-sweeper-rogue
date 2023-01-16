using SnekTech.C;
using SnekTech.Roguelike;
using UnityEngine;

namespace SnekTech.InventorySystem
{
    [CreateAssetMenu(fileName = nameof(ItemPool), menuName = MenuName.InventorySystem + "/" + nameof(ItemPool))]
    public class ItemPool : RandomPool<ItemData>
    {
    }
}
