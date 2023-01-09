using Cysharp.Threading.Tasks;

namespace SnekTech.GridCell
{
    public class CellCoveredState : CellState
    {
        public CellCoveredState(ICellBrain cellBrain) : base(cellBrain)
        {
        }

        public override void OnEnterState()
        {
            Flag.IsActive = false;
        }

        public override void OnLeaveState()
        {
        }

        public override async UniTask<bool> OnLeftClick()
        {
            bool isRevealCompleted = await Cover.RevealAsync();
            if (isRevealCompleted)
            {
                CellBrain.SwitchState(CellBrain.RevealedState);
            }

            return isRevealCompleted;
        }

        public override async UniTask<bool> OnRightLick()
        {
            Flag.IsActive = true;

            bool isLiftFlagCompleted = await Flag.LiftAsync();
            if (isLiftFlagCompleted)
            {
                CellBrain.SwitchState(CellBrain.FlaggedState);
            }

            return isLiftFlagCompleted;
        }
    }
}
