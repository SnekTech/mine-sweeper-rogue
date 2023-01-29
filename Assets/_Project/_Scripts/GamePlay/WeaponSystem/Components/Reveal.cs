using Cysharp.Threading.Tasks;
using SnekTech.GridCell;
using UnityEngine;

namespace SnekTech.GamePlay.WeaponSystem.Components
{
    [CreateAssetMenu(menuName = C.MenuName.WeaponComponents + "/" + nameof(Reveal))]
    public class Reveal : WeaponComponent
    {
        public override UniTask Use(ICell cell)
        {
            var grid = cell.ParentGrid;
            return grid.RevealCellAsync(cell.GridIndex);
        }
    }
}
