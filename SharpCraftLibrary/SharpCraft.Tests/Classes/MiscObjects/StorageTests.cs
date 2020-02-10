using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpCraft;

namespace SharpCraft.Tests.MiscObjects
{
    [TestClass]
    public class StorageTests
    {
        [TestMethod]
        public void TestStorage()
        {
            EmptyNamespace space = EmptyNamespace.GetNamespace("myspace");
            Storage storage = new Storage(space, "storage");
            Assert.AreEqual("storage", storage.Name, "Namespace constructor didn't set name");
            Assert.AreSame(space, storage.PackNamespace, "Namespace constructor should have set namespace to null");

            Assert.ThrowsException<ArgumentNullException>(() => new Storage(null, "name"), "Namespace may not be null");
            Assert.ThrowsException<ArgumentException>(() => new Storage(space, "Inval:d"), "Name should be validated");
        }

        [TestMethod]
        public void TestGetFullName()
        {
            EmptyNamespace space = EmptyNamespace.GetNamespace("myspace");
            Storage storage = new Storage(space, "storage");
            Assert.AreEqual("myspace:storage", storage.GetNamespacedName(), "GetNamespacedName with namespace returns wrong value");
        }
    }
}

