using SnekTech.Core.Animation.CustomAnimator;
using SnekTech.Core.CustomAttributes;
using UnityEngine;

namespace SnekTech.GridCell.Flag
{
    [ClipDataHolder]
    [CreateAssetMenu(menuName = C.MenuName.ClipDataHolder + "/" + nameof(FlagAnimData), fileName = nameof(FlagAnimData))]
    public class FlagAnimData : ScriptableObject
    {
        [ClipDataTargetField]
        [SerializeField]
        private SnekAnimationClip floating;

        [ClipDataTargetField]
        [SerializeField]
        private SnekAnimationClip hide;

        [ClipDataTargetField]
        [SerializeField]
        private SnekAnimationClip lift;

        [ClipDataTargetField]
        [SerializeField]
        private SnekAnimationClip putDown;

        public SnekAnimationClip Float => floating;

        public SnekAnimationClip Hide => hide;

         public SnekAnimationClip Lift => lift;

          public SnekAnimationClip PutDown => putDown;
    }
}