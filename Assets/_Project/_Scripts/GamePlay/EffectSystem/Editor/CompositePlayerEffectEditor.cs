using System;
using System.Linq;
using SnekTech.Editor;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace SnekTech.GamePlay.EffectSystem
{
    [CustomEditor(typeof(CompositePlayerEffect))]
    public class CompositePlayerEffectEditor : UnityEditor.Editor
    {
        public VisualTreeAsset uxml;

        public override VisualElement CreateInspectorGUI()
        {
            var root = new VisualElement();
            uxml.CloneTree(root);

            var addButtonGroup = root.Q<VisualElement>("add-buttons");
            CreateAddEffectButtons(addButtonGroup);

            AppendDefaultInspectorFoldout(root);

            return root;
        }

        private void CreateAddEffectButtons(VisualElement buttonGroup)
        {
            buttonGroup.Clear();
            var effectTypes = ReflectionUtils.GetImplementorsOfInterface<IPlayerEffect>()
                .Where(type => type != typeof(CompositePlayerEffect));

            foreach (var effectType in effectTypes)
            {
                var effectName = effectType.Name;
                var button = new Button
                {
                    name = effectName,
                    text = effectName,
                    viewDataKey = $"add-effect-button-{effectName}",
                };
                button.RegisterCallback<ClickEvent>(e =>
                {
                    var effect = Activator.CreateInstance(effectType) as IPlayerEffect;
                    AddEffect(effect);
                });
                buttonGroup.Add(button);
            }
        }

        private SerializedProperty EffectsProperty => serializedObject.FindProperty("effects");

        private void AddEffect(IPlayerEffect effect)
        {
            serializedObject.Update();
            EffectsProperty.InsertArrayElementAtIndex(EffectsProperty.arraySize);
            EffectsProperty.GetArrayElementAtIndex(EffectsProperty.arraySize - 1).managedReferenceValue = effect;
            serializedObject.ApplyModifiedProperties();
        }

        private void AppendDefaultInspectorFoldout(VisualElement root)
        {
            var foldout = new Foldout
            {
                text = "default-inspector",
                viewDataKey = "default-foldout",
            };
            InspectorElement.FillDefaultInspector(foldout, serializedObject, this);

            root.Add(foldout);
        }
    }
}