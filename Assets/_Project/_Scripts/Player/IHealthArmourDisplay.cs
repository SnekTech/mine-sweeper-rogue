using Cysharp.Threading.Tasks;

namespace SnekTech.Player
{
    public interface IHealthArmourDisplay
    {
        void UpdateContent();
        UniTask PerformHealthDamageAsync(int damage);
        UniTask PerformArmourDamageAsync(int damage);
        UniTask PerformAddHealthAsync(int health);
    }
}