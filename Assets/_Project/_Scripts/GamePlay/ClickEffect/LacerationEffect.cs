using System;
using SnekTech.GamePlay.PlayerSystem;
using SnekTech.UI;

namespace SnekTech.GamePlay.ClickEffect
{
    public class LacerationEffect : IClickEffect
    {
        public event Action Changed;

        private const int DefaultDamagePerClick = 1;
        private const int DefaultRepeatTime = 3;
        
        private readonly TakeDamageClickEffect _takeDamageClickEffect;
        private readonly FiniteClickEffect _finiteClickEffect;
        private bool _isActive;

        public int DamagePerClick
        {
            get => _takeDamageClickEffect.DamagePerClick;
            set
            {
                _takeDamageClickEffect.DamagePerClick = value;
                Changed?.Invoke();
            }
        }

        public int RepeatTime
        {
            get => _finiteClickEffect.RepeatTime;
            set
            {
                _finiteClickEffect.RepeatTime = value;
                Changed?.Invoke();
            }
        }

        public IHoverableIconHolder IconHolder { get; set; }

        public bool IsActive
        {
            get => _isActive && _finiteClickEffect.IsActive;
            set
            {
                _isActive = value;
                Changed?.Invoke();
            }
        }

        public LacerationEffect()
        {
            _takeDamageClickEffect = new TakeDamageClickEffect(DefaultDamagePerClick);
            _finiteClickEffect = new FiniteClickEffect(DefaultRepeatTime, _takeDamageClickEffect);
        }

        public void Take(Player player)
        {
            _finiteClickEffect.Take(player);
            Changed?.Invoke();
        }

        public string Description => $"Take {DamagePerClick} damage per click, {RepeatTime} times remaining.";
    }
}