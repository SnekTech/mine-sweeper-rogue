using System;
using System.Collections.Generic;
using SnekTech.InventorySystem;
using SnekTech.Player;
using UnityEngine;

namespace SnekTech.UI
{
    public class ChooseItemPanelController : MonoBehaviour
    {
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

        private List<ItemButtonController> _itemButtons = new List<ItemButtonController>();

        private void Awake()
        {
            for (int i = 0; i < itemChoiceCount; i++)
            {
                ItemButtonController button = Instantiate(itemButtonPrefab, itemButtonParentPanel);
                button.SetItem(itemPool.GetRandom());
                _itemButtons.Add(button);
            }
        }

        private void OnEnable()
        {
            foreach (ItemButtonController button in _itemButtons)
            {
                button.ItemChosen += AddItem;
            }
        }

        private void OnDisable()
        {
            foreach (ItemButtonController button in _itemButtons)
            {
                button.ItemChosen -= AddItem;
            }
        }

        private void AddItem(ItemData item)
        {
            playerData.Inventory.AddItem(item);
        }
    }
}
