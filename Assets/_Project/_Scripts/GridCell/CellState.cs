using System.Threading.Tasks;

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
        
        public abstract Task<bool> OnLeftClick();

        public abstract Task<bool> OnRightLick();
    }
}
