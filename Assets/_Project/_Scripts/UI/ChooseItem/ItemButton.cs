using SnekTech.InventorySystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SnekTech.UI.ChooseItem
{
    public class ItemButton : MonoBehaviour
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
            _image = GetComponentInChildren<Image>();
            _button = GetComponentInChildren<Button>();
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

        private void HandleItemButtonClicked()
        {
            uiEventManager.InvokeOnChooseItem(_itemData);
        }
    }
}
