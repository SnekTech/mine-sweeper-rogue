﻿using UnityEngine;

namespace SnekTech.Core.Animation
{
    [CreateAssetMenu(menuName = "AnimationData/" + nameof(ClipData))]
    public class ClipData : ScriptableObject
    {
        [SerializeField]
        private int frameCount;

        [field: SerializeField]
        public string ClipName { get; set; }
        
        [field: SerializeField]
        public int Hash { get; set; }

        
        public int FrameCount
        {
            get => frameCount; 
            
            // frame count is at least 1
            // empty animation should be presented as spriteRenderer.sprite == null
            set => frameCount = Mathf.Max(value, 1);
        }
    }
}