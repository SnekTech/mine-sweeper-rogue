using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using SnekTech.Player;
using UnityEngine;

namespace SnekTech.UI
{
    public class PlayerStatePanel : MonoBehaviour, IHealthArmourDisplay
    {
        [SerializeField]
        private LabelController healthLabel;

        [SerializeField]
        private LabelController armourLabel;

        [SerializeField]
        private PlayerState playerState;

        [SerializeField]
        private DamageEffectController damageEffectPrefab;

        private void Awake()
        {
            playerState.AddHealthArmourDisplay(this);
        }

        private void OnEnable()
        {
            playerState.DataChanged += OnPlayerStateDataChanged;
        }

        private void OnDisable()
        {
            playerState.DataChanged -= OnPlayerStateDataChanged;
        }

        private void OnPlayerStateDataChanged()
        {
            UpdateContent();
        }

        public void UpdateContent()
        {
            healthLabel.SetText(playerState.Health);
            armourLabel.SetText(playerState.Armour);
        }

        public async UniTask PerformDamageEffectAsync(int damage)
        {
            // todo: use vertical floating effect
            DamageEffectController damageEffect = Instantiate(damageEffectPrefab, transform);
            damageEffect.SetText(-damage);
            var damageEffectTransform = damageEffect.GetComponent<RectTransform>();

            await damageEffectTransform.DOAnchorPosY(damageEffectTransform.anchoredPosition.y + 10, 1f);

            if (damageEffect != null)
            {
                Destroy(damageEffect.gameObject);
            }
        }
    }
}