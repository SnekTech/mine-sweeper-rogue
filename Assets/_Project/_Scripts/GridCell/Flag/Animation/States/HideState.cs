using SnekTech.Core.Animation;

namespace SnekTech.GridCell.Flag
{
    public class HideState : FlagAnimState<SpriteClipLoop>
    {
        public HideState(FlagAnimFSM flagAnimFSM, SpriteClipLoop spriteClip) : base(flagAnimFSM, spriteClip)
        {
        }

        public override void Exit()
        {
            base.Exit();

            animFSM.Triggers.ShouldLift = false;
        }

        public override void Update()
        {
            if (animFSM.Triggers.ShouldLift)
            {
                animFSM.ChangeState(animFSM.LiftState);
            }
        }
    }
}
