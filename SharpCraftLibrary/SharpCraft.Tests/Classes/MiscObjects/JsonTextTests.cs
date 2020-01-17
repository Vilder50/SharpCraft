using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpCraft;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace SharpCraft.Tests.MiscObjects
{
    [TestClass]
    public class JsonTextTests
    {
        [TestMethod]
        public void TestJsonText()
        {
            Assert.AreEqual("{\"color\":\"red\",\"text\":\"hello\"}", new JsonText.Text("hello") { Color = ID.MinecraftColor.red }.GetJsonString(), "JsonText color doesn't output correct string");
            Assert.AreEqual("{\"insertion\":\"Ins\\\"erted\",\"text\":\"hello\"}", new JsonText.Text("hello") { ShiftClickInsertion = "Ins\"erted" }.GetJsonString(), "JsonText ShiftClickInsertion doesn't output correct string");
            Assert.AreEqual("{\"obfuscated\":false,\"bold\":true,\"italic\":true,\"strikethrough\":false,\"underlined\":false,\"reset\":false,\"text\":\"hello\"}", new JsonText.Text("hello") { Bold = true, Italic = true, Underline = false, Strikethrough = false, Obfuscated = false, Reset = false }.GetJsonString(), "JsonText formatting doesn't output correct string");
            Assert.AreEqual("{\"clickEvent\":{\"action\":\"open_url\",\"value\":\"www\"},\"hoverEvent\":{\"action\":\"show_text\",\"value\":[{\"text\":\"hovered\"}]},\"text\":\"hello\"}", new JsonText.Text("hello") { ClickEvent = new JsonText.OpenUrlClickEvent("www"), HoverEvent = new JsonText.TextHoverEvent(new JsonText.Text("hovered")) }.GetJsonString(), "Click and hover events doesn't output correct string");
        }

        [TestMethod]
        public void TestClickEvents()
        {
            //open url
            Assert.AreEqual("\"clickEvent\":{\"action\":\"open_url\",\"value\":\"www\"}", new JsonText.OpenUrlClickEvent("www").GetEventString(), "OpenUrlClickEvent doesn't return correct string");
            Assert.ThrowsException<ArgumentException>(() => new JsonText.OpenUrlClickEvent("   "), "OpenUrlClickEvent should throw exception when empty");

            //run command
            Assert.AreEqual("\"clickEvent\":{\"action\":\"run_command\",\"value\":\"clear @s\"}", new JsonText.RunCommandClickEvent(new SharpCraft.Commands.ClearCommand(ID.Selector.s)).GetEventString(), "RunCommandClickEvent doesn't return correct string");
            Assert.ThrowsException<ArgumentNullException>(() => new JsonText.RunCommandClickEvent(null), "RunCommandClickEvent should throw exception if command is null");

            //change page
            Assert.AreEqual("\"clickEvent\":{\"action\":\"change_page\",\"value\":3}", new JsonText.ChangePageClickEvent(3).GetEventString(), "ChangePageClickEvent doesn't return correct string");

            //suggest test
            Assert.AreEqual("\"clickEvent\":{\"action\":\"suggest_command\",\"value\":\"text\"}", new JsonText.SuggestTextClickEvent("text").GetEventString(), "SuggestTextClickEvent doesn't return correct string");
            Assert.ThrowsException<ArgumentNullException>(() => new JsonText.SuggestTextClickEvent(null), "SuggestTextClickEvent should throw exception if text is null");

            //set clipboard
            Assert.AreEqual("\"clickEvent\":{\"action\":\"copy_to_clipboard\",\"value\":\"text\"}", new JsonText.SetClipboardClickEvent("text").GetEventString(), "SetClipboardClickEvent doesn't return correct string");
            Assert.ThrowsException<ArgumentNullException>(() => new JsonText.SetClipboardClickEvent(null), "SetClipboardClickEvent should throw exception if text is null");
        }

        [TestMethod]
        public void TestHoverEvents()
        {
            //text
            Assert.AreEqual("\"hoverEvent\":{\"action\":\"show_text\",\"value\":[{\"text\":\"hove\\\"red\"}]}", new JsonText.TextHoverEvent(new JsonText.Text("hove\"red")).GetEventString(), "TextHoverEvent doesn't return correct string");
            Assert.ThrowsException<ArgumentNullException>(() => new JsonText.TextHoverEvent(null), "TextHoverEvent should throw exception if json text is null");

            //item
            Assert.AreEqual("\"hoverEvent\":{\"action\":\"show_item\",\"value\":\"{id:\\\"minecraft:stone\\\",tag:{CanPlaceOn:[\\\"minecraft:dirt\\\"]}}\"}", new JsonText.ItemHoverEvent(new Item(ID.Item.stone) { CanPlaceOn = new BlockType[] { ID.Block.dirt } }).GetEventString(), "ItemHoverEvent doesn't return correct string");
            Assert.ThrowsException<ArgumentNullException>(() => new JsonText.ItemHoverEvent(null), "ItemHoverEvent should throw exception if item is null");
            Assert.ThrowsException<ArgumentNullException>(() => new JsonText.ItemHoverEvent(new Item()), "ItemHoverEvent should throw exception if item id is null");

            //entity
            Assert.AreEqual("\"hoverEvent\":{\"action\":\"show_entity\",\"value\":\"{type:\\\"minecraft:player\\\",name:[{\\\"text\\\":\\\"vilder\\\\\\\"50\\\"}],id:\\\"f6b1914d-b176-4850-b50380412b85\\\"}\"}", new JsonText.EntityHoverEvent(ID.Entity.player,new JsonText.Text("vilder\"50"),new UUID("f6b1914d-b176-4850-b50380412b85")).GetEventString(), "EntityHoverEvent doesn't return correct string");
            Assert.AreEqual("\"hoverEvent\":{\"action\":\"show_entity\",\"value\":\"{type:\\\"minecraft:pig\\\"}\"}", new JsonText.EntityHoverEvent(ID.Entity.pig).GetEventString(), "EntityHoverEvent doesn't return correct string when only type is specified");
            Assert.ThrowsException<ArgumentNullException>(() => new JsonText.EntityHoverEvent(null), "EntityHoverEvent should throw exception if entity type is null");
        }

        [TestMethod]
        public void TestTexts()
        {
            //text
            Assert.AreEqual("{\"text\":\"hel\\nlo\"}",new JsonText.Text("hel\nlo").GetJsonString(), "Text doesn't return correct string");
            Assert.ThrowsException<ArgumentNullException>(() => new JsonText.Text(null), "Text should throw exception when null");

            //translate
            Assert.AreEqual("{\"translate\":\"hel\\\"lo\",with:[[{\"text\":\"insert1\"}],[{\"text\":\"insert2\"}]]}", new JsonText.Translate("hel\"lo",new JsonText[][] {new JsonText.Text("insert1"), new JsonText.Text("insert2") }).GetJsonString(), "Translate doesn't return correct string");
            Assert.AreEqual("{\"translate\":\"hello\"}", new JsonText.Translate("hello").GetJsonString(), "Translate doesn't return correct string when only translate is specified");
            Assert.ThrowsException<ArgumentNullException>(() => new JsonText.Translate(null), "Translate should throw exception if translate is null");

            //names
            Assert.AreEqual("{\"selector\":\"@a[name=\\\"Name\\\"]\"}", new JsonText.Names(new Selector(ID.Selector.a) { SingleName = "Name" }).GetJsonString(), "Names doesn't return correct string");
            Assert.ThrowsException<ArgumentNullException>(() => new JsonText.Names(null), "Names selector should throw exception if null");

            //keybind
            Assert.AreEqual("{\"keybind\":\"key.inventory\"}", new JsonText.KeyBind(ID.Keys.inventory).GetJsonString(), "Keybind doesn't return correct string");

            //data
            Assert.AreEqual("{\"nbt\":\"test.t{a:\\\"hello\\\"}\",\"interpret\":true,\"entity\":\"@s[name=\\\"Name\\\"]\"}", new JsonText.Data(new EntityDataLocation(new Selector(ID.Selector.s) { SingleName = "Name" }, "test.t{a:\"hello\"}"), true).GetJsonString(), "Data entity doesn't return correct string");
            Assert.AreEqual("{\"nbt\":\"path\",\"interpret\":false,\"block\":\"~1 ~2 ~3\"}", new JsonText.Data(new BlockDataLocation(new Coords(1, 2, 3), "path"), false).GetJsonString(), "Data block doesn't return correct string");
            Assert.AreEqual("{\"nbt\":\"path\",\"interpret\":false,\"storage\":\"space:stor\"}", new JsonText.Data(new StorageDataLocation(new Storage(EmptyNamespace.GetNamespace("space"),"stor"), "path"), false).GetJsonString(), "Data storage doesn't return correct string");
            Assert.ThrowsException<ArgumentNullException>(() => new JsonText.Data(null, true), "Data should throw exception if location is null");

            //score
            Assert.AreEqual("{\"score\":{\"name\":\"@s[name=\\\"Name\\\"]\",\"objective\":\"test\"}}", new JsonText.Score(new Selector(ID.Selector.s) { SingleName = "Name" }, new Objective("test")).GetJsonString(), "Score doesn't return correct string");
            Assert.ThrowsException<ArgumentException>(() => new JsonText.Score(ID.Selector.e, new Objective("something")), "Score should throw exception is selector selects too many things");
            _ = new JsonText.Score(AllSelector.GetSelector(), new Objective("something"));
            Assert.ThrowsException<ArgumentNullException>(() => new JsonText.Score(null, new Objective("something")), "Score should throw exception if selector is null");
            Assert.ThrowsException<ArgumentNullException>(() => new JsonText.Score(ID.Selector.s, null), "Score should throw exception if objective is null");
        }
    }
}
