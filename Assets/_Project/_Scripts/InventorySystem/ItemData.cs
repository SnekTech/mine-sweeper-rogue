﻿using UnityEngine;

namespace SnekTech.InventorySystem
{
    [CreateAssetMenu(fileName = nameof(ItemData), menuName = Utils.MyInventoryMenuName + "/" + nameof(ItemData))]
    public class ItemData : ScriptableObject
    {
        [SerializeField]
        private string label;
        [SerializeField]
        private string description;
        [SerializeField]
        private Sprite icon;

        public string Label => label;
        public string Description => description;
        public Sprite Icon => icon;
    }
}
