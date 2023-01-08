using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SnekTech
{
    public static class Extensions
    {
        public static void DestroyAllChildren(this Transform transform)
        {
            List<Transform> children = transform.Cast<Transform>().ToList();
            transform.DetachChildren();
            foreach (Transform child in children)
            {
                Object.Destroy(child.gameObject);
            }
        }

        public static bool WithinRange<T>(this T value, T min, T max) where T : IComparable<T>
        {
            if (min.CompareTo(max) > 0)
            {
                throw new ArgumentException($"min[{min}] > max[{max}]");
            }
            return value.CompareTo(min) >= 0 && value.CompareTo(max) <= 0;
        }

        public static void SetActive(this MonoBehaviour monoBehaviour, bool isActive)
        {
            monoBehaviour.gameObject.SetActive(isActive);
        }

        public static bool IsEmpty(this ICollection collection) => collection.Count == 0;
    }

    namespace Constants
    {
        public static class MenuName
        {
            // todo: refactor create menu names
            public const string Slash = "/";
            public const string EventManager = "MyEventManager";
            public const string Inventory = "MyInventory";
            public const string UI = "MyUI";
            public const string Items = "MyItems";
            public const string GameEvents = "MyGameEvents";
        }

        public static class GameConstants
        {
            public const int DamagePerBomb = 3;
            public const int SweepScopeMin = 1;
            public const int SweepScopeMax = 5;
            public const int InitialHealth = 10;
            public const int InitialArmour = 10;
            public const int InitialItemChoiceCount = 3;
        }
    }
}
