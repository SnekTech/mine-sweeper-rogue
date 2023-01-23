using System.Collections.Generic;
using SnekTech.GamePlay.AbilitySystem;
using SnekTech.UI.Tooltip;
using UnityEngine;

namespace SnekTech.UI.PlayerStatePanel
{
    public class EffectsPanel : MonoBehaviour
    {
        [SerializeField]
        private HoverIconSlot slotPrefab;

        [SerializeField]
        private Transform gridParentTransform;
        
        public void UpdateClickEffects(List<PlayerAbility> abilities)
        {
            gridParentTransform.DestroyAllChildren();
            foreach (var ability in abilities)
            {
                var slot = Instantiate(slotPrefab, gridParentTransform);
                var tooltipContent = new TooltipContent(ability.Label, ability.Description);
                slot.SetContent(ability, tooltipContent);
            }
        }
    }
}
