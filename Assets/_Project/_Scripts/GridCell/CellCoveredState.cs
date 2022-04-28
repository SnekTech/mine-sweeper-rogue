using UnityEngine;

namespace SnekTech.GridCell
{
    public class CellCoveredState : CellState
    {
        public CellCoveredState(ICellBrain cellBrain) : base(cellBrain)
        {
        }

        public override void OnEnterState()
        {
            CellBrain.HideFlag();
            // TODO: cover the cover
        }

        public override void OnLeftClick()
        {
            CellBrain.SwitchState(CellBrain.RevealedState);
        }

        public override void OnRightLick()
        {
            CellBrain.SwitchState(CellBrain.FlaggedState);
        }
    }
}
