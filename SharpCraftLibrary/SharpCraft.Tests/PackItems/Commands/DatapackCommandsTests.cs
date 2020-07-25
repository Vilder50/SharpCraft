using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpCraft;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using SharpCraft.Commands;

namespace SharpCraft.Tests.Commands
{
    [TestClass]
    public class DatapackCommandsTests
    {
        private BaseDatapack GetPack()
        {
            return new MockDatapack("pack");
        }

        [TestMethod]
        public void DatapackDisableCommandTest()
        {
            Assert.AreEqual("datapack disable \"file/pack\"", new DatapackDisableCommand(GetPack()).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new DatapackDisableCommand(null!));
        }

        [TestMethod]
        public void DatapackEnableCommandTest()
        {
            Assert.AreEqual("datapack enable \"file/pack\" first", new DatapackEnableCommand(GetPack(), true).GetCommandString());
            Assert.AreEqual("datapack enable \"file/pack\" last", new DatapackEnableCommand(GetPack(), false).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new DatapackEnableCommand(null!, false));
        }

        [TestMethod]
        public void DatapackEnableAtCommandTest()
        {
            BaseDatapack otherPack = new MockDatapack("other");
            Assert.AreEqual("datapack enable \"file/pack\" after \"file/other\"", new DatapackEnableAtCommand(GetPack(), true, otherPack).GetCommandString());
            Assert.AreEqual("datapack enable \"file/pack\" before \"file/other\"", new DatapackEnableAtCommand(GetPack(), false, otherPack).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new DatapackEnableAtCommand(null!, false, otherPack));
            Assert.ThrowsException<ArgumentNullException>(() => new DatapackEnableAtCommand(GetPack(), false, null!));
        }

        [TestMethod]
        public void DatapackListCommandTest()
        {
            Assert.AreEqual("datapack list", new DatapackListCommand(ID.DatapackList.all).GetCommandString());
            Assert.AreEqual("datapack list available", new DatapackListCommand(ID.DatapackList.disabled).GetCommandString());
            Assert.AreEqual("datapack list enabled", new DatapackListCommand(ID.DatapackList.enabled).GetCommandString());
        }
    }
}
