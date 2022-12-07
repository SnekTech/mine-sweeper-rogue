using Cysharp.Threading.Tasks;
using UnityEngine;

namespace SnekTech.Player
{
    public interface IPlayerDataChangePerformer
    {
        UniTask PerformDamage(Vector3 damageSourcePosition, int damage);
    }
}