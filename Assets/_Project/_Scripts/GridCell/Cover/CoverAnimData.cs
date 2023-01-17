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

        [SerializeField]
        private int coveredIdleIndex;

        [SerializeField]
        private int revealIndex;

        [SerializeField]
        private int revealedIdleIndex;

        [SerializeField]
        private int putCoverIndex;


        public SnekAnimationClip CoveredIdle => clips[coveredIdleIndex];
        public SnekAnimationClip Reveal => clips[revealIndex];
        public SnekAnimationClip RevealedIdle => clips[revealedIdleIndex];
        public SnekAnimationClip PutCover => clips[putCoverIndex];
    }
}