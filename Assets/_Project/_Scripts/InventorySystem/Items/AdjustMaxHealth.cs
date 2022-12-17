using UnityEngine;
using SnekTech.Constants;
using SnekTech.Player;
using SnekTech.Player.OneTimeEffect;

namespace SnekTech.InventorySystem.Items
{
    [CreateAssetMenu(menuName = MenuName.Items + MenuName.Slash + nameof(AdjustMaxHealth))]
    public class AdjustMaxHealth : ItemData
    {
        [SerializeField]
        private int amount = 1;

        private AdjustMaxHealthEffect _adjustMaxHealthEffect;

        public override void OnAdd(PlayerState playerState)
        {
            _adjustMaxHealthEffect = new AdjustMaxHealthEffect(amount);
            _adjustMaxHealthEffect.Take(playerState);
        }
    }
}
