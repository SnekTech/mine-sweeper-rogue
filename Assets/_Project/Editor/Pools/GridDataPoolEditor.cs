using SnekTech.Core.GameEvent;
using SnekTech.Grid;
using UnityEditor;

namespace SnekTech.Editor.Pools
{
    [CustomEditor(typeof(GridDataPool))]
    public class GridDataPoolEditor : RandomPoolEditor<GridData>
    {
        protected override string AssetDirPath => C.DirPath.AssetTypeToDir[typeof(GridData)];
    }
}