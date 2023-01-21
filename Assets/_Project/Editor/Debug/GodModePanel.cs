using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace SnekTech.Editor.Debug
{
    public class GodModePanel : EditorWindow
    {
        [SerializeField]
        private VisualTreeAsset panelUxml;

        [MenuItem("Tools/" + nameof(SnekTech) + "/GodModePanel")]
        private static void ShowWindow()
        {
            var window = GetWindow<GodModePanel>();
            window.titleContent = new GUIContent("God Mode");
        }

        public void CreateGUI()
        {
            panelUxml.CloneTree(rootVisualElement);
        }
    }
}