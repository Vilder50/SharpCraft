using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpCraft;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using SharpCraft.Data;

namespace SharpCraft.Tests.Data
{
    [TestClass]
    public class BlockTests
    {
        [TestMethod]
        public void TestGetFullBlock()
        {
            //make sure blocks gets into their correct categori
            Assert.IsTrue(Block.GetFullBlock(ID.Block.blue_bed) is Blocks.Bed, "fits type doesn't work correct");
            Assert.IsFalse(Block.GetFullBlock(ID.Block.stone) is Blocks.Bed, "fits type doesn't work correct");

            //make sure a normal block like stone doesn't get anything else than block
            Type normalTestType = Block.GetFullBlock(ID.Block.stone).GetType();
            Assert.IsTrue(normalTestType.BaseType == typeof(DataHolderBase), "normal blocks falls into wrong block catagory");

            //make sure all catagories are in use
            List<(MethodInfo method, Type block, bool used)> fitBlockMethods = new List<(MethodInfo method, Type block, bool used)>();
            if (fitBlockMethods is null)
            {
                Type blockType = typeof(Block);
                Type[] types = Assembly.GetAssembly(typeof(Block)).GetTypes().Where(t => t.IsSubclassOf(blockType) && !t.IsAbstract).ToArray();
                foreach (Type classType in types)
                {
                    MethodInfo fitBlockMethod = classType.GetMethod("FitsBlock", BindingFlags.Public | BindingFlags.Static);
                    if (!(fitBlockMethod is null))
                    {
                        fitBlockMethods.Add((fitBlockMethod, classType, false));
                    }
                }
            }

            for (int i = 0; i < (int)ID.Block.BlockEnumEnd - 1; i++)
            {
                for (int j = 0; j < fitBlockMethods.Count; j++)
                {
                    (MethodInfo method, Type block, bool used) = fitBlockMethods[j];
                    if ((bool)method.Invoke(null, new object[] { (ID.Block)i }))
                    {
                        used = true;
                        break;
                    }
                }
            }

            for (int j = 0; j < fitBlockMethods.Count; j++)
            {
                (MethodInfo method, Type block, bool used) = fitBlockMethods[j];
                Assert.IsTrue(used, "0 blocks fits the block: " + block);
            }
        }

        [TestMethod]
        public void TestHasStates()
        {
            Blocks.Anvil anvil = new Blocks.Anvil(ID.Block.anvil);
            Assert.IsFalse(anvil.HasState);
            anvil.SFacing = ID.Facing.east;
            Assert.IsTrue(anvil.HasState);
            Assert.IsFalse(((Block)ID.Block.stone).HasState);
        }

        [TestMethod]
        public void TestClearStates()
        {
            Blocks.Furnace furnace = new Blocks.Furnace(ID.Block.furnace)
            {
                SFacing = ID.Facing.east,
                DItems = new Item[] { new Item(ID.Item.stone, 10) }
            };
            furnace.ClearStates();
            Assert.IsNull(furnace.SFacing);
            Assert.IsTrue(furnace.DItems[0].ID == ID.Item.stone);
        }

        [TestMethod]
        public void TestGetStateProperties()
        {
            Blocks.Furnace furnace = new Blocks.Furnace(ID.Block.furnace)
            {
                SFacing = ID.Facing.east,
                DItems = new Item[] { new Item(ID.Item.stone, 10) }
            };

            List<PropertyInfo> stateProperties = furnace.GetStateProperties().ToList();
            Assert.AreEqual(2, stateProperties.Count);

            PropertyInfo facingState = stateProperties.Single(s => s.Name == "SFacing");
            facingState.SetValue(furnace, ID.Facing.north);

            Assert.AreEqual(ID.Facing.north, furnace.SFacing);
        }

        [TestMethod]
        public void TestFullCloneBlock()
        {
            Blocks.Furnace furnace = new Blocks.Furnace(ID.Block.furnace)
            {
                SFacing = ID.Facing.east,
                DItems = new Item[] { new Item(ID.Item.stone, 10) }
            };
            Blocks.Furnace furnaceCopy = (Blocks.Furnace)furnace.FullClone();
            Assert.AreNotEqual(furnace, furnaceCopy);
            Assert.AreEqual(furnace.SFacing, furnaceCopy.SFacing);
            Assert.AreEqual(furnace.DItems, furnaceCopy.DItems);
            Assert.IsNull(furnaceCopy.SLit);

            furnaceCopy.SLit = true;
            furnaceCopy.SFacing = ID.Facing.north;
            furnaceCopy.DItems = new Item[] { new Item(ID.Item.dirt, 11) };
            Assert.AreNotEqual(furnace.SFacing, furnaceCopy.SFacing);
            Assert.AreNotEqual(furnace.DItems, furnaceCopy.DItems);
            Assert.IsNull(furnace.SLit);

            //can copy blocks with auto id givers
            new Blocks.BrewingStand().FullClone();
        }

        [TestMethod]
        public void TestBlockFromID()
        {
            Assert.AreEqual(((Block)ID.Block.stone).ID, ID.Block.stone);
        }

        [TestMethod]
        public void TestGetAsDataObject()
        {
            IConvertableToDataObject convertable = new Blocks.Chest() { SFacing = ID.Facing.north, DLock = "locked" };
            Assert.AreEqual("{i:\"minecraft:chest\",s:{facing:\"north\"}}", convertable.GetAsDataObject(new object[] { "i", "s" }).GetDataString());
            Assert.AreEqual("{\"i\":\"minecraft:chest\",\"s\":{\"facing\":\"north\"}}", convertable.GetAsDataObject(new object[] { "i", "s", true }).GetDataString());

            Assert.AreEqual("{d:\"{Lock:\\\"locked\\\"}\",i:\"minecraft:chest\",s:{facing:\"north\"}}", convertable.GetAsDataObject(new object[] { "i", "g", "d", "s", false }).GetDataString());
            Assert.AreEqual("{\"d\":\"{Lock:\\\"locked\\\"}\",\"i\":\"minecraft:chest\",\"s\":{\"facing\":\"north\"}}", convertable.GetAsDataObject(new object[] { "i", "g", "d", "s", true }).GetDataString());
            Assert.AreEqual("{g:\"a:b\"}", new Blocks.Chest(new BlockType(new EmptyGroup<BlockType>(EmptyNamespace.GetNamespace("a"),"b"))).GetAsDataObject(new object[] { "i", "g", "d", "s", false }).GetDataString());
        }

        [TestMethod]
        public void TestGetStateData()
        {
            Block block = new Blocks.Chest() { SFacing = ID.Facing.north, DLock = "locked" };
            Assert.AreEqual("{facing:\"north\"}", block.GetStateData(false).GetDataString());
            Assert.AreEqual("{\"facing\":\"north\"}", block.GetStateData(true).GetDataString());
        }
    }
}
