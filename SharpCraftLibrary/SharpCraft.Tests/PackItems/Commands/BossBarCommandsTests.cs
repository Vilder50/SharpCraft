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
    public class BossBarCommandsTests
    {
        private BossBar GetBar()
        {
            EmptyNamespace space = EmptyNamespace.GetNamespace("boss");
            return new BossBar(space, "name");
        }

        [TestMethod]
        public void BossBarAddCommandTest()
        {
            Assert.AreEqual("bossbar add boss:name {\"text\":\"hello\"}", new BossBarAddCommand(GetBar(), new JsonText.Text("hello")).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new BossBarAddCommand(GetBar(), null!));
            Assert.ThrowsException<ArgumentNullException>(() => new BossBarAddCommand(null!, new JsonText.Text("hello")));
        }

        [TestMethod]
        public void BossBarGetValueCommandTest()
        {
            Assert.AreEqual("bossbar get boss:name max", new BossBarGetValueCommand(GetBar(), ID.BossBarValue.max).GetCommandString());
            Assert.AreEqual("bossbar get boss:name players", new BossBarGetValueCommand(GetBar(), ID.BossBarValue.players).GetCommandString());
            Assert.AreEqual("bossbar get boss:name value", new BossBarGetValueCommand(GetBar(), ID.BossBarValue.value).GetCommandString());
            Assert.AreEqual("bossbar get boss:name visible", new BossBarGetValueCommand(GetBar(), ID.BossBarValue.visible).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new BossBarGetValueCommand(null!, ID.BossBarValue.max));
        }

        [TestMethod]
        public void BossBarGetAllCommandTest()
        {
            Assert.AreEqual("bossbar list", new BossBarGetAllCommand().GetCommandString());
        }

        [TestMethod]
        public void BossBarRemoveCommandTest()
        {
            Assert.AreEqual("bossbar remove boss:name", new BossBarRemoveCommand(GetBar()).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new BossBarRemoveCommand(null!));
        }

        [TestMethod]
        public void BossBarChangeColorCommandTest()
        {
            Assert.AreEqual("bossbar set boss:name color green", new BossBarChangeColorCommand(GetBar(), ID.BossBarColor.green).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new BossBarChangeColorCommand(null!, ID.BossBarColor.blue));
        }

        [TestMethod]
        public void BossBarChangeMaxValueCommandTest()
        {
            Assert.AreEqual("bossbar set boss:name max 10", new BossBarChangeMaxValueCommand(GetBar(), 10).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new BossBarChangeMaxValueCommand(null!, 100));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new BossBarChangeMaxValueCommand(GetBar(), 0));
        }

        [TestMethod]
        public void BossBarChangeValueCommandTest()
        {
            Assert.AreEqual("bossbar set boss:name value 10", new BossBarChangeValueCommand(GetBar(), 10).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new BossBarChangeValueCommand(null!, 100));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new BossBarChangeValueCommand(GetBar(), -1));
        }

        [TestMethod]
        public void BossBarChangeNameCommandTest()
        {
            Assert.AreEqual("bossbar set boss:name name {\"text\":\"hello\"}", new BossBarChangeNameCommand(GetBar(), new JsonText.Text("hello")).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new BossBarChangeNameCommand(GetBar(), null!));
            Assert.ThrowsException<ArgumentNullException>(() => new BossBarChangeNameCommand(null!, new JsonText.Text("hello")));
        }

        [TestMethod]
        public void BossBarChangePlayersCommandTest()
        {
            Assert.AreEqual("bossbar set boss:name players @a", new BossBarChangePlayersCommand(GetBar(), ID.Selector.a).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new BossBarChangePlayersCommand(GetBar(), null!));
            Assert.ThrowsException<ArgumentNullException>(() => new BossBarChangePlayersCommand(null!, ID.Selector.a));
        }

        [TestMethod]
        public void BossBarChangeStyleCommandTest()
        {
            Assert.AreEqual("bossbar set boss:name style notched_6", new BossBarChangeStyleCommand(GetBar(), ID.BossBarStyle.notched_6).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new BossBarChangeStyleCommand(null!, ID.BossBarStyle.notched_12));
        }

        [TestMethod]
        public void BossBarChangeVisibilityCommandTest()
        {
            Assert.AreEqual("bossbar set boss:name visible true", new BossBarChangeVisibilityCommand(GetBar(), true).GetCommandString());
            Assert.AreEqual("bossbar set boss:name visible false", new BossBarChangeVisibilityCommand(GetBar(), false).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new BossBarChangeVisibilityCommand(null!, false));
        }
    }
}
