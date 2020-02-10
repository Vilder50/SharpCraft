using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpCraft;

namespace SharpCraft.Tests.MiscObjects
{
    [TestClass]
    public class BossBarTests
    {
        [TestMethod]
        public void TestBossBar()
        {
            BossBar bar1 = new BossBar("myBAR");
            Assert.AreEqual("mybar", bar1.Name, "Constructor didn't set name");
            Assert.IsNull(bar1.Namespace, "Constructor should have set namespace to null");

            EmptyNamespace space = EmptyNamespace.GetNamespace("myspace");
            BossBar bar2 = new BossBar(space, "otherbar");
            Assert.AreEqual("otherbar", bar2.Name, "Namespace constructor didn't set name");
            Assert.AreSame(space, bar2.Namespace, "Namespace constructor should have set namespace to null");

            Assert.ThrowsException<ArgumentException>(() => new BossBar("Inval:d"), "Name should be validated");
        }

        [TestMethod]
        public void TestGetFullName()
        {
            BossBar bar1 = new BossBar("mybar");
            Assert.AreEqual("minecraft:mybar", bar1.GetFullName(), "GetFullName without namespace returns wrong value");

            EmptyNamespace space = EmptyNamespace.GetNamespace("myspace");
            BossBar bar2 = new BossBar(space, "otherbar");
            Assert.AreEqual("myspace:otherbar", bar2.GetFullName(), "GetFullName with namespace returns wrong value");
        }

        [TestMethod]
        public void TestValidateName()
        {
            Assert.IsTrue(BossBar.ValidateName("///...__---"), "BossBar name should accept / . _ and -");
            Assert.IsTrue(BossBar.ValidateName("as0ds9az564xxcy12"), "BossBar name should accept letters and numbers");
            Assert.IsFalse(BossBar.ValidateName("test:test"), "BossBar name should not accept :");
            Assert.IsFalse(BossBar.ValidateName(""), "BossBar name may not be empty");
            Assert.IsFalse(BossBar.ValidateName("ASD"), "BossBar name may not contain capitialized letters");
        }
    }
}