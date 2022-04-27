namespace SnekTech.GridCell
{
    public class CellRevealedState : CellState
    {
        public CellRevealedState(Cell cell) : base(cell)
        {
        }

        public override void OnEnterState()
        {
            Cell.RevealCover();
        }

        public override void OnLeftClick()
        {
        }

        public override void OnRightLick()
        {
        }
    }
}
