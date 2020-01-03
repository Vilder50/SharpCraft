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
    public class ScoreboardCommandsTests
    {
        [TestMethod]
        public void ScoreboardObjectiveAddCommandTest()
        {
            Assert.AreEqual("scoreboard objectives add score dummy [{\"text\":\"name\"}]", new ScoreboardObjectiveAddCommand(new ScoreObject("score"), "dummy", new JSON() { Text = "name" }).GetCommandString());
            Assert.AreEqual("scoreboard objectives add score dummy", new ScoreboardObjectiveAddCommand(new ScoreObject("score"), "dummy", null).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new ScoreboardObjectiveAddCommand(null, "dummy", null));
            Assert.ThrowsException<ArgumentNullException>(() => new ScoreboardObjectiveAddCommand(new ScoreObject("score"), null, null));
        }

        [TestMethod]
        public void ScoreboardObjectiveListCommandTest()
        {
            Assert.AreEqual("scoreboard objectives list", new ScoreboardObjectiveListCommand().GetCommandString());
        }

        [TestMethod]
        public void ScoreboardObjectiveChangeNameCommandTest()
        {
            Assert.AreEqual("scoreboard objectives modify score displayname [{\"text\":\"name\"}]", new ScoreboardObjectiveChangeNameCommand(new ScoreObject("score"), new JSON() { Text = "name" }).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new ScoreboardObjectiveChangeNameCommand(null, new JSON() { Text = "name" }));
            Assert.ThrowsException<ArgumentNullException>(() => new ScoreboardObjectiveChangeNameCommand(new ScoreObject("score"), null));
        }

        [TestMethod]
        public void ScoreboardObjectiveChangeRenderCommandTest()
        {
            Assert.AreEqual("scoreboard objectives modify score rendertype hearts", new ScoreboardObjectiveChangeRenderCommand(new ScoreObject("score"), ID.ObjectiveRender.hearts).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new ScoreboardObjectiveChangeRenderCommand(null, ID.ObjectiveRender.hearts));
        }

        [TestMethod]
        public void ScoreboardObjectiveRemoveCommandTest()
        {
            Assert.AreEqual("scoreboard objectives remove score", new ScoreboardObjectiveRemoveCommand(new ScoreObject("score")).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new ScoreboardObjectiveRemoveCommand(null));
        }

        [TestMethod]
        public void ScoreboardSetDisplayCommandTest()
        {
            Assert.AreEqual("scoreboard objectives setdisplay sidebar score", new ScoreboardSetDisplayCommand(new ScoreObject("score"), ID.ScoreDisplay.sidebar).GetCommandString());
            Assert.AreEqual("scoreboard objectives setdisplay sidebar", new ScoreboardSetDisplayCommand(null, ID.ScoreDisplay.sidebar).GetCommandString());
        }

        [TestMethod]
        public void ScoreboardSetTeamDisplayCommandTest()
        {
            Assert.AreEqual("scoreboard objectives setdisplay sidebar.team.blue score", new ScoreboardSetTeamDisplayCommand(new ScoreObject("score"), ID.MinecraftColor.blue).GetCommandString());
            Assert.AreEqual("scoreboard objectives setdisplay sidebar.team.blue", new ScoreboardSetTeamDisplayCommand(null, ID.MinecraftColor.blue).GetCommandString());
        }

        [TestMethod]
        public void ScoreboardValueChangeCommandTest()
        {
            Assert.AreEqual("scoreboard players remove @s score 10", new ScoreboardValueChangeCommand(ID.Selector.s, new ScoreObject("score"), ID.ScoreChange.remove, 10).GetCommandString());
            Assert.AreEqual("scoreboard players add @s score 10", new ScoreboardValueChangeCommand(ID.Selector.s, new ScoreObject("score"), ID.ScoreChange.remove, -10).GetCommandString());
            Assert.AreEqual("scoreboard players remove @s score 10", new ScoreboardValueChangeCommand(ID.Selector.s, new ScoreObject("score"), ID.ScoreChange.add, -10).GetCommandString());
            Assert.AreEqual("scoreboard players add @s score 10", new ScoreboardValueChangeCommand(ID.Selector.s, new ScoreObject("score"), ID.ScoreChange.add, 10).GetCommandString());
            Assert.AreEqual("scoreboard players set @s score 10", new ScoreboardValueChangeCommand(ID.Selector.s, new ScoreObject("score"), ID.ScoreChange.set, 10).GetCommandString());
            Assert.AreEqual("scoreboard players set @s score -10", new ScoreboardValueChangeCommand(ID.Selector.s, new ScoreObject("score"), ID.ScoreChange.set, -10).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new ScoreboardValueChangeCommand(null, new ScoreObject("score"), ID.ScoreChange.remove, 10));
            Assert.ThrowsException<ArgumentNullException>((Func<object>)(() => new ScoreboardValueChangeCommand((SharpCraft.BaseSelector)ID.Selector.s, (ScoreObject)null, (ID.ScoreChange)ID.ScoreChange.remove, (int)10)));
        }

        [TestMethod]
        public void ScoreboardValueGetCommandTest()
        {
            Assert.AreEqual("scoreboard players get @s score", new ScoreboardValueGetCommand(ID.Selector.s, new ScoreObject("score")).GetCommandString());

            Assert.ThrowsException<ArgumentException>((Func<object>)(() => new ScoreboardValueGetCommand((SharpCraft.BaseSelector)ID.Selector.a, (ScoreObject)new ScoreObject((string)"score"))));
            Assert.ThrowsException<ArgumentNullException>(() => new ScoreboardValueGetCommand(null, new ScoreObject("score")));
            Assert.ThrowsException<ArgumentNullException>((Func<object>)(() => new ScoreboardValueGetCommand((SharpCraft.BaseSelector)ID.Selector.s, (ScoreObject)null)));
        }

        [TestMethod]
        public void ScoreboardEnableTriggerCommandTest()
        {
            Assert.AreEqual("scoreboard players enable @a score", new ScoreboardEnableTriggerCommand(ID.Selector.a, new ScoreObject("score")).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new ScoreboardEnableTriggerCommand(null, new ScoreObject("score")));
            Assert.ThrowsException<ArgumentNullException>((Func<object>)(() => new ScoreboardEnableTriggerCommand((SharpCraft.BaseSelector)ID.Selector.a, (ScoreObject)null)));
        }

        [TestMethod]
        public void ScoreboardResetCommandTest()
        {
            Assert.AreEqual("scoreboard players reset @a score", new ScoreboardResetCommand(ID.Selector.a, new ScoreObject("score")).GetCommandString());
            Assert.AreEqual("scoreboard players reset @a", new ScoreboardResetCommand(ID.Selector.a, null).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new ScoreboardResetCommand(null, new ScoreObject("score")));
        }

        [TestMethod]
        public void ScoreboardListCommandTest()
        {
            Assert.AreEqual("scoreboard players list @s", new ScoreboardListCommand(ID.Selector.s).GetCommandString());

            Assert.ThrowsException<ArgumentException>((Func<object>)(() => new ScoreboardListCommand((SharpCraft.BaseSelector)ID.Selector.a)));
            Assert.ThrowsException<ArgumentNullException>(() => new ScoreboardListCommand(null));
        }

        [TestMethod]
        public void ScoreboardOperationCommandTest()
        {
            ScoreObject scoreObject1 = new ScoreObject("score1");
            ScoreObject scoreObject2 = new ScoreObject("score2");
            BaseSelector selector1 = ID.Selector.s;
            BaseSelector selector2 = ID.Selector.a;

            Assert.AreEqual("scoreboard players operation @s score1 += @a score2", new ScoreboardOperationCommand(selector1, scoreObject1, ID.Operation.Add, selector2, scoreObject2).GetCommandString());
            Assert.AreEqual("scoreboard players operation @s score1 /= @a score2", new ScoreboardOperationCommand(selector1, scoreObject1, ID.Operation.Divide, selector2, scoreObject2).GetCommandString());
            Assert.AreEqual("scoreboard players operation @s score1 = @a score2", new ScoreboardOperationCommand(selector1, scoreObject1, ID.Operation.Equel, selector2, scoreObject2).GetCommandString());
            Assert.AreEqual("scoreboard players operation @s score1 > @a score2", new ScoreboardOperationCommand(selector1, scoreObject1, ID.Operation.GetHigher, selector2, scoreObject2).GetCommandString());
            Assert.AreEqual("scoreboard players operation @s score1 < @a score2", new ScoreboardOperationCommand(selector1, scoreObject1, ID.Operation.GetLowest, selector2, scoreObject2).GetCommandString());
            Assert.AreEqual("scoreboard players operation @s score1 *= @a score2", new ScoreboardOperationCommand(selector1, scoreObject1, ID.Operation.Multiply, selector2, scoreObject2).GetCommandString());
            Assert.AreEqual("scoreboard players operation @s score1 %= @a score2", new ScoreboardOperationCommand(selector1, scoreObject1, ID.Operation.Remainder, selector2, scoreObject2).GetCommandString());
            Assert.AreEqual("scoreboard players operation @s score1 -= @a score2", new ScoreboardOperationCommand(selector1, scoreObject1, ID.Operation.Subtract, selector2, scoreObject2).GetCommandString());
            Assert.AreEqual("scoreboard players operation @s score1 >< @a score2", new ScoreboardOperationCommand(selector1, scoreObject1, ID.Operation.Switch, selector2, scoreObject2).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new ScoreboardOperationCommand(null, scoreObject1, ID.Operation.Add, selector2, scoreObject2));
            Assert.ThrowsException<ArgumentNullException>(() => new ScoreboardOperationCommand(selector1, null, ID.Operation.Add, selector2, scoreObject2));
            Assert.ThrowsException<ArgumentNullException>(() => new ScoreboardOperationCommand(selector1, scoreObject1, ID.Operation.Add, null, scoreObject2));
            Assert.ThrowsException<ArgumentNullException>(() => new ScoreboardOperationCommand(selector1, scoreObject1, ID.Operation.Add, selector2, null));
        }
    }
}
