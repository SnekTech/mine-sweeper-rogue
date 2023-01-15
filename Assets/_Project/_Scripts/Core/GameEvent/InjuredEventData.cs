using SnekTech.C;
using SnekTech.Player;
using SnekTech.Player.ClickEffect;
using UnityEngine;

namespace SnekTech.Core.GameEvent
{
    [CreateAssetMenu(menuName = MenuName.GameEvents + MenuName.Slash + nameof(InjuredEventData))]
    public class InjuredEventData : CellEventData
    {
        [SerializeField]
        private int repeatTimes;

        [SerializeField]
        private int singleDamage;

        protected override void OnTrigger(PlayerState playerState)
        {
            LacerationEffect lacerationEffect = playerState.LacerationEffect;
            if (!lacerationEffect.IsActive)
            {
                lacerationEffect.IconHolder = this;
                lacerationEffect.RepeatTime = repeatTimes;
                lacerationEffect.DamagePerClick = singleDamage;
                lacerationEffect.IsActive = true;
            }
            else
            {
                lacerationEffect.RepeatTime += repeatTimes;
            }
        }
    }
}