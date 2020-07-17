using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpCraft;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using SharpCraft.Commands;

namespace SharpCraft.Tests.PackItems
{
    [TestClass]
    public class FunctionTests
    {
        [TestMethod]
        public void TestFunction()
        {
            //setup
            using Datapack pack = new Datapack("datapacks", "pack", "a pack", 0, new NoneFileCreator());
            PackNamespace space = pack.Namespace("space");

            //test
            space.Function("myfunction");
            Assert.IsTrue(pack.FileCreator.GetDirectories().Any(d => d == "datapacks/pack/data/space/functions/"), "Directory wasn't created");
            Assert.IsTrue(pack.FileCreator.GetWriters().Any(w => w.path == "datapacks/pack/data/space/functions/myfunction.mcfunction"), "File wasn't created");

            space.Function("folder/otherFunction", BaseFile.WriteSetting.OnDispose);
            Assert.IsFalse(pack.FileCreator.GetDirectories().Any(d => d == "datapacks/pack/data/space/functions/folder/"), "Directory wasn't supposed to be created yet since its OnDispose");
            Assert.IsFalse(pack.FileCreator.GetWriters().Any(w => w.path == "datapacks/pack/data/space/functions/folder/otherfunction.mcfunction"), "File wasn't supposed to be created yet since its OnDispose");

            pack.Dispose();
            Assert.IsTrue(pack.FileCreator.GetDirectories().Any(d => d == "datapacks/pack/data/space/functions/folder/"), "Directory wasn't created for file with directory in name");
            Assert.IsTrue(pack.FileCreator.GetWriters().Any(w => w.path == "datapacks/pack/data/space/functions/folder/otherfunction.mcfunction"), "File is supposed to have been created now since Dispose was ran");
        }

        [TestMethod]
        public void TestAddCommand()
        {
            //setup
            using Datapack pack = new Datapack("datapacks", "pack", "a pack", 0, new NoneFileCreator());
            PackNamespace space = pack.Namespace("space");
            Function autoFunction = space.Function("autofunction");
            TextWriter autoFunctionWriter = pack.FileCreator.GetWriters().First(w => w.path == "datapacks/pack/data/space/functions/autofunction.mcfunction").writer as TextWriter;
            Function onDisposeFunction = space.Function("disposefunction", BaseFile.WriteSetting.OnDispose);

            //test
            autoFunction.AddCommand(new SayCommand("hello world"));
            autoFunction.AddCommand(new ClearCommand(ID.Selector.s));
            Assert.AreEqual(0, autoFunction.Commands.Count, "Commands wasn't removed from command list");
            Assert.AreEqual("say hello world" + Environment.NewLine + "clear @s" + Environment.NewLine, autoFunctionWriter.ToString(), "AddCommand failed to write the commands correctly");

            //text execute
            autoFunction = space.Function("autofunction2");
            autoFunctionWriter = pack.FileCreator.GetWriters().First(w => w.path == "datapacks/pack/data/space/functions/autofunction2.mcfunction").writer as TextWriter;
            autoFunction.AddCommand(new ExecuteAs(ID.Selector.s));
            Assert.AreEqual("", autoFunctionWriter.ToString(), "Execute command with no end shouldn't write anything yet");
            autoFunction.AddCommand(new ExecuteAt(ID.Selector.s));
            Assert.AreEqual(1, autoFunction.Commands.Count, "Commands wasn't added correctly to command list");
            autoFunction.AddCommand(new SayCommand("end"));
            Assert.AreEqual("execute as @s at @s run say end" + Environment.NewLine, autoFunctionWriter.ToString(), "Execute command with end should write");
        }

        [TestMethod]
        public void TestDispose()
        {
            //setup
            using Datapack pack = new Datapack("datapacks", "pack", "a pack", 0, new NoneFileCreator());
            PackNamespace space = pack.Namespace("space");
            Function onDisposeFunction = space.Function("disposefunction", BaseFile.WriteSetting.OnDispose);
            Function autoFunction = space.Function("autofunction", BaseFile.WriteSetting.Auto);
            TextWriter autoFunctionWriter = pack.FileCreator.GetWriters().First(w => w.path == "datapacks/pack/data/space/functions/autofunction.mcfunction").writer as TextWriter;

            //test
            onDisposeFunction.AddCommand(new SayCommand("hello world"));
            onDisposeFunction.AddCommand(new ExecuteAs(ID.Selector.a));
            Assert.AreEqual(2, onDisposeFunction.Commands.Count, "Commands wasn't added to command list");
            onDisposeFunction.Dispose();
            TextWriter onDisposeFunctionWriter = pack.FileCreator.GetWriters().First(w => w.path == "datapacks/pack/data/space/functions/disposefunction.mcfunction").writer as TextWriter;
            Assert.AreEqual("say hello world" + Environment.NewLine + "execute as @a" + Environment.NewLine, onDisposeFunctionWriter.ToString(), "Dispose function isn't writing commands correctly on dispose");
            Assert.IsNull(onDisposeFunction.Commands, "Commands wasn't cleared");

            autoFunction.AddCommand(new SayCommand("hello world"));
            autoFunction.AddCommand(new ExecuteAs(ID.Selector.a));
            autoFunction.Dispose();
            Assert.AreEqual("say hello world" + Environment.NewLine + "execute as @a" + Environment.NewLine, autoFunctionWriter.ToString(), "Auto function isn't writing commands correctly on dispose");
        }

        [TestMethod]
        public void TestNewChild()
        {
            //setup
            using Datapack pack = new Datapack("datapacks", "pack", "a pack", 0, new NoneFileCreator());
            PackNamespace space = pack.Namespace("space");
            Function function = space.Function("function", BaseFile.WriteSetting.Auto);

            //test
            Function child = function.NewChild(null, f =>
            {
                f.AddCommand(new SayCommand("hello"));
            }, BaseFile.WriteSetting.Auto);

            Assert.IsTrue(pack.FileCreator.GetDirectories().Any(d => d == "datapacks/pack/data/space/functions/"), "Child directory wasn't created correctly");
            TextWriter writer = pack.FileCreator.GetWriters().SingleOrDefault(w => w.path == "datapacks/pack/data/space/functions/1.mcfunction").writer as TextWriter;
            Assert.IsNotNull(writer, "Child file wasn't created");
            Assert.AreEqual("say hello" + Environment.NewLine, writer.ToString(), "FunctionCreator didn't run correctly");
        }

        [TestMethod]
        public void TestNewSibling()
        {
            //setup
            using Datapack pack = new Datapack("datapacks", "pack", "a pack", 0, new NoneFileCreator());
            PackNamespace space = pack.Namespace("space");
            Function function = space.Function("function", BaseFile.WriteSetting.Auto);

            //test
            Function sibling = function.NewSibling("folder/file", f =>
            {
                f.AddCommand(new SayCommand("hello"));
            }, BaseFile.WriteSetting.Auto);

            Assert.IsTrue(pack.FileCreator.GetDirectories().Any(d => d == "datapacks/pack/data/space/functions/folder/"), "sibling directory wasn't created correctly");
            TextWriter writer = pack.FileCreator.GetWriters().SingleOrDefault(w => w.path == "datapacks/pack/data/space/functions/folder/file.mcfunction").writer as TextWriter;
            Assert.IsNotNull(writer, "sibling file wasn't created");
            Assert.AreEqual("say hello" + Environment.NewLine, writer.ToString(), "FunctionCreator didn't run correctly");
        }

        [TestMethod]
        public void TestCustomTreeSearch()
        {
            //setup
            using Datapack pack = new Datapack("datapacks", "pack", "a pack", 0, new NoneFileCreator());
            PackNamespace space = pack.Namespace("space");
            Function function = space.Function("function", BaseFile.WriteSetting.Auto);

            //test
            //2 branches
            function.Custom.TreeSearch(
                (min, max) => new ExecuteIfScoreMatches("#s", new Objective("scores"), new MCRange(min, max)),
                (number) => new SayCommand(number.ToString()),
                5,
                9
                );

            var treeWriters = pack.FileCreator.GetWriters().Where(w => w.path.StartsWith("datapacks/pack/data/space/functions/")).ToList();
            Assert.AreEqual(4, treeWriters.Count, "The correct amount of writers wasn't created");
            Assert.AreEqual("execute if score #s scores matches 5..6 run function space:function/5-6" + Environment.NewLine +
                "execute if score #s scores matches 7..9 run function space:function/7-9" + Environment.NewLine, treeWriters.Single(w => w.path.EndsWith("function.mcfunction")).writer.ToString(), "Tree base wasn't written correctly");
            Assert.AreEqual("execute if score #s scores matches 7 run say 7" + Environment.NewLine +
                "execute if score #s scores matches 8..9 run function space:function/8-9" + Environment.NewLine, treeWriters.Single(w => w.path.EndsWith("7-9.mcfunction")).writer.ToString(), "Tree branch wasn't written correctly");

            //4 branches
            Function fourBranches = space.Function("four/function", BaseFile.WriteSetting.Auto);
            fourBranches.Custom.TreeSearch(
                (min, max) => new ExecuteIfScoreMatches("#s", new Objective("scores"), new MCRange(min, max)),
                (number) => new SayCommand(number.ToString()),
                5,
                9,
                4
                );
            treeWriters = pack.FileCreator.GetWriters().Where(w => w.path.StartsWith("datapacks/pack/data/space/functions/four")).ToList();
            Assert.AreEqual(2, treeWriters.Count, "The correct amount of writers wasn't created for a 4 tree search");
            Assert.AreEqual("execute if score #s scores matches 8 run say 8" + Environment.NewLine +
                "execute if score #s scores matches 9 run say 9" + Environment.NewLine, treeWriters.Single(w => w.path.EndsWith("8-9.mcfunction")).writer.ToString(), "4 Tree branch wasn't written correctly");
        }

        [TestMethod]
        public void TestCommandListener()
        {
            //setup
            using Datapack pack = new Datapack("datapacks", "pack", "a pack", 0, new NoneFileCreator());
            PackNamespace space = pack.Namespace("space");
            Function function = space.Function("function", BaseFile.WriteSetting.Auto);
            Function otherfunction = space.Function("function2", BaseFile.WriteSetting.OnDispose);
            bool commandWritten = false;
            function.AddCommandListener((f, c) => 
            {
                if (c is SayCommand sayCommand)
                {
                    sayCommand.Text = "2";
                }
                commandWritten = true;
            });
            otherfunction.AddCommandListener((f,c) => commandWritten = true);

            //test
            function.World.Say("1");
            Assert.IsTrue(commandWritten, "Command listener wasn't called");
            Assert.AreEqual("say 2" + Environment.NewLine, pack.FileCreator.GetWriters().Single(w => w.path.EndsWith("function.mcfunction")).writer.ToString(), "Command wasn't changed by command listener");

            commandWritten = false;
            function.Execute.As(ID.Selector.s);
            Assert.IsFalse(commandWritten, "Command listener shouldn't have been called (no command was written yet)");
            function.World.Say("123");
            Assert.IsTrue(commandWritten, "Command listener should have been called since execute listener is done");

            commandWritten = false;
            otherfunction.World.Say("123");
            Assert.IsFalse(commandWritten, "Command listener shouldn't have been called (dispose isn't called yet)");
            otherfunction.Dispose();
            Assert.IsTrue(commandWritten, "Command listener should have been called (dispose was called)");
        }

        [TestMethod]
        public void TestCustomSummonExecute()
        {
            //setup
            using Datapack pack = new Datapack("datapacks", "pack", "a pack", 0, new NoneFileCreator());
            PackNamespace space = pack.Namespace("space");
            Function function = space.Function("function", BaseFile.WriteSetting.Auto);

            //test
            function.Custom.SummonExecute(new Entities.Armorstand() { Tags = new Tag[] { "ATag" } }, new Coords(1, 2, 3), "execute", (f) =>
              {
                  f.World.Say("hello");
              });

            Assert.AreEqual("summon armor_stand ~1 ~2 ~3 {Tags:[\"ATag\",\"SharpSummon\"]}" + Environment.NewLine +
                "execute as @e[tag=SharpSummon] at @s run function space:execute" + Environment.NewLine, pack.FileCreator.GetWriters().Single(w => w.path == "datapacks/pack/data/space/functions/function.mcfunction").writer.ToString(), "Summon and execute file written correctly");
            Assert.AreEqual("tag @s remove SharpSummon" + Environment.NewLine +
                "say hello" + Environment.NewLine, pack.FileCreator.GetWriters().Single(w => w.path == "datapacks/pack/data/space/functions/execute.mcfunction").writer.ToString(), "executing file written correctly");
        }

        [TestMethod]
        public void TestEmptyFunction()
        {
            Assert.AreEqual("name:func", new FileMocks.MockFunction(EmptyDatapack.GetPack().Namespace("name"), "func").GetNamespacedName(), "EmptyFunction doesn't reutrn correct string");
            Assert.AreEqual("space:name", ((FileMocks.MockFunction)"space:name").GetNamespacedName(), "Implicit string to function conversion converts incorrectly");
        }

        [TestMethod]
        public void TestConstantScores()
        {
            //setup
            using Datapack pack = new Datapack("datapacks", "pack", "a pack", 0, new NoneFileCreator());
            PackNamespace space = pack.Namespace("space");
            Function function = space.Function("function", BaseFile.WriteSetting.OnDispose);

            //test
            function.Entity.Score.Operation(ID.Selector.s, new Objective("scores"), ID.Operation.Divide, 3);
            function.Entity.Score.Operation(ID.Selector.s, new Objective("scores"), ID.Operation.Multiply, 5);
            function.Entity.Score.Operation(ID.Selector.s, new Objective("scores"), ID.Operation.GetHigher, 3);

            Assert.AreSame((function.Commands[0] as ScoreboardOperationCommand).Selector2, (function.Commands[2] as ScoreboardOperationCommand).Selector2, "Constant value giver doesn't return same selector for same number");
            Assert.AreEqual("5", ((function.Commands[1] as ScoreboardOperationCommand).Selector2 as NameSelector).Name, "Name selector for selecting constant values are incorrect.");
        }

        [TestMethod]
        public void TestCustomSetToScoreOperation()
        {
            //setup
            using Datapack pack = new Datapack("datapacks", "pack", "a pack", 0, new NoneFileCreator());
            PackNamespace space = pack.Namespace("space");
            Function function = space.Function("function", BaseFile.WriteSetting.OnDispose);

            ScoreValue value1 = new ScoreValue(new Selector(ID.Selector.a) { Limit = 1 }, new Objective("Cakes"));
            ScoreValue value2 = new ScoreValue(new Selector(ID.Selector.e) { Limit = 1 }, new Objective("Tests"));

            //test
            function.Custom.SetToScoreOperation(ID.Selector.s, new Objective("Score"), (value1 + 5) * (value2 + 10));
            Assert.AreEqual(6, function.Commands.Count, "Operation didn't add the correct amount of commands to the function");
            Assert.AreEqual("Score", (function.Commands[5] as ScoreboardOperationCommand).Objective1.Name, "Operation didn't end up setting the correct score");
        }

        [TestMethod]
        public void TestCustomGroupCommands()
        {
            //setup
            using Datapack pack = new Datapack("datapacks", "pack", "a pack", 0, new NoneFileCreator());
            PackNamespace space = pack.Namespace("space");
            Function function = space.Function("function", BaseFile.WriteSetting.OnDispose);

            BaseCommand command1 = new ExecuteAt(ID.Selector.s);
            BaseCommand command2 = new SayCommand("123");
            BaseCommand command3 = new SayMeCommand("123");
            BaseCommand command4 = new ExecuteAs(ID.Selector.s);

            //test
            //without execute at start
            function.Custom.GroupCommands((f) =>
            {
                f.AddCommand(command1.ShallowClone());
                f.AddCommand(command2);
                f.AddCommand(command3);
            });
            Assert.AreEqual("execute at @s run say 123", function.Commands[0].GetCommandString(), "Get first grouped command string returned wrong string");
            Assert.AreEqual("me 123", function.Commands[1].GetCommandString(), "Get second grouped command string returned wrong string");

            //with execute at start
            function.Commands.Clear();
            function.AddCommand(command1.ShallowClone());
            function.Custom.GroupCommands((f) =>
            {
                f.AddCommand(command2);
                f.AddCommand(command4.ShallowClone());
                f.AddCommand(command3);
            });
            Assert.AreEqual("execute at @s run say 123", function.Commands[0].GetCommandString(), "Get first executed grouped command string returned wrong string");
            Assert.AreEqual("execute at @s as @s run me 123", function.Commands[1].GetCommandString(), "Get second executed grouped command string returned wrong string");

            //using function
            function.Commands.Clear();
            function.AddCommand(command1.ShallowClone());
            function.Custom.GroupCommands((f) =>
            {
                f.AddCommand(command2);
                f.AddCommand(command1.ShallowClone());
                f.AddCommand(command3);
            }, true);
            Function addedFunction = ((function.Commands[0] as BaseExecuteCommand).ExecuteCommand as RunFunctionCommand).Function as Function;
            Assert.AreEqual("say 123", addedFunction.Commands[0].GetCommandString(), "Get first function grouped command string returned wrong string");
            Assert.AreEqual("execute at @s run me 123", addedFunction.Commands[1].GetCommandString(), "Get second function grouped command string returned wrong string");
            Assert.AreEqual(2, addedFunction.Commands.Count, "function grouped command doesn't contain the correct amount of commands");

            space.AddSetting(NamespaceSettings.GetSettings().FunctionGroupedCommands());
            function.Commands.Clear();
            function.AddCommand(command1.ShallowClone());
            function.Custom.GroupCommands((f) =>
            {
                f.AddCommand(command2);
                f.AddCommand(command1.ShallowClone());
                f.AddCommand(command3);
            });
            addedFunction = ((function.Commands[0] as BaseExecuteCommand).ExecuteCommand as RunFunctionCommand).Function as Function;
            Assert.AreEqual(2, addedFunction.Commands.Count, "automatic function grouped command doesn't get the correct commands");
        }

        [TestMethod]
        public void TestCustomIfElseCommand()
        {
            //setup
            using Datapack pack = new Datapack("datapacks", "pack", "a pack", 0, new NoneFileCreator());
            PackNamespace space = pack.Namespace("space");
            Function function = space.Function("function", BaseFile.WriteSetting.OnDispose);

            //test
            function.Custom.IfElse(new ExecuteIfBlock(new Coords(), ID.Block.stone), (ifFunction) =>
            {
                ifFunction.World.Say("block!");
            }, (elseFunction) =>
            {
                elseFunction.World.Say("no block!");
            }, "if", "else");

            Assert.AreEqual("scoreboard players set #ifelse math 0", function.Commands[0].GetCommandString(), "Base function commands aren't generated correctly");
            Assert.AreEqual("execute if block ~ ~ ~ minecraft:stone run function space:if", function.Commands[1].GetCommandString(), "Base function commands aren't generated correctly");
            Assert.AreEqual("execute if score #ifelse math matches 0 run function space:else", function.Commands[2].GetCommandString(), "Base function commands aren't generated correctly");

            Assert.AreEqual("say block!" + Environment.NewLine
                + "scoreboard players set #ifelse math 1" + Environment.NewLine, pack.FileCreator.GetWriters().Single(w => w.path == "datapacks/pack/data/space/functions/if.mcfunction").writer.ToString());
            Assert.AreEqual("say no block!" + Environment.NewLine, pack.FileCreator.GetWriters().Single(w => w.path == "datapacks/pack/data/space/functions/else.mcfunction").writer.ToString());
        }
    }
}
