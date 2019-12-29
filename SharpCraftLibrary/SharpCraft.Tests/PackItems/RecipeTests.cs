﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpCraft;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace SharpCraft.Tests.PackItems
{
    [TestClass]
    public class RecipeTests
    {
        [TestMethod]
        public void TestRecipe()
        {
            //setup
            using (Datapack pack = new Datapack("datapacks", "pack", "a pack", 0, new NoneFileCreator()))
            {
                PackNamespace space = pack.Namespace("space");

                //test
                space.Recipe("myrecipe");
                Assert.IsTrue(pack.FileCreator.GetDirectories().Any(d => d == "datapacks\\pack\\data\\space\\recipes\\"), "Directory wasn't created");
                Assert.IsTrue(pack.FileCreator.GetWriters().Any(w => w.path == "datapacks\\pack\\data\\space\\recipes\\myrecipe.json"), "File wasn't created");

                space.Recipe("folder/otherrecipe", SmeltRecipe.SmeltType.smelting, ID.Item.dirt, ID.Item.coarse_dirt, 2, null, null, BaseFile.WriteSetting.OnDispose);
                Assert.IsFalse(pack.FileCreator.GetDirectories().Any(d => d == "datapacks\\pack\\data\\space\\recipes\\folder\\"), "Directory wasn't supposed to be created yet since its OnDispose");
                Assert.IsFalse(pack.FileCreator.GetWriters().Any(w => w.path == "datapacks\\pack\\data\\space\\recipes\\folder\\otherrecipe.json"), "File wasn't supposed to be created yet since its OnDispose");

                pack.Dispose();
                Assert.IsTrue(pack.FileCreator.GetDirectories().Any(d => d == "datapacks\\pack\\data\\space\\recipes\\folder\\"), "Directory wasn't created for file with directory in name");
                Assert.IsTrue(pack.FileCreator.GetWriters().Any(w => w.path == "datapacks\\pack\\data\\space\\recipes\\folder\\otherrecipe.json"), "File is supposed to have been created now since Dispose was ran");
            }
        }

        [TestMethod]
        public void TestCraftingRecipe()
        {
            //setup
            using (Datapack pack = new Datapack("datapacks", "pack", "a pack", 0, new NoneFileCreator()))
            {
                PackNamespace space = pack.Namespace("space");
                ItemGroup woolGroup = space.Group("wool", new List<ItemType>() { ID.Item.white_wool, ID.Item.light_gray_wool });

                //test
                space.Recipe("recipe", new ItemType[,] 
                {
                    { ID.Item.String, null },
                    { ID.Item.slime_ball, woolGroup },
                    { ID.Item.String, ID.Item.air },
                }, ID.Item.cobweb, 8, "web");
                string recipeString = pack.FileCreator.GetWriters().Single(w => w.path == "datapacks\\pack\\data\\space\\recipes\\recipe.json").writer.ToString();
                Assert.AreEqual("{\"type\":\"minecraft:crafting_shaped\",\"group\":\"web\",\"pattern\":[\"0 \",\"12\",\"0 \"],\"key\":{\"0\":{\"item\":\"minecraft:string\"},\"1\":{\"item\":\"minecraft:slime_ball\"},\"2\":{\"tag\":\"space:wool\"}},\"result\":{\"item\":\"minecraft:cobweb\",\"count\":8}}", recipeString, "recipe file wasn't written correctly");

                //exceptions
                Assert.ThrowsException<ArgumentException>(() => space.Recipe("testrecipe1", new ItemType[,] { { ID.Item.stone } }, ID.Item.air, 1), "Recipe may not output air");
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => space.Recipe("testrecipe2", new ItemType[,] { { ID.Item.stone } }, ID.Item.stone, 0), "Item count under 1 shouldn't be allowed");
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => space.Recipe("testrecipe3", new ItemType[,] { { ID.Item.stone } }, ID.Item.stone, 65), "Item count over 64 shouldn't be allowed");
                Assert.ThrowsException<ArgumentException>(() => space.Recipe("testrecipe4", new ItemType[,] { { null, ID.Item.air } }, ID.Item.stone, 1), "Recipe can't be empty");
                Assert.ThrowsException<ArgumentNullException>(() => space.Recipe("testrecipe5", (ItemType[,])null, ID.Item.stone, 1), "Recipe can't be empty");
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => space.Recipe("testrecipe6", new ItemType[,] { { ID.Item.stone, ID.Item.stone, ID.Item.stone, ID.Item.stone } }, ID.Item.stone, 1), "Recipe may not be more than 3 wide");
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => space.Recipe("testrecipe7", new ItemType[,] { { ID.Item.stone }, { ID.Item.stone }, { ID.Item.stone }, { ID.Item.stone } }, ID.Item.stone, 1), "Recipe may not be more than 3 heigh");
            }
        }

        [TestMethod]
        public void TestShapelessRecipe()
        {
            //setup
            using (Datapack pack = new Datapack("datapacks", "pack", "a pack", 0, new NoneFileCreator()))
            {
                PackNamespace space = pack.Namespace("space");

                //test
                space.Recipe("recipe",new ItemType[] { ID.Item.dirt, ID.Item.gravel }, ID.Item.coarse_dirt, 1, null);
                string recipeString = pack.FileCreator.GetWriters().Single(w => w.path == "datapacks\\pack\\data\\space\\recipes\\recipe.json").writer.ToString();
                Assert.AreEqual("{\"type\":\"minecraft:crafting_shapeless\",\"ingredient\":[{\"item\":\"minecraft:dirt\"},{\"item\":\"minecraft:gravel\"}],\"result\":{\"item\":\"minecraft:coarse_dirt\"}}", recipeString, "recipe file wasn't written correctly");

                //exceptions
                Assert.ThrowsException<ArgumentException>(() => space.Recipe("testrecipe1", new ItemType[] { ID.Item.dirt, ID.Item.gravel }, ID.Item.air, 1, null), "Recipe may not output air");
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => space.Recipe("testrecipe2", new ItemType[] { ID.Item.dirt, ID.Item.gravel }, ID.Item.coarse_dirt, 0, null), "Item count under 1 shouldn't be allowed");
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => space.Recipe("testrecipe8", new ItemType[] { ID.Item.dirt, ID.Item.gravel }, ID.Item.coarse_dirt, 65, null), "Item count over 65 shouldn't be allowed");
                Assert.ThrowsException<ArgumentException>(() => space.Recipe("testrecipe3", new ItemType[] { ID.Item.dirt, ID.Item.air }, ID.Item.coarse_dirt, 1, null), "recipe may not contain air");
                Assert.ThrowsException<ArgumentNullException>(() => space.Recipe("testrecipe4", new ItemType[] { ID.Item.dirt, null }, ID.Item.coarse_dirt, 1, null), "recipe may not contain null");
                Assert.ThrowsException<ArgumentNullException>(() => space.Recipe("testrecipe5", (ItemType[])null, ID.Item.coarse_dirt, 1, null), "recipe may not be null");
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => space.Recipe("testrecipe6", new ItemType[] { }, ID.Item.coarse_dirt, 1, null), "Recipe has to have atleast 1 item");
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => space.Recipe("testrecipe7", new ItemType[] { ID.Item.dirt, ID.Item.dirt, ID.Item.dirt, ID.Item.dirt, ID.Item.dirt, ID.Item.dirt, ID.Item.dirt, ID.Item.dirt, ID.Item.dirt, ID.Item.dirt }, ID.Item.coarse_dirt, 1, null), "Recipe may not contain more than 9 item");
            }
        }

        [TestMethod]
        public void TestCuttingRecipe()
        {
            //setup
            using (Datapack pack = new Datapack("datapacks", "pack", "a pack", 0, new NoneFileCreator()))
            {
                PackNamespace space = pack.Namespace("space");

                //test
                space.Recipe("recipe1", new ItemType[] { ID.Item.oak_planks, ID.Item.oak_door }, ID.Item.oak_slab, 2, BaseFile.WriteSetting.LockedAuto);
                string recipeString = pack.FileCreator.GetWriters().Single(w => w.path == "datapacks\\pack\\data\\space\\recipes\\recipe1.json").writer.ToString();
                Assert.AreEqual("{\"type\":\"minecraft:stonecutting\",\"ingredient\":[{\"item\":\"minecraft:oak_planks\"},{\"item\":\"minecraft:oak_door\"}],\"result\":\"minecraft:oak_slab\",\"count\":2}", recipeString, "recipe file with multiple choises wasn't written correctly");

                space.Recipe("recipe2", ID.Item.oak_planks, ID.Item.oak_trapdoor, 1, BaseFile.WriteSetting.LockedAuto);
                recipeString = pack.FileCreator.GetWriters().Single(w => w.path == "datapacks\\pack\\data\\space\\recipes\\recipe2.json").writer.ToString();
                Assert.AreEqual("{\"type\":\"minecraft:stonecutting\",\"ingredient\":{\"item\":\"minecraft:oak_planks\"},\"result\":\"minecraft:oak_trapdoor\",\"count\":1}", recipeString, "recipe file with single choise wasn't written correctly");

                //exceptions
                Assert.ThrowsException<ArgumentException>(() => space.Recipe("testrecipe1", new ItemType[] { ID.Item.oak_planks, ID.Item.oak_door }, ID.Item.air, 2, BaseFile.WriteSetting.LockedAuto), "Recipe may not output air");
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => space.Recipe("testrecipe2", new ItemType[] { ID.Item.oak_planks, ID.Item.oak_door }, ID.Item.stone, 0, BaseFile.WriteSetting.LockedAuto), "Recipe may not output 0 or less items");
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => space.Recipe("testrecipe6", new ItemType[] { ID.Item.oak_planks, ID.Item.oak_door }, ID.Item.stone, 65, BaseFile.WriteSetting.LockedAuto), "Recipe may not output 65 or more items");
                Assert.ThrowsException<ArgumentException>(() => space.Recipe("testrecipe3", new ItemType[] { ID.Item.oak_planks, ID.Item.air }, ID.Item.stone, 2, BaseFile.WriteSetting.LockedAuto), "Recipe may not contain air");
                Assert.ThrowsException<ArgumentNullException>(() => space.Recipe("testrecipe4", new ItemType[] { ID.Item.oak_planks, null }, ID.Item.stone, 2, BaseFile.WriteSetting.LockedAuto), "Recipe may not contain null");
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => space.Recipe("testrecipe5", new ItemType[] { }, ID.Item.stone, 2, BaseFile.WriteSetting.LockedAuto), "Recipe may not contain 0 items");
            }
        }

        [TestMethod]
        public void TestInvalidRecipe()
        {
            //setup
            using (Datapack pack = new Datapack("datapacks", "pack", "a pack", 0, new NoneFileCreator()))
            {
                PackNamespace space = pack.Namespace("space");

                //test
                space.Recipe("recipe");
                string recipeString = pack.FileCreator.GetWriters().Single(w => w.path == "datapacks\\pack\\data\\space\\recipes\\recipe.json").writer.ToString();
                Assert.AreEqual("{\"type\":\"minecraft:invalid\"}", recipeString, "recipe file wasn't written correctly");
            }
        }

        [TestMethod]
        public void TestSmeltRecipe()
        {
            //setup
            using (Datapack pack = new Datapack("datapacks", "pack", "a pack", 0, new NoneFileCreator()))
            {
                PackNamespace space = pack.Namespace("space");

                //test
                space.Recipe("recipe1", SmeltRecipe.SmeltType.smelting, new ItemType[] { ID.Item.oak_sapling, ID.Item.spruce_sapling }, ID.Item.stick, 10);
                string recipeString = pack.FileCreator.GetWriters().Single(w => w.path == "datapacks\\pack\\data\\space\\recipes\\recipe1.json").writer.ToString();
                Assert.AreEqual("{\"type\":\"minecraft:smelting\",\"ingredient\":[{\"item\":\"minecraft:oak_sapling\"},{\"item\":\"minecraft:spruce_sapling\"}],\"result\":\"minecraft:stick\",\"experience\":10}", recipeString, "recipe file with multiple choises wasn't written correctly");

                space.Recipe("recipe2", SmeltRecipe.SmeltType.smoking, ID.Item.obsidian, ID.Item.black_stained_glass, 5.4, 100);
                recipeString = pack.FileCreator.GetWriters().Single(w => w.path == "datapacks\\pack\\data\\space\\recipes\\recipe2.json").writer.ToString();
                Assert.AreEqual("{\"type\":\"minecraft:smoking\",\"ingredient\":{\"item\":\"minecraft:obsidian\"},\"result\":\"minecraft:black_stained_glass\",\"experience\":5.4,\"cookingtime\":100}", recipeString, "recipe file with single choise wasn't written correctly");

                //exceptions
                Assert.ThrowsException<ArgumentException>(() => space.Recipe("testrecipe1", SmeltRecipe.SmeltType.smelting, new ItemType[] { ID.Item.oak_sapling, ID.Item.spruce_sapling }, ID.Item.air, 10), "Recipe may not output air");
                Assert.ThrowsException<ArgumentException>(() => space.Recipe("testrecipe2", SmeltRecipe.SmeltType.smelting, new ItemType[] { ID.Item.oak_sapling, ID.Item.air }, ID.Item.stone, 10), "Recipe may not contain air");
                Assert.ThrowsException<ArgumentNullException>(() => space.Recipe("testrecipe3", SmeltRecipe.SmeltType.smelting, new ItemType[] { ID.Item.oak_sapling, null }, ID.Item.stone, 10), "Recipe may not contain null");
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => space.Recipe("testrecipe4", SmeltRecipe.SmeltType.smelting, new ItemType[] { }, ID.Item.stone, 10), "Recipe may not contain 0 items");
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => space.Recipe("testrecipe5", SmeltRecipe.SmeltType.smelting, new ItemType[] { ID.Item.acacia_sapling }, ID.Item.stone, -1), "xp may not be less than 0");
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => space.Recipe("testrecipe6", SmeltRecipe.SmeltType.smelting, new ItemType[] { ID.Item.acacia_sapling }, ID.Item.stone, 10, -1), "smelt time may not be less than 0");
            }
        }

        [TestMethod]
        public void TestSpecialRecipe()
        {
            //setup
            using (Datapack pack = new Datapack("datapacks", "pack", "a pack", 0, new NoneFileCreator()))
            {
                PackNamespace space = pack.Namespace("space");

                //test
                new SpecialRecipe(space, "recipe", SpecialRecipe.SpecialType.armordye).Dispose();
                string recipeString = pack.FileCreator.GetWriters().Single(w => w.path == "datapacks\\pack\\data\\space\\recipes\\recipe.json").writer.ToString();
                Assert.AreEqual("{\"type\":\"minecraft:crafting_special_armordye\"}", recipeString, "recipe file wasn't written correctly");
            }
        }
    }
}