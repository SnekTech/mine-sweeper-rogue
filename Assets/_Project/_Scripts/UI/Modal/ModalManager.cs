﻿using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using SnekTech.Core;
using SnekTech.GamePlay.InventorySystem;
using UnityEngine;

namespace SnekTech.UI.Modal
{
    [CreateAssetMenu(menuName = C.MenuName.UIManagers + "/" + nameof(ModalManager))]
    public class ModalManager : ScriptableObject, IShouldFinishAfterLevelCompleted
    {
        #region DI
        
        [SerializeField]
        private UIStateManager uiStateManager;

        [SerializeField]
        private UIEventChannel uiEventChannel;
        
        #endregion
        
        #region Animation Config
        
        [Header("Animation Config")]
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
        
        #endregion
        
        private Modal _modal;
        private CanvasGroup _alphaGroup;
        private readonly Queue<UniTask> _showTaskQueue = new Queue<UniTask>();
        private UniTask _currentShowingModalCloseTask; // complete when closed

        #region shortcut getters
        
        private bool HasRemainingShowTask => !_showTaskQueue.IsEmpty();
        private bool IsLastShowTaskPending => _currentShowingModalCloseTask.Status == UniTaskStatus.Pending;
        
        #endregion

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
                if (HasRemainingShowTask)
                {
                    await UniTask.WaitUntil(() => !IsLastShowTaskPending);
                    var showTask = _showTaskQueue.Dequeue();

                    await showTask;
                }

                await UniTask.NextFrame();
            }
            // ReSharper disable once FunctionNeverReturns
        }

        public UniTask FinishAsync()
        {
            return UniTask.WaitUntil(() => !HasRemainingShowTask && !IsLastShowTaskPending);
        }

        #region Show Modal Functions

        private async UniTask ShowChooseItemPanelTask(UniTaskCompletionSource closeTaskCompletionSource, Action actionAfterModalHide)
        {
            _modal.ChangeToChooseItemPanel();
            await Show();

            void HandleOnChooseItem(ItemData itemData) => HandleOnChooseItemAsync().Forget();

            async UniTaskVoid HandleOnChooseItemAsync()
            {
                await Hide();
                actionAfterModalHide();
                uiEventChannel.OnChooseItem -= HandleOnChooseItem;
                closeTaskCompletionSource.TrySetResult();
            }

            uiEventChannel.OnChooseItem += HandleOnChooseItem;
        }

        private async UniTask ShowConfirmTask(UniTaskCompletionSource closeTaskCompletionSource, string header, Sprite image, string annotationText) 
        {
            _modal.ChangeToConfirm(header, image, annotationText);
            await Show();

            void HandleOnModalOk() => HandleOnModalOkAsync().Forget();

            async UniTaskVoid HandleOnModalOkAsync()
            {
                await Hide();
                uiEventChannel.OnModalOk -= HandleOnModalOk;
                closeTaskCompletionSource.TrySetResult();
            }

            uiEventChannel.OnModalOk += HandleOnModalOk;
        }

        public UniTask ShowConfirmAsync(string header, Sprite image, string annotationText) =>
            AppendShowTask(completionSource => ShowConfirmTask(completionSource, header, image, annotationText));

        public UniTask ShowChooseItemPanelAsync(Action actionAfterModalHide) => 
            AppendShowTask(completionSource => ShowChooseItemPanelTask(completionSource, actionAfterModalHide));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskGenerator"></param>
        /// <returns>current showing modal close task</returns>
        private UniTask AppendShowTask(Func<UniTaskCompletionSource, UniTask> taskGenerator)
        {
            var closeTaskCompletionSource = new UniTaskCompletionSource();
            _showTaskQueue.Enqueue(taskGenerator(closeTaskCompletionSource));

            _currentShowingModalCloseTask = closeTaskCompletionSource.Task;
            return _currentShowingModalCloseTask;
        }
        
        #endregion

        #region Show & Hide animation
        
        private async UniTask Show()
        {
            uiStateManager.isBlockingRaycast = true;
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

            uiStateManager.isBlockingRaycast = false;
        }
        
        #endregion
    }
}