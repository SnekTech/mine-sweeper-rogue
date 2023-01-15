using UnityEngine;
using SnekTech.C;

namespace SnekTech.UI
{
    [CreateAssetMenu(fileName = "New UI State", menuName = MenuName.UI + MenuName.Slash + nameof(UIState))]
    public class UIState : ScriptableObject
    {
        public bool isBlockingRaycast;

        private void OnEnable()
        {
            isBlockingRaycast = false;
        }
    }
}
