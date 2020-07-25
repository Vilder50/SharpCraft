using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SharpCraft.Tests.MiscObjects
{
    [TestClass]
    public class RotationTests
    {
        [TestMethod]
        public void TestRotation()
        {
            Rotation rotation = new Rotation(55, 78, true, false);
            Assert.AreEqual(55, rotation.Y, "Y wasn't set correctly by constructor");
            Assert.AreEqual(78, rotation.X, "X wasn't set correctly by constructor");
            Assert.IsTrue(rotation.YRelative, "YRelative wasn't set correctly by constructor");
            Assert.IsFalse(rotation.XRelative, "XRelative wasn't set correctly by constructor");

            rotation = new Rotation(true, 95.8, 38.2);
            Assert.AreEqual(95.8, rotation.Y, "Y wasn't set correctly by constructor");
            Assert.AreEqual(38.2, rotation.X, "X wasn't set correctly by constructor");
            Assert.IsTrue(rotation.YRelative, "YRelative wasn't set correctly by constructor");
            Assert.IsTrue(rotation.XRelative, "XRelative wasn't set correctly by constructor");
        }

        [TestMethod]
        public void TestGetRotationString()
        {
            Assert.AreEqual("~13.5 ~-12.1", new Rotation(true, 13.5, -12.1).GetRotationString());
            Assert.AreEqual("~.5 ~-.1", new Rotation(true, 0.5, -0.1).GetRotationString());
            Assert.AreEqual("13.5 12.1", new Rotation(false, 13.5, 12.1).GetRotationString());
            Assert.AreEqual("~13.5 12.1", new Rotation(13.5, 12.1,true,false).GetRotationString());
        }

        [TestMethod]
        public void TestGetAsArray()
        {
            SharpCraft.Data.IConvertableToDataArrayBase convertable = new Rotation(1.4, 10.999);
            Assert.AreEqual("[1.4d,10.999d]",convertable.GetAsArray(ID.NBTTagType.TagDoubleArray, new object[] { }).GetDataString());

            Assert.ThrowsException<ArgumentException>(() => convertable.GetAsArray(ID.NBTTagType.TagDouble, new object[] { }));
        }
    }
}
