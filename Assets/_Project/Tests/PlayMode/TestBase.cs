using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Tests.PlayMode
{
    public abstract class TestBase
    {
        private List<Object> objectPool;

        protected void AddToPool(Object o) => objectPool.Add(o);
        protected void AddToPool(MonoBehaviour behaviour) => objectPool.Add(behaviour.gameObject);

        [SetUp]
        public void SetUp() => objectPool = new List<Object>();

        [TearDown]
        public void TearDown()
        {
            Debug.Log("destroying test objects");
            objectPool.ForEach(Object.Destroy);
        }
    }
}