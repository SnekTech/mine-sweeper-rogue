using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace SnekTech.Core.Animation
{
    public class SnekAnimator : MonoBehaviour
    {
        public event Action OnClipComplete;

        private SpriteRenderer Renderer { get; set; }
        public int FrameIndex { get; set; }
        private SnekAnimationClip _currentClip;

        private CancellationTokenSource tokenSource;

        public void Init(SpriteRenderer spriteRenderer)
        {
            Renderer = spriteRenderer;
        }

        public async UniTaskVoid Play(SnekAnimationClip clip)
        {
            if (clip == _currentClip) return;
            _currentClip = clip;
            FrameIndex = 0;

            if (tokenSource != null)
            {
                tokenSource.Cancel();
                tokenSource.Dispose();
            }
            tokenSource = new CancellationTokenSource();

            await clip.ClipPlayer.PlayAsync(this, clip, tokenSource.Token);
        }

        public void UpdateSprite(Sprite nextSprite)
        {
            if (Renderer != null)
            {
                // SpriteRenderer maybe destroyed along with its containing GameObject before
                // this animator component goes out of scope
                // so we need to check null
                Renderer.sprite = nextSprite;
            }
        }

        public void InvokeClipComplete()
        {
            OnClipComplete?.Invoke();
        }

        private void OnDestroy()
        {
            if (tokenSource == null) return;
            
            tokenSource.Cancel();
            tokenSource.Dispose();
        }
    }
}