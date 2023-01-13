using UnityEngine;

namespace SnekTech.Core.Animation
{

    public interface IAnimationContext : ICanAnimate, ICanSwitchActiveness
    {
    }

    public struct AnimInfo
    {
        public readonly int Hash;
        public readonly int FrameCount;

        public AnimInfo(int hash, int frameCount)
        {
            Hash = hash;
            FrameCount = frameCount;
        }
    }

    public abstract class SpriteClip
    {
        private const string EmptyAnimName = "";
        public static int EmptyAnimHash { get; } = Animator.StringToHash(EmptyAnimName);

        public int CurrentFrameIndex => frameIndex;

        protected readonly Animator animator;
        protected readonly SpriteRenderer spriteRenderer;
        private readonly int _animHash;

        protected readonly int frameCount;
        protected int frameIndex;

        private readonly IAnimationContext _context;

        protected SpriteClip(IAnimationContext context, AnimInfo animInfo)
        {
            animator = context.Animator;
            spriteRenderer = context.SpriteRenderer;

            _context = context;
            _animHash = animInfo.Hash;
            frameCount = animInfo.FrameCount;
        }

        public void Play()
        {
            if (!_context.IsActive)
            {
                return;
            }

            if (_animHash == EmptyAnimHash)
            {
                spriteRenderer.sprite = null;
                return;
            }

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