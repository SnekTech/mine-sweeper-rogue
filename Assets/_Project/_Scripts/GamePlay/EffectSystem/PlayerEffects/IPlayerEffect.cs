using Cysharp.Threading.Tasks;
using SnekTech.GamePlay.PlayerSystem;

namespace SnekTech.GamePlay.EffectSystem.PlayerEffects
{
    public interface IPlayerEffect
    {
        public UniTask Take(IPlayer target);
    }
}
