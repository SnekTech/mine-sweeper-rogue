using SnekTech.Core.Animation;

namespace SnekTech.GridCell.Cover.Animation
{
    public interface ICoverAnimState : IAnimState
    {
        void Reveal();
        void PutCover();
    }
    
    public abstract class CoverAnimState<T> : SpriteAnimState<T>, ICoverAnimState where T : SpriteClip
    {
        protected readonly CoverAnimFSM coverAnimFSM;
        private readonly bool isTransitional;

        public bool IsTransitional => isTransitional;

        protected CoverAnimState(CoverAnimFSM coverAnimFSM, T spriteClip) :
            base(spriteClip)
        {
            this.coverAnimFSM = coverAnimFSM;
            isTransitional = spriteClip is SpriteClipNonLoop;
        }

        public abstract void Reveal();
        public abstract void PutCover();
    }
}