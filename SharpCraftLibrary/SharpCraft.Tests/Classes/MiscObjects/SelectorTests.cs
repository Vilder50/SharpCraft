using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpCraft;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace SharpCraft.Tests.MiscObjects
{
    [TestClass]
    public class SelectorTests
    {
        [TestMethod]
        public void TestAllSelector()
        {
            Assert.AreEqual(AllSelector.GetSelector(),AllSelector.GetSelector(), "All selector singleton not working");
            Assert.AreEqual("*", AllSelector.GetSelector().GetSelectorString(), "All selector doesn't return correct string");
            Assert.IsFalse(AllSelector.GetSelector().IsLimited(), "All selector doesn't return correct limited value");
            Assert.ThrowsException<InvalidOperationException>(() => AllSelector.GetSelector().LimitSelector(), "LimitSelector didn't throw an exception");
        }

        [TestMethod]
        public void TestNameSelector()
        {
            Assert.AreEqual("My-Selector", new NameSelector("My-Selector").GetSelectorString(), "Name selector doesn't return correct string");
            Assert.AreEqual("#My-Selector", new NameSelector("My-Selector",true).GetSelectorString(), "Hidden name selector doesn't return correct string");
            Assert.IsTrue(new NameSelector("a").IsLimited(), "Name selector should always be limited");
            new NameSelector("a").LimitSelector();
            Assert.AreEqual("My-Selector", new NameSelector("#My-Selector").Name, "Name doesn't remove #");
            Assert.IsTrue(new NameSelector("#My-Selector").IsHidden, "Name with # doesn't set IsHidden to true");
            Assert.AreEqual("#stringT0Selector", ((NameSelector)"#stringT0Selector").GetSelectorString(), "String doesn't convert to selector correctly");
            Assert.ThrowsException<ArgumentException>(() => new NameSelector("    "), "Empty name should throw exception");
            Assert.ThrowsException<ArgumentException>(() => new NameSelector(null!), "Null name should throw exception");
            Assert.ThrowsException<ArgumentException>(() => new NameSelector("asda asdasd"), "Name with space should throw exception");
            Assert.ThrowsException<ArgumentException>(() => new NameSelector("asda\nasdasd"), "Name with whitespace should throw exception");
            Assert.ThrowsException<ArgumentException>(() => new NameSelector("asda*asdasd"), "Name with * should throw exception");
        }

        [TestMethod]
        public void TestSelector()
        {
            Assert.AreEqual("@a[level=1..5,y=2,dx=1]", new SharpCraft.Selector(ID.Selector.a)
            {
                BoxX = 1,
                Y = 2,
                Level = new MCRange(1,5)
            }.GetSelectorString(), "Selector doesn't add parts together correctly");
            Assert.AreEqual("@s", new SharpCraft.Selector().GetSelectorString(), "Simple selector doesn't return correct string");

            Assert.AreEqual("@s[x=1.3,y=2.2,z=3.1]", new SharpCraft.Selector() { X = 1.3, Y = 2.2, Z = 3.1 }.GetSelectorString(), "Selector coordinates doesn't return correct string");
            Assert.AreEqual("@s[dx=1.3,dy=2.2,dz=3.1]", new SharpCraft.Selector() { BoxX = 1.3, BoxY = 2.2, BoxZ = 3.1 }.GetSelectorString(), "Selector box coordinates doesn't return correct string");
            Assert.AreEqual("@s[x_rotation=1.1..3.3,y_rotation=4..6]", new SharpCraft.Selector() { XRotation = new MCRange(1.1,3.3), YRotation = new MCRange(4,6) }.GetSelectorString(), "Selector rotation doesn't return correct string");
            Assert.AreEqual("@s[distance=..1]", new SharpCraft.Selector() { Distance = new MCRange(null, 1) }.GetSelectorString(), "Selector distance doesn't return correct string");
            Assert.AreEqual("@s[level=..1]", new SharpCraft.Selector() { Level = new MCRange(null, 1) }.GetSelectorString(), "Selector level doesn't return correct string");
            Assert.AreEqual("@s[sort=random]", new SharpCraft.Selector() { Sort = ID.Sort.random }.GetSelectorString(), "Selector sort doesn't return correct string");
            Assert.AreEqual("@s[nbt={Size:1}]", new SharpCraft.Selector() { NBT = new Entities.Slime(null) { Size = 1 } }.GetSelectorString(), "Selector nbt doesn't return correct string");
            Assert.AreEqual("@s[nbt=!{Size:1}]", new SharpCraft.Selector() { NBT = new Entities.Slime(null) { Size = 1 }, NotNBT = true }.GetSelectorString(), "Selector nbt not doesn't return correct string");

            Assert.AreEqual("@e[name=\"test\",type=minecraft:creeper,tag=tagtag,predicate=space:name,scores={objective=1..2},gamemode=creative,team=myteam]", new SharpCraft.Selector(ID.Selector.e)
            {
                SingleName = "test",
                SingleType = ID.Entity.creeper,
                SingleTag = "tagtag",
                SinglePredicate = new FileMocks.MockPredicate(new EmptyNamespace(new EmptyDatapack("mypack"), "space"), "name"),
                SingleMode = ID.Gamemode.creative,
                SingleScore = new SharpCraft.Selector.EntityScore(new Objective("objective"), new MCRange(1,2)),
                SingleTeam = new Team("myteam")
            }.GetSelectorString(), "Selector multiple doesn't return correct string");

            SharpCraft.Selector limited = new SharpCraft.Selector(ID.Selector.a);
            Assert.IsFalse(limited.IsLimited(), "IsLimited should return false");
            limited.LimitSelector();
            Assert.AreEqual("@a[limit=1]", limited.GetSelectorString(), "LimitSelector doesn't work");
            Assert.IsTrue(limited.IsLimited(), "IsLimited should return true");
            Assert.IsTrue(new SharpCraft.Selector(ID.Selector.s).IsLimited(), "IsLimited should return true (s only selects 1 thing)");
            Assert.IsTrue(new SharpCraft.Selector(ID.Selector.p).IsLimited(), "IsLimited should return true (p only selects 1 thing)");
        }

        [TestMethod]
        public void TestTypeSelectorClass()
        {
            Assert.AreEqual("type=minecraft:creeper", new SharpCraft.Selector.EntityType(ID.Entity.creeper, true).GetSelectionString(), "EntityType doesn't return correct string");
            Assert.AreEqual("type=!minecraft:player", new SharpCraft.Selector.EntityType(ID.Entity.player, false).GetSelectionString(), "Not EntityType doesn't return correct string");
            Assert.ThrowsException<ArgumentNullException>(() => new SharpCraft.Selector.EntityType(null!, false), "Type may not be null");
        }

        [TestMethod]
        public void TestNameSelectorClass()
        {
            Assert.AreEqual("name=\"test\"", new SharpCraft.Selector.EntityName("test", true).GetSelectionString(), "EntityName doesn't return correct string");
            Assert.AreEqual("name=!\"test test\"", new SharpCraft.Selector.EntityName("test test", false).GetSelectionString(), "Not EntityName doesn't return correct string");
            Assert.ThrowsException<ArgumentException>(() => new SharpCraft.Selector.EntityName("   ", false), "name may not be empty");
        }

        [TestMethod]
        public void TestTagSelectorClass()
        {
            Assert.AreEqual("tag=mytag", new SharpCraft.Selector.EntityTag("mytag", true).GetSelectionString(), "EntityTag doesn't return correct string");
            Assert.AreEqual("tag=!tagtag", new SharpCraft.Selector.EntityTag("tagtag", false).GetSelectionString(), "Not EntityTag doesn't return correct string");
            Assert.ThrowsException<ArgumentNullException>(() => new SharpCraft.Selector.EntityTag(null!, false), "Tag may not be null");
        }

        [TestMethod]
        public void TestScoreSelectorClass()
        {
            Assert.AreEqual("objective=1..2", new SharpCraft.Selector.EntityScore(new Objective("objective"), new MCRange(1, 2)).GetSelectionString(), "EntityScore doesn't return correct string");
            Assert.ThrowsException<ArgumentNullException>(() => new SharpCraft.Selector.EntityScore(new Objective("objective"), null!), "Score may not be null");
            Assert.ThrowsException<ArgumentNullException>(() => new SharpCraft.Selector.EntityScore(null!, new MCRange(1, 2)), "Objective may not be null");
        }

        [TestMethod]
        public void TestTeamSelectorClass()
        {
            Assert.AreEqual("team=team", new SharpCraft.Selector.EntityTeam(new Team("team"), true).GetSelectionString(), "EntityTeam doesn't return correct string");
            Assert.AreEqual("team=!myteam", new SharpCraft.Selector.EntityTeam(new Team("myteam"), false).GetSelectionString(), "Not EntityTeam doesn't return correct string");
            Assert.ThrowsException<ArgumentNullException>(() => new SharpCraft.Selector.EntityTeam(null!, false), "Team may not be null");
        }

        [TestMethod]
        public void TestModeSelectorClass()
        {
            Assert.AreEqual("gamemode=adventure", new SharpCraft.Selector.EntityMode(ID.Gamemode.adventure, true).GetSelectionString(), "EntityMode doesn't return correct string");
            Assert.AreEqual("gamemode=!creative", new SharpCraft.Selector.EntityMode(ID.Gamemode.creative, false).GetSelectionString(), "Not EntityMode doesn't return correct string");
        }

        [TestMethod]
        public void TestPredicateSelectorClass()
        {
            IPredicate predicate = new FileMocks.MockPredicate(new EmptyNamespace(new EmptyDatapack("mypack"), "space"), "name");

            Assert.AreEqual("predicate=space:name", new SharpCraft.Selector.EntityPredicate(predicate, true).GetSelectionString(), "EntityPredicate doesn't return correct string");
            Assert.AreEqual("predicate=!space:name", new SharpCraft.Selector.EntityPredicate(predicate, false).GetSelectionString(), "Not EntityPredicate doesn't return correct string");
            Assert.ThrowsException<ArgumentNullException>(() => new SharpCraft.Selector.EntityPredicate(null!, false), "Predicate may not be null");
        }
    }
}
