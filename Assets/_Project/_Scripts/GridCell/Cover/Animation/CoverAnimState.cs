using SnekTech.Core.Animation;
using SnekTech.Core.FiniteStateMachine;

namespace SnekTech.GridCell.Cover.Animation
{
    public interface ICoverAnimState : IState
    {
        void Reveal();
        void PutCover();
    }
    
    public abstract class CoverAnimState<T> : SpriteAnimState<T>, ICoverAnimState where T : SpriteClip
    {
        protected readonly CoverAnimFSM coverAnimFSM;

        protected CoverAnimState(CoverAnimFSM coverAnimFSM, T spriteClip) :
            base(spriteClip)
        {
            this.coverAnimFSM = coverAnimFSM;
        }

        public abstract void Reveal();
        public abstract void PutCover();
    }
}