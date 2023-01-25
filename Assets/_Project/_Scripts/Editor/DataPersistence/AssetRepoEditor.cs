using SnekTech.DataPersistence;
using UnityEngine;
using UnityEngine.UIElements;

namespace SnekTech.Editor.DataPersistence
{
    public abstract class AssetRepoEditor<T> : UnityEditor.Editor where T : ScriptableObject
    {
        public override VisualElement CreateInspectorGUI()
        {
            serializedObject.Update();
            var assetRepo = serializedObject.targetObject as AssetRepo<T>;

            var listView = new ListView();
            var assets = assetRepo!.Assets;

            VisualElement MakeItem() => new Label();
            void BindItem(VisualElement e, int i) => (e as Label)!.text = assets[i].name;

            listView.itemsSource = assets;
            listView.makeItem = MakeItem;
            listView.bindItem = BindItem;

            return listView;
        }
    }
}
