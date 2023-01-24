using Cysharp.Threading.Tasks;
using SnekTech.Grid;

namespace SnekTech.GamePlay.BattleSystem
{
    public class LoadLevelState : State
    {
        public LoadLevelState(FSM fsm) : base(fsm)
        {
        }
        
        public override void Enter()
        {
            
        }

        private async UniTaskVoid LoadLevel(IGrid grid)
        {
            // judge pick the grid data & game mode
            
            // init grid
            
            // change to WaitForPlayerActionState
            fsm.ChangeState(fsm.WaitForPlayerAction);
        }
    }
}
