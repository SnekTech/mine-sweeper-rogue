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

        private string HealthStr => playerData.HealthArmour.Health.ToString();
        private string ArmourStr => playerData.HealthArmour.Armour.ToString();

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
            healthLabel.LabelText = HealthStr;
            armourLabel.LabelText = ArmourStr;
        }
    }
}
