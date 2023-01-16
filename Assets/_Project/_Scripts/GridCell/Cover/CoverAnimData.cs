using SnekTech.C;
using SnekTech.Core.Animation.CustomAnimator;
using SnekTech.Core.CustomAttributes;
using UnityEngine;

namespace SnekTech.GridCell.Cover
{
    [ClipDataHolder]
    [CreateAssetMenu(menuName = MenuName.ClipDataHolder + "/" + nameof(CoverAnimData), fileName = nameof(CoverAnimData))]
    public class CoverAnimData : ScriptableObject
    {
        [ClipDataTargetField]
        [SerializeField]
        private SnekAnimationClip coveredIdle;

        [ClipDataTargetField]
        [SerializeField]
        private SnekAnimationClip putCover;

        [ClipDataTargetField]
        [SerializeField]
        private SnekAnimationClip reveal;

        [ClipDataTargetField]
        [SerializeField]
        private SnekAnimationClip revealedIdle;

        public SnekAnimationClip CoveredIdle => coveredIdle;

        public SnekAnimationClip PutCover => putCover;

        public SnekAnimationClip Reveal => reveal;

        public SnekAnimationClip RevealedIdle => revealedIdle;
    }
}