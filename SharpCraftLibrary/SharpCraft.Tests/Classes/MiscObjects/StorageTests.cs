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

        [TestMethod]
        public void TestValidateName()
        {
            Assert.IsTrue(Storage.ValidateName("///...__---"), "Storage name should accept / . _ and -");
            Assert.IsTrue(Storage.ValidateName("as0ds9az564xxcy12"), "Storage name should accept letters and numbers");
            Assert.IsFalse(Storage.ValidateName("test:test"), "Storage name should not accept :");
            Assert.IsFalse(Storage.ValidateName(""), "Storage name may not be empty");
            Assert.IsFalse(Storage.ValidateName("ASD"), "Storage name may not contain capitialized letters");
        }
    }
}

