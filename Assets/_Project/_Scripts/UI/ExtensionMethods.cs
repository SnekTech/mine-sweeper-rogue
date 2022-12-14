using UnityEngine;

namespace SnekTech.UI
{
    public static class ExtensionMethods
    {
        public static void SetPosition(this Transform transform, Vector3 worldPosition)
        {
            transform.position = new Vector3(worldPosition.x, worldPosition.y, worldPosition.z);
        }

        public static void SetAnchorAs(this RectTransform rectTransform, RectTransform other)
        {
            rectTransform.anchorMin = other.anchorMin;
            rectTransform.anchorMax = other.anchorMax;
        }
        
        public static Vector2 GetTopRightAnchoredPosition(this RectTransform rectTransform)
        {
            Vector2 anchoredPosition = rectTransform.anchoredPosition;
            Vector2 pivot = rectTransform.pivot;
            Vector2 size = rectTransform.sizeDelta;
            float x0 = anchoredPosition.x;
            float y0 = anchoredPosition.y;
            return new Vector2(x0 + (1 - pivot.x) * size.x, y0 + (1 - pivot.y) * size.y);
        }

        public static void MoveToTopRightOf(this RectTransform rectTransform, RectTransform target)
        {
            rectTransform.SetParent(target.parent);
            rectTransform.localScale = Vector3.one;
            rectTransform.SetAnchorAs(target);
            rectTransform.anchoredPosition = target.GetTopRightAnchoredPosition();
        }
    }
}
