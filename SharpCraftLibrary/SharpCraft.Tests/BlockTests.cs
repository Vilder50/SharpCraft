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
    }
}
