using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SnekTech.Core.GameEvent;
using SnekTech.Grid;
using SnekTech.InventorySystem;

namespace SnekTech.Editor
{
    public static class Utils
    {
        public static List<FieldInfo> GetInstanceFieldsWithAttributeOfType(this Type type, Type attributeType)
        {
            return type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                .Where(field => field.IsDefined(attributeType))
                .ToList();
        }
    }

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
