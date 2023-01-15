using SnekTech.Core.Animation;

namespace SnekTech.GridCell.Cover.Animation
{
    public abstract class CoverAnimState<T> : SpriteAnimState<T> where T : SpriteClip
    {
        protected readonly CoverAnimFSM animFSM;

        protected CoverAnimState(CoverAnimFSM coverAnimFSM, T spriteClip) :
            base(spriteClip)
        {
            animFSM = coverAnimFSM;
        }
    }
}