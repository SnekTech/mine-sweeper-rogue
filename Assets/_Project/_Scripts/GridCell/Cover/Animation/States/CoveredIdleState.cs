using SnekTech.Core.Animation;

namespace SnekTech.GridCell.Cover.Animation
{
    public class CoveredIdleState : CoverAnimState
    {
        public CoveredIdleState(CoverAnimFSM animFSM, ICanAnimateSnek animContext, SnekAnimationClip clip) :
            base(animFSM, animContext, clip)
        {
        }

        public override void Reveal()
        {
            coverAnimFSM.ChangeState(coverAnimFSM.RevealState);
        }

        public override void PutCover()
        {
        }
    }
}