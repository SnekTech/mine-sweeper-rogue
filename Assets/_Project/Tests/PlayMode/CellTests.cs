using System.Collections;
using NUnit.Framework;
using SnekTech.GridCell;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.PlayMode
{
    public class CellTests
    {
        private static readonly Cell CellPrefab = Utils.GetPrefabAsset<Cell>("Cell.prefab");
        
        [UnityTest]
        public IEnumerator flag_should_not_be_null_after_cell_init()
        {
            Cell cell = Object.Instantiate(CellPrefab);
            FlagBehaviour flagBehaviour = cell.FlagBehaviour;
            
            Assert.NotNull(flagBehaviour);
            yield break;
        }
        
        [UnityTest]
        public IEnumerator flag_should_be_inactive_after_cell_init()
        {
            Cell cell = Object.Instantiate(CellPrefab);
            FlagBehaviour flagBehaviour = cell.FlagBehaviour;
            
            Assert.IsFalse(flagBehaviour.IsActive());
            yield break;
        }
    }
}
