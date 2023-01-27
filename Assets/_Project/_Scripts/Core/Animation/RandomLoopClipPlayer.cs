using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = System.Random;

namespace SnekTech.Core.Animation
{
    [CreateAssetMenu(menuName = C.MenuName.Animation + "/" + nameof(RandomLoopClipPlayer))]
    public class RandomLoopClipPlayer : ClipPlayer
    {
        public override async UniTask PlayAsync(SnekAnimator animator, SnekAnimationClip clip,
            CancellationToken cancellationToken)
        {
            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();
                await UniTask.Delay(TimeSpan.FromSeconds(new Random().Next(10, 20)), cancellationToken: cancellationToken);

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

                animator.FrameIndex %= clip.FrameCount;
            }

            // ReSharper disable once FunctionNeverReturns
        }
    }
}