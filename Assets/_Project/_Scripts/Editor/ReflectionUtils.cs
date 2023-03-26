using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SnekTech.Editor
{
    public static class ReflectionUtils
    {
        public static List<Type> GetAllSubClassesOf<T>()
        {
            var targetType = typeof(T);
            return targetType.Assembly.GetTypes().Where(type => type.IsSubclassOf(targetType) && !type.IsGenericType)
                .ToList();
        }

        public static IEnumerable<Type> GetImplementorsOfInterface<T>() where T : class
        {
            var interfaceType = typeof(T);
            return interfaceType.Assembly.GetTypes().Where(type => type != interfaceType && interfaceType.IsAssignableFrom(type));
        }

        public static List<FieldInfo> GetInstanceFieldsWithAttributeOfType<T>(this Type type)
        {
            return type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                .Where(field => field.IsDefined(typeof(T)))
                .ToList();
        }
    }
}
