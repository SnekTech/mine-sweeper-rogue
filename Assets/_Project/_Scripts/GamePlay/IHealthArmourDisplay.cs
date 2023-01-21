using Cysharp.Threading.Tasks;

namespace SnekTech.GamePlay
{
    public interface IHealthArmourDisplay
    {
        void UpdateContent(int health, int maxHealth, int armour);
        UniTask PerformHealthDamageAsync(int damage);
        UniTask PerformArmourDamageAsync(int damage);
        UniTask PerformAddHealthAsync(int health);
        UniTask PerformAddArmourAsync(int armour);
    }
}