using SnekTech.Core.Animation;
using SnekTech.Core.FiniteStateMachine;

namespace SnekTech.GridCell.Flag
{
    public interface IFlagAnimState : IState
    {
        bool IsTransitional { get; }
        void Lift();
        void PutDown();
    }

    public abstract class FlagAnimState<T> : SpriteAnimState<T>, IFlagAnimState where T : SpriteClip
    {
        protected readonly FlagAnimFSM flagAnimFSM;
        private readonly bool isTransitional;

        protected FlagAnimState(FlagAnimFSM flagAnimFSM, T spriteClip) : base(spriteClip)
        {
            this.flagAnimFSM = flagAnimFSM;
            isTransitional = spriteClip is SpriteClipNonLoop;
        }

        public bool IsTransitional => isTransitional;

        public abstract void Lift();
        public abstract void PutDown();
    }
}