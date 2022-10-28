using System;
using SnekTech.InventorySystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SnekTech.UI
{
    public class ItemButtonController : MonoBehaviour
    {
        [SerializeField]
        private UIEventManager uiEventManager;
        
        private ItemData _itemData;
        private TMP_Text _text;
        private Image _image;

        private void Awake()
        {
            _text = GetComponentInChildren<TMP_Text>();
            _image = GetComponent<Image>();
        }

        public void SetItem(ItemData item)
        {
            _itemData = item;
            UpdateButtonContent();
        }

        private void UpdateButtonContent()
        {
            _text.text = _itemData.label;
            _image.sprite = _itemData.icon;
        }

        public void OnItemButtonClicked()
        {
            uiEventManager.InvokeItemChosen(_itemData);
        }
    }
}
