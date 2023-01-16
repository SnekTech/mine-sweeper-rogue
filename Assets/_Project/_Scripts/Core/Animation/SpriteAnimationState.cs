using SnekTech.Core.FiniteStateMachine;

namespace SnekTech.Core.Animation
{
    public abstract class SpriteAnimState<TClip> : IState
        where TClip : SpriteClip
    {
        protected readonly TClip spriteClip;

        protected SpriteAnimState(TClip spriteClip)
        {
            this.spriteClip = spriteClip;
        }

        public virtual void Enter()
        {
            spriteClip.Play();
            spriteClip.RegisterSpriteChange();
        }

        public virtual void Exit()
        {
            spriteClip.UnregisterSpriteChange();
        }
    }
}