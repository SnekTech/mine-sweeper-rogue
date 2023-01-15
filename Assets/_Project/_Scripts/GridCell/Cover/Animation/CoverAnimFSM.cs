using SnekTech.Core.Animation;

namespace SnekTech.GridCell.Cover.Animation
{
    public struct Triggers
    {
        public bool ShouldReveal;
        public bool ShouldPutCover;
    }
    
    public class CoverAnimFSM : SpriteAnimFSM
    {
        public Triggers Triggers = new Triggers
        {
            ShouldReveal = false,
            ShouldPutCover = false,
        };
        
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
    }
}