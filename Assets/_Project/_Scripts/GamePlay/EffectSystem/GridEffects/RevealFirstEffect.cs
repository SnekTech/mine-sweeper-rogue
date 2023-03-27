using System;
using Cysharp.Threading.Tasks;
using SnekTech.GamePlay.EffectSystem.PlayerEffects;
using SnekTech.MineSweeperRogue.GridSystem;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace SnekTech.GamePlay.EffectSystem.GridEffects
{
    [Serializable]
    public class RevealFirstEffect : IGridEffect
    {
        public async UniTask Take(IGrid target)
        {
            await target.RevealAtAsync(GridIndex.First);
        }
    }
    
    #if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(RevealFirstEffect))]
    public class HealEffectDrawer : PropertyDrawer
    {
        private const string UxmlAssetPath = "Assets/_Project/_Scripts/GamePlay/EffectSystem/Editor/HealEditorTemplate.uxml";
        
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var root = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(UxmlAssetPath).Instantiate();
            var nameField = root.Q<Label>("effect-name");
            nameField.text = nameof(RevealFirstEffect);
            
            return root;
        }
    }
    #endif
}
