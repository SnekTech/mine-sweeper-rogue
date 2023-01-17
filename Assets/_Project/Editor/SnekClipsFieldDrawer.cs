using SnekTech.Core.CustomAttributes;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace SnekTech.Editor
{
    [CustomPropertyDrawer(typeof(SnekClipsFieldAttribute))]
    public class SnekClipsFieldDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var root = new VisualElement();
            var defaultPropertyField = new PropertyField(property);
            var label = new Label("clip & index should match");

            root.Add(defaultPropertyField);
            root.Add(label);

            return root;
        }
    }
}
