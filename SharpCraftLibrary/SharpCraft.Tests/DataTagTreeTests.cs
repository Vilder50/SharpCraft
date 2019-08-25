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
    public class DataPartObjectTests
    {
        [TestMethod]
        public void TestDataPartObject()
        {
            new DataPartObject();
        }

        [TestMethod]
        public void TestAddValue()
        {
            DataPartObject dataObject = new DataPartObject();
            dataObject.AddValue(new DataPartPath("path", new DataPartObject()));
            dataObject.AddValue(new DataPartPath("AnotherPath", new DataPartObject()));

            //test exceptions
            Assert.ThrowsException<ArgumentNullException>(() => { dataObject.AddValue(null); });
            Assert.ThrowsException<ArgumentException>(() => { dataObject.AddValue(new DataPartPath("path", new DataPartObject())); });
        }

        [TestMethod]
        public void TestGetValues()
        {
            DataPartObject dataObject = new DataPartObject();
            Assert.AreEqual(0, dataObject.GetValues().Count);

            dataObject.AddValue(new DataPartPath("path", new DataPartObject()));
            dataObject.AddValue(new DataPartPath("AnotherPath", new DataPartObject()));

            Assert.AreEqual(2, dataObject.GetValues().Count);
            Assert.AreEqual("path", dataObject.GetValues()[0].PathName);
        }

        [TestMethod]
        public void TestMerge()
        {
            //Setup
            DataPartObject dataObjectOne = new DataPartObject();
            dataObjectOne.AddValue(new DataPartPath("path", new DataPartObject()));
            dataObjectOne.AddValue(new DataPartPath("AnotherPath", new DataPartObject()));
            dataObjectOne.MergeDataPartObject(dataObjectOne);

            DataPartObject dataObjectInsideInside = new DataPartObject();
            dataObjectInsideInside.AddValue(new DataPartPath("path", new DataPartTag(10)));

            DataPartObject dataObjectInside = new DataPartObject();
            dataObjectInside.AddValue(new DataPartPath("path", new DataPartObject()));
            dataObjectInside.AddValue(new DataPartPath("AnotherPath", dataObjectInsideInside));

            DataPartObject dataObjectTwo = new DataPartObject();
            dataObjectTwo.AddValue(new DataPartPath("path", new DataPartObject()));
            dataObjectTwo.AddValue(new DataPartPath("AnotherPath", dataObjectInside));
            dataObjectTwo.AddValue(new DataPartPath("PathThree", new DataPartTag(10)));

            //test
            dataObjectOne.MergeDataPartObject(dataObjectTwo);
            Assert.AreEqual(3, dataObjectOne.GetValues().Count);
            Assert.AreEqual(10, (int)((DataPartTag)((DataPartObject)((DataPartObject)dataObjectOne.GetValues().Single(v => v.PathName == "AnotherPath").PathValue).GetValues().Single(v => v.PathName == "AnotherPath").PathValue).GetValues().Single(v => v.PathName == "path").PathValue).Value);

            //test exceptions
            Assert.ThrowsException<ArgumentNullException>(() => { dataObjectOne.MergeDataPartObject(null); });
            Assert.ThrowsException<ArgumentException>(() => { dataObjectTwo.MergeDataPartObject(dataObjectTwo); });
        }

        [TestMethod]
        public void TestIsEmpty()
        {
            DataPartObject dataObject = new DataPartObject();
            dataObject.AddValue(new DataPartPath("PathOne", new DataPartArray(new long[][] { null, null }, null, null)));
            dataObject.AddValue(new DataPartPath("PathTwo", new DataPartTag(10)));
            Assert.IsFalse(dataObject.IsEmpty());

            dataObject = new DataPartObject();
            dataObject.AddValue(new DataPartPath("PathTwo", new DataPartTag(null)));
            Assert.IsTrue(dataObject.IsEmpty());
        }

        [TestMethod]
        public void TestGetDataString()
        {
            DataPartObject dataObject = new DataPartObject();
            dataObject.AddValue(new DataPartPath("PathOne", new DataPartArray(new long[][] { null, null }, null, null)));
            dataObject.AddValue(new DataPartPath("PathTwo", new DataPartTag(10.5)));
            Assert.AreEqual("{PathOne:[],PathTwo:10.5d}", dataObject.GetDataString());

            dataObject = new DataPartObject();
            dataObject.AddValue(new DataPartPath("PathOne", new DataPartArray(null, null, null)));
            dataObject.AddValue(new DataPartPath("PathTwo", new DataPartTag(10.5)));
            Assert.AreEqual("{PathTwo:10.5d}", dataObject.GetDataString());
        }
    }

    [TestClass]
    public class DataPartArrayTests
    {
        private class TestClass : IConvertableToDataTag
        {
            public int Value;
            public TestClass(int value)
            {
                Value = value;
            }

            public DataPartTag GetAsTag(ID.NBTTagType? asType = null, object[] extraConversionData = null)
            {
                if (asType == ID.NBTTagType.TagString && (string)extraConversionData[0] == "hello")
                {
                    return new DataPartTag(Value);
                }
                else
                {
                    throw new Exception();
                }
            }
        }

        [TestMethod]
        public void TestDataPartArray()
        {
            DataPartArray array = new DataPartArray(new TestClass[][]
            {
                new TestClass[] { new TestClass(1), new TestClass(2), new TestClass(3) },
                new TestClass[] { new TestClass(4), new TestClass(5) }
            }, ID.NBTTagType.TagStringArray, new object[] { "hello" });
            Assert.AreEqual(ID.NBTTagType.TagArrayArray, array.ArrayType);
            Assert.AreEqual(2, ((DataPartTag)((DataPartArray)array.GetItems()[0]).GetItems()[1]).Value);

            //test exceptions
            Assert.ThrowsException<ArgumentException>(() => new DataPartArray(10, null, null));
            Assert.ThrowsException<ArgumentException>(() => new DataPartArray(new DateTime[] { new DateTime() }, null, null));
        }

        [TestMethod]
        public void TestAddItem()
        {
            DataPartArray array = new DataPartArray(new int[]
            {
                1,2,3,4,5
            }, null, null);
            int oldAmount = array.GetItems().Count;
            array.AddItem(new DataPartTag(10));
            Assert.AreEqual(oldAmount + 1, array.GetItems().Count);

            //test exception
            Assert.ThrowsException<ArgumentException>(() => array.AddItem(new DataPartTag(1.0)));
        }

        [TestMethod]
        public void TestIsEmpty()
        {
            DataPartArray array = new DataPartArray(new DataPartTag[][]
            {
                new DataPartTag[] { new DataPartTag(null), new DataPartTag(null) },
                new DataPartTag[] { new DataPartTag(null), new DataPartTag(10) }
            }, null, null);
            Assert.IsFalse(array.IsEmpty());
            array = new DataPartArray(new DataPartTag[][]
            {
                new DataPartTag[] { new DataPartTag(null), new DataPartTag(null) },
                new DataPartTag[] { new DataPartTag(null), new DataPartTag(null) }
            }, null, null);
            Assert.IsFalse(array.IsEmpty());
        }

        [TestMethod]
        public void TestGetDataString()
        {
            DataPartArray array = new DataPartArray(new int[]
            {
                1,2,3
            }, null, null);
            Assert.AreEqual("[I;1,2,3]", array.GetDataString());
            array = new DataPartArray(new long[][]
            {
                new long[] { 1,2,3 },
                null,
                new long[] { 4,5,6 }
            }, null, null);
            Assert.AreEqual("[[1L,2L,3L],[4L,5L,6L]]", array.GetDataString());
            array = new DataPartArray(new JSON[][]
            {
                new JSON() { Text = "Hey" },
                new JSON() { Text = "Hey2" }
            }, null, null);
            Assert.AreEqual("['[{\"text\":\"Hey\"}]','[{\"text\":\"Hey2\"}]']", array.GetDataString());
            array = new DataPartArray(new int[]
            {
            }, null, null);
            Assert.AreEqual("[]", array.GetDataString());
        }
    }

    [TestClass]
    public class DataPartTagTests
    {
        private enum TestEnum
        {
            valueZero,
            valueOne,
            valueTwo,
            valueThree,
            ValueFour,
            ValueEight = 8
        }

        [TestMethod]
        public void TestDataPartTag()
        {
            DataPartTag dataTag = new DataPartTag(10);
            Assert.AreEqual(10, dataTag.Value);
            new DataPartTag(null);

            dataTag = new DataPartTag("Pancakes");
            Assert.AreEqual("Pancakes", dataTag.Value);

            Assert.ThrowsException<ArgumentException>(() => new DataPartTag(DateTime.Now));
        }

        [TestMethod]
        public void TestIsEmpty()
        {
            DataPartTag dataTag = new DataPartTag(10);
            Assert.IsFalse(dataTag.IsEmpty());
            dataTag = new DataPartTag(null);
            Assert.IsTrue(dataTag.IsEmpty());
        }

        [TestMethod]
        public void TestGetDataString()
        {
            //int
            DataPartTag dataTag = new DataPartTag(10);
            Assert.AreEqual("10", dataTag.GetDataString());
            dataTag = new DataPartTag(TestEnum.ValueEight, ID.NBTTagType.TagInt);
            Assert.AreEqual("8", dataTag.GetDataString());

            //byte
            dataTag = new DataPartTag((byte)10);
            Assert.AreEqual("10b", dataTag.GetDataString());
            dataTag = new DataPartTag(true);
            Assert.AreEqual("1b", dataTag.GetDataString());
            dataTag = new DataPartTag(false);
            Assert.AreEqual("0b", dataTag.GetDataString());
            dataTag = new DataPartTag((byte)128);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => dataTag.GetDataString());
            dataTag = new DataPartTag((sbyte)-128);
            Assert.AreEqual("-128b", dataTag.GetDataString());
            dataTag = new DataPartTag(TestEnum.ValueEight, ID.NBTTagType.TagByte);
            Assert.AreEqual("8b", dataTag.GetDataString());

            //short
            dataTag = new DataPartTag((short)10);
            Assert.AreEqual("10s", dataTag.GetDataString());
            dataTag = new DataPartTag(TestEnum.ValueEight, ID.NBTTagType.TagShort);
            Assert.AreEqual("8s", dataTag.GetDataString());

            //long
            dataTag = new DataPartTag(999999999999);
            Assert.AreEqual("999999999999L", dataTag.GetDataString());
            dataTag = new DataPartTag(TestEnum.ValueEight, ID.NBTTagType.TagLong);
            Assert.AreEqual("8L", dataTag.GetDataString());

            //double
            dataTag = new DataPartTag(1.12);
            Assert.AreEqual("1.12d", dataTag.GetDataString());

            //float
            dataTag = new DataPartTag(1.12f);
            Assert.AreEqual("1.12f", dataTag.GetDataString());

            //string
            dataTag = new DataPartTag("hello world");
            Assert.AreEqual("'hello world'", dataTag.GetDataString());
            dataTag = new DataPartTag("\"\\'");
            Assert.AreEqual("'\"\\\\\\''", dataTag.GetDataString());
            dataTag = new DataPartTag(TestEnum.ValueEight, ID.NBTTagType.TagString);
            Assert.AreEqual("'ValueEight'", dataTag.GetDataString());
            dataTag = new DataPartTag("{test:\"1\"}", ID.NBTTagType.TagCompound);
            Assert.AreEqual("{test:\"1\"}", dataTag.GetDataString());
            dataTag = new DataPartTag(new JSON[] { new JSON() { Text = "hello" } }, ID.NBTTagType.TagCompound);
            Assert.AreEqual("'[{\"text\":\"hello\"}]'", dataTag.GetDataString());
            dataTag = new DataPartTag(new JSON() { Text = "hello" }, ID.NBTTagType.TagCompound);
            Assert.AreEqual("'{\"text\":\"hello\"}'", dataTag.GetDataString());

            //null
            dataTag = new DataPartTag(null);
            Assert.AreEqual(string.Empty, dataTag.GetDataString());
        }
    }
}
