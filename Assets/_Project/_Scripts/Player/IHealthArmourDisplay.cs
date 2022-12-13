using Cysharp.Threading.Tasks;

namespace SnekTech.Player
{
    public interface IHealthArmourDisplay
    {
        void UpdateContent();
        UniTask PerformDamageEffectAsync(int damage);
    }
}