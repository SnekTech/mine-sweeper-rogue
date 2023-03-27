using Cysharp.Threading.Tasks;
using SnekTech.GamePlay.EffectSystem.PlayerEffects;
using SnekTech.GamePlay.PlayerSystem;
using UnityEngine;

namespace SnekTech.GamePlay.CellEventSystem
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
