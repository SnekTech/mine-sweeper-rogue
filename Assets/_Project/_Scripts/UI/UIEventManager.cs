using System;
using SnekTech.C;
using SnekTech.Grid;
using SnekTech.InventorySystem;
using UnityEngine;

namespace SnekTech.UI
{
    [CreateAssetMenu(fileName = nameof(UIEventManager), menuName = MenuName.EventManager + MenuName.Slash + nameof(UIEventManager))]
    public class UIEventManager : ScriptableObject
    {
        public event Action<GridData> ResetButtonClicked;
        public event Action<ItemData> OnChooseItem;
        public event Action OnModalOk;

        public void InvokeResetButtonClicked(GridData gridData)
        {
            ResetButtonClicked?.Invoke(gridData);
        }

        public void InvokeOnChooseItem(ItemData itemData)
        {
            OnChooseItem?.Invoke(itemData);
        }

        public void InvokeOnModalOk()
        {
            OnModalOk?.Invoke();
        }
    }
}
