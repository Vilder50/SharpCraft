using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SharpCraft.Tests.MiscObjects
{
    [TestClass]
    public class RGBColorTests
    {
        [TestMethod]
        public void TestRGBColor()
        {
            RGBColor color = new RGBColor(3, 100, 250);
            Assert.AreEqual(3, color.Red, "Constructor didn't set red correctly");
            Assert.AreEqual(100, color.Green, "Constructor didn't set green correctly");
            Assert.AreEqual(250, color.Blue, "Constructor didn't set blue correctly");

            RGBColor hexColor = new RGBColor("#FF01A0");
            Assert.AreEqual(255, hexColor.Red, "Hex constructor didn't set red correctly");
            Assert.AreEqual(1, hexColor.Green, "Hex constructor didn't set green correctly");
            Assert.AreEqual(160, hexColor.Blue, "Hex constructor didn't set blue correctly");
            Assert.AreEqual(hexColor.ColorInt, new RGBColor("FF01A0").ColorInt, "Hex color without # should still return same color");

            //exceptions
            Assert.ThrowsException<ArgumentException>(() => new RGBColor("abcdgh"), "Hex color should throw exception on failed conversion");
        }

        [TestMethod]
        public void TestRed()
        {
            RGBColor color = new RGBColor(3, 100, 250)
            {
                Red = -1
            };
            Assert.AreEqual(0, color.Red, "Color wasn't clamped to be higher than 0");
            color.Red = 256;
            Assert.AreEqual(255, color.Red, "Color wasn't clamped to be lower than 255");
        }

        [TestMethod]
        public void TestGreen()
        {
            RGBColor color = new RGBColor(3, 100, 250)
            {
                Green = -1
            };
            Assert.AreEqual(0, color.Green, "Color wasn't clamped to be higher than 0");
            color.Green = 256;
            Assert.AreEqual(255, color.Green, "Color wasn't clamped to be lower than 255");
        }

        [TestMethod]
        public void TestBlue()
        {
            RGBColor color = new RGBColor(3, 100, 250)
            {
                Blue = -1
            };
            Assert.AreEqual(0, color.Blue, "Color wasn't clamped to be higher than 0");
            color.Blue = 256;
            Assert.AreEqual(255, color.Blue, "Color wasn't clamped to be lower than 255");
        }

        [TestMethod]
        public void TestColorInt()
        {
            Assert.AreEqual(2259790, new RGBColor(34, 123, 78).ColorInt);
            Assert.AreEqual(16777215, new RGBColor(255, 255, 255).ColorInt);
        }

        [TestMethod]
        public void TestColorHex()
        {
            Assert.AreEqual("#010203", new RGBColor(1, 2, 3).GetHexColor());
            Assert.AreEqual("ffffff", new RGBColor(255, 255, 255).GetHexColor(false));
        }

        [TestMethod]
        public void TestGetAsTag()
        {
            SharpCraft.Data.IConvertableToDataTag convertAble = new RGBColor(34, 123, 78);
            Assert.AreEqual("2259790", convertAble.GetAsTag(null, null).GetDataString());
        }

        [TestMethod]
        public void TestImplicitString()
        {
            RGBColor color = "7F92FF";
            Assert.AreEqual(127, color.Red);
            Assert.AreEqual(146, color.Green);
            Assert.AreEqual(255, color.Blue);
        }
    }
}
