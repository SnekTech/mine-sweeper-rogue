using Cysharp.Threading.Tasks;
using SnekTech.MineSweeperRogue.GridSystem.CellSystem;
using UnityEngine;

namespace SnekTech.GamePlay.WeaponSystem.Components
{
    [CreateAssetMenu(menuName = C.MenuName.WeaponComponents + "/" + nameof(Reveal))]
    public class Reveal : WeaponComponent
    {
        public override async UniTask Use(ICell targetCell)
        {
            var grid = targetCell.Parent;
            await grid.RevealAtAsync(targetCell.Index);
        }
    }
}
