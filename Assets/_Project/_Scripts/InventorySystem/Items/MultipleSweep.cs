using SnekTech.C;
using SnekTech.Player;
using SnekTech.Player.PlayerDataAccumulator;
using UnityEngine;

namespace SnekTech.InventorySystem.Items
{
    [CreateAssetMenu(fileName = nameof(MultipleSweep),
        menuName = MenuName.Items + MenuName.Slash + nameof(MultipleSweep))]
    public class MultipleSweep : ItemData
    {
        [Range(GameConstants.SweepScopeMin, GameConstants.SweepScopeMax)]
        [SerializeField]
        private int sweepScope = 1;

        private PlayerDataAccumulator _dataAccumulator;

        public override void OnAdd(PlayerState playerState)
        {
            _dataAccumulator = new SweepScopeAccumulator(sweepScope);
            playerState.AddDataAccumulator(_dataAccumulator);
        }

        public override void OnRemove(PlayerState playerState)
        {
            playerState.RemoveDataAccumulator(_dataAccumulator);
        }
    }
}
