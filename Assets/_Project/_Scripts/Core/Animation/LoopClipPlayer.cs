using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace SnekTech.Core.Animation
{
    [CreateAssetMenu(menuName = C.MenuName.Animation + "/" + nameof(LoopClipPlayer))]
    public class LoopClipPlayer : ClipPlayer
    {
        public override async UniTask PlayAsync(SnekAnimator animator, SnekAnimationClip clip, CancellationToken cancellationToken)
        {
            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();
                int frameIndex = animator.FrameIndex;
                
                animator.UpdateSprite(clip.Sprites[frameIndex]);
                float frameDelay = clip.FrameDurations[frameIndex] / clip.SpeedFactor;

                await UniTask.Delay(TimeSpan.FromMilliseconds(frameDelay), cancellationToken: cancellationToken);

                int nextFrameIndex = (frameIndex + 1) % clip.FrameCount;
                animator.FrameIndex = nextFrameIndex;
            }

            // ReSharper disable once FunctionNeverReturns
        }
    }
}