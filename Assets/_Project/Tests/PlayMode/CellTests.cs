using System.Collections;
using NUnit.Framework;
using SnekTech.GridCell;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.PlayMode
{
    public class CellTests
    {
        private static readonly CellBehaviour CellBehaviourPrefab = Utils.GetPrefabAsset<CellBehaviour>("Cell.prefab");
        
        [UnityTest]
        public IEnumerator flag_should_not_be_null_after_cell_init()
        {
            ICell cell = Object.Instantiate(CellBehaviourPrefab);
            IFlag flag = cell.Flag;
            
            Assert.NotNull(flag);
            yield break;
        }
        
        [UnityTest]
        public IEnumerator flag_should_be_inactive_after_cell_init()
        {
            ICell cell = Object.Instantiate(CellBehaviourPrefab);
            IFlag flag = cell.Flag;
            
            Assert.IsFalse(flag.IsActive);
            yield break;
        }

        [UnityTest]
        public IEnumerator should_cache_all_states_after_cell_init()
        {
            ICell cell = Object.Instantiate(CellBehaviourPrefab);

            CellState coveredState = cell.CoveredState;
            CellState flaggedState = cell.FlaggedState;
            CellState revealedState = cell.RevealedState;
            
            Assert.IsInstanceOf<CellCoveredState>(coveredState);
            Assert.IsInstanceOf<CellFlaggedState>(flaggedState);
            Assert.IsInstanceOf<CellRevealedState>(revealedState);
            yield break;
        }
    }
}
