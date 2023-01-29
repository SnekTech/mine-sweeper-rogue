using System;
using SnekTech.C;
using SnekTech.GamePlay.InventorySystem;
using SnekTech.Grid;
using UnityEngine;

namespace SnekTech.UI
{
    [CreateAssetMenu(fileName = nameof(UIEventChannel), menuName = MenuName.EventChannels + "/" + nameof(UIEventChannel))]
    public class UIEventChannel : ScriptableObject
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
