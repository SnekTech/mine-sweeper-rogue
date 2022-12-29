using TMPro;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SnekTech.UI
{
    [RequireComponent(typeof(TMP_Text))]
    public class PixelText : MonoBehaviour
    {
#if UNITY_EDITOR
        [MenuItem("GameObject/UI/PixelText")]
        public static void AddPixelText()
        {
            Instantiate(Resources.Load<GameObject>("UI/PixelText"),
                Selection.activeGameObject.transform, false);
        }    
#endif
        
        
        [SerializeField]
        private TMP_Text text;
        
        [SerializeField]
        [Range(1, 5)]
        private int scale = 1;

        private const int BaseFontSize = 12;
        

        private void Awake()
        {
            text = GetComponent<TMP_Text>();
        }

        private void OnValidate()
        {
            text.fontSize = BaseFontSize * scale;
        }
    }
}