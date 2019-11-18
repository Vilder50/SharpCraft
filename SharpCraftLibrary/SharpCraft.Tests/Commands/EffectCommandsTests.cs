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
    public class EffectCommandsTests
    {
        [TestMethod]
        public void EffectGiveCommandTest()
        {
            Assert.AreEqual("effect give @s speed 20 5 true", new EffectGiveCommand(ID.Selector.s, ID.Effect.speed, 20, 5, true).GetCommandString());
            Assert.AreEqual("effect give @s speed 20 5", new EffectGiveCommand(ID.Selector.s, ID.Effect.speed, 20, 5, false).GetCommandString());
            Assert.AreEqual("effect give @s speed 20", new EffectGiveCommand(ID.Selector.s, ID.Effect.speed, 20, 0, false).GetCommandString());
            Assert.AreEqual("effect give @s speed", new EffectGiveCommand(ID.Selector.s, ID.Effect.speed, 30, 0, false).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new EffectGiveCommand(null, ID.Effect.speed, 10, 10, true));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new EffectGiveCommand(ID.Selector.s, ID.Effect.speed, -1, 10, true));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new EffectGiveCommand(ID.Selector.s, ID.Effect.speed, 10000000, 10, true));
        }

        [TestMethod]
        public void EffectClearCommandTest()
        {
            Assert.AreEqual("effect clear @s speed", new EffectClearCommand(ID.Selector.s, ID.Effect.speed).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new EffectClearCommand(null, ID.Effect.speed));
        }
    }
}
