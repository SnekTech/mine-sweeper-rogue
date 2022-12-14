using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace SnekTech.UI.Effect
{
    public class EffectEmitter : MonoBehaviour
    {
        [Header("Floating Text")]
        [SerializeField]
        private FloatingTextEffect floatingTextEffectPrefab;

        [SerializeField]
        private float floatDistance = 10;

        [SerializeField]
        private float duration = 1f;

        [SerializeField]
        private Ease ease = Ease.OutQuint;
        
        
        public async UniTask PerformFloatingTextAsync<T>(T textObj, RectTransform target)
        {
            FloatingTextEffect floatingTextEffect = Instantiate(floatingTextEffectPrefab);
            floatingTextEffect.SetText(textObj);
            var effectTransform = floatingTextEffect.GetComponent<RectTransform>();
            effectTransform.MoveToTopRightOf(target);

            await effectTransform.DOAnchorPosY(effectTransform.anchoredPosition.y + floatDistance, duration)
                .SetEase(ease);

            if (floatingTextEffect != null)
            {
                Destroy(floatingTextEffect.gameObject);
            }
        }
    }
}
