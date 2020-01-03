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
    public class RecipeCommandsTests
    {
        [TestMethod]
        public void RecipeCommandTest()
        {
            using (EmptyDatapack datapack = new EmptyDatapack("pack"))
            {
                EmptyRecipe recipe = new EmptyRecipe(datapack.Namespace("space"),"recipe");
                Assert.AreEqual("recipe give @a space:recipe", new RecipeCommand(recipe, ID.Selector.a, true).GetCommandString());
                Assert.AreEqual("recipe take @a space:recipe", new RecipeCommand(recipe, ID.Selector.a, false).GetCommandString());

                Assert.ThrowsException<ArgumentNullException>(() => new RecipeCommand(null, ID.Selector.a, true));
                Assert.ThrowsException<ArgumentNullException>(() => new RecipeCommand(recipe, null, true));
            }
        }

        [TestMethod]
        public void RecipeAllCommandTest()
        {
            Assert.AreEqual("recipe give @a *", new RecipeAllCommand(ID.Selector.a, true).GetCommandString());
            Assert.AreEqual("recipe take @a *", new RecipeAllCommand(ID.Selector.a, false).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new RecipeAllCommand(null, false));
        }
    }
}
