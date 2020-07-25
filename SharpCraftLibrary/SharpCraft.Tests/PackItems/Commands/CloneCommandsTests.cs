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
    public class CloneCommandsTests
    {
        [TestMethod]
        public void CloneCommandTest()
        {
            Assert.AreEqual("clone 0 0 0 1 1 1 2 2 2 replace force", new CloneCommand(new Coords(false, 0, 0, 0), new Coords(false, 1, 1, 1), new Coords(false, 2, 2, 2), false, ID.BlockCloneWay.force).GetCommandString());
            Assert.AreEqual("clone 0 0 0 1 1 1 2 2 2 masked move", new CloneCommand(new Coords(false, 0, 0, 0), new Coords(false, 1, 1, 1), new Coords(false, 2, 2, 2), true, ID.BlockCloneWay.move).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new CloneCommand(null!, new Coords(false, 1, 1, 1), new Coords(false, 2, 2, 2), false, ID.BlockCloneWay.force));
            Assert.ThrowsException<ArgumentNullException>(() => new CloneCommand(new Coords(false, 0, 0, 0), null!, new Coords(false, 2, 2, 2), false, ID.BlockCloneWay.force));
            Assert.ThrowsException<ArgumentNullException>(() => new CloneCommand(new Coords(false, 0, 0, 0), new Coords(false, 1, 1, 1), null!, false, ID.BlockCloneWay.force));
        }

        [TestMethod]
        public void FilteredCloneCommand()
        {
            Assert.AreEqual("clone 0 0 0 1 1 1 2 2 2 filtered minecraft:stone normal", new FilteredCloneCommand(new Coords(false, 0, 0, 0), new Coords(false, 1, 1, 1), new Coords(false, 2, 2, 2), ID.Block.stone, ID.BlockCloneWay.normal).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new FilteredCloneCommand(null!, new Coords(false, 1, 1, 1), new Coords(false, 2, 2, 2), ID.Block.stone, ID.BlockCloneWay.normal));
            Assert.ThrowsException<ArgumentNullException>(() => new FilteredCloneCommand(new Coords(false, 0, 0, 0), null!, new Coords(false, 2, 2, 2), ID.Block.stone, ID.BlockCloneWay.normal));
            Assert.ThrowsException<ArgumentNullException>(() => new FilteredCloneCommand(new Coords(false, 0, 0, 0), new Coords(false, 1, 1, 1), null!, ID.Block.stone, ID.BlockCloneWay.normal));
            Assert.ThrowsException<ArgumentNullException>(() => new FilteredCloneCommand(new Coords(false, 0, 0, 0), new Coords(false, 1, 1, 1), new Coords(false, 2, 2, 2), null!, ID.BlockCloneWay.normal));
        }
    }
}
