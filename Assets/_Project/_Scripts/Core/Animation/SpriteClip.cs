using UnityEngine;

namespace SnekTech.Core.Animation
{
    public abstract class SpriteClip
    {
        public int CurrentFrameIndex => frameIndex;

        protected readonly Animator animator;
        protected readonly SpriteRenderer spriteRenderer;
        private readonly int _animHash;

        protected readonly int frameCount;
        protected int frameIndex;

        protected SpriteClip(ICanAnimate context, ClipData clipData)
        {
            animator = context.Animator;
            spriteRenderer = context.SpriteRenderer;

            _animHash = clipData.NameHash;
            frameCount = clipData.FrameCount;
        }

        public void Play()
        {
            animator.enabled = true;

            int currentPlayingAnimHash = animator.GetCurrentAnimatorStateInfo(0).shortNameHash;
            if (_animHash == currentPlayingAnimHash)
            {
                return;
            }

            animator.Play(_animHash);
        }

        public void RegisterSpriteChange()
        {
            spriteRenderer.RegisterSpriteChangeCallback(HandleSpriteChange);
        }

        public void UnregisterSpriteChange()
        {
            spriteRenderer.UnregisterSpriteChangeCallback(HandleSpriteChange);
        }

        protected abstract void HandleSpriteChange(SpriteRenderer renderer);
    }
}