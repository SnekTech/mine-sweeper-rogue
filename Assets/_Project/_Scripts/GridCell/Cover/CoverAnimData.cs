using System.Collections.Generic;
using SnekTech.C;
using SnekTech.Core.Animation;
using SnekTech.Core.CustomAttributes;
using UnityEngine;

namespace SnekTech.GridCell.Cover
{
    [SnekClipHolder]
    [CreateAssetMenu(menuName = MenuName.ClipDataHolder + "/" + nameof(CoverAnimData),
        fileName = nameof(CoverAnimData))]
    public class CoverAnimData : ScriptableObject
    {
        [SnekClipsField]
        [SerializeField]
        private List<SnekAnimationClip> clips;

        [Min(0)]
        [SerializeField]
        private int coveredIdleIndex = 0;

        [Min(0)]
        [SerializeField]
        private int revealIndex = 1;

        [Min(0)]
        [SerializeField]
        private int revealedIdleIndex = 2;

        [Min(0)]
        [SerializeField]
        private int putCoverIndex = 3;


        public SnekAnimationClip CoveredIdle => clips[coveredIdleIndex];
        public SnekAnimationClip Reveal => clips[revealIndex];
        public SnekAnimationClip RevealedIdle => clips[revealedIdleIndex];
        public SnekAnimationClip PutCover => clips[putCoverIndex];
    }
}