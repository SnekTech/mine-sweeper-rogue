using System.Collections.Generic;
using SnekTech.C;
using SnekTech.Core.Animation;
using SnekTech.Core.CustomAttributes;
using UnityEngine;

namespace SnekTech.GridCell.Flag
{
    [SnekClipHolder]
    [CreateAssetMenu(menuName = MenuName.ClipDataHolder + "/" + nameof(FlagAnimData), fileName = nameof(FlagAnimData))]
    public class FlagAnimData : ScriptableObject
    {
        [SnekClipsField]
        [SerializeField]
        private List<SnekAnimationClip> clips;

        [Min(0)]
        [SerializeField]
        private int floatClipIndex = 0;

        [Min(0)]
        [SerializeField]
        private int hideClipIndex = 1;

        [Min(0)]
        [SerializeField]
        private int liftClipIndex = 2;

        [Min(0)]
        [SerializeField]
        private int putDownClipIndex = 3;


        public SnekAnimationClip Float => clips[floatClipIndex];
        public SnekAnimationClip Hide => clips[hideClipIndex];
        public SnekAnimationClip Lift => clips[liftClipIndex];
        public SnekAnimationClip PutDown => clips[putDownClipIndex];
    }
}