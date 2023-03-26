using Cysharp.Threading.Tasks;
using SnekTech.C;
using SnekTech.MineSweeperRogue.GridSystem.CellSystem;
using UnityEngine;

namespace SnekTech.GamePlay.WeaponSystem
{
    [CreateAssetMenu(menuName = MenuName.WeaponSystem + "/" + nameof(Weapon))]
    public class Weapon : ScriptableObject, IWeapon
    {
        [SerializeReference]
        private WeaponComponent primary;

        [SerializeReference]
        private WeaponComponent secondary;

        public async UniTask Primary(ICell cell)
        {
            await primary.Use(cell);
        }

        public UniTask Secondary(ICell cell)
        {
            return secondary.Use(cell);
        }
    }
}