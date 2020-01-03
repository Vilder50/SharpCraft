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
    public class TimeCommandsTests
    {
        [TestMethod]
        public void TimeModifyCommandTest()
        {
            Assert.AreEqual("time set 1000t", new TimeModifyCommand(1000, ID.AddSetModifier.set).GetCommandString());
            Assert.AreEqual("time add 1s", new TimeModifyCommand(new Time(1, ID.TimeType.seconds), ID.AddSetModifier.add).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new TimeModifyCommand(null, ID.AddSetModifier.add));
        }

        [TestMethod]
        public void TimeQueryCommandTest()
        {
            Assert.AreEqual("time query daytime", new TimeQueryCommand(ID.QueryTime.daytime).GetCommandString());
        }
    }
}
