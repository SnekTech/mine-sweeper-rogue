using System.Collections.Generic;
using SnekTech.GamePlay.EffectSystem;
using SnekTech.GamePlay.PlayerSystem;
using UnityEngine;

namespace SnekTech.GamePlay.InventorySystem.Components
{
    [CreateAssetMenu(menuName = C.MenuName.ItemComponents + "/" + nameof(TakeEffectsComponent))]
    public class TakeEffectsComponent : ItemComponent
    {
        [SerializeReference]
        private List<PlayerEffect> playerEffects = new List<PlayerEffect>();

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
    }
}
