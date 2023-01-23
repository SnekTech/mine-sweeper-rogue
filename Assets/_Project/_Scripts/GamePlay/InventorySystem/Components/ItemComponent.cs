using System.Collections.Generic;
using SnekTech.GamePlay.EffectSystem;
using SnekTech.GamePlay.PlayerSystem;
using UnityEngine;

namespace SnekTech.GamePlay.InventorySystem.Components
{
    public abstract class ItemComponent : ScriptableObject
    {
        public abstract void OnAdd(IPlayer player);
        public abstract void OnRemove(IPlayer player);
    }
}
