using SnekTech.GamePlay.InventorySystem;
using UnityEditor;

namespace SnekTech.Editor.DataPersistence
{
    [CustomEditor(typeof(ItemDataAssetRepo))]
    public class ItemDataAssetRepoEditor : AssetRepoEditor<ItemData>
    {
    }
}
