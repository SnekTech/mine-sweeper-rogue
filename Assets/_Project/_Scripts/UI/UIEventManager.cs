using System;
using SnekTech.Grid;
using UnityEngine;

namespace SnekTech.UI
{
    [CreateAssetMenu(fileName = nameof(UIEventManager), menuName = Utils.MyEventManagerMenuName + nameof(UIEventManager))]
    public class UIEventManager : ScriptableObject
    {
        public event Action<GridData> ResetButtonClicked;

        public void InvokeResetButtonClicked(GridData gridData)
        {
            ResetButtonClicked?.Invoke(gridData);
        }
    }
}
