using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace SnekTech.Editor.Debug.Triggers
{
    public class TriggerField : Attribute {}
    
    public class TriggersPanel : VisualElement
    {
        private readonly Dictionary<string, TriggerWithAmount> _fieldNameToTrigger;

        public new class UxmlFactory : UxmlFactory<TriggersPanel>
        {
        }

        public TriggersPanel()
        {
            _fieldNameToTrigger = new Dictionary<string, TriggerWithAmount>();
            GenerateTriggers();
        }

        private void GenerateTriggers()
        {
            foreach (var field in typeof(TriggersPanelData).GetInstanceFieldsWithAttributeOfType(typeof(TriggerField)))
            {
                var trigger = new TriggerWithAmount();
                _fieldNameToTrigger[field.Name] = trigger;
                hierarchy.Add(trigger);
            }
        }

        public void Init(TriggersPanelData triggersPanelData)
        {
            foreach ((string fieldName, var trigger) in _fieldNameToTrigger)
            {
                trigger.Init(fieldName, triggersPanelData.fieldActionDict[fieldName]);
            }
            hierarchy.parent.Bind(new SerializedObject(triggersPanelData));
        }
    }
}