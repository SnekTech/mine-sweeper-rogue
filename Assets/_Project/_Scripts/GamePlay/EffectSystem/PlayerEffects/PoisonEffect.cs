﻿using System;
using Cysharp.Threading.Tasks;
using SnekTech.GamePlay.PlayerSystem;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace SnekTech.GamePlay.EffectSystem.PlayerEffects
{
    [Serializable]
    public class PoisonEffect : IPlayerEffect
    {
        [SerializeField]
        private int amount = 3;
        
        public UniTask Take(IPlayer target)
        {
            target.TakeDamageOnHealth(amount);
            return UniTask.CompletedTask;
        }
    }
    
    #if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(PoisonEffect))]
    public class PoisonEffectDrawer : PropertyDrawer
    {
        private const string UxmlAssetPath = "Assets/_Project/_Scripts/GamePlay/EffectSystem/Editor/HealEditorTemplate.uxml";
        
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var root = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(UxmlAssetPath).Instantiate();
            var nameField = root.Q<Label>("effect-name");
            nameField.text = nameof(PoisonEffect);
            var amountField = new PropertyField(property.FindPropertyRelative("amount"));
            root.Q("root").Add(amountField);
            
            return root;
        }
    }
    #endif
}
