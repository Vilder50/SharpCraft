using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SharpCraft.Tests.MiscObjects
{
    [TestClass]
    public class TeamTests
    {
        [TestMethod]
        public void TestTeam()
        {
            Team team = new Team("MyTeam");
            Assert.AreEqual("MyTeam", team.Name, "Constructor didn't set name correctly");

            Assert.ThrowsException<ArgumentException>(() => new Team(""), "Team name may not be empty");
            Assert.ThrowsException<ArgumentException>(() => new Team(null), "Team name may not be null");
            Assert.ThrowsException<ArgumentException>(() => new Team("$asd$"), "Team name may not be invalid");
        }

        [TestMethod]
        public void TestGetAsTag()
        {
            SharpCraft.Data.IConvertableToDataTag convertable = new Tag("MyTeam");
            Assert.AreEqual("\"MyTeam\"", convertable.GetAsTag(ID.NBTTagType.TagString, null).GetDataString(), "Team wasn't converted correctly");
            Assert.ThrowsException<ArgumentException>(() => convertable.GetAsTag(ID.NBTTagType.TagInt, null), "Team should only allow string conversion");
        }
    }
}
