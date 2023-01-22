using SnekTech.GamePlay.AbilitySystem;
using SnekTech.GamePlay.PlayerSystem;
using UnityEngine;

namespace SnekTech.GamePlay.InventorySystem.Components
{
    [CreateAssetMenu(menuName = C.MenuName.Abilities + "/" + nameof(AddClickAbilityComponent))]
    public class AddClickAbilityComponent : AffectPlayerComponent
    {
        [SerializeReference]
        private PlayerAbility playerAbility;
        
        public override void OnAdd(IPlayer player)
        {
            player.AddClickAbility(playerAbility);
        }

        public override void OnRemove(IPlayer player)
        {
        }
    }
}
