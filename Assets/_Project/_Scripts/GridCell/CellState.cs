namespace SnekTech.GridCell
{
    public abstract class CellState
    {
        protected readonly ICellBrain CellBrain;

        protected CellState(ICellBrain cellBrain)
        {
            CellBrain = cellBrain;
        }
        
        public abstract void OnEnterState();
        public abstract void OnLeaveState();
        
        public abstract void OnLeftClick();

        public abstract void OnRightLick();
    }
}
