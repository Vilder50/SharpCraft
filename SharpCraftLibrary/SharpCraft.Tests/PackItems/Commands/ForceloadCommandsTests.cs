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
    public class ForceloadCommandsTests
    {
        [TestMethod]
        public void ForceloadChunkCommandTest()
        {
            Assert.AreEqual("forceload add ~1 ~3", new ForceloadChunkCommand(new Coords(1, 2, 3), true).GetCommandString());
            Assert.AreEqual("forceload remove ^1 ^3", new ForceloadChunkCommand(new LocalCoords(1, 2, 3), false).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new ForceloadChunkCommand(null!, true));
        }

        [TestMethod]
        public void ForceloadChunksCommandTest()
        {
            Assert.AreEqual("forceload add ~1 ~3 ~4 ~6", new ForceloadChunksCommand(new Coords(1, 2, 3), new Coords(4, 5, 6), true).GetCommandString());
            Assert.AreEqual("forceload remove ~1 ~3 ~4 ~6", new ForceloadChunksCommand(new Coords(1, 2, 3), new Coords(4, 5, 6), false).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new ForceloadChunksCommand(null!, new Coords(), true));
            Assert.ThrowsException<ArgumentNullException>(() => new ForceloadChunksCommand(new Coords(), null!, true));
        }

        [TestMethod]
        public void ForceloadRemoveAllCommandTest()
        {
            Assert.AreEqual("forceload remove all", new ForceloadRemoveAllCommand().GetCommandString());
        }

        [TestMethod]
        public void ForceloadQueryCommandTest()
        {
            Assert.AreEqual("forceload query", new ForceloadQueryCommand().GetCommandString());
        }

        [TestMethod]
        public void ForceloadQueryChunkTest()
        {
            Assert.AreEqual("forceload query ~1 ~3", new ForceloadQueryChunkCommand(new Coords(1,2,3)).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new ForceloadQueryChunkCommand(null!));
        }
    }
}
