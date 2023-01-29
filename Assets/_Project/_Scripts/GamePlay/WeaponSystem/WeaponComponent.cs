using Cysharp.Threading.Tasks;
using SnekTech.GridCell;
using UnityEngine;

namespace SnekTech.GamePlay.WeaponSystem
{
    public abstract class WeaponComponent : ScriptableObject
    {
        public abstract UniTask Use(ICell targetCell);
    }
}
