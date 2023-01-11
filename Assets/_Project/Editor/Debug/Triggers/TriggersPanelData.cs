using System;
using System.Collections.Generic;
using SnekTech.Player;
using UnityEngine;

namespace SnekTech.Editor.Debug.Triggers
{
    [CreateAssetMenu(menuName = "Editor/" + nameof(TriggersPanelData))]
    public class TriggersPanelData : ScriptableObject
    {
        #region bindable fields

        [TriggerField]
        [HideInInspector]
        [SerializeField]
        private int damage;

        [TriggerField]
        [HideInInspector]
        [SerializeField]
        private int addHealth;

        [TriggerField]
        [HideInInspector]
        [SerializeField]
        private int addArmour;

        [TriggerField]
        [HideInInspector]
        [SerializeField]
        private int damageOnArmour;

        [TriggerField]
        [HideInInspector]
        [SerializeField]
        private int damageOnHealth;

        [TriggerField]
        [HideInInspector]
        [SerializeField]
        private int addMaxHealth;

        #endregion

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