using System;
using System.Collections.Generic;
using SnekTech.GamePlay.AbilitySystem;
using SnekTech.GamePlay.InventorySystem;
using UnityEngine;

namespace SnekTech.GamePlay.PlayerSystem
{
    [CreateAssetMenu(menuName = C.MenuName.EventChannels + "/" + nameof(PlayerEventChannel))]
    public class PlayerEventChannel : ScriptableObject
    {
        public event Action<List<PlayerAbility>> AbilitiesChanged;
        public event Action HealthRanOut;
        public event Action<List<InventoryItem>> InventoryItemChanged; 

        public void InvokeAbilitiesChanged(List<PlayerAbility> abilities) => AbilitiesChanged?.Invoke(abilities);
        public void InvokeHealthRanOut() => HealthRanOut?.Invoke();
        public void InvokeInventoryItemChanged(List<InventoryItem> items) => InventoryItemChanged?.Invoke(items);
    }
}
