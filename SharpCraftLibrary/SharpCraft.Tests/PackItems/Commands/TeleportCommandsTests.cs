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
    public class TeleportCommandsTests
    {
        [TestMethod]
        public void TeleportToCommandTest()
        {
            Assert.AreEqual("tp @a ~1 ~2 ~3", new TeleportToCommand(new Coords(1,2,3), ID.Selector.a).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new TeleportToCommand(null!, ID.Selector.a));
            Assert.ThrowsException<ArgumentNullException>(() => new TeleportToCommand(new Coords(1, 2, 3), null!));
        }

        [TestMethod]
        public void TeleportToEntityCommandTest()
        {
            Assert.AreEqual("tp @a @s", new TeleportToEntityCommand(ID.Selector.a, ID.Selector.s).GetCommandString());

            Assert.ThrowsException<ArgumentException>(() => new TeleportToEntityCommand(ID.Selector.a, ID.Selector.a));
            Assert.ThrowsException<ArgumentNullException>(() => new TeleportToEntityCommand(null!, ID.Selector.s));
            Assert.ThrowsException<ArgumentNullException>(() => new TeleportToEntityCommand(ID.Selector.a, null!));
        }

        [TestMethod]
        public void TeleportToFacingCommandTest()
        {
            Assert.AreEqual("tp @a ~1 ~2 ~3 facing ~4 ~5 ~6", new TeleportToFacingCommand(new Coords(1,2,3), ID.Selector.a, new Coords(4,5,6)).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new TeleportToFacingCommand(null!, ID.Selector.a, new Coords(4, 5, 6)));
            Assert.ThrowsException<ArgumentNullException>(() => new TeleportToFacingCommand(new Coords(1, 2, 3), null!, new Coords(4, 5, 6)));
            Assert.ThrowsException<ArgumentNullException>(() => new TeleportToFacingCommand(new Coords(1, 2, 3), ID.Selector.a, null!));
        }

        [TestMethod]
        public void TeleportToFacingEntityCommandTest()
        {
            Assert.AreEqual("tp @a ~1 ~2 ~3 facing entity @s eyes", new TeleportToFacingEntityCommand(new Coords(1,2,3), ID.Selector.a, ID.Selector.s, ID.FacingAnchor.eyes).GetCommandString());

            Assert.ThrowsException<ArgumentException>(() => new TeleportToFacingEntityCommand(new Coords(1, 2, 3), ID.Selector.a, ID.Selector.a, ID.FacingAnchor.eyes));
            Assert.ThrowsException<ArgumentNullException>(() => new TeleportToFacingEntityCommand(null!, ID.Selector.a, ID.Selector.s, ID.FacingAnchor.eyes));
            Assert.ThrowsException<ArgumentNullException>(() => new TeleportToFacingEntityCommand(new Coords(1, 2, 3), null!, ID.Selector.s, ID.FacingAnchor.eyes));
            Assert.ThrowsException<ArgumentNullException>(() => new TeleportToFacingEntityCommand(new Coords(1, 2, 3), ID.Selector.a, null!, ID.FacingAnchor.eyes));
        }

        [TestMethod]
        public void TeleportToRotationCommandTest()
        {
            Assert.AreEqual("tp @a ~1 ~2 ~3 ~1 2", new TeleportToRotationCommand(new Coords(1,2,3), ID.Selector.a, new Rotation(1,2,true,false)).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new TeleportToRotationCommand(null!, ID.Selector.a, new Rotation(1, 2, true, false)));
            Assert.ThrowsException<ArgumentNullException>(() => new TeleportToRotationCommand(new Coords(1, 2, 3), null!, new Rotation(1, 2, true, false)));
            Assert.ThrowsException<ArgumentNullException>(() => new TeleportToRotationCommand(new Coords(1, 2, 3), ID.Selector.a, null!));
        }
    }
}
