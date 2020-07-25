using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpCraft;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using SharpCraft.Commands;

namespace SharpCraft.Tests.Commands
{
    [TestClass]
    public class TeamCommandsTests
    {
        [TestMethod]
        public void TeamAddCommandTest()
        {
            Assert.AreEqual("team add name {\"text\":\"Name\"}", new TeamAddCommand(new Team("name"), new JsonText.Text("Name")).GetCommandString());
            Assert.AreEqual("team add name", new TeamAddCommand(new Team("name"), null).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new TeamAddCommand(null!, null));
        }

        [TestMethod]
        public void TeamEmptyCommandTest()
        {
            Assert.AreEqual("team empty name", new TeamEmptyCommand(new Team("name")).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new TeamEmptyCommand(null!));
        }

        [TestMethod]
        public void TeamJoinCommandTest()
        {
            Assert.AreEqual("team join name @a", new TeamJoinCommand(new Team("name"), ID.Selector.a).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new TeamJoinCommand(null!, ID.Selector.a));
            Assert.ThrowsException<ArgumentNullException>(() => new TeamJoinCommand(new Team("name"), null!));
        }

        [TestMethod]
        public void TeamLeaveCommandTest()
        {
            Assert.AreEqual("team leave @a", new TeamLeaveCommand(ID.Selector.a).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new TeamLeaveCommand(null!));
        }

        [TestMethod]
        public void TeamListCommandTest()
        {
            Assert.AreEqual("team list", new TeamListCommand().GetCommandString());
        }

        [TestMethod]
        public void TeamPlayerListCommandTest()
        {
            Assert.AreEqual("team list name", new TeamPlayerListCommand(new Team("name")).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new TeamPlayerListCommand(null!));
        }

        [TestMethod]
        public void TeamRemoveCommandTest()
        {
            Assert.AreEqual("team remove name", new TeamRemoveCommand(new Team("name")).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new TeamRemoveCommand(null!));
        }

        [TestMethod]
        public void TeamModifyDisplayCommandTest()
        {
            Assert.AreEqual("team modify name displayName {\"text\":\"Name\"}", new TeamModifyDisplayCommand(new Team("name"), ID.TeamDisplayName.displayName, new JsonText.Text("Name")).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new TeamModifyDisplayCommand(new Team("name"), ID.TeamDisplayName.displayName, null!));
            Assert.ThrowsException<ArgumentNullException>(() => new TeamModifyDisplayCommand(null!, ID.TeamDisplayName.displayName, new JsonText.Text("Name")));
        }

        [TestMethod]
        public void TeamModifyCollisionCommandTest()
        {
            Assert.AreEqual("team modify name collisionRule never", new TeamModifyCollisionCommand(new Team("name"), ID.TeamCollision.never).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new TeamModifyCollisionCommand(null!, ID.TeamCollision.pushOtherTeams));
        }

        [TestMethod]
        public void TeamModifyColorCommandTest()
        {
            Assert.AreEqual("team modify name color blue", new TeamModifyColorCommand(new Team("name"), ID.MinecraftColor.blue).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new TeamModifyColorCommand(null!, ID.MinecraftColor.black));
        }

        [TestMethod]
        public void TeamModifyDeathMessageCommandTest()
        {
            Assert.AreEqual("team modify name deathMessageVisibility hideForOtherTeams", new TeamModifyDeathMessageCommand(new Team("name"), ID.TeamVisibility.hideForOtherTeams).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new TeamModifyDeathMessageCommand(null!, ID.TeamVisibility.hideForOtherTeams));
        }

        [TestMethod]
        public void TeamModifyNameVisibilityCommandTest()
        {
            Assert.AreEqual("team modify name nametagVisibility hideForOtherTeams", new TeamModifyNameVisibilityCommand(new Team("name"), ID.TeamVisibility.hideForOtherTeams).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new TeamModifyNameVisibilityCommand(null!, ID.TeamVisibility.hideForOtherTeams));
        }

        [TestMethod]
        public void TeamModifyFriendlyFireCommandTest()
        {
            Assert.AreEqual("team modify name friendlyFire true", new TeamModifyFriendlyFireCommand(new Team("name"), true).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new TeamModifyFriendlyFireCommand(null!, false));
        }

        [TestMethod]
        public void TeamModifyInvisibilityCommandTest()
        {
            Assert.AreEqual("team modify name seeFriendlyInvisibles true", new TeamModifyInvisibilityCommand(new Team("name"), true).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new TeamModifyInvisibilityCommand(null!, false));
        }
    }
}
