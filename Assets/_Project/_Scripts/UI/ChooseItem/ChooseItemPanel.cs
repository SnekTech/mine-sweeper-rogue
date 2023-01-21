using SnekTech.GamePlay.InventorySystem;
using SnekTech.GamePlay.PlayerSystem;
using UnityEngine;

namespace SnekTech.UI.ChooseItem
{
    public class ChooseItemPanel : MonoBehaviour
    {
        [SerializeField]
        private UIEventManager uiEventManager;

        [SerializeField]
        private PlayerHolder playerHolder;
        
        [SerializeField]
        private ItemPool itemPool;
        
        [SerializeField]
        private ItemButton itemButtonPrefab;

        private Player Player => playerHolder.Player;

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
            Player.Inventory.AddItem(item);
        }

        public void GenerateItemButtons()
        {
            transform.DestroyAllChildren();
            for (int i = 0; i < Player.ItemChoiceCount; i++)
            {
                var button = Instantiate(itemButtonPrefab, transform);
                button.SetItem(itemPool.GetRandom());
            }
        }
    }
}