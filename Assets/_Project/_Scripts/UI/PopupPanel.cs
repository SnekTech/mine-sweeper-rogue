using System;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

namespace SnekTech.UI
{
    public class PopupPanel : MonoBehaviour
    {
        [Header("DI")]
        [SerializeField]
        private InputEventManager inputEventManager;
        
        [SerializeField]
        private RectTransform frameRectTransform;
        
        [Header("Animation Options")]
        [SerializeField]
        private float duration = 1f;

        [SerializeField]
        private Ease showEase;

        [SerializeField]
        private Ease hideEase;

        
        private CanvasGroup _canvasGroup;

        private bool _bVisible;
        private bool _bIsTweening;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        private void OnEnable()
        {
            inputEventManager.PausePerformed += Toggle;
        }

        private void OnDisable()
        {
            inputEventManager.PausePerformed -= Toggle;
        }

        private async void Toggle()
        {
            if (_bIsTweening)
            {
                return;
            }

            _bIsTweening = true;
            if (!_bVisible)
            {
                frameRectTransform.gameObject.SetActive(true);
                await Show();
            }
            else
            {
                await Hide();
                frameRectTransform.gameObject.SetActive(false);
            }

            _bIsTweening = false;
            _bVisible = !_bVisible;
        }

        private Task Show()
        {
            _canvasGroup.alpha = 0;
            transform.localScale = Vector3.zero;
            Task scaleUp = transform.DOScale(Vector3.one, duration)
                .SetEase(showEase)
                .AsyncWaitForCompletion();
            Task fadeIn = _canvasGroup.DOFade(1, duration).AsyncWaitForCompletion();
            return Task.WhenAll(scaleUp, fadeIn);
        }

        private Task Hide()
        {
            Task scaleDown = transform.DOScale(Vector3.zero, duration)
                .SetEase(hideEase)
                .AsyncWaitForCompletion();
            Task fadeOut = _canvasGroup.DOFade(0, duration).AsyncWaitForCompletion();
            return Task.WhenAll(scaleDown, fadeOut);
        }
    }
}
