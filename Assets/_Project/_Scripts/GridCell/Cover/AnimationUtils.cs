using SnekTech.Core.Animation;

namespace SnekTech.GridCell.Cover
{
    namespace Animation
    {
        public struct Triggers
        {
            public bool ShouldReveal;
            public bool ShouldPutCover;
        }

        public interface ICoverAnimStateProvider
        {
            CoveredIdleState CoveredIdleState { get; }
            RevealState RevealState { get; }
            RevealedIdleState RevealedIdleState { get; }
            PutCoverState PutCoverState { get; }
        }

        public abstract class CoverAnimState<T> : SpriteAnimState<T> where T : SpriteClip
        {
            protected readonly ICover cover;
            protected readonly CoverAnimFSM animFSM;
            protected CoverAnimState(ICover cover, CoverAnimFSM coverAnimFSM, T spriteClip) : 
                base(spriteClip)
            {
                this.cover = cover;
                animFSM = coverAnimFSM;
            }
        }
        
        public class CoverAnimFSM: SpriteAnimFSM
        {
            public Triggers Triggers = new Triggers
            {
                ShouldReveal = false,
                ShouldPutCover = false,
            };
        }
    }
}