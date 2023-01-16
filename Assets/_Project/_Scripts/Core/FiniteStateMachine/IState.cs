namespace SnekTech.Core.FiniteStateMachine
{
    public interface IState
    {
        public void Enter();

        public void Exit();
    }
}
