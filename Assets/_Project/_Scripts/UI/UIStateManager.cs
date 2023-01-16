using UnityEngine;
using SnekTech.C;

namespace SnekTech.UI
{
    [CreateAssetMenu(fileName = nameof(UIStateManager), menuName = MenuName.UIManagers+ "/" + nameof(UIStateManager))]
    public class UIStateManager : ScriptableObject
    {
        public bool isBlockingRaycast;

        private void OnEnable()
        {
            isBlockingRaycast = false;
        }
    }
}
