using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using SnekTech.Player;
using SnekTech.UI.Effect;
using UnityEngine;

namespace SnekTech.UI
{
    [RequireComponent(typeof(EffectEmitter))]
    public class PlayerStatePanel : MonoBehaviour, IHealthArmourDisplay
    {
        [SerializeField]
        private LabelController healthLabel;

        [SerializeField]
        private LabelController armourLabel;

        [SerializeField]
        private PlayerState playerState;

        private EffectEmitter _effectEmitter;
        private RectTransform _healthRectTransform;
        private RectTransform _armourRectTransform;

        private void Awake()
        {
            playerState.AddHealthArmourDisplay(this);
            
            _effectEmitter = GetComponent<EffectEmitter>();
            _healthRectTransform = healthLabel.transform as RectTransform;
            _armourRectTransform = armourLabel.transform as RectTransform;
        }

        public void UpdateContent()
        {
            healthLabel.SetText(playerState.Health);
            armourLabel.SetText(playerState.Armour);
        }

        public UniTask PerformHealthDamageAsync(int damage)
        {
            var damageStr = new SignedIntStr(-damage);
            return _effectEmitter.PerformFloatingTextAsync(damageStr, _healthRectTransform);
        }

        public UniTask PerformArmourDamageAsync(int damage)
        {
            var damageStr = new SignedIntStr(-damage);
            return _effectEmitter.PerformFloatingTextAsync(damageStr, _armourRectTransform);
        }
    }
}