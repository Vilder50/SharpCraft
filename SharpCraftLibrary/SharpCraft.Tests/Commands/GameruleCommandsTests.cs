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
    public class GameruleCommandsTests
    {
        [TestMethod]
        public void GameruleSetBoolCommandTest()
        {
            Assert.AreEqual("gamerule commandBlockOutput true", new GameruleSetBoolCommand(ID.BoolGamerule.commandBlockOutput, true).GetCommandString());
            Assert.AreEqual("gamerule commandBlockOutput false", new GameruleSetBoolCommand(ID.BoolGamerule.commandBlockOutput, false).GetCommandString());
            Assert.AreEqual("gamerule commandBlockOutput", new GameruleSetBoolCommand(ID.BoolGamerule.commandBlockOutput, null).GetCommandString());
        }

        [TestMethod]
        public void GameruleSetIntTest()
        {
            Assert.AreEqual("gamerule randomTickSpeed 0", new GameruleSetIntCommand(ID.IntGamerule.randomTickSpeed, 0).GetCommandString());
            Assert.AreEqual("gamerule randomTickSpeed", new GameruleSetIntCommand(ID.IntGamerule.randomTickSpeed, null).GetCommandString());

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new GameruleSetIntCommand(ID.IntGamerule.spawnRadius, -1));
        }
    }
}
