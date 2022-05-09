using UnityEngine;
using TMPro;

namespace SnekTech.UI
{
    public class LabelController : MonoBehaviour
    {
        private TMP_Text _text;

        private void Awake()
        {
            _text = GetComponentInChildren<TMP_Text>();
        }

        public string LabelText
        {
            get => _text.text;
            set => _text.text = value;
        }
    }
}
