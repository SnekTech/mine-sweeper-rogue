using System;
using UnityEngine;

namespace SnekTech.Core.Animation
{
    public class SpriteClipNonLoop : SpriteClip
    {
        public event Action OnComplete;

        public SpriteClipNonLoop(ICanAnimate context, ClipData clipData) : base(context, clipData)
        {
        }

        protected override void HandleSpriteChange(SpriteRenderer renderer)
        {
            frameIndex++;
            if (frameIndex == frameCount)
            {
                frameIndex = 0; // get ready for the next run
                OnComplete?.Invoke();
            }
        }
    }
}