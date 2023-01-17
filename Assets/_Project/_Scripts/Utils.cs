using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
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

        public static bool GetActiveSelf(this MonoBehaviour monoBehaviour) => monoBehaviour.gameObject.activeSelf;

        public static void SetActive(this MonoBehaviour monoBehaviour, bool isActive)
        {
            monoBehaviour.gameObject.SetActive(isActive);
        }

        public static bool IsEmpty(this ICollection collection) => collection.Count == 0;

        public static bool IsPending(this UniTask task) => task.Status == UniTaskStatus.Pending;
        public static bool IsPending<T>(this UniTask<T> task) => task.Status == UniTaskStatus.Pending;

        public static bool HasDuplicates<T>(this List<T> list) where T : IComparable<T>
        {
            var set = new HashSet<T>(list);
            return set.Count != list.Count;
        }
    }

    namespace C // Constants
    {
        public static class MenuName
        {
            private const string Root = nameof(SnekTech);

            public const string EditorScripting = Root + "/" + nameof(EditorScripting);
            public const string TriggersPanel = EditorScripting + "/" + nameof(TriggersPanel);

            public const string Managers = Root + "/" + nameof(Managers);
            public const string UIManagers = Managers + "/" + nameof(UIManagers);
            public const string EventManagers = Managers + "/" + nameof(EventManagers);
            
            public const string InventorySystem = Root + "/" + nameof(InventorySystem);
            public const string Items = InventorySystem + "/" + nameof(Items);

            public const string GameModeSystem = Root + "/" + nameof(GameModeSystem);
            
            public const string GameEventSystem = Root + "/" + nameof(GameEventSystem);
            public const string CellEvents = GameEventSystem + "/" + nameof(CellEvents);

            public const string GameHistory = Root + "/" + nameof(GameHistory);

            public const string Grid = Root + "/" + nameof(Grid);

            public const string Animation = Root + "/" + nameof(Animation);
            public const string SnekAnimator = Animation + "/" + nameof(SnekAnimator);
            public const string ClipDataHolder = Animation + "/" + nameof(ClipDataHolder);
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