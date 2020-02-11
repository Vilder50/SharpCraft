using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SharpCraft.Tests.Data
{
    [TestClass]
    public class EntityTests
    {
        [TestMethod]
        public void TestGetDataWithoutID()
        {
            Entity.BaseEntity entity = new Entity.Creeper(ID.Entity.creeper) { Charged = true };
            Assert.AreEqual("{id:\"minecraft:creeper\",powered:1b}", entity.GetDataString());
            Assert.AreEqual("{powered:1b}", entity.GetDataWithoutID());
        }
    }
}
