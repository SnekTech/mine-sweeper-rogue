using UnityEngine;

namespace SnekTech.UI
{
    public static class ExtensionMethodUI
    {
        public static void LocalMoveY(this RectTransform rectTransform, float localY)
        {
            Vector3 newLocalPosition = rectTransform.localPosition;
            newLocalPosition.y = localY;
            rectTransform.localPosition = newLocalPosition;
        }
    }
}
