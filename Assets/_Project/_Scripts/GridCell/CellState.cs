namespace SnekTech.GridCell
{
    public abstract class CellState
    {
        protected readonly ICell Cell;

        protected CellState(ICell cell)
        {
            Cell = cell;
        }
        
        public abstract void OnEnterState();
        
        public abstract void OnLeftClick();

        public abstract void OnRightLick();
    }
}
