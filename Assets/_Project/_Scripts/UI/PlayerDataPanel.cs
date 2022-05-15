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
        private PlayerData playerData;

        [SerializeField]
        private DamageEffectController damageEffectPrefab;

        private void OnEnable()
        {
            playerData.DataChanged += UpdatePlayerData;
            playerData.TakenDamage += OnPlayerTakenDamage;
        }

        private async void OnPlayerTakenDamage(IGrid grid, ICell cell, int damage)
        {
            await DamageEffectTask(cell.WorldPosition, damage);
            playerData.InvokeDataChanged();
        }

        private void OnDisable()
        {
            playerData.DataChanged -= UpdatePlayerData;
            playerData.TakenDamage -= OnPlayerTakenDamage;
        }

        private void UpdatePlayerData()
        {
            healthLabel.SetText(playerData.HealthArmour.Health);
            armourLabel.SetText(playerData.HealthArmour.Armour);
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