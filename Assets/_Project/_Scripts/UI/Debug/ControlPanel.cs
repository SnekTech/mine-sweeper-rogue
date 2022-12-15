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
        private int damageAmount = 1;

        public void OnAddHealthButtonClick()
        {
            playerState.AddHealth(healthIncrement).Forget();
        }

        public void OnTakeDamageButtonClick()
        {
            playerState.TakeDamage(damageAmount).Forget();
        }
    }
}
