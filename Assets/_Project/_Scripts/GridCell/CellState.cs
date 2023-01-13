using Cysharp.Threading.Tasks;
using SnekTech.GridCell.Cover;

namespace SnekTech.GridCell
{
    public abstract class CellState
    {
        // todo: the abstract Core.FSM
        protected readonly ICellBrain CellBrain;
        protected readonly ICell Cell;
        protected readonly IFlag Flag;
        protected readonly ICover Cover;

        protected CellState(ICellBrain cellBrain)
        {
            CellBrain = cellBrain;
            Cell = cellBrain.Cell;
            Flag = Cell.Flag;
            Cover = Cell.Cover;
        }
        
        public abstract void OnEnterState();
        public abstract void OnLeaveState();
        
        public abstract UniTask<bool> OnLeftClick();

        public abstract UniTask<bool> OnRightLick();
    }
}
