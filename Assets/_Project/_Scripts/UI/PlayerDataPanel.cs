using System;
using SnekTech.Grid;
using SnekTech.Player;
using UnityEngine;

namespace SnekTech.UI
{
    public class PlayerDataPanel : MonoBehaviour
    {
        [SerializeField]
        private LabelController healthLabel;
        
        [SerializeField]
        private LabelController armourLabel;

        [SerializeField]
        private PlayerData playerData;

        private void OnEnable()
        {
            playerData.DataChanged += OnPlayerDataChanged;
        }

        private void OnDisable()
        {
            playerData.DataChanged -= OnPlayerDataChanged;
        }

        private void OnPlayerDataChanged()
        {
            healthLabel.SetText(playerData.HealthArmour.Health);
            armourLabel.SetText(playerData.HealthArmour.Armour);
        }
    }
}
