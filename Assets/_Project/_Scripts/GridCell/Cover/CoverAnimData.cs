using SnekTech.Core.Animation;
using SnekTech.Core.CustomAttributes;
using UnityEngine;

namespace SnekTech.GridCell.Cover
{
    [ClipDataHolder]
    [CreateAssetMenu(menuName = C.MenuName.ClipDataHolder + "/" + nameof(CoverAnimData), fileName = nameof(CoverAnimData))]
    public class CoverAnimData : ScriptableObject
    {
        [ClipDataTargetField]
        [SerializeField]
        private ClipData coveredIdle;

        [ClipDataTargetField]
        [SerializeField]
        private ClipData putCover;

        [ClipDataTargetField]
        [SerializeField]
        private ClipData reveal;

        [ClipDataTargetField]
        [SerializeField]
        private ClipData revealedIdle;

        public ClipData CoveredIdle => coveredIdle;

        public ClipData PutCover => putCover;

        public ClipData Reveal => reveal;

        public ClipData RevealedIdle => revealedIdle;
    }
}