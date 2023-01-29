using Cysharp.Threading.Tasks;
using SnekTech.GridCell;
using UnityEngine;

namespace SnekTech.GamePlay.WeaponSystem.Components
{
    [CreateAssetMenu(menuName = C.MenuName.WeaponComponents + "/" + nameof(Reveal))]
    public class Reveal : WeaponComponent
    {
        public override UniTask Use(ICell targetCell)
        {
            var grid = targetCell.ParentGrid;
            return grid.RevealCellAsync(targetCell.GridIndex);
        }
    }
}
