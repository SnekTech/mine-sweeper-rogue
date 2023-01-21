using System;
using System.Collections.Generic;
using SnekTech.C;
using SnekTech.GamePlay.InventorySystem.Components;
using SnekTech.UI;
using UnityEngine;

namespace SnekTech.GamePlay.InventorySystem
{
    [CreateAssetMenu(fileName = nameof(ItemData), menuName = MenuName.Items + "/" + nameof(ItemData))]
    public class ItemData : ScriptableObject, IComparable<ItemData>, IHoverableIconHolder
    {
        [SerializeField]
        private string label;
        [SerializeField]
        private string description;
        [SerializeField]
        private Sprite icon;

        [SerializeReference]
        private List<AffectPlayerComponent> affectPlayerComponents;


        public string Label => label;
        public string Description => description;
        public Sprite Icon => icon;
        public List<AffectPlayerComponent> AffectPlayerComponents => affectPlayerComponents;

        public int CompareTo(ItemData other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return string.Compare(label, other.label, StringComparison.Ordinal);
        }
    }
}
