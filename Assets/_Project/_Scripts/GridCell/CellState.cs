namespace SnekTech.GridCell
{
    public abstract class CellState
    {
        protected readonly CellBehaviour CellBehaviour;

        protected CellState(CellBehaviour cellBehaviour)
        {
            CellBehaviour = cellBehaviour;
        }
        
        public abstract void OnEnterState();
        
        public abstract void OnLeftClick();

        public abstract void OnRightLick();
    }
}
