using SnekTech.Player;
using UnityEngine;

namespace SnekTech.UI.Debug
{
    public class ControlPanel : MonoBehaviour
    {
        [SerializeField]
        private PlayerState playerState;

        [Header("Health")]
        [SerializeField]
        private int healthIncrement = 1;

        [SerializeField]
        private int armourIncrement = 1;

        [SerializeField]
        private int maxHealthIncrement = 1;

        [SerializeField]
        private int damageAmount = 1;

        public void OnAddHealthButtonClick()
        {
            playerState.AddHealth(healthIncrement);
        }

        public void OnAddArmourButtonClick()
        {
            playerState.AddArmour(armourIncrement);
        }

        public void OnTakeDamageButtonClick()
        {
            playerState.TakeDamage(damageAmount);
        }

        public void OnTakeDamageOnArmourButtonClick()
        {
            playerState.TakeDamageOnArmour(damageAmount);
        }

        public void OnTakeDamageOnHealthButtonClick()
        {
            playerState.TakeDamageOnHealth(damageAmount);
        }

        public void OnAdjustMaxHealthButtonClick()
        {
            playerState.AdjustMaxHealth(maxHealthIncrement);
        }
    }
}
