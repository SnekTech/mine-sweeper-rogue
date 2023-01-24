using UnityEngine;

namespace SnekTech.GamePlay.BattleSystem
{
    public enum PlayerActionType
    {
        Primary,
        Secondary,
        DoublePrimary,
    }
    
    public class FSM
    {
        public readonly State LoadLevel;
        public readonly State WaitForPlayerAction;
        public readonly State ProcessPlayerAction;
        public readonly State Win;
        public readonly State Lose;
        
        public FSM()
        {
            LoadLevel = new LoseState(this);
            WaitForPlayerAction = new WaitForPlayerActionState(this);
            ProcessPlayerAction = new ProcessPlayerAction(this);
            Win = new WinState(this);
            Lose = new LoseState(this);
            
            ChangeState(LoadLevel);
        }

        public State Current { get; set; }
        
        public PlayerActionType PlayerActionType { get; set; }
        public Vector2 MousePosition { get; set; }

        public void ChangeState(State newState)
        {
            Current = newState;
            Current.Enter();
        }
    }
}
