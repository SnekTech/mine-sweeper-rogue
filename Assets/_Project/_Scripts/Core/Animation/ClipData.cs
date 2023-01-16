using UnityEngine;

namespace SnekTech.Core.Animation
{
    [CreateAssetMenu(menuName = C.MenuName.Animation + "/" + nameof(ClipData), fileName = nameof(ClipData))]
    public class ClipData : ScriptableObject
    {
        [SerializeField]
        private int frameCount;

        [field: SerializeField]
        public string ClipName { get; set; }
        
        [field: SerializeField]
        public int NameHash { get; set; }

        
        public int FrameCount
        {
            get => frameCount; 
            
            // frame count is at least 1
            // empty animation should be presented as spriteRenderer.sprite == null
            set => frameCount = Mathf.Max(value, 1);
        }
    }
}