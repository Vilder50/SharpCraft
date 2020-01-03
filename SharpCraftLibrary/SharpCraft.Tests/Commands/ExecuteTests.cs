using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpCraft;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using SharpCraft.Commands;

namespace SharpCraft.Tests.Commands
{
    class TestICommand : BaseCommand
    {
        public override string GetCommandString()
        {
            return "hello world";
        }
    }

    [TestClass]
    public class ExecuteTests
    {
        [TestMethod]
        public void TestExecuteGetCommandString()
        {
            //test
            Assert.AreEqual("execute align xyz", new ExecuteAlign(true,true,true).GetCommandString(), "ExecuteAlign does not return correct GetCommandString");
            Assert.AreEqual("execute anchored feet", new ExecuteAnchored(ID.FacingAnchor.feet).GetCommandString(), "ExecuteAnchored does not return correct GetCommandString");
            Assert.AreEqual("execute as @s[level=1..2]", new ExecuteAs(new Selector() { Level = new Range(1, 2) }).GetCommandString(), "ExecuteAs does not return correct GetCommandString");
            Assert.AreEqual("execute at @s[level=1..2]", new ExecuteAt(new Selector() { Level = new Range(1, 2) }).GetCommandString(), "ExecuteAt does not return correct GetCommandString");
            Assert.AreEqual("execute in the_end", new ExecuteDimension(ID.Dimension.the_end).GetCommandString(), "ExecuteDimension does not return correct GetCommandString");
            Assert.AreEqual("execute facing ~3 ~4 ~5", new ExecuteFacingCoord(new Coords(3,4,5)).GetCommandString(), "ExecuteFacingCoord does not return correct GetCommandString");
            Assert.AreEqual("execute facing entity @s[level=1..2] feet", new ExecuteFacingEntity(new Selector() { Level = new Range(1, 2) }, ID.FacingAnchor.feet).GetCommandString(), "ExecuteFacingEntity does not return correct GetCommandString");
            Assert.AreEqual("execute if block ~0 ~0 ~0 minecraft:anvil[facing=north]", new ExecuteIfBlock(new Coords(), new Block.Anvil(ID.Block.anvil) {SFacing = ID.Facing.north }).GetCommandString(), "ExecuteIfBlock does not return correct GetCommandString");
            Assert.AreEqual("execute if data block ~0 ~0 ~0 test.data", new ExecuteIfData(new BlockDataLocation(new Coords(), "test.data")).GetCommandString(), "ExecuteIfData does not return correct GetCommandString");
            Assert.AreEqual("execute unless blocks ~1 ~1 ~1 ~2 ~2 ~2 3 3 3 masked", new ExecuteIfBlocks(new Coords(1,1,1), new Coords(2,2,2), new Coords(ID.CoordType.Normal, 3, 3, 3), true, false).GetCommandString(), "ExecuteIfBlocks does not return correct GetCommandString");
            Assert.AreEqual("execute if blocks ~1 ~1 ~1 ~2 ~2 ~2 3 3 3 all", new ExecuteIfBlocks(new Coords(1, 1, 1), new Coords(2, 2, 2), new Coords(ID.CoordType.Normal, 3, 3, 3), false, true).GetCommandString(), "ExecuteIfBlocks does not return correct GetCommandString");
            Assert.AreEqual("execute if entity @s[level=1..2]", new ExecuteIfEntity(new Selector() { Level = new Range(1, 2) }).GetCommandString(), "ExecuteIfEntity does not return correct GetCommandString");
            Assert.AreEqual("execute if score @s test matches 1..5", new ExecuteIfScoreMatches(new Selector(), new ScoreObject("test"), new Range(1,5)).GetCommandString(), "ExecuteIfScoreMatches does not return correct GetCommandString");
            Assert.AreEqual("execute if score @s test > @s test", new ExecuteIfScoreRelative(new Selector(), new ScoreObject("test"), ID.IfScoreOperation.Higher, new Selector(), new ScoreObject("test")).GetCommandString(), "ExecuteIfScoreRelative does not return correct GetCommandString");
            Assert.AreEqual("execute positioned ~0 ~0 ~0", new ExecutePosition(new Coords()).GetCommandString(), "ExecutePosition does not return correct GetCommandString");
            Assert.AreEqual("execute positioned as @s[level=1..2]", new ExecutePositionedAs(new Selector() { Level = new Range(1, 2) }).GetCommandString(), "ExecutePositionedAs does not return correct GetCommandString");
            Assert.AreEqual("execute rotated 10 15", new ExecuteRotated(new Rotation(false,10,15)).GetCommandString(), "ExecuteRotated does not return correct GetCommandString");
            Assert.AreEqual("execute rotated as @s[level=1..2]", new ExecuteRotatedAs(new Selector() { Level = new Range(1, 2) }).GetCommandString(), "ExecuteRotatedAs does not return correct GetCommandString");
            Assert.AreEqual("execute store result block ~0 ~0 ~0 test.data double 100.3335", new ExecuteStoreBlock(new Coords(), "test.data", ID.StoreTypes.Double, 100.3335).GetCommandString(), "ExecuteStoreBlock does not return correct GetCommandString");
            Assert.AreEqual("execute store success bossbar test:test value", new ExecuteStoreBossbar(new BossBar("test:test"), true, false).GetCommandString(), "ExecuteStoreBossbar does not return correct GetCommandString");
            Assert.AreEqual("execute store result bossbar test:test max", new ExecuteStoreBossbar(new BossBar("test:test"), false, true).GetCommandString(), "ExecuteStoreBossbar does not return correct GetCommandString");
            Assert.AreEqual("execute store result entity @s cake long 10", new ExecuteStoreEntity(new Selector(), "cake", ID.StoreTypes.Long, 10).GetCommandString(), "ExecuteStoreEntity does not return correct GetCommandString");
            Assert.AreEqual("execute store result score @s test", new ExecuteStoreScore(new Selector(), new ScoreObject("test")).GetCommandString(), "ExecuteStoreScore does not return correct GetCommandString");
            Assert.AreEqual("execute if predicate space:name", new ExecuteIfPredicate(new EmptyPredicate(new EmptyNamespace(new EmptyDatapack("mypack"), "space"), "name")).GetCommandString(), "ExecuteIfPredicate does not return correct GetCommandString");
            Assert.IsNull(new StopExecuteCommand().GetCommandString());
        }

        [TestMethod]
        public void TestExecuteCommandStacking()
        {
            //setup
            ExecuteAt command1 = new ExecuteAt(new Selector());
            ExecuteAlign command2 = new ExecuteAlign(true,false,true);
            TestICommand command3 = new TestICommand();

            command1.ExecuteCommand = command2;
            command2.ExecuteCommand = command3;

            //test
            Assert.AreEqual("execute at @s align xz run hello world", command1.GetCommandString(), "Execute commands does not return correct GetCommandString");
        }

        [TestMethod]
        public void TestChangeCommand()
        {
            //setup
            BaseExecuteCommand command1 = new ExecuteAt(new Selector()) { ExecuteCommand = new ExecuteAlign(true, true, true) };
            BaseExecuteCommand command2 = new ExecuteAt(new Selector()) { ExecuteCommand = new ExecuteAlign(true, true, true) };

            //test
            Assert.IsNull(command1.ChangeCommand(new ExecuteAs(ID.Selector.a)));
            Assert.IsFalse(command1.DoneChanging, "Adding another execute command shouldnt stop the changer");

            Assert.IsNull(command1.ChangeCommand(new SayCommand("hello")));
            Assert.IsTrue(command1.DoneChanging, "Adding an end command should stop the changer");

            Assert.IsNotNull(command1.ChangeCommand(new SayCommand("hello")));
            Assert.IsTrue(command1.DoneChanging, "Execute command ending in a command should stop the changer");

            Assert.IsNull(command2.ChangeCommand(new StopExecuteCommand()));
            Assert.IsTrue(command2.DoneChanging, "Execute command is not done executing after adding stop command");
        }

        [TestMethod]
        public void TestHasEndCommand()
        {
            //setup
            BaseExecuteCommand command1 = new ExecuteAt(new Selector()) { ExecuteCommand = new ExecuteAlign(true, true, true) { ExecuteCommand = new SayCommand("hello") } };
            BaseExecuteCommand command2 = new ExecuteAt(new Selector()) { ExecuteCommand = new ExecuteAlign(true, true, true) { ExecuteCommand = new ExecuteAs(ID.Selector.a) } };

            //test
            Assert.IsTrue(command1.HasEndCommand(), "HasEndCommand doesn't see the end command");
            Assert.IsFalse(command2.HasEndCommand(), "HasEndCommand doesn't see that the last command is an execute command");
        }

        [TestMethod]
        public void TestAddCommand()
        {
            Assert.AreEqual("execute as @a at @p run say hello", new ExecuteAs(ID.Selector.a).AddCommand(new ExecuteAt(ID.Selector.p)).AddCommand(new SayCommand("hello")).GetCommandString(), "AddCommand doesn't work correctly");
        }

        [TestMethod]
        public void TestShallowClone()
        {
            //setup
            BaseCommand command = new ExecuteAs(ID.Selector.s).AddCommand(new ExecuteAt(ID.Selector.a)).AddCommand(new ExecuteAlign());

            //test
            BaseCommand clonedCommand = command.ShallowClone();
            Assert.AreEqual(command.GetCommandString(), clonedCommand.GetCommandString(), "Cloned command doesn't return correct command string");
            ((BaseExecuteCommand)clonedCommand).AddCommand(new SayCommand("hello"));
            Assert.AreNotEqual(command.GetCommandString(), clonedCommand.GetCommandString(), "Cloned command should return a different string since it was changed and old command shouldnt have been changed");
        }
    }
}
