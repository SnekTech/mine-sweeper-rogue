using System;
using SnekTech.GridCell;
using SnekTech.GridSystem;
using SnekTech.MineSweeperRogue.GridSystem;
using SnekTech.MineSweeperRogue.GridSystem.CellSystem;
using UnityEngine;

namespace SnekTech.UI
{
    public class GridDataPanel : MonoBehaviour
    {
        [SerializeField]
        private GridEventChannel gridEventChannel;

        [SerializeField]
        private LabelController remainingCoverCountLabel;

        [SerializeField]
        private LabelController flaggedCellCountLabel;

        private void OnEnable()
        {
            gridEventChannel.OnGridInitComplete += HandleOnGridInitComplete;
            gridEventChannel.OnCellReveal += HandleOnCellReveal;
            gridEventChannel.OnCellFlagOperateComplete += HandleOnCellFlagOperateComplete;
        }

        private void OnDisable()
        {
            gridEventChannel.OnGridInitComplete -= HandleOnGridInitComplete;
            gridEventChannel.OnCellReveal -= HandleOnCellReveal;
            gridEventChannel.OnCellFlagOperateComplete -= HandleOnCellFlagOperateComplete;
        }

        private void HandleOnGridInitComplete(IGrid grid) => UpdateGridData(grid);

        private void HandleOnCellReveal(IGrid grid, ICell cell) => UpdateRemainingCoverCount(grid);

        private void HandleOnCellFlagOperateComplete(IGrid grid) => UpdateFlaggedCellCount(grid);

        private void UpdateGridData(IGrid grid)
        {
            UpdateRemainingCoverCount(grid);
            UpdateFlaggedCellCount(grid);
        }

        private void UpdateFlaggedCellCount(IGrid grid)
        {
            flaggedCellCountLabel.SetText(grid.FlaggedCellCount);
        }

        private void UpdateRemainingCoverCount(IGrid grid)
        {
            var remainingCoverCount = grid.Size.TotalCount - grid.RevealedCellCount;
            remainingCoverCountLabel.SetText(remainingCoverCount);
        }
    }
}
