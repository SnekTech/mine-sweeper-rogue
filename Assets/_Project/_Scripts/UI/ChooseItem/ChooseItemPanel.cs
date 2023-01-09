using SnekTech.InventorySystem;
using SnekTech.Player;
using UnityEngine;

namespace SnekTech.UI.ChooseItem
{
    public class ChooseItemPanel : MonoBehaviour
    {
        [SerializeField]
        private UIEventManager uiEventManager;
        
        [SerializeField]
        private PlayerState playerState;
        
        [SerializeField]
        private ItemPool itemPool;
        
        [SerializeField]
        private ItemButton itemButtonPrefab;


        private void OnEnable()
        {
            uiEventManager.OnChooseItem += HandleOnChooseItem;
        }

        private void OnDisable()
        {
            uiEventManager.OnChooseItem -= HandleOnChooseItem;
        }

        private void HandleOnChooseItem(ItemData item)
        {
            playerState.Inventory.AddItem(item);
        }

        public void GenerateItemButtons()
        {
            transform.DestroyAllChildren();
            for (int i = 0; i < playerState.ItemChoiceCount; i++)
            {
                var button = Instantiate(itemButtonPrefab, transform);
                button.SetItem(itemPool.GetRandom());
            }
        }
    }
}