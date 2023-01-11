using System;
using System.Collections.Generic;
using SnekTech.Player;
using UnityEngine;

namespace SnekTech.Editor.Debug
{
    [CreateAssetMenu(menuName = "Editor/" + nameof(TriggersPanelData))]
    public class TriggersPanelData : ScriptableObject
    {
        [TriggerField]
        [SerializeField]
        private int damage;

        [TriggerField]
        [SerializeField]
        private int addHealth;

        [TriggerField]
        [SerializeField]
        private int addArmour;

        [TriggerField]
        [SerializeField]
        private int damageOnArmour;

        [TriggerField]
        [SerializeField]
        private int damageOnHealth;

        [TriggerField]
        [SerializeField]
        private int addMaxHealth;

        public Dictionary<string, Action<int>> fieldActionDict;

        public void Init(PlayerState playerState)
        {
            fieldActionDict = new Dictionary<string, Action<int>>
            {
                {nameof(damage), playerState.TakeDamage},
                {nameof(addHealth), playerState.AddHealth},
                {nameof(addArmour), playerState.AddArmour},
                {nameof(damageOnArmour), playerState.TakeDamageOnArmour},
                {nameof(damageOnHealth), playerState.TakeDamageOnHealth},
                {nameof(addMaxHealth), playerState.AdjustMaxHealth},
            };
        }
    }
}