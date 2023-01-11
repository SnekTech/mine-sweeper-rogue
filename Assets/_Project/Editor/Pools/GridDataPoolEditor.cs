﻿using SnekTech.Grid;
using UnityEditor;

namespace SnekTech.Editor.Pools
{
    [CustomEditor(typeof(GridDataPool))]
    public class GridDataPoolEditor : RandomPoolEditor<GridDataPool, GridData>
    {
        protected override string AssetDirPath => C.DirPath.GridDataDir;
    }
}