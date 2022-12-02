using System;
using System.Threading.Tasks;
using DG.Tweening;
using SnekTech.Grid;
using SnekTech.GridCell;
using SnekTech.Player;
using UnityEngine;

namespace SnekTech.UI
{
    public class PlayerDataPanel : MonoBehaviour
    {
        [SerializeField]
        private LabelController healthLabel;

        [SerializeField]
        private LabelController armourLabel;

        [SerializeField]
        private PlayerState playerState;

        [SerializeField]
        private DamageEffectController damageEffectPrefab;

        private void OnEnable()
        {
            playerState.DataChanged += UpdatePlayerState;
            playerState.TakenDamage += OnPlayerTakenDamage;
        }

        private async void OnPlayerTakenDamage(IGrid grid, ICell cell, int damage)
        {
            await DamageEffectTask(cell.WorldPosition, damage);
            playerState.InvokeDataChanged();
        }

        private void OnDisable()
        {
            playerState.DataChanged -= UpdatePlayerState;
            playerState.TakenDamage -= OnPlayerTakenDamage;
        }

        private void UpdatePlayerState()
        {
            healthLabel.SetText(playerState.HealthArmour.Health);
            armourLabel.SetText(playerState.HealthArmour.Armour);
        }

        private async Task DamageEffectTask(Vector3 damageSourcePosition, int damage)
        {
            DamageEffectController damageEffect = Instantiate(damageEffectPrefab, transform);
            damageEffect.SetText(-damage);
            var damageEffectTransform = damageEffect.GetComponent<RectTransform>();
            damageEffectTransform.position = damageSourcePosition;
            await damageEffectTransform.DOPath(new[] {transform.position}, 1f, PathType.CatmullRom).AsyncWaitForCompletion();
            
            Destroy(damageEffect.gameObject);
        }
    }
}