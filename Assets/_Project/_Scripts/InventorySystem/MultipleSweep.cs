using SnekTech.Player;
using UnityEngine;
using SnekTech.Constants;

namespace SnekTech.InventorySystem
{
    [CreateAssetMenu(fileName = nameof(MultipleSweep),
        menuName = MenuName.Items + MenuName.Slash + nameof(MultipleSweep))]
    public class MultipleSweep : ItemData
    {
        [Range(1, 5)]
        [SerializeField]
        private int sweepScope = 1;
        
        public override void OnAdd(PlayerData playerData)
        {
            playerData.SweepScope += sweepScope;
        }

        public override void OnRemove(PlayerData playerData)
        {
            playerData.SweepScope -= sweepScope;
        }
    }
}
