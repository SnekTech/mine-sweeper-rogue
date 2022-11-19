using System;
using SnekTech.Constants;
using SnekTech.Grid;
using SnekTech.InventorySystem;
using UnityEngine;

namespace SnekTech.UI
{
    [CreateAssetMenu(fileName = nameof(UIEventManager), menuName = MenuName.EventManager + MenuName.Slash + nameof(UIEventManager))]
    public class UIEventManager : ScriptableObject
    {
        public event Action<GridData> ResetButtonClicked;
        public event Action<ItemData> ItemChosen;

        public void InvokeResetButtonClicked(GridData gridData)
        {
            ResetButtonClicked?.Invoke(gridData);
        }

        public void InvokeItemChosen(ItemData itemData)
        {
            ItemChosen?.Invoke(itemData);
        }
    }
}
