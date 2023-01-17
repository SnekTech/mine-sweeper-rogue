using System.Collections.Generic;
using UnityEngine;

namespace SnekTech.Core.Animation
{
    [CreateAssetMenu(menuName = C.MenuName.SnekAnimator + "/" + nameof(SnekAnimationClip))]
    public class SnekAnimationClip : ScriptableObject
    {
        [SerializeField]
        private List<Sprite> sprites;

        [SerializeField]
        private List<float> frameDurations;

        [Range(0.1f, 5f)]
        [SerializeField]
        private float speedFactor = 1f;

        [SerializeField]
        private bool isLooping;

        public int FrameCount => sprites.Count;
        public float SpeedFactor => speedFactor;
        public bool IsLooping => isLooping;

        public List<Sprite> Sprites
        {
            get => sprites;
            set => sprites = value;
        }

        public List<float> FrameDurations
        {
            get => frameDurations;
            set => frameDurations = value;
        }
    }
}