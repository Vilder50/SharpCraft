using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpCraft;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using SharpCraft.Data;

namespace SharpCraft.Tests
{
    [TestClass]
    public class DataPathTests
    {
        private class NestedArraysTestClass : DataHolderBase
        {
            public int[][][] Nested { get; set; }
        }

        [TestMethod]
        public void TestGetDataPath()
        {
            //Test simple path
            DataPath path = DataPath.GetDataPath<Block.Furnace>(f => f.DLock);
            Assert.AreEqual("Lock", path.ToString());

            Assert.ThrowsException<ArgumentException>(() => { DataPath.GetDataPath<Block.Furnace>(f => f.SLit); });

            //test get nested array
            path = DataPath.GetDataPath<NestedArraysTestClass>(t => t.Nested);
            Assert.AreEqual(3, path.ArrayCount);
        }
    }
}
