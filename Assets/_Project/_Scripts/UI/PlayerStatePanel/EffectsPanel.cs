using System.Collections.Generic;
using SnekTech.Player.ClickEffect;
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
                slot.SetContent(effect.IconHolder);
            }
        }
    }
}
