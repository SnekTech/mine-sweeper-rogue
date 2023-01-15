using SnekTech.Core.Animation;

namespace SnekTech.GridCell.Flag
{
    public abstract class FlagAnimState<T> : SpriteAnimState<T> where T : SpriteClip
    {
        protected readonly FlagAnimFSM animFSM;
        
        protected FlagAnimState(FlagAnimFSM flagAnimFSM, T spriteClip) : base(spriteClip)
        {
            animFSM = flagAnimFSM;
        }
    }
}
