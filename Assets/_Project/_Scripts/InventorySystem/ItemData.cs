using SnekTech.Constants;
using SnekTech.Player;
using UnityEngine;

namespace SnekTech.InventorySystem
{
    [CreateAssetMenu(fileName = nameof(ItemData), menuName = MenuName.Items + MenuName.Slash + nameof(ItemData))]
    public class ItemData : ScriptableObject
    {
        [SerializeField]
        private string label;
        [SerializeField]
        private string description;
        [SerializeField]
        private Sprite icon;

        public string Label => label;
        public string Description => description;
        public Sprite Icon => icon;


        public virtual void OnAdd(PlayerState playerState)
        {
        }

        public virtual void OnRemove(PlayerState playerState)
        {
        }
    }
}
