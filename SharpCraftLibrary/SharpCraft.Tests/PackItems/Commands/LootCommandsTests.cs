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
    public class LootCommandsTests
    {
        //TODO add tests for the parts of the loot command

        [TestMethod]
        public void LootCommandTest()
        {
            Assert.AreEqual("loot replace block ~1 ~2 ~3 container.20 kill @s", new LootCommand(new LootTargets.BlockTarget(new Coords(1,2,3), new Slots.ContainerSlot(20)), new LootSources.KillSource(ID.Selector.s)).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new LootCommand(null!, new LootSources.KillSource(ID.Selector.s)));
            Assert.ThrowsException<ArgumentNullException>(() => new LootCommand(new LootTargets.BlockTarget(new Vector(1, 2, 3), new Slots.ContainerSlot(20)), null!));
        }

        [TestMethod]
        public void SpawnTargetPartTest()
        {
            Assert.AreEqual("spawn ~1 ~2 ~3", new LootTargets.SpawnTarget(new Coords(1,2,3)).GetTargetString());

            Assert.ThrowsException<ArgumentNullException>(() => new LootTargets.SpawnTarget(null!));
        }

        [TestMethod]
        public void EntityTargetPartTest()
        {
            Assert.AreEqual("replace entity @s enderchest.2", new LootTargets.EntityTarget(ID.Selector.s, new Slots.EnderChestSlot(2)).GetTargetString());

            Assert.ThrowsException<ArgumentNullException>(() => new LootTargets.EntityTarget(null!, new Slots.EnderChestSlot(2)));
            Assert.ThrowsException<ArgumentNullException>(() => new LootTargets.EntityTarget(ID.Selector.s, null!));
        }

        [TestMethod]
        public void BlockTargetPartTest()
        {
            Assert.AreEqual("replace block ~1 ~2 ~3 container.2", new LootTargets.BlockTarget(new Coords(1,2,3), new Slots.ContainerSlot(2)).GetTargetString());

            Assert.ThrowsException<ArgumentNullException>(() => new LootTargets.BlockTarget(null!, new Slots.ContainerSlot(2)));
            Assert.ThrowsException<ArgumentNullException>(() => new LootTargets.BlockTarget(new Vector(1, 2, 3), null!));
        }

        [TestMethod]
        public void GiveTargetPartTest()
        {
            Assert.AreEqual("give @s", new LootTargets.GiveTarget(ID.Selector.s).GetTargetString());

            Assert.ThrowsException<ArgumentNullException>(() => new LootTargets.GiveTarget(null!));
        }

        [TestMethod]
        public void InsertTargetPartTest()
        {
            Assert.AreEqual("insert ~1 ~2 ~3", new LootTargets.InsertTarget(new Coords(1, 2, 3)).GetTargetString());

            Assert.ThrowsException<ArgumentNullException>(() => new LootTargets.InsertTarget(null!));
        }

        [TestMethod]
        public void FishHandSourcePartTest()
        {
            using MockDatapack datapack = new MockDatapack("pack");
            FileMocks.MockLootTable table = new FileMocks.MockLootTable(datapack.Namespace("test"), "loot");
            Assert.AreEqual("fish test:loot ~1 ~2 ~3 mainhand", new LootSources.FishHandSource(table, new Coords(1, 2, 3), true).GetSourceString());
            Assert.AreEqual("fish test:loot ~1 ~2 ~3 offhand", new LootSources.FishHandSource(table, new Coords(1, 2, 3), false).GetSourceString());

            Assert.ThrowsException<ArgumentNullException>(() => new LootSources.FishHandSource(null!, new Coords(1, 2, 3), true));
            Assert.ThrowsException<ArgumentNullException>(() => new LootSources.FishHandSource(table, null!, true));
        }

        [TestMethod]
        public void FishItemSourcePartTest()
        {
            using MockDatapack datapack = new MockDatapack("pack");
            FileMocks.MockLootTable table = new FileMocks.MockLootTable(datapack.Namespace("test"), "loot");
            Assert.AreEqual("fish test:loot ~1 ~2 ~3 minecraft:dirt", new LootSources.FishItemSource(table, new Coords(1, 2, 3), ID.Item.dirt).GetSourceString());

            Assert.ThrowsException<ArgumentNullException>(() => new LootSources.FishItemSource(null!, new Vector(1, 2, 3), ID.Item.dirt));
            Assert.ThrowsException<ArgumentNullException>(() => new LootSources.FishItemSource(table, null!, ID.Item.dirt));
            Assert.ThrowsException<ArgumentNullException>(() => new LootSources.FishItemSource(table, new Vector(1, 2, 3), null!));
        }

        [TestMethod]
        public void LoottableSourcePartTest()
        {
            using MockDatapack datapack = new MockDatapack("pack");
            FileMocks.MockLootTable table = new FileMocks.MockLootTable(datapack.Namespace("test"), "loot");
            Assert.AreEqual("loot test:loot", new LootSources.LoottableSource(table).GetSourceString());

            Assert.ThrowsException<ArgumentNullException>(() => new LootSources.LoottableSource(null!));
        }

        [TestMethod]
        public void KillSourcePartTest()
        {
            Assert.AreEqual("kill @s", new LootSources.KillSource(ID.Selector.s).GetSourceString());

            Assert.ThrowsException<ArgumentNullException>(() => new LootSources.KillSource(null!));
            Assert.ThrowsException<ArgumentException>(() => new LootSources.KillSource(ID.Selector.e));
        }

        [TestMethod]
        public void MineHandSourcePartTest()
        {
            Assert.AreEqual("mine ~1 ~2 ~3 mainhand", new LootSources.MineHandSource(new Coords(1, 2, 3), true).GetSourceString());
            Assert.AreEqual("mine ~1 ~2 ~3 offhand", new LootSources.MineHandSource(new Coords(1, 2, 3), false).GetSourceString());

            Assert.ThrowsException<ArgumentNullException>(() => new LootSources.MineHandSource(null!, true));
        }

        [TestMethod]
        public void MineItemSourcePartTest()
        {
            Assert.AreEqual("mine ~1 ~2 ~3 minecraft:dirt", new LootSources.MineItemSource(new Coords(1, 2, 3), ID.Item.dirt).GetSourceString());

            Assert.ThrowsException<ArgumentNullException>(() => new LootSources.MineItemSource(null!, ID.Item.dirt));
            Assert.ThrowsException<ArgumentNullException>(() => new LootSources.MineItemSource(new Coords(), null!));
        }
    }
}
