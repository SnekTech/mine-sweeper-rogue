using System;
using System.Collections.Generic;
using SnekTech.Core.GameEvent;
using SnekTech.GamePlay.InventorySystem;
using SnekTech.Grid;

namespace SnekTech.Editor
{
    namespace C
    {
        // relative to "Assets" directory
        public static class DirPath
        {
            public const string ScenesDir = "/_Project/Scenes";
            
            private const string ScriptableObjectDir = "/_Project/MyScriptableObjects";
            private const string GridDataDir = ScriptableObjectDir + "/Static";
            private const string ItemDataDir = ScriptableObjectDir + "/Inventory/Items";
            private const string CellEventDataDir = ScriptableObjectDir + "/CellEvents";

            public static readonly Dictionary<Type, string> AssetTypeToDir = new Dictionary<Type, string>
            {
                {typeof(GridData), GridDataDir},
                {typeof(ItemData), ItemDataDir},
                {typeof(CellEventData), CellEventDataDir},
            };
        }
    }
}
