using Cysharp.Threading.Tasks;

namespace SnekTech.GamePlay.EffectSystem
{
    public interface IEffect<in T>
    {
        UniTask Take(T target);
    }
}