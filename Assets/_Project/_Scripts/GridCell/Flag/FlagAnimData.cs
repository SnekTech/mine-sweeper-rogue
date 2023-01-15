using SnekTech.Core.Animation;
using UnityEngine;

namespace SnekTech.GridCell.Flag
{
    [CreateAssetMenu(menuName = nameof(ClipData) + "/Flag", fileName = nameof(FlagAnimData))]
    public class FlagAnimData : ScriptableObject
    {
        [ClipDataTargetField]
        [SerializeField]
        private ClipData floating;

        [ClipDataTargetField]
        [SerializeField]
        private ClipData hide;

        [ClipDataTargetField]
        [SerializeField]
        private ClipData lift;

        [ClipDataTargetField]
        [SerializeField]
        private ClipData putDown;

        public ClipData Float => floating;

        public ClipData Hide => hide;

         public ClipData Lift => lift;

          public ClipData PutDown => putDown;
    }
}