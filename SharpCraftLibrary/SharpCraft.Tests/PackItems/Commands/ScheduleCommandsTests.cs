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
    public class ScheduleCommandsTests
    {
        [TestMethod]
        public void ScheduleAddTest()
        {
            using MockDatapack datapack = new MockDatapack("pack");
            FileMocks.MockFunction function = new FileMocks.MockFunction(datapack.Namespace("space"), "function");
            Assert.AreEqual("schedule function space:function 100d", new ScheduleAddCommand(function, new NoneNegativeTime<int>(100, ID.TimeType.days)).GetCommandString());
            Assert.AreEqual("schedule function space:function 100d append", new ScheduleAddCommand(function, new NoneNegativeTime<int>(100, ID.TimeType.days), true).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new ScheduleAddCommand(null!, new NoneNegativeTime<int>(100, ID.TimeType.days)));
            Assert.ThrowsException<ArgumentNullException>(() => new ScheduleAddCommand(function, null!));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ScheduleAddCommand(function, -10));
        }

        [TestMethod]
        public void ScheduleClearTest()
        {
            using MockDatapack datapack = new MockDatapack("pack");
            FileMocks.MockFunction function = new FileMocks.MockFunction(datapack.Namespace("space"), "function");
            Assert.AreEqual("schedule clear space:function", new ScheduleClearCommand(function).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new ScheduleClearCommand(null!));
        }
    }
}
