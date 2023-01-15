using SnekTech.InventorySystem;
using UnityEditor;

namespace SnekTech.Editor.Pools
{
    [CustomEditor(typeof(ItemPool))]
    public class ItemDataPoolEditor : RandomPoolEditor<ItemData>
    {
        protected override string AssetDirPath => C.DirPath.AssetTypeToDir[typeof(ItemData)];
    }
}
