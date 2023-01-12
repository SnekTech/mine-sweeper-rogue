using SnekTech.Core.FiniteStateMachine;
using SnekTech.Core.FiniteStateMachine.Animation;

namespace SnekTech.GridCell.Cover
{
    namespace Animation
    {
        public struct Triggers
        {
            public bool ShouldReveal;
            public bool ShouldPutCover;
        }

        public interface ICoverAnimStateProvider
        {
            CoveredIdleState CoveredIdleState { get; }
            RevealState RevealState { get; }
            RevealedIdleState RevealedIdleState { get; }
            PutCoverState PutCoverState { get; }
        }

        public abstract class CoverAnimState : AnimationState<ICover, CoverAnimState, CoverStateMachine>
        {
            protected CoverAnimState(ICover context, CoverStateMachine stateMachine, int animHash, bool shouldLoop) : base(context, stateMachine, animHash, shouldLoop)
            {
            }
        }

        public class CoverStateMachine : StateMachine<ICover, CoverAnimState, CoverStateMachine>
        {
            public Triggers Triggers = new Triggers
            {
                ShouldReveal = false,
                ShouldPutCover = false,
            };
        }
    }
}