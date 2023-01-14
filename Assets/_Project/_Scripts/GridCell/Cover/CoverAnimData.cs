using SnekTech.Core.Animation;
using UnityEngine;

namespace SnekTech.GridCell.Cover
{
    [CreateAssetMenu(menuName = "Cover/" + nameof(CoverAnimData), order = 0)]
    public class CoverAnimData : ScriptableObject
    {
        [field: SerializeField]
        public ClipData CoveredIdle { get; private set; }
        
        [field: SerializeField]
        public ClipData Reveal { get; private set; }
        
        [field: SerializeField]
        public ClipData RevealedIdle { get; private set; }
        
        [field: SerializeField]
        public ClipData PutCover { get; private set; }
    }
}