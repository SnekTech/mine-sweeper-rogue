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

        public override void OnAdd(PlayerState playerState)
        {
            _propertyModifier = new SweepScopeModifier(sweepScope);
            playerState.AddDataAccumulator(_propertyModifier);
        }

        public override void OnRemove(PlayerState playerState)
        {
            playerState.RemoveDataAccumulator(_propertyModifier);
        }
    }
}
