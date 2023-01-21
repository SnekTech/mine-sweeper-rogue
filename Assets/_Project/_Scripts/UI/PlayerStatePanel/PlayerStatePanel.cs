using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using SnekTech.Core;
using SnekTech.GamePlay;
using SnekTech.GamePlay.AbilitySystem;
using SnekTech.GamePlay.PlayerSystem;
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
        private PlayerHolder playerHolder;

        [SerializeField]
        private PlayerEventChannel playerEventChannel;

        [SerializeField]
        private EffectsPanel abilitiesPanel;

        private EffectEmitter _effectEmitter;
        private RectTransform _healthRectTransform;
        private RectTransform _armourRectTransform;

        private readonly Color _damageColor = PalettePico8.Brown;
        private readonly Color _healthColor = PalettePico8.Green;
        private readonly Color _armourColor = PalettePico8.Blue;

        private Player Player => playerHolder.Player;

        private void Awake()
        {
            Player.AddHealthArmourDisplay(this);
            
            _effectEmitter = GetComponent<EffectEmitter>();
            _healthRectTransform = healthBar.transform as RectTransform;
            _armourRectTransform = armourLabel.transform as RectTransform;
        }

        private void OnEnable()
        {
            playerEventChannel.AbilitiesChanged += OnClickEffectsChanged;
        }

        private void OnDisable()
        {
            playerEventChannel.AbilitiesChanged -= OnClickEffectsChanged;
        }

        private void OnClickEffectsChanged(List<IPlayerAbility> abilities)
        {
            abilitiesPanel.UpdateClickEffects(abilities);
        }

        public void UpdateContent(int health, int maxHealth, int armour)
        {
            healthBar.Init(0, maxHealth, health);
            armourLabel.SetText(armour);
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

        public UniTask PerformAddArmourAsync(int armour)
        {
            var armourStr = new SignedIntStr(armour);
            return _effectEmitter.PerformFloatingTextAsync(armourStr, _armourColor, _armourRectTransform);
        }
    }
}