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
            public byte[] SmallNumberArray { get; set; }

            [DataTag]
            public short? ShortNumber { get; set; }

            [DataTag("Slices")]
            public int? Number { get; set; }

            [DataTag]
            public int[] NumberArray { get; set; }

            [DataTag]
            public long? LongNumber { get; set; }

            [DataTag]
            public long[] LongNumberArray { get; set; }

            [DataTag]
            public float? FloatPointNumber { get; set; }

            [DataTag]
            public double? PointNumber { get; set; }

            [DataTag]
            public double[] PointNumberArray { get; set; }

            [DataTag("Double.Tag")]
            public string Text { get; set; }

            [DataTag]
            public string[] TextArray { get; set; }

            [DataTag]
            public bool? Boolean { get; set; }

            [DataTag]
            public BaseJsonText[] Name { get; set; }

            [DataTag("JsonTag")]
            public BaseJsonText[] JsonTag { get; set; }

            [DataTag("Fake", ForceType = ID.NBTTagType.TagCompound)]
            public string Fake { get; set; }
        }

        /// <summary>
        /// Class for testing <see cref="DataHolderBase"/>
        /// </summary>
        private class DataHolderTestCompoundClass : DataHolderBase
        {
            [DataTag("Inside.String")]
            public string String { get; set; }

            [DataTag]
            public int? Number { get; set; }

            [DataTag("Inside")]
            public DataHolderTestClass OtherObject { get; set; }
        }

        private class JsonDataHolderTestClass : DataHolderBase
        {
            [DataTag(JsonTag = true)]
            public double[] Array { get; set; }

            [DataTag(JsonTag = true)]
            public string String { get; set; }

            [DataTag(JsonTag = true)]
            public int? Int { get; set; }

            [DataTag(JsonTag = true)]
            public double? Double { get; set; }

            [DataTag(JsonTag = true)]
            public bool? Bool { get; set; }

            [DataTag(JsonTag = false)]
            public bool? NoJson { get; set; }

            [DataTag(ForceType = ID.NBTTagType.TagString, JsonTag = true)]
            public DataHolderTestCompoundClass Object { get; set; }

            [DataTag(JsonTag = true)]
            public JsonDataHolderTestClass JsonObject { get; set; }

            [DataTag(ForceType = ID.NBTTagType.TagString, JsonTag = true)]
            public JsonDataHolderTestClass StringObject { get; set; }
        }

        #region custom tag classes
        private class CustomDataTag : IConvertableToDataTag
        {
            public bool Bad = false;

            public DataPartTag GetAsTag(ID.NBTTagType? asType = null, object[] extraConversionData = null)
            {
                if (Bad)
                {
                    throw new Exception();
                }

                if (asType == ID.NBTTagType.TagByte && extraConversionData.Length == 0)
                {
                    return new DataPartTag((sbyte)10);
                }
                else if (asType == ID.NBTTagType.TagDouble && (int)extraConversionData[0] == 1)
                {
                    return new DataPartTag((long)10);
                }
                else
                {
                    throw new Exception();
                }
            }
        }

        private class CustomDataArray : IConvertableToDataArray
        {
            public DataPartArray GetAsArray(ID.NBTTagType? asType = null, object[] extraConversionData = null)
            {
                if (asType == ID.NBTTagType.TagIntArray && extraConversionData.Length == 0)
                {
                    return new DataPartArray(new int[] { 1, 2, 3 }, null, null);
                }
                else if (asType == ID.NBTTagType.TagDoubleArray && (int)extraConversionData[0] == 2)
                {
                    return new DataPartArray(new double[] { 1.1, 2.2, 3.3 }, null, null);
                }
                else
                {
                    throw new Exception();
                }
            }
        }

        private class CustomDataObject : IConvertableToDataObject
        {
            public DataPartObject GetAsDataObject(object[] conversionData = null)
            {
                if (conversionData.Length == 0)
                {
                    DataPartObject dataPartObject = new DataPartObject();
                    dataPartObject.AddValue(new DataPartPath("test", new DataPartTag(1)));
                    return dataPartObject;
                }
                else if ((int)conversionData[0] == 3)
                {
                    DataPartObject dataPartObject = new DataPartObject();
                    dataPartObject.AddValue(new DataPartPath("other", new DataPartTag(5)));
                    return dataPartObject;
                }
                else
                {
                    throw new Exception();
                }
            }
        }

        private class CustomTagTestClass : DataHolderBase
        {
            [DataTag(ForceType = ID.NBTTagType.TagByte)]
            public CustomDataTag CustomTag { get; set; }

            [DataTag(ForceType = ID.NBTTagType.TagInt)]
            public CustomDataTag InvalidCustomTag { get; set; }

            [DataTag(ForceType = ID.NBTTagType.TagDouble, ConversionParams = new object[] { 1 })]
            public CustomDataTag OtherCustomTag { get; set; }

            [DataTag(ForceType = ID.NBTTagType.TagIntArray)]
            public CustomDataArray CustomArray { get; set; }

            [DataTag(ForceType = ID.NBTTagType.TagDoubleArray, ConversionParams = new object[] { 2 })]
            public CustomDataArray OtherCustomArray { get; set; }

            [DataTag(ForceType = ID.NBTTagType.TagByteArray)]
            public CustomDataArray InvalidCustomArray { get; set; }

            [DataTag]
            public CustomDataObject CustomObject { get; set; }

            [DataTag(ConversionParams = new object[] { 3 })]
            public CustomDataObject OtherCustomObject { get; set; }

            [DataTag(ConversionParams = new object[] { 2 })]
            public CustomDataObject InvalidCustomObject { get; set; }

            [DataTag(Merge = true)]
            public CustomDataObject MergeObject { get; set; }

            [DataTag(ForceType = ID.NBTTagType.TagByteArray)]
            public CustomDataTag[] CustomTagArray { get; set; }
        }
        #endregion
        #endregion

        [TestMethod]
        public void TestGetDataProperties()
        {
            DataHolderTestClass testObject = new DataHolderTestClass();
            List<PropertyInfo> properties = testObject.GetDataProperties().ToList();
            Assert.AreEqual(16, properties.Count);
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

        [TestMethod]
        public void TestGetDataString()
        {
            DataHolderTestCompoundClass testObject = new DataHolderTestCompoundClass()
            {
                Number = 10,
                String = "Hello World",
                OtherObject = new DataHolderTestClass()
                {
                    Boolean = true,
                    FloatPointNumber = 0.1f,
                    LongNumber = 999999999999999,
                    LongNumberArray = new long[] { 0, 1, 1 },
                    NotData = 1,
                    Number = 101,
                    NumberArray = new int[] { 1, 2 },
                    PointNumber = 11.10,
                    PointNumberArray = new double[] { 32.3, 22.44 },
                    ShortNumber = short.MaxValue,
                    SmallNumber = 127,
                    SmallNumberArray = new byte[] { 10, 5 },
                    Text = "hey",
                    TextArray = new string[] { "hello", "world" },
                    Name = new BaseJsonText[] { "1", "2" },
                    Fake = "{hey:1}",
                    JsonTag = new BaseJsonText[] { "1", "2" }
                }
            };
            Assert.AreEqual("{Inside:{" +
                "Boolean:1b," +
                "Double:{Tag:\"hey\"}," +
                "Fake:{hey:1}," +
                "FloatPointNumber:0.1f," +
                "JsonTag:[\"{\\\"text\\\":\\\"1\\\"}\",\"{\\\"text\\\":\\\"2\\\"}\"]," +
                "LongNumber:999999999999999L," +
                "LongNumberArray:[0L,1L,1L]," +
                "Name:[\"{\\\"text\\\":\\\"1\\\"}\",\"{\\\"text\\\":\\\"2\\\"}\"]," +
                "NumberArray:[I;1,2]," +
                "PointNumber:11.1d," +
                "PointNumberArray:[32.3d,22.44d]," +
                "ShortNumber:32767s," +
                "Slices:101," +
                "SmallNumber:127b," +
                "SmallNumberArray:[10b,5b]," +
                "String:\"Hello World\"," +
                "TextArray:[\"hello\",\"world\"]}," +
                "Number:10}", testObject.GetDataString());

            testObject = new DataHolderTestCompoundClass()
            {
                Number = 10,
                OtherObject = new DataHolderTestClass()
                {
                    LongNumber = 20
                }
            };
            Assert.AreEqual("{Inside:{LongNumber:20L},Number:10}", testObject.GetDataString());

            CustomTagTestClass otherTestObject = new CustomTagTestClass()
            {
                CustomArray = new CustomDataArray(),
                CustomObject = new CustomDataObject(),
                CustomTag = new CustomDataTag(),
                OtherCustomArray = new CustomDataArray(),
                OtherCustomObject = new CustomDataObject(),
                OtherCustomTag = new CustomDataTag(),
                MergeObject = new CustomDataObject()
            };
            Assert.AreEqual("{CustomArray:[I;1,2,3],CustomObject:{test:1},CustomTag:10b,OtherCustomArray:[1.1d,2.2d,3.3d],OtherCustomObject:{other:5},OtherCustomTag:10L,test:1}",otherTestObject.GetDataString());
        }

        [TestMethod]
        public void TestJsonTag()
        {
            //setup
            JsonDataHolderTestClass object1 = new JsonDataHolderTestClass()
            {
                Array = new double[] { 1.1, 2.2, 3.3 },
                String = "Something\"", 
                Int = 100, 
                Double = 14.154, 
                Bool = true,
                NoJson = false,
                Object = new DataHolderTestCompoundClass 
                {
                    String = "Hel\"lo", 
                    Number = 10 
                },
                JsonObject = new JsonDataHolderTestClass
                {
                    Double = 100.105
                },
                StringObject = new JsonDataHolderTestClass
                {
                    Double = 100.155
                }
            };

            //test
            Assert.AreEqual("{\"Array\":[1.1,2.2,3.3],\"Bool\":true,\"Double\":14.154,\"Int\":100,\"JsonObject\":{\"Double\":100.105},\"Object\":\"{Inside:{String:\\\"Hel\\\\\\\"lo\\\"},Number:10}\",\"String\":\"Something\\\"\",\"StringObject\":\"{\\\"Double\\\":100.155}\",NoJson:0b}", object1.GetDataString(), "JsonTag doesn't work correctly");
        }

        [TestMethod]
        public void TestIConvertException()
        {
            bool threwException = false;
            try
            {
                new CustomTagTestClass() { InvalidCustomArray = new CustomDataArray() }.GetDataString();
            }
            catch (InvalidCastException ex)
            {
                threwException = true;
                if (!(ex.InnerException is Exception))
                {
                    Assert.Fail("No inner exception for array conversion fail");
                }
            }
            if (!threwException)
            {
                Assert.Fail("No exception was thrown for array even though the property conversion is invalid");
            }

            threwException = false;
            try
            {
                new CustomTagTestClass() { InvalidCustomObject = new CustomDataObject() }.GetDataString();
            }
            catch (InvalidCastException ex)
            {
                threwException = true;
                if (!(ex.InnerException is Exception))
                {
                    Assert.Fail("No inner exception for object conversion fail");
                }
            }
            if (!threwException)
            {
                Assert.Fail("No exception was thrown for object even though the property conversion is invalid");
            }

            threwException = false;
            try
            {
                new CustomTagTestClass() { InvalidCustomTag = new CustomDataTag() }.GetDataString();
            }
            catch (InvalidCastException ex)
            {
                threwException = true;
                if (!(ex.InnerException is Exception))
                {
                    Assert.Fail("No inner exception for tag conversion fail");
                }
            }
            if (!threwException)
            {
                Assert.Fail("No exception was thrown for tag even though the property conversion is invalid");
            }

            threwException = false;
            try
            {
                new CustomTagTestClass() { CustomTagArray = new CustomDataTag[] { new CustomDataTag(), new CustomDataTag() { Bad = true } } }.GetDataString();
            }
            catch (InvalidCastException ex)
            {
                threwException = true;
                if (!(ex.InnerException is Exception))
                {
                    Assert.Fail("No inner exception for tag array conversion fail");
                }
                Assert.AreEqual("Failed to convert object at index 1 into a data tag (See inner exception)", ex.Message, "Tag array exception message is incorrect");
            }
            if (!threwException)
            {
                Assert.Fail("No exception was thrown for tag array even though the property conversion is invalid");
            }
        }
    }
}
