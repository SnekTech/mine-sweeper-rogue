using UnityEngine;

namespace SnekTech.InventorySystem
{
    [CreateAssetMenu(fileName = nameof(ItemData), menuName = Utils.MyInventoryMenuName + "/" + nameof(ItemData))]
    public class ItemData : ScriptableObject
    {
        public string label;
        public string description;
        public Sprite icon;
    }
}
