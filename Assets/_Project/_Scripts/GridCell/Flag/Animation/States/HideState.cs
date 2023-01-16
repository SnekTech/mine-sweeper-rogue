using SnekTech.Core.Animation;

namespace SnekTech.GridCell.Flag
{
    public class HideState : FlagAnimState<SpriteClipLoop>
    {
        public HideState(FlagAnimFSM flagAnimFSM, SpriteClipLoop spriteClip) : base(flagAnimFSM, spriteClip)
        {
        }

        public override void Lift()
        {
            flagAnimFSM.ChangeState(flagAnimFSM.LiftState);
        }

        public override void PutDown()
        {
        }
    }
}
