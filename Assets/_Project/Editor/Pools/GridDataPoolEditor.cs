﻿using SnekTech.GridSystem;
using UnityEditor;

namespace SnekTech.Editor.Pools
{
    [CustomEditor(typeof(GridDataPool))]
    public class GridDataPoolEditor : RandomPoolEditor<GridData>
    {
        protected override string AssetDirPath => C.DirPath.AssetTypeToDir[typeof(GridData)];
    }
}