﻿using UnityEngine;

namespace SnekTech
{
    public static class Extensions
    {
        public static void DestroyAllChildren(this Transform transform)
        {
            foreach (Transform child in transform)
            {
                Object.Destroy(child);
            }
        }

        public static void DetachFromParent(this GameObject gameObject)
        {
            gameObject.transform.SetParent(null, true);
        }
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
            public const float DefaultCountDownDuration = 3;// todo: randomize this across different levels
            public const int InitialHealth = 10;
            public const int InitialArmour = 10;
            public const int InitialItemChoiceCount = 3;
        }
    }
}
