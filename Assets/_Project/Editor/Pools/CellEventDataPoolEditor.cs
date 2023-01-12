﻿using SnekTech.Core.GameEvent;
using UnityEditor;

namespace SnekTech.Editor.Pools
{
    [CustomEditor(typeof(CellEventPool))]
    public class CellEventDataPoolEditor : RandomPoolEditor<CellEventPool, CellEventData>
    {
        protected override string AssetDirPath => C.DirPath.AssetTypeToDir[typeof(CellEventData)];
    }
}