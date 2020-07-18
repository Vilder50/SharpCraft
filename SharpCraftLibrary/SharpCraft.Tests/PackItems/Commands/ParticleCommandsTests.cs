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
    public class ParticleCommandsTests
    {
        [TestMethod]
        public void ParticleNormalCommandTest()
        {
            Assert.AreEqual("particle barrier ~ ~ ~ 2 3 4 1.2 10 force @s", new ParticleNormalCommand(ID.Particle.barrier, new Coords(0, 0, 0), new Coords(2, 3, 4), 1.2, 10, true, ID.Selector.s).GetCommandString());
            Assert.AreEqual("particle barrier ~ ~ ~ 2 2 2 1 10 force", new ParticleNormalCommand(ID.Particle.barrier, new Coords(0, 0, 0), new Coords(2, 2, 2), 1, 10, true, null).GetCommandString());
            Assert.AreEqual("particle barrier ~ ~ ~ 2.1 2.1 2.1 1 10", new ParticleNormalCommand(ID.Particle.barrier, new Coords(0, 0, 0), new Coords(2.1, 2.1, 2.1), 1, 10, false, null).GetCommandString());

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ParticleNormalCommand(ID.Particle.barrier, new Coords(), new Coords(), -1, 0, true, null));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ParticleNormalCommand(ID.Particle.barrier, new Coords(), new Coords(), 0, -1, true, null));
            Assert.ThrowsException<ArgumentNullException>(() => new ParticleNormalCommand(ID.Particle.barrier, null!, new Coords(), 0, 0, true, null));
            Assert.ThrowsException<ArgumentNullException>(() => new ParticleNormalCommand(ID.Particle.barrier, new Coords(), null!, 0, 0, true, null));
        }

        [TestMethod]
        public void ParticleColoredDustTest()
        {
            Assert.AreEqual("particle dust 0.4980392156862745098039215686 1 0.4980392156862745098039215686 3.2 ~ ~ ~ 2 3 4 1.2 5 force @s", new ParticleColoredDustCommand(new RGBColor(127, 255, 127), 3.2, new Coords(0, 0, 0), new Coords(2, 3, 4), 1.2, 5, true, ID.Selector.s).GetCommandString());
            Assert.AreEqual("particle dust 1 1 1 3.2 ~ ~ ~ 2 3 4 1.2 5 force", new ParticleColoredDustCommand(new RGBColor(255, 255, 255), 3.2, new Coords(0, 0, 0), new Coords(2, 3, 4), 1.2, 5, true, null).GetCommandString());
            Assert.AreEqual("particle dust 1 1 1 3.2 ~ ~ ~ 2.1 3.1 4.1 1.2 5", new ParticleColoredDustCommand(new RGBColor(255, 255, 255), 3.2, new Coords(0, 0, 0), new Coords(2.1, 3.1, 4.1), 1.2, 5, false, null).GetCommandString());

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ParticleColoredDustCommand(new RGBColor(255, 255, 255), 3.2, new Coords(0, 0, 0), new Coords(2, 3, 4), -1, 5, false, null));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ParticleColoredDustCommand(new RGBColor(255, 255, 255), 3.2, new Coords(0, 0, 0), new Coords(2, 3, 4), 1.2, -1, false, null));
            Assert.ThrowsException<ArgumentNullException>(() => new ParticleColoredDustCommand(null!, 3.2, new Coords(0, 0, 0), new Coords(2, 3, 4), 1.2, -1, false, null));
            Assert.ThrowsException<ArgumentNullException>(() => new ParticleColoredDustCommand(new RGBColor(255, 255, 255), 3.2, null!, new Coords(2, 3, 4), 1.2, -1, false, null));
            Assert.ThrowsException<ArgumentNullException>(() => new ParticleColoredDustCommand(new RGBColor(255, 255, 255), 3.2, new Coords(0, 0, 0), null!, 1.2, -1, false, null));
        }

        [TestMethod]
        public void ParticleBlockTest()
        {
            Assert.AreEqual("particle falling_dust minecraft:anvil[facing=east] ~ ~ ~ 2 2 2 0.2 5 force @s", new ParticleBlockCommand(new Blocks.Anvil(ID.Block.anvil) { SFacing = ID.Facing.east }, new Coords(), new Coords(2, 2, 2), 0.2, 5, true, true, ID.Selector.s).GetCommandString());
            Assert.AreEqual("particle falling_dust minecraft:anvil[facing=east] ~ ~ ~ 2 2 2 0.2 5 force", new ParticleBlockCommand(new Blocks.Anvil(ID.Block.anvil) { SFacing = ID.Facing.east }, new Coords(), new Coords(2, 2, 2), 0.2, 5, true, true, null).GetCommandString());
            Assert.AreEqual("particle falling_dust minecraft:anvil[facing=east] ~ ~ ~ 2 2 2 0.2 5", new ParticleBlockCommand(new Blocks.Anvil(ID.Block.anvil) { SFacing = ID.Facing.east }, new Coords(), new Coords(2, 2, 2), 0.2, 5, true, false, null).GetCommandString());
            Assert.AreEqual("particle block minecraft:anvil[facing=east] ~ ~ ~ 2.1 2.1 2.1 0.2 5", new ParticleBlockCommand(new Blocks.Anvil(ID.Block.anvil) { SFacing = ID.Facing.east }, new Coords(), new Coords(2.1, 2.1, 2.1), 0.2, 5, false, false, null).GetCommandString());

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ParticleBlockCommand(new Blocks.Anvil(ID.Block.anvil) { SFacing = ID.Facing.east }, new Coords(), new Coords(2, 2, 2), -1, 5, true, true, ID.Selector.s));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ParticleBlockCommand(new Blocks.Anvil(ID.Block.anvil) { SFacing = ID.Facing.east }, new Coords(), new Coords(2, 2, 2), 0.2, -1, true, true, ID.Selector.s));
            Assert.ThrowsException<ArgumentNullException>(() => new ParticleBlockCommand(null!, new Coords(), new Coords(2, 2, 2), 0.2, 5, true, true, ID.Selector.s));
            Assert.ThrowsException<ArgumentNullException>(() => new ParticleBlockCommand(new Blocks.Anvil(ID.Block.anvil) { SFacing = ID.Facing.east }, null!, new Coords(2, 2, 2), 0.2, 5, true, true, ID.Selector.s));
            Assert.ThrowsException<ArgumentNullException>(() => new ParticleBlockCommand(new Blocks.Anvil(ID.Block.anvil) { SFacing = ID.Facing.east }, new Coords(), null!, 0.2, 5, true, true, ID.Selector.s));
        }

        [TestMethod]
        public void ParticleItemTest()
        {
            Assert.AreEqual("particle item minecraft:stone{CustomModelData:1} ~ ~ ~ 2 3 4 0.2 10 force @s", new ParticleItemCommand(new Item(ID.Item.stone) {CustomModelData = 1 }, new Coords(), new Coords(2, 3, 4), 0.2 , 10, true, ID.Selector.s).GetCommandString());
            Assert.AreEqual("particle item minecraft:stone{CustomModelData:1} ~ ~ ~ 2 3 4 0.2 10 force", new ParticleItemCommand(new Item(ID.Item.stone) { CustomModelData = 1 }, new Coords(), new Coords(2, 3, 4), 0.2, 10, true, null).GetCommandString());
            Assert.AreEqual("particle item minecraft:stone{CustomModelData:1} ~ ~ ~ 2.1 3.1 4.1 0.2 10", new ParticleItemCommand(new Item(ID.Item.stone) { CustomModelData = 1 }, new Coords(), new Coords(2.1, 3.1, 4.1), 0.2, 10, false, null).GetCommandString());

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ParticleItemCommand(new Item(ID.Item.stone) { CustomModelData = 1 }, new Coords(), new Coords(2, 3, 4), -1, 10, false, null));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ParticleItemCommand(new Item(ID.Item.stone) { CustomModelData = 1 }, new Coords(), new Coords(2, 3, 4), 0.2, -1, false, null));
            Assert.ThrowsException<ArgumentNullException>(() => new ParticleItemCommand(null!, new Coords(), new Coords(2, 3, 4), 0.2, 10, true, ID.Selector.s));
            Assert.ThrowsException<ArgumentNullException>(() => new ParticleItemCommand(new Item(ID.Item.stone) { CustomModelData = 1 }, null!, new Coords(2, 3, 4), 0.2, 10, true, ID.Selector.s));
            Assert.ThrowsException<ArgumentNullException>(() => new ParticleItemCommand(new Item(ID.Item.stone) { CustomModelData = 1 }, new Coords(), null!, 0.2, 10, true, ID.Selector.s));
        }
    }
}
