using System.Linq;
using Cysharp.Threading.Tasks;
using SnekTech.C;
using SnekTech.MineSweeperRogue.GridSystem.CellSystem;
using SnekTech.Roguelike;
using UnityEngine;

namespace SnekTech.GamePlay.WeaponSystem.Components
{
    /// <summary>
    /// random reveal a covered cell
    /// </summary>
    [CreateAssetMenu(menuName = MenuName.WeaponComponents + "/" + nameof(GambleReveal))]
    public class GambleReveal : WeaponComponent
    {
        public override async UniTask Use(ICell targetCell)
        {
            var grid = targetCell.Parent;

            var coveredCells = grid.Cells.Where(c => c.IsCovered).ToList();
            if (coveredCells.Count == 0)
            {
                return;
            }

            var randomCoveredCell = coveredCells.GetRandom();
            await grid.RevealAtAsync(randomCoveredCell.Index);
        }
    }
}