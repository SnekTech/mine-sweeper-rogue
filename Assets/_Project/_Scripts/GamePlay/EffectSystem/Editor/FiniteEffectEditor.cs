using System;
using System.Collections.Generic;
using SnekTech.Editor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace SnekTech.GamePlay.EffectSystem.Editor
{
    public abstract class FiniteEffectEditor : UnityEditor.Editor
    {
        [SerializeField]
        private VisualTreeAsset uxml;
        
        protected abstract List<Type> DecoratedEffectTypeCandidates { get; }

        private ScriptableObject _finiteEffectAsset;

        private string DecoratedEffectSaveFolder => _finiteEffectAsset.GetParentFolder();

        private void OnEnable()
        {
            _finiteEffectAsset = serializedObject.targetObject as ScriptableObject;
        }

        public override VisualElement CreateInspectorGUI()
        {
            base.CreateInspectorGUI();
            
            var templateContainer = uxml.CloneTree();
            var addEffectButtonGroup = templateContainer.Q("add-effect-button-group");
            var decoratedEffectFieldParent = templateContainer.Q("decorated-parent");
            

            foreach (var effectType in DecoratedEffectTypeCandidates)
            {
                var button = new Button(() => SetDecoratedEffect(effectType, decoratedEffectFieldParent))
                {
                    text = $"Set {effectType.Name}",
                };

                addEffectButtonGroup.Add(button);
            }
            
            return templateContainer;
        }

        private void SetDecoratedEffect(Type decoratedEffectType, VisualElement decoratedFieldParent)
        {
            serializedObject.Update();
            
            var decoratedEffectProperty = serializedObject.FindProperty("decoratedEffect");
            
            string assetPath = $"{DecoratedEffectSaveFolder}/{decoratedEffectType.Name}.asset";
            var decoratedEffectAsset = AssetFileUtils.CreateAndSaveAsset(decoratedEffectType, assetPath, true);
            
            decoratedEffectProperty.objectReferenceValue = decoratedEffectAsset;

            decoratedFieldParent.Clear();
            var newDecoratedField = new PropertyField(decoratedEffectProperty);
            newDecoratedField.BindProperty(serializedObject);
            newDecoratedField.Bind(serializedObject);
            decoratedFieldParent.Add(newDecoratedField);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
