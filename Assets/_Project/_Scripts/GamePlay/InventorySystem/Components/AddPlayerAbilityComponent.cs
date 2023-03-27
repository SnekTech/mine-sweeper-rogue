using Cysharp.Threading.Tasks;
using SnekTech.GamePlay.AbilitySystem;
using SnekTech.GamePlay.PlayerSystem;
using UnityEngine;

namespace SnekTech.GamePlay.InventorySystem.Components
{
    [CreateAssetMenu(menuName = C.MenuName.ItemComponents + "/" + nameof(AddPlayerAbilityComponent))]
    public class AddPlayerAbilityComponent : ItemComponent
    {
        [SerializeReference]
        private PlayerAbility playerAbility;

        [SerializeField]
        private int repeatTimes = 3;
        
        public override UniTask OnAdd(IPlayer player)
        {
            player.AddAbility(playerAbility, repeatTimes);
            return UniTask.CompletedTask;
        }

        public override UniTask OnRemove(IPlayer player)
        {
            return UniTask.CompletedTask;
        }
    }
}
