using SnekTech.Core.Animation;
using SnekTech.Core.FiniteStateMachine;

namespace SnekTech.GridCell.Cover.Animation
{
    public class CoverAnimFSM : FSM<ICoverAnimState>
    {

        public readonly CoveredIdleState CoveredIdleState;
        public readonly RevealState RevealState;
        public readonly RevealedIdleState RevealedIdleState;
        public readonly PutCoverState PutCoverState;

        public bool IsInTransitionalState => Current.IsTransitional;

        public CoverAnimFSM(ICanAnimate animContext, CoverAnimData animData)
        {
            CoveredIdleState = new CoveredIdleState(this, new SpriteClipLoop(animContext, animData.CoveredIdle));
            RevealState = new RevealState(this, new SpriteClipNonLoop(animContext, animData.Reveal));
            RevealedIdleState = new RevealedIdleState(this, new SpriteClipLoop(animContext, animData.RevealedIdle));
            PutCoverState = new PutCoverState(this, new SpriteClipNonLoop(animContext, animData.PutCover));
            
            Init(CoveredIdleState);
        }

        public void Reveal() => Current.Reveal();
        public void PutCover() => Current.PutCover();
    }
}