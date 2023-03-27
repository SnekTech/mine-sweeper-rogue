using Cysharp.Threading.Tasks;
using SnekTech.GamePlay.EffectSystem.PlayerEffects;
using SnekTech.GamePlay.PlayerSystem;
using UnityEngine;

namespace SnekTech.GamePlay.InventorySystem.Components
{
    [CreateAssetMenu(menuName = C.MenuName.ItemComponents + "/" + nameof(AffectPlayerComponent))]
    public class AffectPlayerComponent : ItemComponent
    {
        [SerializeField]
        private CompositePlayerEffect effect;
        
        public override UniTask OnAdd(IPlayer player)
        {
            return effect.Take(player);
        }

        public override UniTask OnRemove(IPlayer player)
        {
            return UniTask.CompletedTask;
        }
    }
}
