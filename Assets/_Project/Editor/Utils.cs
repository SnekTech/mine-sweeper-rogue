using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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
            // todo: change to dict
            public const string ScenesDir = "/_Project/Scenes";
            
            private const string ScriptableObjectDir = "/_Project/MyScriptableObjects";
            public const string GridDataDir = ScriptableObjectDir + "/Static";
            public const string ItemDataDir = ScriptableObjectDir + "/Inventory/Items";
            public const string CellEventDataDir = ScriptableObjectDir + "/CellEvents";
        }
    }
}
