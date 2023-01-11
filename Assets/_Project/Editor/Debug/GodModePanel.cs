using System;
using SnekTech.Player;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace SnekTech.Editor.Debug
{
    public class GodModePanel : EditorWindow
    {
        [SerializeField]
        private VisualTreeAsset panelUxml;

        [SerializeField]
        private PlayerState playerState;

        [SerializeField]
        private int damage;
        [SerializeField]
        private int addHealth;

        [SerializeField]
        private int addArmour;

        [SerializeField]
        private int damageOnArmour;

        [SerializeField]
        private int damageOnHealth;

        [SerializeField]
        private int addMaxHealth;
        
        private const string DamageTriggerName = "damageTrigger";
        private const string AddHealthTriggerName = "addHealthTrigger";
        private const string AddArmourTriggerName = "addArmourTrigger";
        private const string DamageOnArmourTriggerName = "damageOnArmourTrigger";
        private const string DamageOnHealthTriggerName = "damageOnHealthTrigger";
        private const string AdjustMaxHealthTriggerName = "adjustMaxHealthTrigger";

        private SerializedObject _target;

        [MenuItem("Tools/" + nameof(SnekTech) + "/GodModePanel")]
        private static void ShowWindow()
        {
            var window = GetWindow<GodModePanel>();
            window.titleContent = new GUIContent("God Mode");
        }

        public void CreateGUI()
        {
            panelUxml.CloneTree(rootVisualElement);
            _target = new SerializedObject(this);

            InitTrigger(DamageTriggerName, nameof(damage),
                amount => playerState.TakeDamage(amount));
            InitTrigger(AddHealthTriggerName, nameof(addHealth),
                h => playerState.AddHealth(h));
            InitTrigger(AddArmourTriggerName, nameof(addArmour),
                amount => playerState.AddArmour(amount));
            InitTrigger(DamageOnArmourTriggerName, nameof(damageOnArmour),
                amount => playerState.TakeDamageOnArmour(amount));
            InitTrigger(DamageOnHealthTriggerName, nameof(damageOnHealth),
                amount => playerState.TakeDamageOnHealth(amount));
            InitTrigger(AdjustMaxHealthTriggerName, nameof(addMaxHealth),
                amount => playerState.AdjustMaxHealth(amount), -10);


            rootVisualElement.Bind(_target);
        }

        private void InitTrigger(string triggerName, string bindingPath, Action<int> onTrigger, int min = TriggerWithAmount.DefaultMinValue, int max = TriggerWithAmount.DefaultMaxValue)
        {
            var trigger = rootVisualElement.Q<TriggerWithAmount>(triggerName);
            trigger.bindingPath = bindingPath;
            trigger.Init(bindingPath, onTrigger, min, max);
        }
    }
}