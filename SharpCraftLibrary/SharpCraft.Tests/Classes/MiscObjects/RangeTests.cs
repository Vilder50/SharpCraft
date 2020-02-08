using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SharpCraft.Tests.MiscObjects
{
    [TestClass]
    public class RangeTests
    {
        [TestMethod]
        public void RangeToMCRangeTest()
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

            Assert.ThrowsException<InvalidCastException>(() => (MCRange)(5..^5), "End might not be after start");
            Assert.ThrowsException<InvalidCastException>(() => (MCRange)(..1), "Shouldn't be able to figure out range start.");
            Assert.ThrowsException<InvalidCastException>(() => (MCRange)(^1..), "Shouldn't be able to figure out range end");
        }
    }
}
