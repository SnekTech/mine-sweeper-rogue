namespace SnekTech.Core.FiniteStateMachine
{
    public interface IFiniteStateMachine<in T> where T : IState
    {
        public void Init(T initialState);
        public void ChangeState(T newState);
    }
}
