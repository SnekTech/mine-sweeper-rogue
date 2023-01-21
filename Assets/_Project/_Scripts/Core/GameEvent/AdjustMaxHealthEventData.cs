using SnekTech.C;
using SnekTech.GamePlay;
using SnekTech.GamePlay.OneTimeEffect;
using SnekTech.GamePlay.PlayerSystem;
using UnityEngine;

namespace SnekTech.Core.GameEvent
{
    [CreateAssetMenu(
        menuName = MenuName.CellEvents + "/" + nameof(AdjustMaxHealthEventData),
        fileName = nameof(AdjustMaxHealthEventData))]
    public class AdjustMaxHealthEventData : CellEventData
    {
        [SerializeField]
        private int amount;

        private IOneTimeEffect _adjustMaxHealthEffect;

        protected override void OnTrigger(Player player)
        {
            _adjustMaxHealthEffect = new AdjustMaxHealthEffect(amount);
            _adjustMaxHealthEffect.Take(player);
        }
    }
}