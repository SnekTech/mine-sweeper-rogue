using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SnekTech.Editor
{
    public static class Utils
    {
        public static bool HasAttributeOfType(this FieldInfo field, Type attributeType) =>
            field.CustomAttributes.Any(attribute => attribute.AttributeType == attributeType);

        public static List<FieldInfo> GetInstanceFieldsWithAttributeOfType(this Type type, Type attributeType)
        {
            return type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                .Where(field => field.HasAttributeOfType(attributeType))
                .ToList();
        }
    }
}
