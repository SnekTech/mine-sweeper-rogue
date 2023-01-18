using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace SnekTech.Core.Animation
{
    public class SnekAnimator : MonoBehaviour
    {
        public event Action OnClipComplete;

        private SpriteRenderer _renderer;
        private int _frameIndex;
        private SnekAnimationClip _currentClip;
        private bool _shouldLoop;

        public void Init(SpriteRenderer spriteRenderer) => _renderer = spriteRenderer;

        public void Play(SnekAnimationClip clip)
        {
            if (clip == _currentClip) return;

            _frameIndex = 0;
            _shouldLoop = false;

            if (clip.IsLooping)
            {
                BeginPlayLoop(clip).Forget();
            }
            else
            {
                BeginPlayNonLoop(clip).Forget();
            }
        }

        private void UpdateSprite(Sprite nextSprite)
        {
            if (_renderer != null)
            {
                // SpriteRenderer maybe destroyed along with its containing GameObject before
                // this animator component goes out of scope
                // so we need to check null
                _renderer.sprite = nextSprite;
            }
        }

        private async UniTaskVoid BeginPlayLoop(SnekAnimationClip clip)
        {
            _shouldLoop = true;

            while (_shouldLoop)
            {
                if (_frameIndex == 0)
                {
                    OnClipComplete?.Invoke();
                }
                
                UpdateSprite(clip.Sprites[_frameIndex]);
                float frameDelay = clip.FrameDurations[_frameIndex] / clip.SpeedFactor;
                
                await UniTask.Delay(TimeSpan.FromMilliseconds(frameDelay));
                
                _frameIndex++;
                _frameIndex %= clip.FrameCount;
            }
        }

        private async UniTaskVoid BeginPlayNonLoop(SnekAnimationClip clip)
        {
            while (_frameIndex < clip.FrameCount)
            {
                UpdateSprite(clip.Sprites[_frameIndex]);
                await UniTask.Delay(TimeSpan.FromMilliseconds(clip.FrameDurations[_frameIndex] / clip.SpeedFactor));

                _frameIndex++;
            }

            OnClipComplete?.Invoke();
        }
    }
}