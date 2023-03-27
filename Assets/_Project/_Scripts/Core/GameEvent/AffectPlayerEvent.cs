using Cysharp.Threading.Tasks;
using SnekTech.GamePlay.EffectSystem.PlayerEffects;
using SnekTech.GamePlay.PlayerSystem;
using UnityEngine;

namespace SnekTech.Core.GameEvent
{
    [CreateAssetMenu]
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
