using SnekTech.Core.FiniteStateMachine;

namespace SnekTech.Core.Animation
{
    public abstract class SpriteAnimFSM : FSM
    {
    }

    public abstract class SpriteAnimState<TClip> : State
        where TClip : SpriteClip
    {
        protected readonly TClip spriteClip;

        protected SpriteAnimState(TClip spriteClip)
        {
            this.spriteClip = spriteClip;
        }

        public override void Enter()
        {
            spriteClip.Play();
            spriteClip.RegisterSpriteChange();
        }

        public override void Exit()
        {
            spriteClip.UnregisterSpriteChange();
        }
    }
}