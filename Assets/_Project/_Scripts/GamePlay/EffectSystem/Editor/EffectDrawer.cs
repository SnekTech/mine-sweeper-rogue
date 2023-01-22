using SnekTech.Editor;
using SnekTech.Editor.CustomAttributes;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

#if false

namespace SnekTech.GamePlay.EffectSystem.Editor
{
    [CustomPropertyDrawer(typeof(EffectObjectAttribute))]
    public class EffectDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            Debug.Log("fk");
            var container = new VisualElement();
            container.Add(new Label(property.serializedObject.GetType().Name));
            container.Add(new PropertyField(property));

            return container;
            
            var effect = property.managedReferenceValue;
            var uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>($"{Constants.UxmlFolder}/{nameof(EffectDrawer)}.uxml");
            var templateContainer = uxml.CloneTree();
            var root = templateContainer.Q("root");

            var label = root.Q<Label>();
            label.text = effect.GetType().Name;

            var effectFields = effect.GetType()
                .GetInstanceFieldsWithAttributeOfType<EffectFieldAttribute>();
            foreach (var effectField in effectFields)
            {
                var propField = new PropertyField(property.FindPropertyRelative(effectField.Name));
                root.Add(propField);
            }

            return templateContainer;
        }
    }
}

#endif