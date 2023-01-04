using System.Collections.Generic;
using SnekTech.Player.ClickEffect;
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
        
        public void UpdateClickEffects(List<IClickEffect> clickEffects)
        {
            gridParentTransform.DestroyAllChildren();
            foreach (IClickEffect effect in clickEffects)
            {
                HoverIconSlot slot = Instantiate(slotPrefab, gridParentTransform);
                var tooltipContent = new TooltipContent(effect.IconHolder.Label, effect.Description);
                slot.SetContent(effect.IconHolder, tooltipContent);
            }
        }
    }
}
