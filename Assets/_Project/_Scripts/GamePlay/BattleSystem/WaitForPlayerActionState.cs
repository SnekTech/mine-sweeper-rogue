using UnityEngine;

namespace SnekTech.GamePlay.BattleSystem
{
    public class WaitForPlayerActionState : State
    {
        public WaitForPlayerActionState(FSM fsm) : base(fsm)
        {
        }
        
        public override void Enter()
        {
            // process player action async
        }

        public override void OnPlayerAction(PlayerActionType playerActionType, Vector2 mousePosition)
        {
            fsm.PlayerActionType = playerActionType;
            fsm.MousePosition = mousePosition;
            
            fsm.ChangeState(fsm.ProcessPlayerAction);
        }

    }
}
