namespace SnekTech.GridCell
{
    public class CellRevealedState : CellState
    {
        public CellRevealedState(ICellBrain cellBrain) : base(cellBrain)
        {
        }

        public override void OnEnterState()
        {
            CellBrain.RevealCover();
        }

        public override void OnLeftClick()
        {
        }

        public override void OnRightLick()
        {
        }
    }
}
