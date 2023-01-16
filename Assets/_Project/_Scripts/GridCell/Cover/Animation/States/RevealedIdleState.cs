using SnekTech.Core.Animation;

namespace SnekTech.GridCell.Cover.Animation
{
    public class RevealedIdleState : CoverAnimState<SpriteClipLoop>
    {
        public RevealedIdleState(CoverAnimFSM animFSM, SpriteClipLoop spriteClipLoop) :
            base(animFSM, spriteClipLoop)
        {
        }

        public override void Reveal()
        {
        }

        public override void PutCover()
        {
            coverAnimFSM.ChangeState(coverAnimFSM.PutCoverState);
        }
    }
}