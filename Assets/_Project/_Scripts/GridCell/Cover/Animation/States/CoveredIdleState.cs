using SnekTech.Core.Animation;

namespace SnekTech.GridCell.Cover.Animation
{
    public class CoveredIdleState : CoverAnimState<SpriteClipLoop>
    {
        public CoveredIdleState(ICoverAnimStateProvider coverAnimStateProvider, CoverAnimFSM animFSM, SpriteClipLoop spriteClipLoop) :
            base(coverAnimStateProvider, animFSM, spriteClipLoop)
        {
        }

        public override void Exit()
        {
            base.Exit();

            animFSM.Triggers.ShouldReveal = false;
            // spriteClip.StopAndHide();
        }

        public override void Update()
        {
            if (animFSM.Triggers.ShouldReveal)
            {
                animFSM.ChangeState(coverAnimStateProvider.RevealState);
            }
        }
    }
}