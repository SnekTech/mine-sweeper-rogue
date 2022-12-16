using TMPro;
using UnityEngine;

namespace SnekTech.UI.Modal
{
    public class Modal : MonoBehaviour
    {
        [SerializeField]
        private GameObject header;

        [SerializeField]
        private CanvasGroup alphaAlphaGroup;

        public CanvasGroup BackgroundGroup => alphaAlphaGroup;
        public RectTransform ParentRect => transform.parent as RectTransform;

        public void SetContent(ModalContent content)
        {
            header.DetachFromParent();
            transform.DestroyAllChildren();

            header.GetComponentInChildren<TMP_Text>().text = content.HeaderText;
            header.transform.SetParent(transform);

            content.Body.transform.SetParent(transform);
            content.Body.transform.localScale = Vector3.one;
        }
    }
}
