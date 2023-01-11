using SnekTech.Constants;
using SnekTech.Roguelike;
using UnityEngine;

namespace SnekTech.InventorySystem
{
    [CreateAssetMenu(fileName = nameof(ItemPool), menuName = MenuName.Inventory + MenuName.Slash + nameof(ItemPool))]
    public class ItemPool : RandomPool<ItemData>
    {
    }
}
