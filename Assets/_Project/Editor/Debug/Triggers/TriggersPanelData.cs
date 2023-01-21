using System;
using System.Collections.Generic;
using SnekTech.C;
using SnekTech.GamePlay;
using SnekTech.GamePlay.PlayerSystem;
using UnityEngine;

namespace SnekTech.Editor.Debug.Triggers
{
    [CreateAssetMenu(menuName = MenuName.TriggersPanel + "/" + nameof(TriggersPanelData))]
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

        public void Init(Player player)
        {
            fieldActionDict = new Dictionary<string, Action<int>>
            {
                {nameof(damage), player.TakeDamage},
                {nameof(addHealth), player.AddHealth},
                {nameof(addArmour), player.AddArmour},
                {nameof(damageOnArmour), player.TakeDamageOnArmour},
                {nameof(damageOnHealth), player.TakeDamageOnHealth},
                {nameof(addMaxHealth), player.AddMaxHealth},
            };
        }
    }
}