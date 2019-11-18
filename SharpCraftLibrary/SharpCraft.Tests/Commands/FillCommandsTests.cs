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
    public class FillCommandsTests
    {
        [TestMethod]
        public void FillCommandTest()
        {
            Assert.AreEqual("fill ~1 ~2 ~3 ~4 ~5 ~6 minecraft:stone", new FillCommand(new Coords(1, 2, 3), new Coords(4, 5, 6), ID.Block.stone, ID.BlockFill.replace).GetCommandString());
            Assert.AreEqual("fill ~1 ~2 ~3 ~4 ~5 ~6 minecraft:stone hollow", new FillCommand(new Coords(1, 2, 3), new Coords(4, 5, 6), ID.Block.stone, ID.BlockFill.hollow).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new FillCommand(null, new Coords(), ID.Block.stone, ID.BlockFill.destroy));
            Assert.ThrowsException<ArgumentNullException>(() => new FillCommand(new Coords(), null, ID.Block.stone, ID.BlockFill.destroy));
            Assert.ThrowsException<ArgumentNullException>(() => new FillCommand(new Coords(), new Coords(), null, ID.BlockFill.destroy));
        }

        [TestMethod]
        public void FillReplaceCommandTest()
        {
            Assert.AreEqual("fill ~1 ~2 ~3 ~4 ~5 ~6 minecraft:stone replace minecraft:dirt", new FillReplaceCommand(new Coords(1, 2, 3), new Coords(4, 5, 6), ID.Block.stone, ID.Block.dirt).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new FillReplaceCommand(null, new Coords(), ID.Block.stone, ID.Block.dirt));
            Assert.ThrowsException<ArgumentNullException>(() => new FillReplaceCommand(new Coords(), null, ID.Block.stone, ID.Block.dirt));
            Assert.ThrowsException<ArgumentNullException>(() => new FillReplaceCommand(new Coords(), new Coords(), null, ID.Block.dirt));
            Assert.ThrowsException<ArgumentNullException>(() => new FillReplaceCommand(new Coords(), new Coords(), ID.Block.stone, null));
        }
    }
}
