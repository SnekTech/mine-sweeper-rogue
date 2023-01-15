using SnekTech.Core.Animation;

namespace SnekTech.GridCell.Cover.Animation
{
    public class RevealedIdleState : CoverAnimState<SpriteClipLoop>
    {
        public RevealedIdleState(ICoverAnimStateProvider coverAnimStateProvider, CoverAnimFSM animFSM, SpriteClipLoop spriteClipLoop) :
            base(coverAnimStateProvider, animFSM, spriteClipLoop)
        {
        }

        public override void Exit()
        {
            base.Exit();

            animFSM.Triggers.ShouldPutCover = false;
        }

        public override void Update()
        {
            if (animFSM.Triggers.ShouldPutCover)
            {
                animFSM.ChangeState(coverAnimStateProvider.PutCoverState);
            }
        }
    }
}