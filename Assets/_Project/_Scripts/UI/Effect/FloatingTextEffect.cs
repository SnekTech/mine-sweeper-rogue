using TMPro;
using UnityEngine;

namespace SnekTech.UI.Effect
{
    public class FloatingTextEffect : MonoBehaviour
    {
        private TMP_Text _text;
        private RectTransform _rectTransform;

        private void Awake()
        {
            _text = GetComponentInChildren<TMP_Text>();
        }

        public void SetText<T>(T textObj, Color color)
        {
            SetText(textObj);
            _text.color = color;
        }

        public void SetText<T>(T textObj)
        {
            _text.SetText(textObj.ToString());
        }
    }
}
