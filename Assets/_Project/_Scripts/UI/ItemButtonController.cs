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
        private Button _button;

        private void Awake()
        {
            _text = GetComponentInChildren<TMP_Text>();
            _image = GetComponent<Image>();
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(HandleItemButtonClicked);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(HandleItemButtonClicked);
        }

        public void SetItem(ItemData item)
        {
            _itemData = item;
            UpdateButtonContent();
        }

        private void UpdateButtonContent()
        {
            _text.text = _itemData.Label;
            _image.sprite = _itemData.Icon;
        }

        public void HandleItemButtonClicked()
        {
            uiEventManager.InvokeItemChosen(_itemData);
        }
    }
}
