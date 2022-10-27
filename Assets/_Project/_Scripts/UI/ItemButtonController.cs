using System;
using SnekTech.InventorySystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SnekTech.UI
{
    public class ItemButtonController : MonoBehaviour
    {
        private ItemData _itemData;

        public event Action<ItemData> ItemChosen; 

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

        public void InvokeItemChosen()
        {
            ItemChosen?.Invoke(_itemData);
        }
    }
}
