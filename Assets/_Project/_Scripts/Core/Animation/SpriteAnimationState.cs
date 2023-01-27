namespace SnekTech.Core.Animation
{
    public abstract class SpriteAnimState : IAnimState
    {
        protected readonly SnekAnimator animator;
        private readonly SnekAnimationClip clip;

        protected SpriteAnimState(ICanAnimateSnek animContext, SnekAnimationClip clip)
        {
            animator = animContext.SnekAnimator;
            animator.Init(animContext.SpriteRenderer);
            this.clip = clip;
        }

        public virtual void Enter()
        {
            animator.Play(clip).Forget();
        }

        public virtual void Exit()
        {
        }
    }
}