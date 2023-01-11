using System;
using System.Collections.Generic;
using System.Linq;
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

            GenerateSceneButtons();
            
            InitTriggers();


            rootVisualElement.Bind(_target);
        }

        #region trigger fields

        private void InitTriggers()
        {
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
        }

        private void InitTrigger(string triggerName, string bindingPath, Action<int> onTrigger,
            int min = TriggerWithAmount.DefaultMinValue, int max = TriggerWithAmount.DefaultMaxValue)
        {
            var trigger = rootVisualElement.Q<TriggerWithAmount>(triggerName);
            trigger.bindingPath = bindingPath;
            trigger.Init(bindingPath, onTrigger, min, max);
        }

        #endregion

        #region switch scenes

        private void GenerateSceneButtons()
        {
            var sceneSwitchRootFoldout = rootVisualElement.Q<Foldout>("sceneSwitchFoldout");

            var sceneNames = new List<string>();
            var scenePaths = new List<string>();

            var sceneDirPaths = new List<string>
            {
                Application.dataPath + "/_Project/Scenes",
            };

            var sceneFiles = sceneDirPaths.Select(dirPath => FileUtils.GetFilePaths(dirPath, "*Unity"))
                .Where(filePaths => !filePaths.IsEmpty()).SelectMany(filePaths => filePaths);
            foreach (string filePath in sceneFiles)
            {
                scenePaths.Add(filePath);
                sceneNames.Add(FileUtils.GetFileName(filePath));
            }
            
            for (int i = 0; i < sceneNames.Count; i++)
            {
                string scenePath = scenePaths[i];
                var button = new Button(() => SceneUtils.OpenScene(scenePath))
                {
                    text = sceneNames[i],
                };
                sceneSwitchRootFoldout.Add(button);
            }
        }

        #endregion
    }
}