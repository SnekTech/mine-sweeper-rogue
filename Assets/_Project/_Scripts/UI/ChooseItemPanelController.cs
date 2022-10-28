using SnekTech.InventorySystem;
using SnekTech.Player;
using UnityEngine;

namespace SnekTech.UI
{
    public class ChooseItemPanelController : MonoBehaviour
    {
        [SerializeField]
        private UIEventManager uiEventManager;
        
        [SerializeField]
        private PlayerData playerData;
        
        [SerializeField]
        private ItemPool itemPool;
        
        [SerializeField]
        private ItemButtonController itemButtonPrefab;

        [SerializeField]
        private int itemChoiceCount = 3;

        [SerializeField]
        private RectTransform itemButtonParentPanel;

        private void Awake()
        {
            for (int i = 0; i < itemChoiceCount; i++)
            {
                ItemButtonController button = Instantiate(itemButtonPrefab, itemButtonParentPanel);
                button.SetItem(itemPool.GetRandom());
            }
        }

        private void OnEnable()
        {
            uiEventManager.ItemChosen += OnItemChosen;
        }

        private void OnDisable()
        {
            uiEventManager.ItemChosen -= OnItemChosen;
        }

        private void OnItemChosen(ItemData item)
        {
            playerData.Inventory.AddItem(item);
        }
    }
}
