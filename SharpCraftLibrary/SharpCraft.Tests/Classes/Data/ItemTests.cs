using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SharpCraft.Tests.Data
{
    [TestClass]
    public class ItemTests
    {
        [TestMethod]
        public void TestGetItemTagString()
        {
            Item item = new Item(ID.Item.dirt, 1) { CustomModelData = 12 };
            Assert.AreEqual("{CustomModelData:12}",item.GetItemTagString());
        }

        [TestMethod]
        public void TestIDDataString()
        {
            Item item = new Item(ID.Item.dirt, 1) { CustomModelData = 12 };
            Assert.AreEqual("minecraft:dirt{CustomModelData:12}", item.GetIDDataString());
        }
    }
}
