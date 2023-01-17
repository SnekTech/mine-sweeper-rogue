using SnekTech.Core.Animation;

namespace SnekTech.GridCell.Cover.Animation
{
    public abstract class CoverAnimState : SpriteAnimState
    {
        protected readonly CoverAnimFSM coverAnimFSM;

        protected CoverAnimState(CoverAnimFSM coverAnimFSM, ICanAnimateSnek animContext, SnekAnimationClip spriteClip) :
            base(animContext, spriteClip)
        {
            this.coverAnimFSM = coverAnimFSM;
        }

        public abstract void Reveal();
        public abstract void PutCover();
    }
}