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
            Time time = new Time(100, ID.TimeType.seconds);
            Assert.AreEqual(100 * 20, time.AsTicks(), "Time and time format wasn't set correctly by constructor");
        }

        [TestMethod]
        public void TestGetTimeString()
        {
            //setup
            Time time1 = new Time(55, ID.TimeType.ticks);
            Time time2 = new Time(453, ID.TimeType.seconds);
            Time time3 = new Time(123, ID.TimeType.days);

            //test
            Assert.AreEqual("55t", time1.GetTimeString(), "Tick string is incorrect");
            Assert.AreEqual("453s", time2.GetTimeString(), "Seconds string is incorrect");
            Assert.AreEqual("123d", time3.GetTimeString(), "Days string is incorrect");
        }

        [TestMethod]
        public void TestIsNegative()
        {
            Assert.IsFalse(new Time(10,ID.TimeType.ticks).IsNegative(), "Time isn't negative");
            Assert.IsTrue(new Time(-10, ID.TimeType.ticks).IsNegative(), "Time is negative");
        }

        [TestMethod]
        public void TestAsTicks()
        {
            //setup
            Time time1 = new Time(3, ID.TimeType.ticks);
            Time time2 = new Time(4, ID.TimeType.seconds);
            Time time3 = new Time(5, ID.TimeType.days);
            Time time4 = new Time(int.MaxValue, ID.TimeType.ticks);
            Time time5 = new Time(int.MaxValue, ID.TimeType.days);

            //test
            Assert.AreEqual(3, time1.AsTicks());
            Assert.AreEqual(80, time2.AsTicks());
            Assert.AreEqual(120000, time3.AsTicks());
            Assert.AreEqual(int.MaxValue, time4.AsTicks());
            Assert.ThrowsException<OverflowException>(() => time5.AsTicks());

            Assert.AreEqual(3, time1.AsTicks(Time.TimerType.Short));
            Assert.AreEqual(80, time2.AsTicks(Time.TimerType.Short));
            Assert.ThrowsException<OverflowException>(() => time3.AsTicks(Time.TimerType.Short));
        }

        [TestMethod]
        public void TestGetAsTag()
        {
            SharpCraft.Data.IConvertableToDataTag convertable = new Time(4, ID.TimeType.seconds);
            Assert.AreEqual("80",convertable.GetAsTag(ID.NBTTagType.TagInt, null).GetDataString(), "Int tag conversion is wrong");
            Assert.AreEqual("80s", convertable.GetAsTag(ID.NBTTagType.TagShort, null).GetDataString(), "Short tag conversion is wrong");
            Assert.AreEqual("80L", convertable.GetAsTag(ID.NBTTagType.TagLong, null).GetDataString(), "Long tag conversion is wrong");
            Assert.ThrowsException<ArgumentException>(() => convertable.GetAsTag(ID.NBTTagType.TagDouble, null), "Tag only allows int,short and long");
        }
    }
}
