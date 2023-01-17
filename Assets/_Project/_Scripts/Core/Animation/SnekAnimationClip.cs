using System.Collections.Generic;
using UnityEngine;

namespace SnekTech.Core.Animation
{
    [CreateAssetMenu(menuName = C.MenuName.SnekAnimator + "/" + nameof(SnekAnimationClip))]
    public class SnekAnimationClip : ScriptableObject
    {
        [SerializeField]
        private List<Sprite> sprites;

        [Tooltip("Duration for each frame in milliseconds")]
        [Min(0)]
        [SerializeField]
        private float frameDuration = 16;

        [SerializeField]
        private bool isLooping;

        public int FrameCount => sprites.Count;
        public float FrameDuration => frameDuration;
        public bool IsLooping => isLooping;

        public Sprite this[int i] => sprites[i];
    }
}