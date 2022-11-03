using System;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

namespace SnekTech.UI
{
    public class PopupPanel : MonoBehaviour
    {
        [SerializeField]
        private InputEventManager inputEventManager;
        
        [SerializeField]
        private float duration = 1f;

        [SerializeField]
        private RectTransform frameRectTransform;
        
        private CanvasGroup _canvasGroup;

        private bool _bVisible;
        private bool _bIsTweening;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();

            frameRectTransform.LocalMoveY(Screen.height);
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
            Task moveToScreen = frameRectTransform.DOLocalMoveY(0, duration).AsyncWaitForCompletion();
            Task fadeIn = _canvasGroup.DOFade(1, duration).AsyncWaitForCompletion();
            return Task.WhenAll(moveToScreen, fadeIn);
        }

        private Task Hide()
        {
            Task moveOffScreen = frameRectTransform.DOLocalMoveY(Screen.height, duration).AsyncWaitForCompletion();
            Task fadeOut = _canvasGroup.DOFade(0, duration).AsyncWaitForCompletion();
            return Task.WhenAll(moveOffScreen, fadeOut);
        }
    }
}
