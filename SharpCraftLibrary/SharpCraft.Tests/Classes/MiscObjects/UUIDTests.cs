using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SharpCraft.Tests.MiscObjects
{
    [TestClass]
    public class UUIDTests
    {
        [TestMethod]
        public void TestUUID()
        {
            UUID id = new UUID(1,2);
            Assert.AreEqual(1, id.Most, "Most wasn't set by constructor");
            Assert.AreEqual(2, id.Least, "Least wasn't set by constructor");
            Assert.AreEqual("00000000-0000-0001-0000-000000000002", id.UUIDString, "UUID string wasn't set correctly by constructor");

            id = new UUID("f6b1914d-b176-4850-8a06-b50380412b85");
            Assert.AreEqual(-8500908219973948539, id.Least, "Least wasn't gotten correctly from uuid");
            Assert.AreEqual(-670595106625664944, id.Most, "Most wasn't gotten correctly from uuid");
            Assert.AreEqual("f6b1914d-b176-4850-8a06-b50380412b85", id.UUIDString, "UUID wasn't formated correctly");

            Assert.ThrowsException<ArgumentNullException>(() => new UUID(null), "UUID may not be null");
        }

        [TestMethod]
        public void TestGetAsDataObject()
        {
            SharpCraft.Data.IConvertableToDataObject convertable = new UUID("00000000-0000-0001-0000-000000000002");
            Assert.AreEqual("{l:2L,m:1L}", convertable.GetAsDataObject(new object[] { "m","l" }).GetDataString());
        }

        [TestMethod]
        public void TestGetAsTag()
        {
            SharpCraft.Data.IConvertableToDataTag convertable = new UUID("00000000-0000-0002-0000-000000000001");
            Assert.AreEqual("\"00000000-0000-0002-0000-000000000001\"", convertable.GetAsTag(ID.NBTTagType.TagString,null).GetDataString());

            Assert.ThrowsException<ArgumentException>(() => convertable.GetAsTag(ID.NBTTagType.TagLong, null), "UUID cannot be long");
        }

        [TestMethod]
        public void TestRandom()
        {
            UUID.SetRandomSeed(1);
            UUID uuid1 = new UUID();
            UUID.SetRandomSeed(1);
            UUID uuid2 = new UUID();
            Assert.AreEqual(uuid1.UUIDString, uuid2.UUIDString);

            uuid1 = new UUID();
            uuid2 = new UUID();
            if (uuid1.UUIDString == uuid2.UUIDString)
            {
                Assert.Inconclusive();
            }
        }
    }
}
