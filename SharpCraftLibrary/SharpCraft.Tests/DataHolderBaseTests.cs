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
    public class DataHolderBaseTests
    {
        #region Test Classes
        /// <summary>
        /// Class for testing <see cref="DataHolderBase"/>
        /// </summary>
        private class DataHolderTestClass : DataHolderBase
        {
            public int? NotData { get; set; }

            [DataTag]
            public byte? SmallNumber { get; set; }

            [DataTag]
            public byte?[] SmallNumberArray { get; set; }

            [DataTag]
            public short? ShortNumber { get; set; }

            [DataTag("Slices")]
            public int? Number { get; set; }

            [DataTag]
            public int?[] NumberArray { get; set; }

            [DataTag]
            public long? LongNumber { get; set; }

            [DataTag]
            public long?[] LongNumberArray { get; set; }

            [DataTag]
            public float? FloatPointNumber { get; set; }

            [DataTag]
            public double? PointNumber { get; set; }

            [DataTag]
            public double?[] PointNumberArray { get; set; }

            [DataTag]
            public string Text { get; set; }

            [DataTag]
            public string[] TextArray { get; set; }

            [DataTag]
            public bool? Boolean { get; set; }
        }

        private class DataHolderTestCompoundClass : DataHolderBase
        {
            [DataTag]
            public int? Number { get; set; }

            [DataTag]
            public DataHolderTestClass OtherObject { get; set; }
        }
        #endregion

        [TestMethod]
        public void TestGetDataProperties()
        {
            DataHolderTestClass testObject = new DataHolderTestClass();
            List<PropertyInfo> properties = testObject.GetDataProperties().ToList();
            Assert.AreEqual(13, properties.Count);
            PropertyInfo boolProperty = properties.Single(p => p.Name == "Boolean");
            boolProperty.SetValue(testObject, true);
            Assert.IsTrue(testObject.Boolean.Value);
        }

        [TestMethod]
        public void TestClone()
        {
            DataHolderTestCompoundClass testObject = new DataHolderTestCompoundClass()
            {
                Number = 10,
                OtherObject = new DataHolderTestClass()
                {
                    NotData = 100,
                    Boolean = true
                }
            };

            DataHolderTestCompoundClass cloneObject = (DataHolderTestCompoundClass)testObject.Clone();
            Assert.AreEqual(testObject.Number, cloneObject.Number);
            Assert.AreEqual(testObject.OtherObject.Boolean, cloneObject.OtherObject.Boolean);
            Assert.AreNotEqual(testObject.OtherObject.NotData, cloneObject.OtherObject.NotData);

            cloneObject.Number = 1;
            cloneObject.OtherObject.Boolean = false;
            Assert.AreNotEqual(testObject.Number, cloneObject.Number);
            Assert.AreNotEqual(testObject.OtherObject.Boolean, cloneObject.OtherObject.Boolean);
        }
    }
}
