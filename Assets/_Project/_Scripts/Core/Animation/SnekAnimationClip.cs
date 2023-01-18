using System.Collections.Generic;
using UnityEngine;

namespace SnekTech.Core.Animation
{
    [CreateAssetMenu(menuName = C.MenuName.SnekAnimator + "/" + nameof(SnekAnimationClip))]
    public class SnekAnimationClip : ScriptableObject
    {
        [SerializeField]
        private string animName;
        
        [SerializeField]
        private List<Sprite> sprites;

        [SerializeField]
        private List<int> frameDurations;

        [Range(0.1f, 5f)]
        [SerializeField]
        private float speedFactor = 1f;

        [SerializeField]
        private bool isLooping;

        public string AnimName
        {
            get => animName;
            set => animName = value;
        }
        public int FrameCount => sprites.Count;
        public float SpeedFactor => speedFactor;

        public bool IsLooping
        {
            get => isLooping;
            set => isLooping = value;
        }

        public List<Sprite> Sprites
        {
            get => sprites;
            set => sprites = value;
        }

        public List<int> FrameDurations
        {
            get => frameDurations;
            set => frameDurations = value;
        }
    }
}