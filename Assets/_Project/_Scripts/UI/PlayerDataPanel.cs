using Cysharp.Threading.Tasks;
using DG.Tweening;
using SnekTech.Player;
using UnityEngine;

namespace SnekTech.UI
{
    public class PlayerDataPanel : MonoBehaviour, IPlayerDataChangePerformer
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
            playerState.AddDataChangePerformer(this);
            UpdatePlayerStateDisplay();
        }

        private void OnEnable()
        {
            playerState.DataChanged += UpdatePlayerStateDisplay;
        }

        private void OnDisable()
        {
            playerState.DataChanged -= UpdatePlayerStateDisplay;
        }

        private void UpdatePlayerStateDisplay()
        {
            healthLabel.SetText(playerState.HealthArmour.Health);
            armourLabel.SetText(playerState.HealthArmour.Armour);
        }

        public async UniTask PerformDamage(Vector3 damageSourcePosition, int damage)
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