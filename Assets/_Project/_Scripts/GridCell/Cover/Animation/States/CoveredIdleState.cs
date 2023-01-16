using SnekTech.Core.Animation;

namespace SnekTech.GridCell.Cover.Animation
{
    public class CoveredIdleState : CoverAnimState<SpriteClipLoop>
    {
        public CoveredIdleState(CoverAnimFSM animFSM, SpriteClipLoop spriteClipLoop) :
            base(animFSM, spriteClipLoop)
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