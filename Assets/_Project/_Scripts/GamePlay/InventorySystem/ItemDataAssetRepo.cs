using SnekTech.DataPersistence;
using UnityEngine;

namespace SnekTech.GamePlay.InventorySystem
{
    [CreateAssetMenu(menuName = C.MenuName.DataPersistenceSystem + "/" + nameof(ItemDataAssetRepo))]
    public class ItemDataAssetRepo : AssetRepo<ItemData>
    {
    }
}
