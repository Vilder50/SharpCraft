using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpCraft;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using SharpCraft.LootObjects;
using SharpCraft.Conditions;

namespace SharpCraft.Tests.PackItems
{
    [TestClass]
    public class LootTableTests
    {
        [TestMethod]
        public void TestLootTable()
        {
            //setup
            using (Datapack pack = new Datapack("datapacks", "pack", "a pack", 0, new NoneFileCreator()))
            {
                PackNamespace space = pack.Namespace("space");

                //test
                space.Loottable("myTable", new LootPool[] { new LootPool(new EmptyEntry(), 1) },null, BaseFile.WriteSetting.Auto);
                Assert.IsTrue(pack.FileCreator.GetDirectories().Any(d => d == "datapacks\\pack\\data\\space\\loot_tables\\"), "Directory wasn't created");
                Assert.IsTrue(pack.FileCreator.GetWriters().Any(w => w.path == "datapacks\\pack\\data\\space\\loot_tables\\mytable.json"), "File wasn't created");

                space.Loottable("folder/otherTable", new LootPool[] { new LootPool(new EmptyEntry(), 1) }, null, BaseFile.WriteSetting.OnDispose);
                Assert.IsFalse(pack.FileCreator.GetDirectories().Any(d => d == "datapacks\\pack\\data\\space\\loot_tables\\folder\\"), "Directory wasn't supposed to be created yet since its OnDispose");
                Assert.IsFalse(pack.FileCreator.GetWriters().Any(w => w.path == "datapacks\\pack\\data\\space\\loot_tables\\folder\\othertable.json"), "File wasn't supposed to be created yet since its OnDispose");

                pack.Dispose();
                Assert.IsTrue(pack.FileCreator.GetWriters().Any(w => w.path == "datapacks\\pack\\data\\space\\loot_tables\\folder\\othertable.json"), "File is supposed to have been created now since Dispose was ran");
                Assert.IsTrue(pack.FileCreator.GetDirectories().Any(d => d == "datapacks\\pack\\data\\space\\loot_tables\\folder\\"), "Directory wasn't created for file with directory in name");
            }
        }

        [TestMethod]
        public void TestWrite()
        {
            //setup
            using (Datapack pack = new Datapack("datapacks", "pack", "a pack", 0, new NoneFileCreator()))
            {
                PackNamespace space = pack.Namespace("space");

                //test
                space.Loottable("mytable", new LootPool(new ItemEntry(ID.Item.cobblestone)
                {
                    Weight = 5,
                    Changes = new BaseChange[]
                    {
                        new SmeltChange() { Conditions = new RandomCondition(0.5) }
                    }, Conditions = new RandomCondition(0.3) & !new RandomCondition(0.3)
                }, new Range(1,3)), LootTable.TableType.block, BaseFile.WriteSetting.Auto);
                TextWriter writer = pack.FileCreator.GetWriters().First(w => w.path == "datapacks\\pack\\data\\space\\loot_tables\\mytable.json").writer;

                Assert.AreEqual("{\"type\":\"block\",\"pools\":[" +
                        "{\"entries\":[" +
                            "{\"conditions\":[" +
                                "{\"condition\":\"minecraft:inverted\",\"term\":{\"condition\":\"minecraft:alternative\",\"terms\":[" +
                                    "{\"condition\":\"minecraft:inverted\",\"term\":{\"chance\":0.3,\"condition\":\"minecraft:random_chance\"}},{\"chance\":0.3,\"condition\":\"minecraft:random_chance\"}" +
                                "]}}" +
                            "],\"functions\":[" +
                                "{\"conditions\":[{\"chance\":0.5,\"condition\":\"minecraft:random_chance\"}],\"function\":\"minecraft:furnace_smelt\"}" +
                            "],\"name\":\"minecraft:cobblestone\",\"type\":\"minecraft:item\",\"weight\":5}" +
                        "],\"rolls\":{\"max\":3,\"min\":1}}" +
                    "]}", writer.ToString());
            }
        }

        [TestMethod]
        public void TestEmptyLoottable()
        {
            Assert.AreEqual("name:loot", new EmptyLoottable(EmptyDatapack.GetPack().Namespace("name"), "loot").GetNamespacedName(), "EmptyLoottable doesn't reutrn correct string");
            Assert.AreEqual("space:name", ((EmptyLoottable)"space:name").GetNamespacedName(), "Implicit string to loottable conversion converts incorrectly");
        }
    }
}
