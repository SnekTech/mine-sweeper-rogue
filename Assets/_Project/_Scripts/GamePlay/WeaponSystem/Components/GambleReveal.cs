using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using SnekTech.GridCell;
using SnekTech.Roguelike;
using UnityEngine;

namespace SnekTech.GamePlay.WeaponSystem.Components
{
    /// <summary>
    /// random reveal a covered cell
    /// </summary>
    [CreateAssetMenu(menuName = C.MenuName.WeaponComponents + "/" + nameof(GambleReveal))]
    public class GambleReveal : WeaponComponent
    {
        public override async UniTask Use(ICell targetCell)
        {
            var grid = targetCell.ParentGrid;
            
            var coveredCells = grid.Cells.Where(c => c.IsCovered).ToList();
            if (coveredCells.Count == 0)
            {
                return;
            }

            var randomCoveredCell = coveredCells.GetRandom();
            await grid.RevealCellAsync(randomCoveredCell.GridIndex);
        }
    }
}
