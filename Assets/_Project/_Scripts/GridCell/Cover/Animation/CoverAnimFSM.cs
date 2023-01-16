using SnekTech.Core.FiniteStateMachine;

namespace SnekTech.GridCell.Cover.Animation
{
    public class CoverAnimFSM : FSM<CoverAnimState>
    {

        public readonly CoveredIdleState CoveredIdleState;
        public readonly RevealState RevealState;
        public readonly RevealedIdleState RevealedIdleState;
        public readonly PutCoverState PutCoverState;

        public CoverAnimFSM(ICanAnimateSnek animContext, CoverAnimData animData)
        {
            CoveredIdleState = new CoveredIdleState(this, animContext, animData.CoveredIdle);
            RevealState = new RevealState(this, animContext, animData.Reveal);
            RevealedIdleState = new RevealedIdleState(this, animContext, animData.RevealedIdle);
            PutCoverState = new PutCoverState(this, animContext, animData.PutCover);
            
            Init(CoveredIdleState);
        }

        public void Reveal() => Current.Reveal();
        public void PutCover() => Current.PutCover();
    }
}