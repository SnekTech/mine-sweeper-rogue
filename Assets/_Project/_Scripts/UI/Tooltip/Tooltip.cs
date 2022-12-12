using System;
using TMPro;
using UnityEngine;

namespace SnekTech.UI.Tooltip
{
    public class Tooltip : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text headerText;

        [SerializeField]
        private TMP_Text bodyText;

        [SerializeField]
        private InputListener inputListener;

        private RectTransform _rectTransform;

        private Vector2 MousePosition => inputListener.MousePosition;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            SetActive(false);
        }

        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }

        public void SetContent(TooltipContent tooltipContent)
        {
            headerText.text = tooltipContent.Header;
            bodyText.text = tooltipContent.Body;
        }

        public void MoveToMouse()
        {
            float pivotX = MousePosition.x / Screen.width;
            float pivotY = MousePosition.y / Screen.height;
            _rectTransform.pivot = new Vector2(pivotX, pivotY);
            
            _rectTransform.anchoredPosition = MousePosition;
            
        }
    }
}
