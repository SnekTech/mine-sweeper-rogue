using Cysharp.Threading.Tasks;
using SnekTech.GamePlay.PlayerSystem;
using UnityEngine;

namespace SnekTech.GamePlay.InventorySystem.Components
{
    public abstract class ItemComponent : ScriptableObject
    {
        public abstract UniTask OnAdd(IPlayer player);
        public abstract UniTask OnRemove(IPlayer player);
    }
}
