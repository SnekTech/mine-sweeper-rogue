using SnekTech.Core.Animation;

namespace SnekTech.GridCell.Cover.Animation
{
    public class CoveredIdleState : CoverAnimState<SpriteClipLoop>
    {
        public CoveredIdleState(CoverAnimFSM animFSM, SpriteClipLoop spriteClipLoop) :
            base(animFSM, spriteClipLoop)
        {
        }

        public override void Exit()
        {
            base.Exit();

            animFSM.Triggers.ShouldReveal = false;
        }

        public override void Update()
        {
            if (animFSM.Triggers.ShouldReveal)
            {
                animFSM.ChangeState(animFSM.RevealState);
            }
        }
    }
}