using SnekTech.Core.FiniteStateMachine;

namespace SnekTech.GridCell.Cover.Animation
{
    public class CoverAnimFSM : IFiniteStateMachine<ICoverAnimState>
    {
        private ICoverAnimState _current;

        public CoveredIdleState CoveredIdleState { get; private set; }
        public RevealState RevealState { get; private set; }
        public RevealedIdleState RevealedIdleState { get; private set; }
        public PutCoverState PutCoverState { get; private set; }

        public void PopulateStates(CoveredIdleState coveredIdleState, RevealState revealState,
            RevealedIdleState revealedIdleState, PutCoverState putCoverState)
        {
            CoveredIdleState = coveredIdleState;
            RevealState = revealState;
            RevealedIdleState = revealedIdleState;
            PutCoverState = putCoverState;
        }

        public void Init(ICoverAnimState initialState)
        {
            _current = initialState;
            _current.Enter();
        }

        public void ChangeState(ICoverAnimState newState)
        {
            _current.Exit();
            _current = newState;
            _current.Enter();
        }

        public void Reveal() => _current.Reveal();
        public void PutCover() => _current.PutCover();
    }
}