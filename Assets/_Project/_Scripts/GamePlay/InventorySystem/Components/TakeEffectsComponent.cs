using System.Collections.Generic;
using SnekTech.C;
using SnekTech.GamePlay.EffectSystem;
using SnekTech.GamePlay.PlayerSystem;
using UnityEngine;

namespace SnekTech.GamePlay.InventorySystem.Components
{
    [CreateAssetMenu(menuName = MenuName.ItemComponents + "/" + nameof(TakeEffectsComponent))]
    public class TakeEffectsComponent : AffectPlayerComponent
    {
        [SerializeReference]
        private List<IPlayerEffect> playerEffects = new List<IPlayerEffect>();

        public override void OnAdd(IPlayer player)
        {
            foreach (var playerEffect in playerEffects)
            {
                playerEffect.Take(player);
            }
        }

        public override void OnRemove(IPlayer player)
        {
        }
        
        // todo: custom editor
        [ContextMenu(nameof(AddDamageEffect))]
        public void AddDamageEffect()
        {
            playerEffects.Add(new DamageEffect());
        }

        [ContextMenu(nameof(AddCompositeEffect))]
        public void AddCompositeEffect()
        {
            playerEffects.Add(new CompositePlayerEffect());
        }
    }
}
