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
            IFlag flag = cell.Flag;
            
            Assert.NotNull(flag);
            yield break;
        }
        
        [UnityTest]
        public IEnumerator flag_should_be_inactive_after_cell_init()
        {
            Cell cell = Object.Instantiate(CellPrefab);
            IFlag flag = cell.Flag;
            
            Assert.IsFalse(flag.IsActive);
            yield break;
        }
    }
}
