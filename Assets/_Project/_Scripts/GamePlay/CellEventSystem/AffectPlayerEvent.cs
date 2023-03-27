using Cysharp.Threading.Tasks;
using SnekTech.C;
using SnekTech.GamePlay.EffectSystem.PlayerEffects;
using SnekTech.GamePlay.PlayerSystem;
using UnityEngine;

namespace SnekTech.GamePlay.CellEventSystem
{
    [CreateAssetMenu(menuName = MenuName.CellEvents + "/" + nameof(AffectPlayerEvent))]
    public class AffectPlayerEvent : CellEvent
    {
        [SerializeField]
        private CompositePlayerEffect effect;
        
        public override async UniTask Trigger(Player player)
        {
            await effect.Take(player);
        }
    }
}
