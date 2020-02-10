using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SharpCraft.Tests.MiscObjects
{
    [TestClass]
    public class MCRangeTests
    {
        [TestMethod]
        public void TestMcRange()
        {
            MCRange range = new MCRange(10.3,50.77);
            Assert.AreEqual(10.3, range.Minimum, "Constructor didn't set minimum correctly");
            Assert.AreEqual(50.77, range.Maximum, "Constructor didn't set maximum correctly");

            range = new MCRange(5);
            Assert.AreEqual(5, range.Minimum, "Single constructor didn't set minimum correctly");
            Assert.AreEqual(5, range.Maximum, "Single constructor didn't set maximum correctly");

            range = new MCRange(null, 50.77);
            Assert.IsNull(range.Minimum, "Constructor didn't set minimum to null");
            range = new MCRange(10.3, null);
            Assert.IsNull(range.Maximum, "Constructor didn't set maximum to null");

            Assert.ThrowsException<ArgumentNullException>(() => new MCRange(null, null), "Min and max may not both be null");
            Assert.ThrowsException<ArgumentException>(() => new MCRange(50, 10), "Max has be higher than min");
        }

        [TestMethod]
        public void TestImplicitMCRange()
        {
            //setup
            MCRange range1 = 1..2;
            MCRange range2 = ^5..5;
            MCRange range3 = ^3;
            MCRange range4 = 1..;
            MCRange range5 = ..^1;

            //test
            Assert.AreEqual("1..2",range1.SelectorString(), "Range conversion doesn't work");
            Assert.AreEqual("-5..5", range2.SelectorString(), "Range doesn't work correctly with negative numbers");
            Assert.AreEqual("-3", range3.SelectorString(), "Index to range doesn't work correctly");
            Assert.AreEqual("1..", range4.SelectorString(), "Range without end converted incorrectly");
            Assert.AreEqual("..-1", range5.SelectorString(), "Range without beginning converted incorrectly");
            Assert.AreEqual("1", ((MCRange)1).SelectorString(), "Double converted incorrectly");

            Assert.ThrowsException<InvalidCastException>(() => (MCRange)(5..^5), "End might not be after start");
            Assert.ThrowsException<InvalidCastException>(() => (MCRange)(..1), "Shouldn't be able to figure out range start.");
            Assert.ThrowsException<InvalidCastException>(() => (MCRange)(^1..), "Shouldn't be able to figure out range end");
        }

        [TestMethod]
        public void TestSelectorString()
        {
            Assert.AreEqual("1..2",new MCRange(1, 2).SelectorString());
            Assert.AreEqual("..2", new MCRange(null, 2).SelectorString());
            Assert.AreEqual("1..", new MCRange(1, null).SelectorString());
            Assert.AreEqual("test=1..2", new MCRange(1, 2).SelectorString("test"));
            Assert.AreEqual("test=-5", new MCRange(-5).SelectorString("test"));
        }

        [TestMethod]
        public void TestGetAsDataObject()
        {
            SharpCraft.Data.IConvertableToDataObject convertable1 = new MCRange(1, 2);
            SharpCraft.Data.IConvertableToDataObject convertable2 = new MCRange(null, 2);
            SharpCraft.Data.IConvertableToDataObject convertable3 = new MCRange(1, null);

            Assert.AreEqual("{max:2,min:1}",convertable1.GetAsDataObject(new object[] { "min", "max", ID.NBTTagType.TagInt }).GetDataString());
            Assert.AreEqual("{max:2}", convertable2.GetAsDataObject(new object[] { "min", "max", ID.NBTTagType.TagInt }).GetDataString());
            Assert.AreEqual("{min:1}", convertable3.GetAsDataObject(new object[] { "min", "max", ID.NBTTagType.TagInt }).GetDataString());
            Assert.AreEqual("{max:2d,min:1d}", convertable1.GetAsDataObject(new object[] { "min", "max", ID.NBTTagType.TagDouble }).GetDataString());
            Assert.AreEqual("{max:2s,min:1s}", convertable1.GetAsDataObject(new object[] { "min", "max", ID.NBTTagType.TagShort }).GetDataString());
            Assert.AreEqual("{\"max\":2,\"min\":1}", convertable1.GetAsDataObject(new object[] { "min", "max", ID.NBTTagType.TagDouble, true }).GetDataString());
        }
    }
}
