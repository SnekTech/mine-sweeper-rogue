using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SnekTech.UI.Modal
{
    public class ConfirmBody : MonoBehaviour
    {
        [SerializeField]
        private Image image;

        [SerializeField]
        private TMP_Text annotationText;

        public Image Image => image;
        public TMP_Text AnnotationText => annotationText;
    }
}
