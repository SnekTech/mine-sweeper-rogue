using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace SnekTech.UI.Modal
{
    [CreateAssetMenu]
    public class ModalManager : ScriptableObject
    {
        [SerializeField]
        private UIState uiState;
        [SerializeField]
        [Range(0, 1)]
        private float targetBackgroundAlpha = 0.6f;

        [SerializeField]
        [Min(0)]
        private float duration = 0.3f;

        [SerializeField]
        private Ease easeShow = Ease.OutQuint;

        [SerializeField]
        private Ease easeHide = Ease.InQuint;
        
        private Modal _modal;
        private CanvasGroup _alphaGroup;

        public void Init(Modal modal)
        {
            _modal = modal;
            _alphaGroup = _modal.BackgroundGroup;
        }

        public async UniTask ShowChooseItemPanel()
        {
            _modal.ChangeToChooseItemPanel();
            await Show();
        }

        public async UniTask ShowConfirm(string header, Sprite image, string annotationText)
        {
            _modal.ChangeToConfirm(header, image, annotationText);
            await Show();
        }

        private async UniTask Show()
        {
            uiState.isBlockingRaycast = true;
            _alphaGroup.alpha = 0;

            await UniTask.WhenAll(
                _alphaGroup.DOFade(targetBackgroundAlpha, duration).SetEase(easeShow).ToUniTask(), 
                _modal.ParentRect.DOAnchorPosY(0, duration).SetEase(easeShow).ToUniTask()
            );
        }

        public async UniTask Hide()
        {
            await UniTask.WhenAll(
                _alphaGroup.DOFade(0, duration).SetEase(easeHide).ToUniTask(), 
                _modal.ParentRect.DOAnchorPosY(CanvasInfo.Instance.ReferenceHeight, duration).SetEase(easeHide).ToUniTask()
            );

            uiState.isBlockingRaycast = false;
        }
    }
}