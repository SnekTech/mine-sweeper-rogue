using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

namespace SnekTech.UI
{
    public class PopupPanel : MonoBehaviour
    {
        // todo: move the popup stuff to root scene
        [Header("DI")]
        [SerializeField]
        private InputEventManager inputEventManager;

        [SerializeField]
        private UIState uiState;
        
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
            
            Init();
        }

        private void OnEnable()
        {
            inputEventManager.PausePerformed += Toggle;
        }

        private void OnDisable()
        {
            inputEventManager.PausePerformed -= Toggle;
        }

        private void Init()
        {
            _canvasGroup.alpha = 0;
            transform.localScale = Vector3.zero;
            uiState.isBlockingRaycast = false;
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
                await Show();
            }
            else
            {
                await Hide();
            }

            _bIsTweening = false;
            _bVisible = !_bVisible;
        }

        private Task Show()
        {
            frameRectTransform.gameObject.SetActive(true);
            uiState.isBlockingRaycast = true;
            
            Task scaleUp = transform.DOScale(Vector3.one, duration)
                .SetEase(showEase)
                .AsyncWaitForCompletion();
            Task fadeIn = _canvasGroup.DOFade(1, duration).AsyncWaitForCompletion();
            return Task.WhenAll(scaleUp, fadeIn);
        }

        private async Task Hide()
        {
            Task scaleDown = transform.DOScale(Vector3.zero, duration)
                .SetEase(hideEase)
                .AsyncWaitForCompletion();
            Task fadeOut = _canvasGroup.DOFade(0, duration).AsyncWaitForCompletion();
            await Task.WhenAll(scaleDown, fadeOut);
            
            frameRectTransform.gameObject.SetActive(false);
            uiState.isBlockingRaycast = false;
        }
    }
}
