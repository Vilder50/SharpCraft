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
    public class AdvancementCommandsTests
    {
        [TestMethod]
        public void AdvancementAllCommandTest()
        {
            Assert.AreEqual("advancement grant @a everything", new AdvancementAllCommand(ID.Selector.a).GetCommandString());
            Assert.AreEqual("advancement revoke @a everything", new AdvancementAllCommand(ID.Selector.a, false).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new AdvancementAllCommand(null, false));
        }

        [TestMethod]
        public void AdvancementSingleCommandTest()
        {
            using EmptyDatapack datapack = new EmptyDatapack("name");
            EmptyNamespace packNamespace = datapack.Namespace("space");
            Assert.AreEqual("advancement grant @a only space:adv", new AdvancementSingleCommand(ID.Selector.a, new EmptyAdvancement(packNamespace, "adv"), null, true).GetCommandString());
            Assert.AreEqual("advancement revoke @a only space:adv", new AdvancementSingleCommand(ID.Selector.a, new EmptyAdvancement(packNamespace, "adv"), null, false).GetCommandString());
            Assert.AreEqual("advancement grant @a only space:adv test", new AdvancementSingleCommand(ID.Selector.a, new EmptyAdvancement(packNamespace, "adv"), new AdvancementObjects.BredAnimalsTrigger() { Name = "test" }, true).GetCommandString());
            Assert.AreEqual("advancement revoke @a only space:adv test", new AdvancementSingleCommand(ID.Selector.a, new EmptyAdvancement(packNamespace, "adv"), new AdvancementObjects.BredAnimalsTrigger() { Name = "test" }, false).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new AdvancementSingleCommand(null, new EmptyAdvancement(packNamespace, "adv"), null, true));
            Assert.ThrowsException<ArgumentNullException>(() => new AdvancementSingleCommand(ID.Selector.a, null, null, true));
        }

        [TestMethod]
        public void AdvancementSomeCommandTest()
        {
            using EmptyDatapack datapack = new EmptyDatapack("name");
            EmptyNamespace packNamespace = datapack.Namespace("space");
            Assert.AreEqual("advancement grant @a from space:adv", new AdvancementSomeCommand(ID.Selector.a, new EmptyAdvancement(packNamespace, "adv"), ID.RelativeAdvancement.from, true).GetCommandString());
            Assert.AreEqual("advancement revoke @a from space:adv", new AdvancementSomeCommand(ID.Selector.a, new EmptyAdvancement(packNamespace, "adv"), ID.RelativeAdvancement.from, false).GetCommandString());
            Assert.AreEqual("advancement grant @a through space:adv", new AdvancementSomeCommand(ID.Selector.a, new EmptyAdvancement(packNamespace, "adv"), ID.RelativeAdvancement.through, true).GetCommandString());
            Assert.AreEqual("advancement revoke @a through space:adv", new AdvancementSomeCommand(ID.Selector.a, new EmptyAdvancement(packNamespace, "adv"), ID.RelativeAdvancement.through, false).GetCommandString());
            Assert.AreEqual("advancement grant @a until space:adv", new AdvancementSomeCommand(ID.Selector.a, new EmptyAdvancement(packNamespace, "adv"), ID.RelativeAdvancement.until, true).GetCommandString());
            Assert.AreEqual("advancement revoke @a until space:adv", new AdvancementSomeCommand(ID.Selector.a, new EmptyAdvancement(packNamespace, "adv"), ID.RelativeAdvancement.until, false).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new AdvancementSomeCommand(null, new EmptyAdvancement(packNamespace, "adv"), ID.RelativeAdvancement.from, true));
            Assert.ThrowsException<ArgumentNullException>(() => new AdvancementSomeCommand(ID.Selector.a, null, ID.RelativeAdvancement.until, true));
        }
    }
}
