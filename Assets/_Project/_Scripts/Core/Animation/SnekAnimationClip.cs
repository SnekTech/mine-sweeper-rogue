using System.Collections.Generic;
using UnityEngine;

namespace SnekTech.Core.Animation
{
    [CreateAssetMenu(menuName = C.MenuName.SnekAnimator + "/" + nameof(SnekAnimationClip))]
    public class SnekAnimationClip : ScriptableObject
    {
        [SerializeField]
        private List<Sprite> sprites;

        [HideInInspector]
        [SerializeField]
        private List<float> frameDurations;

        [SerializeField]
        private bool isLooping;

        public int FrameCount => frameDurations.Count;
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