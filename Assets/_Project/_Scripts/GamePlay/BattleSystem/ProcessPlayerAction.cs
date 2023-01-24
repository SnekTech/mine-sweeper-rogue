using Cysharp.Threading.Tasks;

namespace SnekTech.GamePlay.BattleSystem
{
    public class ProcessPlayerAction : State
    {
        public ProcessPlayerAction(FSM fsm) : base(fsm)
        {
        }

        public override void Enter()
        {

            // process grid & UI according to action

            // change state according to action result
        }

        private async UniTaskVoid BeginProcessPlayerAction()
        {
            var actionType = fsm.PlayerActionType;
            var mousePosition = fsm.MousePosition;
            switch (actionType)
            {
                case PlayerActionType.Primary:
                    // await grid.OnPrimary(mousePosition)
                    break;
                case PlayerActionType.Secondary:
                    // await grid.OnSecondary();
                    break;
                case PlayerActionType.DoublePrimary:
                    // await grid.OnDoublePrimary();
                    break;
            }
            
            
        }
    }
}