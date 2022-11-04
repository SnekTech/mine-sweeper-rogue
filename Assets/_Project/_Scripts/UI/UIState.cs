using UnityEngine;

namespace SnekTech.UI
{
    [CreateAssetMenu(fileName = "New UI State", menuName = Utils.MyUIMenuName + "/" + nameof(UIState))]
    public class UIState : ScriptableObject
    {
        public bool isBlockingRaycast;
    }
}
