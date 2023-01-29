using SnekTech.UI.ChooseItem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SnekTech.UI.Modal
{
    public class Modal : MonoBehaviour
    {
        [SerializeField]
        private UIEventChannel uiEventChannel;
        
        [SerializeField]
        private TMP_Text header;

        [SerializeField]
        private CanvasGroup alphaAlphaGroup;

        [Header("Confirm")]
        [SerializeField]
        private ConfirmBody confirmBody;

        [SerializeField]
        private Button okButton;

        [Header("Choose Item Panel")]
        [SerializeField]
        private ChooseItemPanel chooseItemPanel;

        private const string ChooseItemHeader = "Choose An Item";

        public CanvasGroup BackgroundGroup => alphaAlphaGroup;
        public RectTransform ParentRect => transform.parent as RectTransform;

        private void OnEnable()
        {
            okButton.onClick.AddListener(OnOkButtonClick);
        }

        private void OnDisable()
        {
            okButton.onClick.RemoveListener(OnOkButtonClick);
        }

        private void OnOkButtonClick()
        {
            uiEventChannel.InvokeOnModalOk();
        }

        public void ChangeToChooseItemPanel()
        {
            SetConfirmPanelComponentsActive(false);
            SetChooseItemPanelComponentsActive(true);
            
            header.text = ChooseItemHeader;
            chooseItemPanel.GenerateItemButtons();
        }

        public void ChangeToConfirm(string headerText, Sprite image, string annotationText)
        {
            SetChooseItemPanelComponentsActive(false);
            SetConfirmPanelComponentsActive(true);

            header.text = headerText;
            confirmBody.Image.sprite = image;
            confirmBody.AnnotationText.text = annotationText;
        }

        #region SetAcitve Utitlity

        private void SetChooseItemPanelComponentsActive(bool isActive)
        {
            chooseItemPanel.SetActive(isActive);
        }

        private void SetConfirmPanelComponentsActive(bool isActive)
        {
            confirmBody.SetActive(isActive);
            okButton.SetActive(isActive);
        }

        #endregion
    }
}