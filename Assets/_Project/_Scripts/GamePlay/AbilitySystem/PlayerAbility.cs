﻿using SnekTech.GamePlay.PlayerSystem;
using SnekTech.UI;
using UnityEngine;

namespace SnekTech.GamePlay.AbilitySystem
{
    public abstract class PlayerAbility : ScriptableObject, IPlayerAbility
    {
        [SerializeField]
        private Sprite icon;

        [SerializeField]
        private string label;

        [SerializeField]
        private string description;

        public Sprite Icon => icon;
        public string Label => label;
        public string Description => description;
        
        public abstract void Use(IPlayer player);
        
    }
}