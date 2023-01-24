using UnityEngine;

namespace SnekTech.GamePlay.BattleSystem
{
    public class WinState : State
    {
        public WinState(FSM fsm) : base(fsm)
        {
        }

        public override void Enter()
        {
            Debug.Log("You won!");
        }
    }
}
