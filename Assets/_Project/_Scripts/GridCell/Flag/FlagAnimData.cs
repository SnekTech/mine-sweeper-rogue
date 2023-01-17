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

        [SerializeField]
        private int floatClipIndex;
        
        [SerializeField]
        private int hideClipIndex;
        
        [SerializeField]
        private int liftClipIndex;
        
        [SerializeField]
        private int putDownClipIndex;
        
        
        public SnekAnimationClip Float => clips[floatClipIndex];
        public SnekAnimationClip Hide => clips[hideClipIndex];
         public SnekAnimationClip Lift => clips[liftClipIndex];
          public SnekAnimationClip PutDown => clips[putDownClipIndex];
    }
}