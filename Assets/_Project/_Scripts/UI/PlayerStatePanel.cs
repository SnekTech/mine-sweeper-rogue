using Cysharp.Threading.Tasks;
using DG.Tweening;
using SnekTech.Player;
using UnityEngine;

namespace SnekTech.UI
{
    public class PlayerStatePanel : MonoBehaviour, IPlayerStateDisplay
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
            playerState.AddDataChangeDisplay(this);
        }

        public void UpdateDisplay()
        {
            healthLabel.SetText(playerState.Health);
            armourLabel.SetText(playerState.Armour);
        }

        public async UniTask PerformDamageEffectAsync(Vector3 damageSourcePosition, int damage)
        {
            DamageEffectController damageEffect = Instantiate(damageEffectPrefab, transform);
            damageEffect.SetText(-damage);
            var damageEffectTransform = damageEffect.GetComponent<RectTransform>();
            damageEffectTransform.position = damageSourcePosition;
            await damageEffectTransform.DOPath(new[] {transform.position}, 1f, PathType.CatmullRom);
            
            Destroy(damageEffect.gameObject);
        }
    }
}