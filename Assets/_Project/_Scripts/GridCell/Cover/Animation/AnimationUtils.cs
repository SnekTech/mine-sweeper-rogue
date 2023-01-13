using SnekTech.Core.Animation;

namespace SnekTech.GridCell.Cover.Animation
{
    public interface ICoverAnimStateProvider
    {
        CoveredIdleState CoveredIdleState { get; }
        RevealState RevealState { get; }
        RevealedIdleState RevealedIdleState { get; }
        PutCoverState PutCoverState { get; }
    }

    public abstract class CoverAnimState<T> : SpriteAnimState<T> where T : SpriteClip
    {
        // don't set cover activeness in cover itself,
        // so we don't use ICover here, use ICoverAnimStateProvider is better
        protected readonly ICoverAnimStateProvider coverAnimStateProvider;
        protected readonly CoverAnimFSM animFSM;

        protected CoverAnimState(ICoverAnimStateProvider coverAnimStateProvider, CoverAnimFSM coverAnimFSM, T spriteClip) :
            base(spriteClip)
        {
            this.coverAnimStateProvider = coverAnimStateProvider;
            animFSM = coverAnimFSM;
        }
    }
    
    public struct Triggers
    {
        public bool ShouldReveal;
        public bool ShouldPutCover;
    }

    public class CoverAnimFSM : SpriteAnimFSM
    {
        public Triggers Triggers = new Triggers
        {
            ShouldReveal = false,
            ShouldPutCover = false,
        };
    }
}