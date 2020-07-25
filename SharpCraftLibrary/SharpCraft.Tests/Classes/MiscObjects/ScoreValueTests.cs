using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SharpCraft.Tests.MiscObjects
{
    [TestClass]
    public class ScoreValueTests
    {
        [TestMethod]
        public void TestScoreValue()
        {
            ScoreValue scoreValue = new ScoreValue(ID.Selector.s, new Objective("scores"));
            Assert.AreEqual("@s", scoreValue.Selector.GetSelectorString(), "Constructor didn't set selector correctly");
            Assert.AreEqual("scores", scoreValue.ScoreObject.Name, "Constructor didn't set objective correctly");

            Assert.ThrowsException<ArgumentNullException>(() => new ScoreValue(ID.Selector.s, null!), "Objective may not be null");
            Assert.ThrowsException<ArgumentNullException>(() => new ScoreValue(null!, new Objective("scores")), "Selector may not be null");
            Assert.ThrowsException<ArgumentException>(() => new ScoreValue(ID.Selector.e, new Objective("scores")), "Selector may not select multiple entities");
        }

        [TestMethod]
        public void TestImplicitScoreValue()
        {
            ScoreValue scoreValue = new ScoreValue(ID.Selector.s, new Objective("scores"));
            Objective objective = scoreValue;
            BaseSelector selector = scoreValue;
            Assert.AreEqual("scores", objective.Name, "Objective wasn't converted correctly");
            Assert.AreEqual("@s", selector.GetSelectorString(), "Selector wasn't converted correctly");
        }
    }
}
