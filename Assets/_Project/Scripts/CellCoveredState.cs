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
            Debug.Log("left clicking a covered cell, what to do?");
        }

        public override void OnRightLick()
        {
            Cell.SwitchState(Cell.FlaggedState);
        }
    }
}
