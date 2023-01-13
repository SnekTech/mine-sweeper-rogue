using System;
using UnityEngine;

namespace SnekTech.Core.Animation
{
    public class SpriteClipNonLoop : SpriteClip
    {
        public event Action OnComplete;

        public SpriteClipNonLoop(IAnimationContext context, AnimInfo animInfo) : base(context, animInfo)
        {
        }

        protected override void HandleSpriteChange(SpriteRenderer renderer)
        {
            frameIndex++;
            if (frameIndex == frameCount)
            {
                OnComplete?.Invoke();
            }
        }
    }
}