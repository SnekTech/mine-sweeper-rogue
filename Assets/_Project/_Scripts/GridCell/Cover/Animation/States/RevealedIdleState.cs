using SnekTech.Core.Animation;
     
namespace SnekTech.GridCell.Cover.Animation
{
    public class RevealedIdleState : CoverAnimState<SpriteClipLoop>
    {
        public RevealedIdleState(CoverAnimFSM animFSM, SpriteClipLoop spriteClipLoop) :
            base(animFSM, spriteClipLoop)
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
                animFSM.ChangeState(animFSM.PutCoverState);
            }
        }
    }
}