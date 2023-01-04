using System;
using SnekTech.Grid;
using UnityEngine;

namespace SnekTech.UI
{
    public class GridDataPanel : MonoBehaviour
    {
        [SerializeField]
        private GridEventManager gridEventManager;

        [SerializeField]
        private LabelController remainingCoverCountLabel;

        [SerializeField]
        private LabelController flaggedCellCountLabel;

        private void OnEnable()
        {
            gridEventManager.GridInitCompleted += UpdateGridData;
            gridEventManager.CellRevealed += UpdateRemainingCoverCount;
            gridEventManager.CellFlagOperated += UpdateFlaggedCellCount;
        }

        private void OnDisable()
        {
            gridEventManager.GridInitCompleted -= UpdateGridData;
            gridEventManager.CellRevealed -= UpdateRemainingCoverCount;
            gridEventManager.CellFlagOperated -= UpdateFlaggedCellCount;
        }

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
            int remainingCoverCount = grid.CellCount - grid.RevealedCellCount;
            remainingCoverCountLabel.SetText(remainingCoverCount);
        }
    }
}
