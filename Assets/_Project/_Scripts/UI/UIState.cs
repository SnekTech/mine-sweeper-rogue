using UnityEngine;
using SnekTech.Constants;

namespace SnekTech.UI
{
    [CreateAssetMenu(fileName = "New UI State", menuName = MenuName.UI + MenuName.Slash + nameof(UIState))]
    public class UIState : ScriptableObject
    {
        // todo: figure out UI & game scene ray casting
        public bool isBlockingRaycast;
    }
}
