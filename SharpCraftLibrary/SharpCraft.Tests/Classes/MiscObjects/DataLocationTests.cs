using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpCraft;

namespace SharpCraft.Tests.MiscObjects
{
    [TestClass]
    public class DataLocationTests
    {
        [TestMethod]
        public void TestBlockDataLocation()
        {
            //test
            BlockDataLocation location = new BlockDataLocation(new Coords(), "test1.test2");
            Assert.AreEqual("~ ~ ~", location.Coordinates.GetVectorString(), "Constructor didn't set coords correctly");
            Assert.AreEqual("test1.test2", location.DataPath, "Constructor didn't set DataPath correctly");
            Assert.AreEqual("block ~ ~ ~ test1.test2", location.GetLocationString(), "GetLocationString returns wrong string");

            //exceptions
            Assert.ThrowsException<ArgumentNullException>(() => new BlockDataLocation(null!, "test"), "Coords may not be null");
            Assert.ThrowsException<ArgumentException>(() => new BlockDataLocation(new Coords(), ""), "Path may not be empty");
            Assert.ThrowsException<ArgumentException>(() => new BlockDataLocation(new Coords(), null!), "Path may not be null");
        }

        [TestMethod]
        public void TestEntityDataLocation()
        {
            //test
            EntityDataLocation location = new EntityDataLocation(ID.Selector.s, "test1.test2");
            Assert.AreEqual("@s", location.Selector.GetSelectorString(), "Constructor didn't set selector correctly");
            Assert.AreEqual("test1.test2", location.DataPath, "Constructor didn't set DataPath correctly");
            Assert.AreEqual("entity @s test1.test2", location.GetLocationString(), "GetLocationString returns wrong string");

            //exceptions
            Assert.ThrowsException<ArgumentNullException>(() => new EntityDataLocation(null!, "test"), "Selector may not be null");
            Assert.ThrowsException<ArgumentException>(() => new EntityDataLocation(ID.Selector.e, "test"), "Selector may not select multiple entities");
            Assert.ThrowsException<ArgumentException>(() => new EntityDataLocation(ID.Selector.s, ""), "Path may not be empty");
            Assert.ThrowsException<ArgumentException>(() => new EntityDataLocation(ID.Selector.s, null!), "Path may not be null");
        }

        [TestMethod]
        public void TestStorageDataLocation()
        {
            //test
            Storage storage = new Storage(MockNamespace.GetNamespace("space"), "stor");
            StorageDataLocation location = new StorageDataLocation(storage, "test1.test2");
            Assert.AreEqual("space:stor", location.Storage.GetNamespacedName(), "Constructor didn't set storage correctly");
            Assert.AreEqual("test1.test2", location.DataPath, "Constructor didn't set DataPath correctly");
            Assert.AreEqual("storage space:stor test1.test2", location.GetLocationString(), "GetLocationString returns wrong string");

            //exceptions
            Assert.ThrowsException<ArgumentNullException>(() => new StorageDataLocation(null!, "test"), "storage may not be null");
            Assert.ThrowsException<ArgumentException>(() => new StorageDataLocation(storage, ""), "Path may not be empty");
            Assert.ThrowsException<ArgumentException>(() => new StorageDataLocation(storage, null!), "Path may not be null");
        }
    }
}
