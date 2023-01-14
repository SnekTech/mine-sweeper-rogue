using UnityEngine;

namespace SnekTech.Core.Animation
{
    public class SpriteClipLoop : SpriteClip
    {
        public SpriteClipLoop(ICanAnimate context, ClipData clipData) : base(context, clipData)
        {
        }

        protected override void HandleSpriteChange(SpriteRenderer renderer)
        {
            if (renderer.sprite == null)
            {
                return;
            }

            frameIndex++;
            frameIndex %= frameCount;
            if (frameIndex == 0)
            {
                // Debug.Log("last loop complete");
            }
        }
    }
}