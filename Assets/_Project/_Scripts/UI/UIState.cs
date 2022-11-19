﻿using UnityEngine;
using SnekTech.Constants;

namespace SnekTech.UI
{
    [CreateAssetMenu(fileName = "New UI State", menuName = MenuName.UI + MenuName.Slash + nameof(UIState))]
    public class UIState : ScriptableObject
    {
        public bool isBlockingRaycast;
    }
}
