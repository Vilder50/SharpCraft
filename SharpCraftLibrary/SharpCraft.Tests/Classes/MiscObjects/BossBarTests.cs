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
    }
}