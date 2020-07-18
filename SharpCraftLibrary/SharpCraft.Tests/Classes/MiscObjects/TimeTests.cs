using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SharpCraft.Tests.MiscObjects
{
    [TestClass]
    public class TimeTests
    {
        [TestMethod]
        public void TestTime()
        {
            Time<int> time = new Time<int>(100, ID.TimeType.seconds);
            _ = new Time<short>(100, ID.TimeType.seconds);
            _ = new Time<long>(100, ID.TimeType.seconds);
            Assert.AreEqual(100 * 20, time.GetAsTicks(), "Time and time format wasn't set correctly by constructor");

            Assert.ThrowsException<OverflowException>(() => new Time<long>(long.MaxValue, ID.TimeType.seconds));
            Assert.ThrowsException<OverflowException>(() => new Time<short>(short.MaxValue, ID.TimeType.seconds));
            Assert.ThrowsException<OverflowException>(() => new Time<int>(int.MaxValue, ID.TimeType.seconds));
        }

        [TestMethod]
        public void TestGetTimeString()
        {
            //setup
            Time<int> time1 = new Time<int>(55, ID.TimeType.ticks);
            Time<int> time2 = new Time<int>(453, ID.TimeType.seconds);
            Time<int> time3 = new Time<int>(123, ID.TimeType.days);

            //test
            Assert.AreEqual("55t", time1.GetTimeString(), "Tick string is incorrect");
            Assert.AreEqual("453s", time2.GetTimeString(), "Seconds string is incorrect");
            Assert.AreEqual("123d", time3.GetTimeString(), "Days string is incorrect");
        }

        [TestMethod]
        public void TestIsNegative()
        {
            new NoneNegativeTime<int>(10, ID.TimeType.ticks);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new NoneNegativeTime<int>(-10, ID.TimeType.ticks));
        }

        [TestMethod]
        public void TestAsTicks()
        {
            //setup
            Time<int> time1 = new Time<int>(3, ID.TimeType.ticks);
            Time<int> time2 = new Time<int>(4, ID.TimeType.seconds);
            Time<int> time3 = new Time<int>(5, ID.TimeType.days);

            //test
            Assert.AreEqual(3, time1.GetAsTicks());
            Assert.AreEqual(80, time2.GetAsTicks());
            Assert.AreEqual(120000, time3.GetAsTicks());
        }

        [TestMethod]
        public void TestGetAsTag()
        {
            SharpCraft.Data.IConvertableToDataTag convertable1 = new Time<int>(4, ID.TimeType.seconds);
            SharpCraft.Data.IConvertableToDataTag convertable2 = new Time<short>(4, ID.TimeType.seconds);
            SharpCraft.Data.IConvertableToDataTag convertable3 = new Time<long>(4, ID.TimeType.seconds);
            Assert.AreEqual("80", convertable1.GetAsTag(null, new object[] { }).GetDataString(), "Int tag conversion is wrong");
            Assert.AreEqual("80s", convertable2.GetAsTag(null, new object[] { }).GetDataString(), "Short tag conversion is wrong");
            Assert.AreEqual("80L", convertable3.GetAsTag(null, new object[] { }).GetDataString(), "Long tag conversion is wrong");
        }
    }
}
