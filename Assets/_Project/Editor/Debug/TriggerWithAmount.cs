using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace SnekTech.Editor.Debug
{
    public class TriggerWithAmount : BaseField<int>
    {
        public const int DefaultMinValue = 0;
        public const int DefaultMaxValue = 10;
        
        private readonly SliderInt _sliderInt;
        private readonly Button _button;

        public new class UxmlFactory : UxmlFactory<TriggerWithAmount, BaseFieldTraits<int, UxmlIntAttributeDescription>>
        {
        }

        public TriggerWithAmount() : this("")
        {
        }

        public TriggerWithAmount(string label) : base(label, new Label())
        {
            HideBaseFieldLabel();

            var styleSheet = Resources.Load<StyleSheet>("ToolingUI/TriggerWithAmount");

            var root = new VisualElement();
            root.AddToClassList("control-group");
            root.styleSheets.Add(styleSheet);

            _sliderInt = new SliderInt();
            _button = new Button
            {
                text = "Trigger",
            };
            root.Add(_sliderInt);
            root.Add(_button);
            
            _sliderInt.AddToClassList("amount-field");
            _sliderInt.showInputField = true;
            _sliderInt.RegisterValueChangedCallback(evt => value = evt.newValue);

            hierarchy.Add(root);
        }

        private void HideBaseFieldLabel()
        {
            var fieldLabel = this.Q<Label>();
            fieldLabel.style.display = DisplayStyle.None;
        }

        public override void SetValueWithoutNotify(int newValue)
        {
            base.SetValueWithoutNotify(newValue);

            _sliderInt.value = value;
        }

        public void Init(string amountFieldName, Action<int> onTrigger, int min, int max)
        {
            _sliderInt.label = amountFieldName;
            _sliderInt.lowValue = min;
            _sliderInt.highValue = max;
            if (EditorApplication.isPlaying)
            {
                _button.clicked += () => onTrigger(value);
            }
        }
    }
}