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
    }
}
