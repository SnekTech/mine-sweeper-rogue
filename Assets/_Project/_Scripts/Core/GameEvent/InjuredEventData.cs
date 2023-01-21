using SnekTech.C;
using SnekTech.GamePlay;
using SnekTech.GamePlay.ClickEffect;
using SnekTech.GamePlay.PlayerSystem;
using UnityEngine;

namespace SnekTech.Core.GameEvent
{
    [CreateAssetMenu(menuName = MenuName.CellEvents + "/" + nameof(InjuredEventData),
        fileName = nameof(InjuredEventData))]
    public class InjuredEventData : CellEventData
    {
        [SerializeField]
        private int repeatTimes;

        [SerializeField]
        private int singleDamage;

        protected override void OnTrigger(Player player)
        {
            // LacerationEffect lacerationEffect = player.LacerationEffect;
            // if (!lacerationEffect.IsActive)
            // {
            //     lacerationEffect.IconHolder = this;
            //     lacerationEffect.RepeatTime = repeatTimes;
            //     lacerationEffect.DamagePerClick = singleDamage;
            //     lacerationEffect.IsActive = true;
            // }
            // else
            // {
            //     lacerationEffect.RepeatTime += repeatTimes;
            // }
        }
    }
}