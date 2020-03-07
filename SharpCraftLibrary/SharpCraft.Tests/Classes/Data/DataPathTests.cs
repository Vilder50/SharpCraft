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
    public class DataPathTests
    {
        private class NestedArraysTestClass : DataHolderBase
        {
            [DataTag("arrays.nested")]
            public int[][][] Nested { get; set; }

            [DataTag]
            public string OtherData { get; set; }
        }

        [TestMethod]
        public void TestGetDataPath()
        {
            //Test simple path
            DataPath path = DataPath.GetDataPath<Block.Furnace>(f => f.DLock);
            Assert.AreEqual("Lock", path.ToString());

            //Test not getting a data tag property
            Assert.ThrowsException<ArgumentException>(() => { DataPath.GetDataPath<Block.Furnace>(f => f.SLit); });

            //test get nested array
            path = DataPath.GetDataPath<NestedArraysTestClass>(t => t.Nested);
            Assert.AreEqual(3, path.ArrayCount);
            Assert.IsTrue(path.IsArray);

            //test getting tag in tag
            path = DataPath.GetDataPath<Entity.ItemFrame>(f => f.FrameItem.Damage);
        }

        [TestMethod]
        public void TestCondition()
        {
            DataPath path = DataPath.GetDataPath<NestedArraysTestClass>(t => t.Nested);
            path.Condition(1, "{raw:\"Data\"}", new NestedArraysTestClass());

            DataPath otherPath = DataPath.GetDataPath<NestedArraysTestClass>(t => t.OtherData);
            otherPath.Condition("{raw:\"Data\"}");
            otherPath.Condition(new NestedArraysTestClass());

            //exceptions
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => path.Condition(1,1));

            Assert.ThrowsException<ArgumentException>(() => otherPath.Condition(1));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => otherPath.Condition("{a:1}","{b:1}"));

            Assert.ThrowsException<ArgumentNullException>(() => otherPath.Condition(conditions: null ));
            Assert.ThrowsException<ArgumentNullException>(() => otherPath.Condition(null));

        }

        [TestMethod]
        public void TestSetGetNextpath()
        {
            DataPath path = DataPath.GetDataPath<NestedArraysTestClass>(t => t.Nested);
            DataPath otherPath = DataPath.GetDataPath<NestedArraysTestClass>(t => t.OtherData);
            path.SetNextPath(otherPath);

            Assert.AreEqual(otherPath, path.GetNextpath());
        }

        [TestMethod]
        public void TestToString()
        {
            DataPath path = DataPath.GetDataPath<NestedArraysTestClass>(t => t.Nested).SetNextPath(DataPath.GetDataPath<NestedArraysTestClass>(t => t.OtherData));
            Assert.AreEqual("arrays.nested[][][].OtherData", path.ToString());
            path.Condition(1, "{raw:\"Data\"}", new NestedArraysTestClass() { OtherData = "hello world" });
            path.GetNextpath().Condition("{raw:\"Other Data\"}");
            Assert.AreEqual("arrays.nested[1][{raw:\"Data\"}][].OtherData{raw:\"Other Data\"}", path.ToString());
        }

        [TestMethod]
        public void TestDataPathCondition()
        {
            DataPathCondition condition = new DataPathCondition(1);
            Assert.AreEqual(DataPathCondition.ConditionType.Index, condition.Type);
            Assert.AreEqual("1", condition.GetConditionString());

            condition = new DataPathCondition((int?)null);
            Assert.AreEqual(DataPathCondition.ConditionType.Index, condition.Type);
            Assert.AreEqual(string.Empty, condition.GetConditionString());

            condition = new DataPathCondition("{data:1b}");
            Assert.AreEqual(DataPathCondition.ConditionType.RawData, condition.Type);
            Assert.AreEqual("{data:1b}", condition.GetConditionString());

            condition = new DataPathCondition(new NestedArraysTestClass());
            Assert.AreEqual(DataPathCondition.ConditionType.Data, condition.Type);

            //test implicit convertsion
            condition = -1;
            Assert.AreEqual(DataPathCondition.ConditionType.Index, condition.Type);

            condition = "{data:[{i:0}]}";
            Assert.AreEqual(DataPathCondition.ConditionType.RawData, condition.Type);

            condition = new NestedArraysTestClass();
            Assert.AreEqual(DataPathCondition.ConditionType.Data, condition.Type);

            //test exceptions
            Assert.ThrowsException<ArgumentException>(() => new DataPathCondition((string)null));
            Assert.ThrowsException<ArgumentNullException>(() => new DataPathCondition((DataHolderBase)null));
        }
    }
}
