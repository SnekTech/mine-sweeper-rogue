using UnityEngine;

namespace SnekTech
{
    public class CellCoveredState : CellState
    {
        public CellCoveredState(Cell cell) : base(cell)
        {
        }

        public override void OnEnterState()
        {
            Debug.Log("Entering covered state.");
        }

        public override void OnLeftClick()
        {
            Cell.SwitchState(Cell.UncoveredState);
        }

        public override void OnRightLick()
        {
            Cell.SwitchState(Cell.FlaggedState);
        }
    }
}
