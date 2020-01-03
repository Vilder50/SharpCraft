using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpCraft;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using SharpCraft.AdvancementObjects;

namespace SharpCraft.Tests.PackItems
{
    [TestClass]
    public class PredicateTests
    {
        [TestMethod]
        public void TestPredicate()
        {
            //setup
            using (Datapack pack = new Datapack("datapacks", "pack", "a pack", 0, new NoneFileCreator()))
            {
                PackNamespace space = pack.Namespace("space");

                //test
                space.Predicate("MyPredicate", new Conditions.RandomCondition(0.5));
                Assert.IsTrue(pack.FileCreator.GetDirectories().Any(d => d == "datapacks\\pack\\data\\space\\predicates\\"), "Directory wasn't created");
                Assert.IsTrue(pack.FileCreator.GetWriters().Any(w => w.path == "datapacks\\pack\\data\\space\\predicates\\mypredicate.json"), "File wasn't created");

                space.Predicate("folder/otherpredicate", new Conditions.RandomCondition(0.5), BaseFile.WriteSetting.OnDispose);
                Assert.IsFalse(pack.FileCreator.GetDirectories().Any(d => d == "datapacks\\pack\\data\\space\\predicates\\folder\\"), "Directory wasn't supposed to be created yet since its OnDispose");
                Assert.IsFalse(pack.FileCreator.GetWriters().Any(w => w.path == "datapacks\\pack\\data\\space\\predicates\\folder\\otherpredicate.json"), "File wasn't supposed to be created yet since its OnDispose");

                pack.Dispose();
                Assert.IsTrue(pack.FileCreator.GetDirectories().Any(d => d == "datapacks\\pack\\data\\space\\predicates\\folder\\"), "Directory wasn't created for file with directory in name");
                Assert.IsTrue(pack.FileCreator.GetWriters().Any(w => w.path == "datapacks\\pack\\data\\space\\predicates\\folder\\otherpredicate.json"), "File is supposed to have been created now since Dispose was ran");
            }
        }

        [TestMethod]
        public void TestWrite()
        {
            //setup
            using (Datapack pack = new Datapack("datapacks", "pack", "a pack", 0, new NoneFileCreator()))
            {
                PackNamespace space = pack.Namespace("space");

                //test
                space.Predicate("predicate", new Conditions.RandomCondition(0.5));
                string predicateString = pack.FileCreator.GetWriters().Single(w => w.path == "datapacks\\pack\\data\\space\\predicates\\predicate.json").writer.ToString();
                Assert.AreEqual("{\"chance\":0.5,\"condition\":\"minecraft:random_chance\"}", predicateString, "file wasn't written correctly");
            }
        }
    }
}
