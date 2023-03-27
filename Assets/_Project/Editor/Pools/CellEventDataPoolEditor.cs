using SnekTech.GamePlay.CellEventSystem;
using UnityEditor;

namespace SnekTech.Editor.Pools
{
    [CustomEditor(typeof(CellEventPool))]
    public class CellEventDataPoolEditor : RandomPoolEditor<CellEvent>
    {
        protected override string AssetDirPath => C.DirPath.AssetTypeToDir[typeof(CellEvent)];
    }
}