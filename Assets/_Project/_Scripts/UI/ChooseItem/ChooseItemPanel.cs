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
            uiEventManager.ItemChosen += OnItemChosen;
        }

        private void OnDisable()
        {
            uiEventManager.ItemChosen -= OnItemChosen;
        }

        private void OnItemChosen(ItemData item)
        {
            playerState.Inventory.AddItem(item);
        }

        public void GenerateItemButtons()
        {
            transform.DestroyAllChildren();
            for (int i = 0; i < playerState.ItemChoiceCount; i++)
            {
                ItemButton button = Instantiate(itemButtonPrefab, transform);
                button.SetItem(itemPool.GetRandom());
            }
        }
    }
}