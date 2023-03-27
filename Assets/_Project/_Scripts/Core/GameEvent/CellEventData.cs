using Cysharp.Threading.Tasks;
using SnekTech.GamePlay.PlayerSystem;
using SnekTech.UI;
using UnityEngine;

namespace SnekTech.Core.GameEvent
{
    public abstract class CellEventData : ScriptableObject, IHoverableIconHolder
    {
        [SerializeField]
        private string label;

        [SerializeField]
        private Sprite icon;

        [SerializeField]
        private string description;

        public string Label => label;
        public Sprite Icon => icon;
        public string Description => description;

        public abstract UniTask Trigger(Player player);

        private void OnEnable()
        {
            CellEventAssetRepo.Instance.Set(name, this);
        }
    }
}
