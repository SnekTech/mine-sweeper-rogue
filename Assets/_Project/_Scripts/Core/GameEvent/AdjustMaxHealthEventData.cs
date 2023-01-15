using SnekTech.C;
using SnekTech.Player;
using SnekTech.Player.OneTimeEffect;
using UnityEngine;

namespace SnekTech.Core.GameEvent
{
    [CreateAssetMenu(menuName =
        MenuName.GameEvents + MenuName.Slash + nameof(AdjustMaxHealthEventData))]
    public class AdjustMaxHealthEventData : CellEventData
    {
        [SerializeField]
        private int amount;

        private IOneTimeEffect _adjustMaxHealthEffect;

        protected override void OnTrigger(PlayerState playerState)
        {
            _adjustMaxHealthEffect = new AdjustMaxHealthEffect(amount);
            _adjustMaxHealthEffect.Take(playerState);
        }
    }
}