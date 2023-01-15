using SnekTech.Core.Animation;

namespace SnekTech.GridCell.Flag
{
    public class FloatState : FlagAnimState<SpriteClipLoop>
    {
        public FloatState(FlagAnimFSM flagAnimFSM, SpriteClipLoop spriteClip) : base(flagAnimFSM, spriteClip)
        {
        }

        public override void Exit()
        {
            base.Exit();

            animFSM.Triggers.ShouldPutDown = false;
        }

        public override void Update()
        {
            if (animFSM.Triggers.ShouldPutDown)
            {
                animFSM.ChangeState(animFSM.PutDownState);
            }
        }
    }
}
