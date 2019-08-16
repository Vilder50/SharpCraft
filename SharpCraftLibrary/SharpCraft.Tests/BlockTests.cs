using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpCraft;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace SharpCraft.Tests
{
    [TestClass]
    public class BlockTests
    {
        [TestMethod]
        public void TestGetFullBlock()
        {
            //make sure blocks gets into their correct categori
            Assert.IsTrue(Block.GetFullBlock(ID.Block.blue_bed) is Block.Bed, "fits type doesn't work correct");
            Assert.IsFalse(Block.GetFullBlock(ID.Block.stone) is Block.Bed, "fits type doesn't work correct");

            //make sure a normal block like stone doesn't get anything else than block
            Type normalTestType = Block.GetFullBlock(ID.Block.stone).GetType();
            Assert.IsTrue(normalTestType.BaseType == typeof(object), "normal blocks falls into wrong block catagory");

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
            Block.Anvil anvil = new Block.Anvil(ID.Block.anvil);
            Assert.IsFalse(anvil.HasState);
            anvil.SFacing = ID.Facing.east;
            Assert.IsTrue(anvil.HasState);
            Assert.IsFalse(((Block)ID.Block.stone).HasState);
        }

        [TestMethod]
        public void TestClearStates()
        {
            Block.Furnace furnace = new Block.Furnace(ID.Block.furnace)
            {
                SFacing = ID.Facing.east,
                DItems = new Item[] { new Item(ID.Item.stone, 10) }
            };
            furnace.ClearStates();
            Assert.IsNull(furnace.SFacing);
            Assert.AreEqual(ID.Item.stone, furnace.DItems[0].ID);
        }

        [TestMethod]
        public void TestGetStates()
        {
            Block.Furnace furnace = new Block.Furnace(ID.Block.furnace)
            {
                SFacing = ID.Facing.east,
                DItems = new Item[] { new Item(ID.Item.stone, 10) }
            };

            List<PropertyInfo> stateProperties = furnace.GetStates().ToList();
            Assert.AreEqual(2, stateProperties.Count);

            PropertyInfo facingState = stateProperties.Single(s => s.Name == "SFacing");
            facingState.SetValue(furnace, ID.Facing.north);

            Assert.AreEqual(ID.Facing.north, furnace.SFacing);
        }

        [TestMethod]
        public void TestHasData()
        {
            Block.Furnace furnace = new Block.Furnace(ID.Block.furnace);
            Assert.IsFalse(furnace.HasData);
            furnace.DBurnTime = new Time(100, ID.TimeType.seconds);
            Assert.IsTrue(furnace.HasData);
            Assert.IsFalse(((Block)ID.Block.stone).HasData);
        }

        [TestMethod]
        public void TestClearData()
        {
            Block.Furnace furnace = new Block.Furnace(ID.Block.furnace)
            {
                SFacing = ID.Facing.east,
                DItems = new Item[] { new Item(ID.Item.stone, 10) }
            };
            furnace.ClearData();
            Assert.IsNull(furnace.DItems);
            Assert.AreEqual(ID.Facing.east, furnace.SFacing);
        }

        [TestMethod]
        public void TestGetData()
        {
            Block.Furnace furnace = new Block.Furnace(ID.Block.furnace)
            {
                SFacing = ID.Facing.east,
                DItems = new Item[] { new Item(ID.Item.stone, 10) }
                
            };

            List<PropertyInfo> dataProperties = furnace.GetData().ToList();
            Assert.AreEqual(8, dataProperties.Count);

            PropertyInfo itemsData = dataProperties.Single(s => s.Name == "DItems");
            itemsData.SetValue(furnace, new Item[] { new Item(ID.Item.dirt, 10) });

            Assert.AreEqual(ID.Item.dirt, furnace.DItems[0].ID);
        }

        [TestMethod]
        public void TestCloneBlock()
        {
            Block.Furnace furnace = new Block.Furnace(ID.Block.furnace)
            {
                SFacing = ID.Facing.east,
                DItems = new Item[] { new Item(ID.Item.stone, 10) }
            };
            Block.Furnace furnaceCopy = (Block.Furnace)furnace.Clone();
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
        }

        [TestMethod]
        public void TestBlockFromID()
        {
            Assert.AreEqual(((Block)ID.Block.stone).ID, ID.Block.stone);
        }
    }
}
