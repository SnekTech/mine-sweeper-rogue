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
        private LabelController remainingCoverLabel;

        private void OnEnable()
        {
            gridEventManager.GridInitCompleted += UpdateRemainingCoverLabel;
            gridEventManager.EmptyCellRevealed += UpdateRemainingCoverLabel;
        }

        private void OnDisable()
        {
            gridEventManager.GridInitCompleted -= UpdateRemainingCoverLabel;
            gridEventManager.EmptyCellRevealed -= UpdateRemainingCoverLabel;
        }

        private void UpdateRemainingCoverLabel(IGrid grid)
        {
            int remainingCoverCount = grid.CellCount - grid.RevealedCellCount;
            remainingCoverLabel.LabelText = remainingCoverCount.ToString();
        }
    }
}
