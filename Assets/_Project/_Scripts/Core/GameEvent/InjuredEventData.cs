using SnekTech.Player;
using SnekTech.Player.ClickEffect;
using UnityEngine;

namespace SnekTech.Core.GameEvent
{
    [CreateAssetMenu(menuName = Constants.MenuName.GameEvents + Constants.MenuName.Slash + nameof(InjuredEventData))]
    public class InjuredEventData : CellEventData
    {
        [SerializeField]
        private int repeatTimes;

        [SerializeField]
        private int singleDamage;
        
        private FiniteClickEffect _takeDamageFiniteTimesEffect;

        protected override void OnTrigger(PlayerState playerState)
        {
            _takeDamageFiniteTimesEffect = new FiniteClickEffect(repeatTimes, new TakeDamageClickEffect(singleDamage));
            _takeDamageFiniteTimesEffect.Completed += OnFiniteClickEffectCompleted;
            
            playerState.AddClickEffect(_takeDamageFiniteTimesEffect);
        }

        private void OnFiniteClickEffectCompleted()
        {
            _takeDamageFiniteTimesEffect.Completed -= OnFiniteClickEffectCompleted;
        }
    }
}
