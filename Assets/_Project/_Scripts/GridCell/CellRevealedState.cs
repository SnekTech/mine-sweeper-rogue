namespace SnekTech.GridCell
{
    public class CellRevealedState : CellState
    {
        public CellRevealedState(CellBehaviour cellBehaviour) : base(cellBehaviour)
        {
        }

        public override void OnEnterState()
        {
            CellBehaviour.RevealCover();
        }

        public override void OnLeftClick()
        {
        }

        public override void OnRightLick()
        {
        }
    }
}
