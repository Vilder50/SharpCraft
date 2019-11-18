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
    public class ExperienceCommandsTests
    {
        [TestMethod]
        public void ExperienceModifyCommandTest()
        {
            Assert.AreEqual("xp add @s 10 levels", new ExperienceModifyCommand(ID.Selector.s, true, ID.AddSetModifier.add, 10).GetCommandString());
            Assert.AreEqual("xp set @s 10 levels", new ExperienceModifyCommand(ID.Selector.s, true, ID.AddSetModifier.set, 10).GetCommandString());
            Assert.AreEqual("xp add @s 10 points", new ExperienceModifyCommand(ID.Selector.s, false, ID.AddSetModifier.add, 10).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new ExperienceModifyCommand(null, true, ID.AddSetModifier.add, 10));
        }

        [TestMethod]
        public void ExperienceGetCommandTest()
        {
            Assert.AreEqual("xp query @s levels", new ExperienceGetCommand(ID.Selector.s, true).GetCommandString());
            Assert.AreEqual("xp query @s points", new ExperienceGetCommand(ID.Selector.s, false).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new ExperienceGetCommand(null, false));
        }
    }
}
