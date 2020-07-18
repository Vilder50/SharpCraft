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
    public class SpectateCommandsTests
    {
        [TestMethod]
        public void SpectateTest()
        {
            Assert.AreEqual("spectate @s @s", new SpectateCommand(ID.Selector.s, ID.Selector.s).GetCommandString());

            Assert.ThrowsException<ArgumentException>(() => new SpectateCommand(ID.Selector.a, ID.Selector.s));
            Assert.ThrowsException<ArgumentException>(() => new SpectateCommand(ID.Selector.s, ID.Selector.a));
            Assert.ThrowsException<ArgumentNullException>(() => new SpectateCommand(null!, ID.Selector.s));
            Assert.ThrowsException<ArgumentNullException>(() => new SpectateCommand(ID.Selector.s, null!));
        }

        [TestMethod]
        public void SpectateStopTest()
        {
            Assert.AreEqual("spectate", new SpectateStopCommand().GetCommandString());
        }
    }
}
