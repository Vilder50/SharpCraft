﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SharpCraft.Tests.MiscObjects
{
    [TestClass]
    public class ObjectiveTests
    {
        [TestMethod]
        public void TestObjective()
        {
            Objective objective = new Objective("MyObjective");
            Assert.AreEqual("MyObjective", objective.Name, "Constructor didn't set name correctly");

            Assert.ThrowsException<ArgumentException>(() => new Objective(""), "Objective name may not be empty");
            Assert.ThrowsException<ArgumentException>(() => new Objective(null!), "Objective name may not be null");
            Assert.ThrowsException<ArgumentException>(() => new Objective("$asd$"), "Objective name may not be invalid");
        }
    }
}
