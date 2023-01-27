using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace SnekTech.Core.Animation
{
    [CreateAssetMenu(menuName = C.MenuName.Animation + "/" + nameof(NonLoopClipPlayer))]
    public class NonLoopClipPlayer : ClipPlayer
    {
        public override async UniTask PlayAsync(SnekAnimator animator, SnekAnimationClip clip, CancellationToken cancellationToken)
        {
            while (animator.FrameIndex < clip.FrameCount)
            {
                cancellationToken.ThrowIfCancellationRequested();
                int frameIndex = animator.FrameIndex;
                animator.UpdateSprite(clip.Sprites[frameIndex]);
                
                float frameDelay = clip.FrameDurations[frameIndex] / clip.SpeedFactor;
                await UniTask.Delay(TimeSpan.FromMilliseconds(frameDelay), cancellationToken: cancellationToken);

                int nextFrameIndex = frameIndex + 1;
                animator.FrameIndex = nextFrameIndex;
            }
            
            animator.InvokeClipComplete();
        }
    }
}
