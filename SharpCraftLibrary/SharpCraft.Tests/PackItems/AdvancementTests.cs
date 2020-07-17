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
    public class AdvancementTests
    {
        [TestMethod]
        public void TestAdvancement()
        {
            //setup
            using Datapack pack = new Datapack("datapacks", "pack", "a pack", 0, new NoneFileCreator());
            PackNamespace space = pack.Namespace("space");

            //test
            space.Advancement("myadvancement");
            Assert.IsTrue(pack.FileCreator.GetDirectories().Any(d => d == "datapacks/pack/data/space/advancements/"), "Directory wasn't created");
            Assert.IsTrue(pack.FileCreator.GetWriters().Any(w => w.path == "datapacks/pack/data/space/advancements/myadvancement.json"), "File wasn't created");

            space.Advancement("folder/otherAdvancement", new BredAnimalsTrigger(), null, BaseFile.WriteSetting.OnDispose);
            Assert.IsFalse(pack.FileCreator.GetDirectories().Any(d => d == "datapacks/pack/data/space/advancements/folder/"), "Directory wasn't supposed to be created yet since its OnDispose");
            Assert.IsFalse(pack.FileCreator.GetWriters().Any(w => w.path == "datapacks/pack/data/space/advancements/folder/otheradvancement.json"), "File wasn't supposed to be created yet since its OnDispose");

            pack.Dispose();
            Assert.IsTrue(pack.FileCreator.GetDirectories().Any(d => d == "datapacks/pack/data/space/advancements/folder/"), "Directory wasn't created for file with directory in name");
            Assert.IsTrue(pack.FileCreator.GetWriters().Any(w => w.path == "datapacks/pack/data/space/advancements/folder/otheradvancement.json"), "File is supposed to have been created now since Dispose was ran");
        }

        private ChildAdvancement GetChildAdvancement(PackNamespace space)
        {
            return space.Advancement("child", GetParentAdvancement(space), new EnchantedItemTrigger() { Levels = 5, Item = ID.Item.wooden_sword }, null, new JsonText.Text("Name"), new JsonText.Text("Description"), ID.Item.stone, ID.AdvancementFrame.goal, true, false, true);
        }

        [TestMethod]
        public void TestWriteChild()
        {
            //setup
            using Datapack pack = new Datapack("datapacks", "pack", "a pack", 0, new NoneFileCreator());
            PackNamespace space = pack.Namespace("space");

            //test
            ChildAdvancement childAdvancement = GetChildAdvancement(space);
            string advancementString = pack.FileCreator.GetWriters().Single(w => w.path == "datapacks/pack/data/space/advancements/child.json").writer.ToString();
            Assert.AreEqual("{\"requirements\":[[\"trigger_0\"]],\"criteria\":" +
                                "{\"trigger_0\":{\"conditions\":{\"item\":{\"item\":\"minecraft:wooden_sword\"},\"levels\":{\"max\":5,\"min\":5}},\"trigger\":\"minecraft:enchanted_item\"}}" +
                            ",\"display\":{\"icon\":{\"item\":\"minecraft:stone\"},\"title\":{\"text\":\"Name\"},\"description\":{\"text\":\"Description\"},\"frame\":\"goal\",\"show_toast\":false,\"announce_to_chat\":true,\"hidden\":true},\"parent\":\"space:parent\"}", advancementString, "Child file wasn't written correctly");
            Assert.IsNull(childAdvancement.Requirements, "requirements weren't cleared");
            Assert.IsNull(childAdvancement.Reward, "reward wasn't cleared");
            Assert.IsNull(childAdvancement.Description, "description wasn't cleared");
            Assert.IsNull(childAdvancement.Name, "name wasn't cleared");
        }

        private ParentAdvancement GetParentAdvancement(PackNamespace space)
        {
            return space.Advancement("parent", new EnchantedItemTrigger() { Levels = 5 }, new Reward() {Experience = 5 }, new JsonText.Text("Name"), new JsonText.Text("Description"), ID.Item.String, "background");
        }

        [TestMethod]
        public void TestWriteParent()
        {
            //setup
            using Datapack pack = new Datapack("datapacks", "pack", "a pack", 0, new NoneFileCreator());
            PackNamespace space = pack.Namespace("space");

            //test
            ParentAdvancement parentAdvancement = GetParentAdvancement(space);
            string advancementString = pack.FileCreator.GetWriters().Single(w => w.path == "datapacks/pack/data/space/advancements/parent.json").writer.ToString();
            Assert.AreEqual("{\"requirements\":[[\"trigger_0\"]],\"criteria\":" +
                                "{\"trigger_0\":{\"conditions\":{\"levels\":{\"max\":5,\"min\":5}},\"trigger\":\"minecraft:enchanted_item\"}}" +
                            ",\"rewards\":{\"experience\":5},\"display\":{\"icon\":{\"item\":\"minecraft:string\"},\"title\":{\"text\":\"Name\"},\"description\":{\"text\":\"Description\"},\"frame\":\"task\",\"show_toast\":true,\"announce_to_chat\":false,\"hidden\":false,\"background\":\"background\"}}", advancementString, "parent file wasn't written correctly");
            Assert.IsNull(parentAdvancement.Requirements, "requirements weren't cleared");
            Assert.IsNull(parentAdvancement.Reward, "reward wasn't cleared");
            Assert.IsNull(parentAdvancement.Description, "description wasn't cleared");
            Assert.IsNull(parentAdvancement.Name, "name wasn't cleared");
            Assert.IsNull(parentAdvancement.Background, "background wasn't cleared");
        }

        [TestMethod]
        public void TestWriteHidden()
        {
            //setup
            using Datapack pack = new Datapack("datapacks", "pack", "a pack", 0, new NoneFileCreator());
            PackNamespace space = pack.Namespace("space");

            //test
            _ = space.Advancement("hidden", new Requirement(new IRequirementItem[] { new EnchantedItemTrigger() { Levels = 5 }, new BredAnimalsTrigger() }), null);
            string advancementString = pack.FileCreator.GetWriters().Single(w => w.path == "datapacks/pack/data/space/advancements/hidden.json").writer.ToString();
            Assert.AreEqual("{\"requirements\":[[\"trigger_0\",\"trigger_1\"]],\"criteria\":" +
                                "{\"trigger_0\":{\"conditions\":{\"levels\":{\"max\":5,\"min\":5}},\"trigger\":\"minecraft:enchanted_item\"},\"trigger_1\":{\"trigger\":\"minecraft:bred_animals\"}}" +
                            "}", advancementString, "hidden file wasn't written correctly");
        }

        [TestMethod]
        public void TestWriteInvalid()
        {
            //setup
            using Datapack pack = new Datapack("datapacks", "pack", "a pack", 0, new NoneFileCreator());
            PackNamespace space = pack.Namespace("space");

            //test
            space.Advancement("invalid");
            string advancementString = pack.FileCreator.GetWriters().Single(w => w.path == "datapacks/pack/data/space/advancements/invalid.json").writer.ToString();
            Assert.AreEqual("{\"invalid\":true}", advancementString);
        }

        [TestMethod]
        public void TestRequirement()
        {
            //setup
            BaseTrigger trigger1 = new BredAnimalsTrigger();
            BaseTrigger trigger2 = new BredAnimalsTrigger() { Name = "mytrigger" };
            BaseTrigger trigger3 = new BredAnimalsTrigger();

            //test
            Requirement requirement1 = new Requirement((BaseTrigger[])trigger1);
            Requirement requirement2 = new Requirement(new IRequirementItem[] { requirement1, trigger2, trigger3 });

            Assert.AreEqual("[[\"trigger_0\"],\"mytrigger\",\"trigger_1\"]", requirement2.GetRequirementString(null), "GetRequirementString didn't return correct value");
            Assert.AreEqual("trigger_0", trigger1.Name, "trigger1 wasn't given the correct name");
            Assert.AreEqual("trigger_1", trigger3.Name, "trigger3 wasn't given the correct name");

            List<BaseTrigger> triggers = requirement2.GetChildTriggers().ToList();
            Assert.IsTrue(triggers.Contains(trigger1), "GetChildTriggers didn't return trigger1");
            Assert.IsTrue(triggers.Contains(trigger2), "GetChildTriggers didn't return trigger2");
            Assert.IsTrue(triggers.Contains(trigger3), "GetChildTriggers didn't return trigger3");
        }

        [TestMethod]
        public void TestNewSibling()
        {
            //setup
            using Datapack pack = new Datapack("datapacks", "pack", "a pack", 0, new NoneFileCreator());
            PackNamespace space = pack.Namespace("space");

            //test
            ChildAdvancement advancement = GetChildAdvancement(space);
            advancement.NewSibling("sibling", new EnchantedItemTrigger() { Levels = 5 }, null, new JsonText.Text("Name"), new JsonText.Text("Description"), ID.Item.stone);
            Assert.IsTrue(pack.FileCreator.GetDirectories().Any(d => d == "datapacks/pack/data/space/advancements/"), "Directory wasn't created");
            Assert.IsTrue(pack.FileCreator.GetWriters().Any(w => w.path == "datapacks/pack/data/space/advancements/sibling.json"), "File wasn't created");
        }

        [TestMethod]
        public void TestNewChild()
        {
            //setup
            using Datapack pack = new Datapack("datapacks", "pack", "a pack", 0, new NoneFileCreator());
            PackNamespace space = pack.Namespace("space");

            //test
            ChildAdvancement advancement = GetChildAdvancement(space);
            advancement.NewChild("childchild", new EnchantedItemTrigger() { Levels = 5 }, null, new JsonText.Text("Name"), new JsonText.Text("Description"), ID.Item.stone);
            Assert.IsTrue(pack.FileCreator.GetDirectories().Any(d => d == "datapacks/pack/data/space/advancements/"), "Directory wasn't created");
            Assert.IsTrue(pack.FileCreator.GetWriters().Any(w => w.path == "datapacks/pack/data/space/advancements/childchild.json"), "File wasn't created");
        }

        [TestMethod]
        public void TestEmptyAdvancement()
        {
            Assert.AreEqual("name:adv",new FileMocks.MockAdvancement(EmptyDatapack.GetPack().Namespace("name"),"adv").GetNamespacedName(), "EmptyAdvancement doesn't reutrn correct string");
            Assert.AreEqual("space:name", ((FileMocks.MockAdvancement)"space:name").GetNamespacedName(), "Implicit string to advancement conversion converts incorrectly");
        }
    }
}
