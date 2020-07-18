using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SharpCraft.Tests.MiscObjects
{
    [TestClass]
    public class TagTests
    {
        [TestMethod]
        public void TestTag()
        {
            Tag tag = new Tag("MyTag");
            Assert.AreEqual("MyTag", tag.Name, "Constructor didn't set name correctly");

            Assert.ThrowsException<ArgumentException>(() => new Tag(""), "Tag name may not be empty");
            Assert.ThrowsException<ArgumentException>(() => new Tag(null!), "Tag name may not be null");
            Assert.ThrowsException<ArgumentException>(() => new Tag("$asd$"), "Tag name may not be invalid");
        }

        [TestMethod]
        public void TestGetAsTag()
        {
            SharpCraft.Data.IConvertableToDataTag convertable = new Tag("MyTag");
            Assert.AreEqual("\"MyTag\"", convertable.GetAsTag(ID.NBTTagType.TagString, null!).GetDataString(), "Tag wasn't converted correctly");
            Assert.ThrowsException<ArgumentException>(() => convertable.GetAsTag(ID.NBTTagType.TagInt, null!), "Tag should only allow string conversion");
        }

        [TestMethod]
        public void TestImplicitConvert()
        {
            Tag tag = "hello";
            Assert.AreEqual("hello",tag.Name, "String to tag didn't convert correctly");

            Tag[] tags = new Tag("hello");
            Assert.AreEqual(1, tags.Length, "Tag didn't add itself to the array");
            Assert.AreEqual("hello", tags[0].Name, "Tag didn't add itself to the array");
        }
    }
}
