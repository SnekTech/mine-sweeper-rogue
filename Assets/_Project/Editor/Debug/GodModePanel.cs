using SnekTech.Editor.Debug.Triggers;
using SnekTech.GamePlay;
using SnekTech.GamePlay.PlayerSystem;
using SnekTech.UI;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace SnekTech.Editor.Debug
{
    public class GodModePanel : EditorWindow
    {
        [SerializeField]
        private VisualTreeAsset panelUxml;

        [SerializeField]
        private PlayerHolder playerHolder;

        [SerializeField]
        private TriggersPanelData triggersPanelData;

        [SerializeField]
        private UIEventManager uiEventManager;

        [MenuItem("Tools/" + nameof(SnekTech) + "/GodModePanel")]
        private static void ShowWindow()
        {
            var window = GetWindow<GodModePanel>();
            window.titleContent = new GUIContent("God Mode");
        }

        public void CreateGUI()
        {
            panelUxml.CloneTree(rootVisualElement);

            triggersPanelData.Init(playerHolder.Player);
            var triggersPanel = rootVisualElement.Q<TriggersPanel>();
            triggersPanel.Init(triggersPanelData);

            var resetGamePanel = rootVisualElement.Q<ResetGamePanel>();
            resetGamePanel.Init(uiEventManager);
        }
    }
}