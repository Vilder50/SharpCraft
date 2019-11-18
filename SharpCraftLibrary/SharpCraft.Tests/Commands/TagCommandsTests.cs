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
    public class TagCommandsTests
    {
        [TestMethod]
        public void TagCommandTest()
        {
            Assert.AreEqual("tag @a add test", new TagCommand(ID.Selector.a, new Tag("test"), true).GetCommandString());
            Assert.AreEqual("tag @a remove test", new TagCommand(ID.Selector.a, new Tag("test"), false).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new TagCommand(null, new Tag("test"), true));
            Assert.ThrowsException<ArgumentNullException>(() => new TagCommand(ID.Selector.a, null, true));
        }

        [TestMethod]
        public void TagListCommandTest()
        {
            Assert.AreEqual("tag @a list", new TagListCommand(ID.Selector.a).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new TagListCommand(null));
        }
    }
}
