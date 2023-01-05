using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using SnekTech.InventorySystem;
using UnityEngine;

namespace SnekTech.UI.Modal
{
    [CreateAssetMenu]
    public class ModalManager : ScriptableObject
    {
        [SerializeField]
        private UIState uiState;

        [SerializeField]
        private UIEventManager uiEventManager;
        
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
        private readonly Queue<UniTask> _showTaskQueue = new Queue<UniTask>();

        public void Init(Modal modal)
        {
            _modal = modal;
            _alphaGroup = _modal.BackgroundGroup;
            _showTaskQueue.Clear();

            StartWatchingShowTaskQueue().Forget();
        }

        private async UniTaskVoid StartWatchingShowTaskQueue()
        {
            while (true)
            {
                if (_showTaskQueue.Count > 0)
                {
                    UniTask showTask = _showTaskQueue.Dequeue();
                    await showTask;
                }

                await UniTask.NextFrame();
            }
            // ReSharper disable once FunctionNeverReturns
        }

        private async UniTask ShowChooseItemPanelTask(UniTaskCompletionSource closeTaskCompletionSource, Action actionAfterModalHide)
        {
            _modal.ChangeToChooseItemPanel();
            await Show();


            async void OnItemChosen(ItemData itemData)
            {
                await Hide();
                actionAfterModalHide();
                uiEventManager.ItemChosen -= OnItemChosen;
                closeTaskCompletionSource.TrySetResult();
            }

            uiEventManager.ItemChosen += OnItemChosen;
        }

        private async UniTask ShowConfirmTask(UniTaskCompletionSource closeTaskCompletionSource, string header, Sprite image, string annotationText) 
        {
            _modal.ChangeToConfirm(header, image, annotationText);
            await Show();

            async void OnModalOk()
            {
                await Hide();
                uiEventManager.ModalOk -= OnModalOk;
                closeTaskCompletionSource.TrySetResult();
            }

            uiEventManager.ModalOk += OnModalOk;
        }

        public UniTask ShowConfirmAsync(string header, Sprite image, string annotationText)
        {
            var closeTaskCompletionSource = new UniTaskCompletionSource();
            _showTaskQueue.Enqueue(ShowConfirmTask(closeTaskCompletionSource, header, image, annotationText));
            
            return closeTaskCompletionSource.Task;
        }

        public UniTask ShowChooseItemPanelAsync(Action actionAfterModalHide)
        {
            var closeTaskCompletionSource = new UniTaskCompletionSource();
            _showTaskQueue.Enqueue(ShowChooseItemPanelTask(closeTaskCompletionSource, actionAfterModalHide));
            return closeTaskCompletionSource.Task;
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

        private async UniTask Hide()
        {
            await UniTask.WhenAll(
                _alphaGroup.DOFade(0, duration).SetEase(easeHide).ToUniTask(), 
                _modal.ParentRect.DOAnchorPosY(CanvasInfo.Instance.ReferenceHeight, duration).SetEase(easeHide).ToUniTask()
            );

            uiState.isBlockingRaycast = false;
        }
    }
}