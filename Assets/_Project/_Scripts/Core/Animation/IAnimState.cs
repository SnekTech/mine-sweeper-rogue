using SnekTech.Core.FiniteStateMachine;

namespace SnekTech.Core.Animation
{
    public interface IAnimState : IState
    {
        bool IsTransitional { get; }
    }
}