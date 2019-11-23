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
            using (Datapack pack = new Datapack("datapacks", "pack", "a pack", 0, new NoneFileCreator()))
            {
                PackNamespace space = pack.Namespace("space");

                //test
                space.NewFunction("myfunction");
                Assert.IsTrue(pack.FileCreator.GetDirectories().Any(d => d == "datapacks\\pack\\data\\space\\functions\\"), "Directory wasn't created");
                Assert.IsTrue(pack.FileCreator.GetWriters().Any(w => w.path == "datapacks\\pack\\data\\space\\functions\\myfunction.mcfunction"), "File wasn't created");

                space.NewFunction("folder/otherFunction", BaseFile.WriteSetting.OnDispose);
                Assert.IsTrue(pack.FileCreator.GetDirectories().Any(d => d == "datapacks\\pack\\data\\space\\functions\\folder\\"), "Directory wasn't created for file with directory in name");
                Assert.IsFalse(pack.FileCreator.GetWriters().Any(w => w.path == "datapacks\\pack\\data\\space\\functions\\folder\\otherfunction.mcfunction"), "File wasn't supposed to be created yet since its OnDispose");

                pack.Dispose();
                Assert.IsTrue(pack.FileCreator.GetWriters().Any(w => w.path == "datapacks\\pack\\data\\space\\functions\\folder\\otherfunction.mcfunction"), "File is supposed to have been created now since Dispose was ran");
            }
        }

        [TestMethod]
        public void TestAddCommand()
        {
            //setup
            using (Datapack pack = new Datapack("datapacks", "pack", "a pack", 0, new NoneFileCreator()))
            {
                PackNamespace space = pack.Namespace("space");
                Function autoFunction = space.NewFunction("autofunction");
                TextWriter autoFunctionWriter = pack.FileCreator.GetWriters().First(w => w.path == "datapacks\\pack\\data\\space\\functions\\autofunction.mcfunction").writer;
                Function onDisposeFunction = space.NewFunction("disposefunction", BaseFile.WriteSetting.OnDispose);

                //test
                autoFunction.AddCommand(new SayCommand("hello world"));
                autoFunction.AddCommand(new ClearCommand(ID.Selector.s));
                Assert.AreEqual(0, autoFunction.Commands.Count, "Commands wasn't removed from command list");
                Assert.AreEqual("say hello world" + Environment.NewLine + "clear @s" + Environment.NewLine, autoFunctionWriter.ToString(), "AddCommand failed to write the commands correctly");

                //text execute
                autoFunction = space.NewFunction("autofunction2");
                autoFunctionWriter = pack.FileCreator.GetWriters().First(w => w.path == "datapacks\\pack\\data\\space\\functions\\autofunction2.mcfunction").writer;
                autoFunction.AddCommand(new ExecuteAs(ID.Selector.s));
                Assert.AreEqual("", autoFunctionWriter.ToString(), "Execute command with no end shouldn't write anything yet");
                autoFunction.AddCommand(new ExecuteAt(ID.Selector.s));
                Assert.AreEqual(1, autoFunction.Commands.Count, "Commands wasn't added correctly to command list");
                autoFunction.AddCommand(new SayCommand("end"));
                Assert.AreEqual("execute as @s at @s run say end" + Environment.NewLine, autoFunctionWriter.ToString(), "Execute command with end should write");
            }
        }

        [TestMethod]
        public void TestDispose()
        {
            //setup
            using (Datapack pack = new Datapack("datapacks", "pack", "a pack", 0, new NoneFileCreator()))
            {
                PackNamespace space = pack.Namespace("space");
                Function onDisposeFunction = space.NewFunction("disposefunction", BaseFile.WriteSetting.OnDispose);
                Function autoFunction = space.NewFunction("autofunction", BaseFile.WriteSetting.Auto);
                TextWriter autoFunctionWriter = pack.FileCreator.GetWriters().First(w => w.path == "datapacks\\pack\\data\\space\\functions\\autofunction.mcfunction").writer;

                //test
                onDisposeFunction.AddCommand(new SayCommand("hello world"));
                onDisposeFunction.AddCommand(new ExecuteAs(ID.Selector.a));
                Assert.AreEqual(2, onDisposeFunction.Commands.Count, "Commands wasn't added to command list");
                onDisposeFunction.Dispose();
                TextWriter onDisposeFunctionWriter = pack.FileCreator.GetWriters().First(w => w.path == "datapacks\\pack\\data\\space\\functions\\disposefunction.mcfunction").writer;
                Assert.AreEqual("say hello world" + Environment.NewLine + "execute as @a" + Environment.NewLine, onDisposeFunctionWriter.ToString(), "Dispose function isn't writing commands correctly on dispose");

                autoFunction.AddCommand(new SayCommand("hello world"));
                autoFunction.AddCommand(new ExecuteAs(ID.Selector.a));
                autoFunction.Dispose();
                Assert.AreEqual("say hello world" + Environment.NewLine + "execute as @a" + Environment.NewLine, autoFunctionWriter.ToString(), "Auto function isn't writing commands correctly on dispose");
            }
        }

        [TestMethod]
        public void TestNewChild()
        {
            //setup
            using (Datapack pack = new Datapack("datapacks", "pack", "a pack", 0, new NoneFileCreator()))
            {
                PackNamespace space = pack.Namespace("space");
                Function function = space.NewFunction("function", BaseFile.WriteSetting.Auto);

                //test
                Function child = function.NewChild(null, f => 
                {
                    f.AddCommand(new SayCommand("hello"));
                }, BaseFile.WriteSetting.Auto);

                Assert.IsTrue(pack.FileCreator.GetDirectories().Any(d => d == "datapacks\\pack\\data\\space\\functions\\function\\"), "Child directory wasn't created correctly");
                TextWriter writer = pack.FileCreator.GetWriters().SingleOrDefault(w => w.path == "datapacks\\pack\\data\\space\\functions\\function\\1.mcfunction").writer;
                Assert.IsNotNull(writer, "Child file wasn't created");
                Assert.AreEqual("say hello" + Environment.NewLine, writer.ToString(), "FunctionCreator didn't run correctly");
            }
        }

        [TestMethod]
        public void TestNewSibling()
        {
            //setup
            using (Datapack pack = new Datapack("datapacks", "pack", "a pack", 0, new NoneFileCreator()))
            {
                PackNamespace space = pack.Namespace("space");
                Function function = space.NewFunction("function", BaseFile.WriteSetting.Auto);

                //test
                Function sibling = function.NewSibling("folder/file", f =>
                {
                    f.AddCommand(new SayCommand("hello"));
                }, BaseFile.WriteSetting.Auto);

                Assert.IsTrue(pack.FileCreator.GetDirectories().Any(d => d == "datapacks\\pack\\data\\space\\functions\\folder\\"), "sibling directory wasn't created correctly");
                TextWriter writer = pack.FileCreator.GetWriters().SingleOrDefault(w => w.path == "datapacks\\pack\\data\\space\\functions\\folder\\file.mcfunction").writer;
                Assert.IsNotNull(writer, "sibling file wasn't created");
                Assert.AreEqual("say hello" + Environment.NewLine, writer.ToString(), "FunctionCreator didn't run correctly");
            }
        }

        [TestMethod]
        public void TestCustomTreeSearch()
        {
            //setup
            using (Datapack pack = new Datapack("datapacks", "pack", "a pack", 0, new NoneFileCreator()))
            {
                PackNamespace space = pack.Namespace("space");
                Function function = space.NewFunction("function", BaseFile.WriteSetting.Auto);

                //test
                function.Custom.TreeSearch(
                    (min, max) => new ExecuteIfScoreMatches("#s", new ScoreObject("scores"), new Range(min, max)),
                    (number) => new SayCommand(number.ToString()),
                    5,
                    9
                    ); ;

                var treeWriters = pack.FileCreator.GetWriters().Where(w => w.path.StartsWith("datapacks\\pack\\data\\space\\functions\\")).ToList();
                Assert.AreEqual(4, treeWriters.Count, "The correct amount of writers wasn't created");
                Assert.AreEqual("execute if score #s scores matches 5..7 run function space:function/5-7" + Environment.NewLine +
                    "execute if score #s scores matches 8..9 run function space:function/8-9" + Environment.NewLine, treeWriters.Single(w => w.path.EndsWith("function.mcfunction")).writer.ToString(), "Tree base wasn't written correctly");
                Assert.AreEqual("execute if score #s scores matches 5..6 run function space:function/5-6" + Environment.NewLine +
                    "execute if score #s scores matches 7 run say 7" + Environment.NewLine, treeWriters.Single(w => w.path.EndsWith("5-7.mcfunction")).writer.ToString(), "Tree branch wasn't written correctly");
            }
        }

        [TestMethod]
        public void TestCustomSummonExecute()
        {
            //setup
            using (Datapack pack = new Datapack("datapacks", "pack", "a pack", 0, new NoneFileCreator()))
            {
                PackNamespace space = pack.Namespace("space");
                Function function = space.NewFunction("function", BaseFile.WriteSetting.Auto);

                //test
                function.Custom.SummonExecute(new Entity.Armorstand() { Tags = new Tag[] { "ATag" } }, new Coords(1,2,3), "execute", (f) => 
                {
                    f.World.Say("hello");
                });

                Assert.AreEqual("summon armor_stand ~1 ~2 ~3 {Tags:[\"ATag\",\"SharpSummon\"]}" + Environment.NewLine +
                    "execute as @e[tag=SharpSummon] at @s run function space:execute" + Environment.NewLine, pack.FileCreator.GetWriters().Single(w => w.path == "datapacks\\pack\\data\\space\\functions\\function.mcfunction").writer.ToString(), "Summon and execute file written correctly");
                Assert.AreEqual("tag @s remove SharpSummon" + Environment.NewLine+
                    "say hello" + Environment.NewLine, pack.FileCreator.GetWriters().Single(w => w.path == "datapacks\\pack\\data\\space\\functions\\execute.mcfunction").writer.ToString(), "executing file written correctly");
            }
        }
    }
}
