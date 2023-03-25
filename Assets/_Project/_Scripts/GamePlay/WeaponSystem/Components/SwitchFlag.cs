using Cysharp.Threading.Tasks;
using SnekTech.MineSweeperRogue.GridSystem.CellSystem;
using UnityEngine;

namespace SnekTech.GamePlay.WeaponSystem.Components
{
    [CreateAssetMenu(menuName = C.MenuName.WeaponComponents + "/" + nameof(SwitchFlag))]
    public class SwitchFlag : WeaponComponent
    {
        public override async UniTask Use(ICell targetCell)
        {
            await targetCell.SwitchFlagAsync();
        }
    }
}