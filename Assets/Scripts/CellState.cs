namespace SnekTech
{
    public abstract class CellState
    {
        protected Cell Cell;

        protected CellState(Cell cell)
        {
            Cell = cell;
        }
        
        public abstract void OnEnterState();
        
        public abstract void OnLeftClick();

        public abstract void OnRightLick();
    }
}
