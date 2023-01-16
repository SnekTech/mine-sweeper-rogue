using SnekTech.Core.Animation;

namespace SnekTech.GridCell.Flag
{
    public class FloatState : FlagAnimState<SpriteClipLoop>
    {
        public FloatState(FlagAnimFSM flagAnimFSM, SpriteClipLoop spriteClip) : base(flagAnimFSM, spriteClip)
        {
        }

        public override void Lift()
        {
        }

        public override void PutDown()
        {
            flagAnimFSM.ChangeState(flagAnimFSM.PutDownState);
        }
    }
}
