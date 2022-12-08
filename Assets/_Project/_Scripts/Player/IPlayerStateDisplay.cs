using Cysharp.Threading.Tasks;
using UnityEngine;

namespace SnekTech.Player
{
    public interface IPlayerStateDisplay
    {
        void UpdateDisplay();
        UniTask PerformDamageEffectAsync(Vector3 damageSourcePosition, int damage);
    }
}