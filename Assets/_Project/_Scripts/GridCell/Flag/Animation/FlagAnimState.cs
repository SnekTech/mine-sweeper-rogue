using SnekTech.Core.Animation;
using SnekTech.Core.FiniteStateMachine;

namespace SnekTech.GridCell.Flag
{
    public interface IFlagAnimState : IState
    {
        void Lift();
        void PutDown();
    }

    public abstract class FlagAnimState<T> : SpriteAnimState<T>, IFlagAnimState where T : SpriteClip
    {
        protected readonly FlagAnimFSM flagAnimFSM;

        protected FlagAnimState(FlagAnimFSM flagAnimFSM, T spriteClip) : base(spriteClip)
        {
            this.flagAnimFSM = flagAnimFSM;
        }

        public abstract void Lift();
        public abstract void PutDown();
    }
}