namespace SnekTech
{
    public class CellRevealedState : CellState
    {
        public CellRevealedState(Cell cell) : base(cell)
        {
        }

        public override void OnEnterState()
        {
            Cell.Reveal();
        }

        public override void OnLeftClick()
        {
        }

        public override void OnRightLick()
        {
        }
    }
}
