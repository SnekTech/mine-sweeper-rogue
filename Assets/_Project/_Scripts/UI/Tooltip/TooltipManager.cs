using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace SnekTech.UI.Tooltip
{
    [CreateAssetMenu(fileName = nameof(TooltipManager))]
    public class TooltipManager : ScriptableObject
    {
        [SerializeField]
        private float duration = 0.2f;

        [SerializeField]
        private Ease easeShow = Ease.OutQuint;
        
        private Tooltip _tooltip;
        private RectTransform _tooltipRect;

        public void Init(Tooltip tooltip)
        {
            _tooltip = tooltip;
            _tooltipRect = tooltip.Rect;
        }

        public async UniTaskVoid ShowAsync(TooltipContent tooltipContent)
        {
            
            _tooltip.SetContent(tooltipContent);
            _tooltip.MoveToMouse();
            _tooltip.SetActive(true);
            _tooltipRect.localScale = Vector3.zero;

            await _tooltipRect.DOScale(Vector3.one, duration).SetEase(easeShow);
        }

        public void Hide()
        {
            _tooltip.SetActive(false);
        }
    }
}