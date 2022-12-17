using Cysharp.Threading.Tasks;
using SnekTech.Core;
using SnekTech.Player;
using SnekTech.UI.Effect;
using UnityEngine;

namespace SnekTech.UI.PlayerStatePanel
{
    [RequireComponent(typeof(EffectEmitter))]
    public class PlayerStatePanel : MonoBehaviour, IHealthArmourDisplay
    {
        [SerializeField]
        private ProgressBar healthBar;

        [SerializeField]
        private LabelController armourLabel;

        [SerializeField]
        private Player.PlayerState playerState;

        private EffectEmitter _effectEmitter;
        private RectTransform _healthRectTransform;
        private RectTransform _armourRectTransform;

        private readonly Color _damageColor = PalettePico8.Brown;
        private readonly Color _healthColor = PalettePico8.Green;

        private void Awake()
        {
            playerState.AddHealthArmourDisplay(this);
            
            _effectEmitter = GetComponent<EffectEmitter>();
            _healthRectTransform = healthBar.transform as RectTransform;
            _armourRectTransform = armourLabel.transform as RectTransform;
        }

        public void UpdateContent()
        {
            healthBar.Init(0, playerState.MaxHealth, playerState.Health);
            armourLabel.SetText(playerState.Armour);
        }

        public UniTask PerformHealthDamageAsync(int damage)
        {
            var damageStr = new SignedIntStr(-damage);
            return _effectEmitter.PerformFloatingTextAsync(damageStr, _damageColor, _healthRectTransform);
        }

        public UniTask PerformArmourDamageAsync(int damage)
        {
            var damageStr = new SignedIntStr(-damage);
            return _effectEmitter.PerformFloatingTextAsync(damageStr, _damageColor, _armourRectTransform);
        }

        public UniTask PerformAddHealthAsync(int health)
        {
            var healthStr = new SignedIntStr(health);
            return _effectEmitter.PerformFloatingTextAsync(healthStr, _healthColor, _healthRectTransform);
        }
    }
}