using SnekTech.Grid;
using UnityEngine;

namespace SnekTech.UI
{
    public class ResetButtonControl : MonoBehaviour
    {
        [SerializeField]
        private UIEventManager uiEventManager;
        [SerializeField]
        private GridData gridData;

        public void OnResetButtonClick()
        {
            uiEventManager.InvokeResetButtonClicked(gridData);
        }
    }
}
