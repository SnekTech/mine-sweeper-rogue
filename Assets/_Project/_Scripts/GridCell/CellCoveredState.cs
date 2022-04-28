using UnityEngine;

namespace SnekTech.GridCell
{
    public class CellCoveredState : CellState
    {
        public CellCoveredState(CellBehaviour cellBehaviour) : base(cellBehaviour)
        {
        }

        public override void OnEnterState()
        {
            Debug.Log("Entering covered state.");
        }

        public override void OnLeftClick()
        {
            CellBehaviour.SwitchState(CellBehaviour.RevealedState);
        }

        public override void OnRightLick()
        {
            CellBehaviour.SwitchState(CellBehaviour.FlaggedState);
        }
    }
}
