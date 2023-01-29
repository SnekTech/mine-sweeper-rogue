using Cysharp.Threading.Tasks;
using SnekTech.GridCell;
using UnityEngine;

namespace SnekTech.GamePlay.WeaponSystem
{
    [CreateAssetMenu(menuName = C.MenuName.WeaponSystem + "/" + nameof(Weapon))]
    public class Weapon : ScriptableObject, IWeapon
    {
        [SerializeReference]
        private WeaponComponent primary;

        [SerializeReference]
        private WeaponComponent secondary;

        public UniTask Primary(ICell cell)
        {
            return primary.Use(cell);
        }

        public UniTask Secondary(ICell cell)
        {
            return secondary.Use(cell);
        }
    }
}