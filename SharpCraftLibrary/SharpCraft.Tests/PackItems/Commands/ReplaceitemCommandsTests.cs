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
    public class ReplaceitemCommandsTests
    {
        [TestMethod]
        public void ReplaceitemBlockCommandTest()
        {
            Assert.AreEqual("replaceitem block ~0 ~0 ~0 container.5 minecraft:dirt{CustomModelData:1} 10", new ReplaceitemBlockCommand(new Coords(), new Slots.ContainerSlot(5), new Item(ID.Item.dirt) { CustomModelData = 1 }, 10).GetCommandString());

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReplaceitemBlockCommand(new Coords(), new Slots.ContainerSlot(5), new Item(ID.Item.dirt) { CustomModelData = 1 }, 0));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReplaceitemBlockCommand(new Coords(), new Slots.ContainerSlot(5), new Item(ID.Item.dirt) { CustomModelData = 1 }, 65));
            Assert.ThrowsException<ArgumentNullException>(() => new ReplaceitemBlockCommand(null, new Slots.ContainerSlot(5), new Item(ID.Item.dirt) { CustomModelData = 1 }, 10));
            Assert.ThrowsException<ArgumentNullException>(() => new ReplaceitemBlockCommand(new Coords(), null, new Item(ID.Item.dirt) { CustomModelData = 1 }, 10));
            Assert.ThrowsException<ArgumentNullException>(() => new ReplaceitemBlockCommand(new Coords(), new Slots.ContainerSlot(5), null, 10));
        }

        [TestMethod]
        public void ReplaceitemEntityCommandTest()
        {
            Assert.AreEqual("replaceitem entity @s hotbar.5 minecraft:dirt{CustomModelData:1} 10", new ReplaceitemEntityCommand(ID.Selector.s, new Slots.HotbarSlot(5), new Item(ID.Item.dirt) { CustomModelData = 1 }, 10).GetCommandString());

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReplaceitemEntityCommand(ID.Selector.s, new Slots.ContainerSlot(5), new Item(ID.Item.dirt) { CustomModelData = 1 }, 0));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReplaceitemEntityCommand(ID.Selector.s, new Slots.ContainerSlot(5), new Item(ID.Item.dirt) { CustomModelData = 1 }, 65));
            Assert.ThrowsException<ArgumentNullException>(() => new ReplaceitemEntityCommand(null, new Slots.ContainerSlot(5), new Item(ID.Item.dirt) { CustomModelData = 1 }, 10));
            Assert.ThrowsException<ArgumentNullException>(() => new ReplaceitemEntityCommand(ID.Selector.s, null, new Item(ID.Item.dirt) { CustomModelData = 1 }, 10));
            Assert.ThrowsException<ArgumentNullException>(() => new ReplaceitemEntityCommand(ID.Selector.s, new Slots.ContainerSlot(5), null, 10));
        }
    }
}
