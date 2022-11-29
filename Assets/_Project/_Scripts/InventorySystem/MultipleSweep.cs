using System;
using SnekTech.Player;
using UnityEngine;
using SnekTech.Constants;
using SnekTech.InventorySystem.Items;

namespace SnekTech.InventorySystem
{
    [CreateAssetMenu(fileName = nameof(MultipleSweep),
        menuName = MenuName.Items + MenuName.Slash + nameof(MultipleSweep))]
    public class MultipleSweep : ItemData
    {
        [Range(GameData.SweepScopeMin, GameData.SweepScopeMax)]
        [SerializeField]
        private int sweepScope = 1;

        private PlayerPropertyModifier _propertyModifier;

        public override void OnAdd(PlayerData playerData)
        {
            _propertyModifier = new SweepScopeModifier(sweepScope);
            _propertyModifier.Apply(playerData);
        }

        public override void OnRemove(PlayerData playerData)
        {
            _propertyModifier.Resume(playerData);
        }
    }
}
