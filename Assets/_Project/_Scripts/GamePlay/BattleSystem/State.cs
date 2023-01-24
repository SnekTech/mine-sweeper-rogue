using Cysharp.Threading.Tasks;
using UnityEngine;

namespace SnekTech.GamePlay.BattleSystem
{
    public abstract class State
    {
        protected readonly FSM fsm;

        protected State(FSM fsm)
        {
            this.fsm = fsm;
        }

        public virtual void Enter()
        {
        }

        public virtual void Exit()
        {
        }

        public virtual void OnPlayerAction(PlayerActionType playerActionType, Vector2 mousePosition)
        {
        }
    }
}