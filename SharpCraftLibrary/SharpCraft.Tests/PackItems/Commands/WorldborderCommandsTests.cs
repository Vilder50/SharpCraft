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
    public class WorldborderCommandsTests
    {
        [TestMethod]
        public void WorldborderSizeCommandTest()
        {
            Assert.AreEqual("worldborder add -1.1 400", new WorldborderSizeCommand(-1.1, ID.AddSetModifier.add, new NoneNegativeTime<int>(20, ID.TimeType.seconds)).GetCommandString());
            Assert.AreEqual("worldborder set 1.1", new WorldborderSizeCommand(1.1, ID.AddSetModifier.set, null).GetCommandString());

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new WorldborderSizeCommand(-0.9, ID.AddSetModifier.set, null));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new WorldborderSizeCommand(60_000_001, ID.AddSetModifier.set, null));
        }

        [TestMethod]
        public void WorldborderCenterCommandTest()
        {
            Assert.AreEqual("worldborder center ~1 ~3", new WorldborderCenterCommand(new Coords(1,2,3)).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new WorldborderCenterCommand(null!));
        }

        [TestMethod]
        public void WorldborderDamageAmountCommandTest()
        {
            Assert.AreEqual("worldborder damage amount 1.1", new WorldborderDamageAmountCommand(1.1).GetCommandString());

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new WorldborderDamageAmountCommand(-0.1));
        }

        [TestMethod]
        public void WorldborderDamageBufferCommandTest()
        {
            Assert.AreEqual("worldborder damage buffer 1.1", new WorldborderDamageBufferCommand(1.1).GetCommandString());

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new WorldborderDamageBufferCommand(-0.1));
        }

        [TestMethod]
        public void WorldborderGetCommandTest()
        {
            Assert.AreEqual("worldborder get", new WorldborderGetCommand().GetCommandString());
        }

        [TestMethod]
        public void WorldborderWarningDistanceCommandTest()
        {
            Assert.AreEqual("worldborder warning distance 1", new WorldborderWarningDistanceCommand(1).GetCommandString());

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new WorldborderWarningDistanceCommand(-1));
        }

        [TestMethod]
        public void WorldborderWarningTimeCommandTest()
        {
            Assert.AreEqual("worldborder warning time 20", new WorldborderWarningTimeCommand(new NoneNegativeTime<int>(1, ID.TimeType.seconds)).GetCommandString());

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new WorldborderWarningTimeCommand(-1));
        }
    }
}
