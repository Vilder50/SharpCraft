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
    public class SimpleCommandsTests
    {
        [TestMethod]
        public void SayMeCommandTest()
        {
            Assert.AreEqual("me hello", new SayMeCommand("hello").GetCommandString());

            Assert.ThrowsException<ArgumentException>(() => new SayMeCommand("  "));
        }

        [TestMethod]
        public void CommentTest()
        {
            Assert.AreEqual("#hello", new Comment("hello").GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new Comment(null));
        }

        [TestMethod]
        public void SayCommandTest()
        {
            Assert.AreEqual("say hello", new SayCommand("hello").GetCommandString());

            Assert.ThrowsException<ArgumentException>(() => new SayCommand("  "));
        }

        [TestMethod]
        public void MsgCommandTest()
        {
            Assert.AreEqual("msg @s hello", new MsgCommand(ID.Selector.s, "hello").GetCommandString());

            Assert.ThrowsException<ArgumentException>(() => new MsgCommand(ID.Selector.s, "  "));
            Assert.ThrowsException<ArgumentNullException>(() => new MsgCommand(null, "hello"));
        }

        [TestMethod]
        public void TeamMsgCommandTest()
        {
            Assert.AreEqual("teammsg hello", new TeamMsgCommand("hello").GetCommandString());

            Assert.ThrowsException<ArgumentException>(() => new TeamMsgCommand("  "));
        }

        [TestMethod]
        public void SeedCommandTest()
        {
            Assert.AreEqual("seed", new SeedCommand().GetCommandString());
        }

        [TestMethod]
        public void LocateCommandTest()
        {
            Assert.AreEqual("locate EndCity", new LocateStructureCommand(ID.Structure.EndCity).GetCommandString());
        }

        [TestMethod]
        public void LocateBiomeCommandTest()
        {
            Assert.AreEqual("locatebiome birch_forest", new LocateBiomeCommand(ID.Biome.birch_forest).GetCommandString());
        }

        [TestMethod]
        public void TriggerCommandTest()
        {
            Assert.AreEqual("trigger test set 10", new TriggerCommand(new Objective("test"), true, 10).GetCommandString());
            Assert.AreEqual("trigger test add -10", new TriggerCommand(new Objective("test"), false, -10).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new TriggerCommand(null, true, 10));
        }

        [TestMethod]
        public void DefaultGamemodeCommandTest()
        {
            Assert.AreEqual("defaultgamemode spectator", new DefaultGamemodeCommand(ID.Gamemode.spectator).GetCommandString());
        }

        [TestMethod]
        public void DifficultyCommandTest()
        {
            Assert.AreEqual("difficulty easy", new DifficultyCommand(ID.Difficulty.easy).GetCommandString());
        }

        [TestMethod]
        public void ReloadCommandTest()
        {
            Assert.AreEqual("reload", new ReloadCommand().GetCommandString());
        }

        [TestMethod]
        public void EnchantCommandTest()
        {
            Assert.AreEqual("enchant @a sharpness 5", new EnchantCommand(ID.Selector.a, 5, ID.Enchant.sharpness).GetCommandString());

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new EnchantCommand(ID.Selector.a, 0, ID.Enchant.aqua_infinity));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new EnchantCommand(ID.Selector.a, 6, ID.Enchant.aqua_infinity));
            Assert.ThrowsException<ArgumentNullException>(() => new EnchantCommand(null, 2, ID.Enchant.bane_of_arthropods));
        }

        [TestMethod]
        public void RunFunctionCommandTest()
        {
            using EmptyDatapack datapack = new EmptyDatapack("pack");
            EmptyFunction function = new EmptyFunction(datapack.Namespace("space"), "function");
            Assert.AreEqual("function space:function", new RunFunctionCommand(function).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new RunFunctionCommand(null));
        }

        [TestMethod]
        public void GamemodeCommandTest()
        {
            Assert.AreEqual("gamemode adventure @a", new GamemodeCommand(ID.Selector.a, ID.Gamemode.adventure).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new GamemodeCommand(null, ID.Gamemode.adventure));
        }

        [TestMethod]
        public void KillCommandTest()
        {
            Assert.AreEqual("kill @e", new KillCommand(ID.Selector.e).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new KillCommand(null));
        }

        [TestMethod]
        public void SetblockCommandTest()
        {
            Assert.AreEqual("setblock ~1 ~2 ~3 minecraft:anvil[facing=east] destroy", new SetblockCommand(new Coords(1, 2, 3), new Block.Anvil(ID.Block.anvil) { SFacing = ID.Facing.east }, ID.BlockAdd.destroy).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new SetblockCommand(null, new Block.Anvil(ID.Block.anvil) { SFacing = ID.Facing.east }, ID.BlockAdd.destroy));
            Assert.ThrowsException<ArgumentNullException>(() => new SetblockCommand(new Vector(1, 2, 3), null, ID.BlockAdd.destroy));
        }

        [TestMethod]
        public void SetWorldSpawnCommandTest()
        {
            Assert.AreEqual("setworldspawn ~1 ~2 ~3", new SetWorldSpawnCommand(new Coords(1,2,3)).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new SetWorldSpawnCommand(null));
        }

        [TestMethod]
        public void SpreadPlayersCommandTest()
        {
            Assert.AreEqual("spreadplayers ~1 ~3 3 10 true @a", new SpreadPlayersCommand(new Coords(1, 2, 3), ID.Selector.a, 3, 10, true).GetCommandString());

            Assert.ThrowsException<ArgumentException>(() => new SpreadPlayersCommand(new Vector(1, 2, 3), ID.Selector.a, 10.1, 10, true));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new SpreadPlayersCommand(new Vector(1, 2, 3), ID.Selector.a, -0.1, 10, true));
            Assert.ThrowsException<ArgumentNullException>(() => new SpreadPlayersCommand(null, ID.Selector.a, 3, 10, true));
            Assert.ThrowsException<ArgumentNullException>(() => new SpreadPlayersCommand(new Vector(1, 2, 3), null, 3, 10, true));
        }

        [TestMethod]
        public void SummonCommandTest()
        {
            Assert.AreEqual("summon armor_stand ~1 ~2 ~3 {Invisible:1b}", new SummonCommand(new Entity.Armorstand() { Invisible = true }, new Coords(1,2,3)).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new SummonCommand(new Entity.Armorstand() { Invisible = true }, null));
            Assert.ThrowsException<ArgumentNullException>(() => new SummonCommand(null, new Vector(1, 2, 3)));
        }

        [TestMethod]
        public void TellrawCommandTest()
        {
            Assert.AreEqual("tellraw @a {\"text\":\"hello\"}", new TellrawCommand(ID.Selector.a, new JsonText.Text("hello")).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new TellrawCommand(null, new JsonText.Text("hello")));
            Assert.ThrowsException<ArgumentNullException>(() => new TellrawCommand(ID.Selector.a, null));
        }

        [TestMethod]
        public void ClearCommandTest()
        {
            Assert.AreEqual("clear @a minecraft:diamond{CustomModelData:1} 64", new ClearCommand(ID.Selector.a, new Item(ID.Item.diamond) { CustomModelData = 1 }, 64).GetCommandString());
            Assert.AreEqual("clear @a minecraft:diamond{CustomModelData:1}", new ClearCommand(ID.Selector.a, new Item(ID.Item.diamond) { CustomModelData = 1 }, null).GetCommandString());
            Assert.AreEqual("clear @a", new ClearCommand(ID.Selector.a, null, null).GetCommandString());

            Assert.ThrowsException<ArgumentException>(() => new ClearCommand(ID.Selector.a, null, 10));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ClearCommand(ID.Selector.a, ID.Item.stone, -1));
            Assert.ThrowsException<ArgumentNullException>(() => new ClearCommand(null, null, null));
        }

        [TestMethod]
        public void GiveCommandTest()
        {
            Assert.AreEqual("give @a minecraft:diamond{CustomModelData:1} 10", new GiveCommand(ID.Selector.a, new Item(ID.Item.diamond) { CustomModelData = 1 }, 10).GetCommandString());
            Assert.AreEqual("give @a minecraft:diamond{CustomModelData:1}", new GiveCommand(ID.Selector.a, new Item(ID.Item.diamond) { CustomModelData = 1 }, 1).GetCommandString());

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new GiveCommand(ID.Selector.a, new Item(ID.Item.diamond) { CustomModelData = 1 }, 0));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new GiveCommand(ID.Selector.a, new Item(ID.Item.diamond) { CustomModelData = 1 }, 65));
            Assert.ThrowsException<ArgumentNullException>(() => new GiveCommand(null, new Item(ID.Item.diamond) { CustomModelData = 1 }, 10));
            Assert.ThrowsException<ArgumentNullException>(() => new GiveCommand(ID.Selector.a, null, 10));
        }

        [TestMethod]
        public void PlaySoundCommandTest()
        {
            Assert.AreEqual("playsound block.bell.use ambient @a ~1 ~2 ~3 10.1 1.1 0.1", new PlaySoundCommand(ID.Sounds.Block.Bell, ID.SoundSource.ambient, ID.Selector.a, new Coords(1, 2, 3), 10.1, 1.1, 0.1).GetCommandString());

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new PlaySoundCommand(ID.Sounds.Block.Bell, ID.SoundSource.ambient, ID.Selector.a, new Vector(1, 2, 3), -0.1, 1.1, 0.1));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new PlaySoundCommand(ID.Sounds.Block.Bell, ID.SoundSource.ambient, ID.Selector.a, new Vector(1, 2, 3), 10.1, -0.1, 0.1));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new PlaySoundCommand(ID.Sounds.Block.Bell, ID.SoundSource.ambient, ID.Selector.a, new Vector(1, 2, 3), 10.1, 2.1, 0.1));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new PlaySoundCommand(ID.Sounds.Block.Bell, ID.SoundSource.ambient, ID.Selector.a, new Vector(1, 2, 3), 10.1, 1, -0.1));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new PlaySoundCommand(ID.Sounds.Block.Bell, ID.SoundSource.ambient, ID.Selector.a, new Vector(1, 2, 3), 10.1, 1, 1.1));
            Assert.ThrowsException<ArgumentException>(() => new PlaySoundCommand(null, ID.SoundSource.ambient, ID.Selector.a, new Vector(1, 2, 3), 10.1, 1.1, 0.1));
            Assert.ThrowsException<ArgumentNullException>(() => new PlaySoundCommand(ID.Sounds.Block.Bell, ID.SoundSource.ambient, null, new Vector(1, 2, 3), 10.1, 1.1, 0.1));
            Assert.ThrowsException<ArgumentNullException>(() => new PlaySoundCommand(ID.Sounds.Block.Bell, ID.SoundSource.ambient, ID.Selector.a, null, 10.1, 1.1, 0.1));
        }

        [TestMethod]
        public void SpawnPointCommandTest()
        {
            Assert.AreEqual("spawnpoint @a ~1 ~2 ~3", new SpawnPointCommand(new Coords(1,2,3), ID.Selector.a).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new SpawnPointCommand(null, ID.Selector.a));
            Assert.ThrowsException<ArgumentNullException>(() => new SpawnPointCommand(new Vector(1, 2, 3), null));
        }

        [TestMethod]
        public void StopSoundCommandTest()
        {
            Assert.AreEqual("stopsound @a master block.bell.use", new StopSoundCommand(ID.Selector.a, ID.Sounds.Block.Bell, ID.SoundSource.master).GetCommandString());
            Assert.AreEqual("stopsound @a * block.bell.use", new StopSoundCommand(ID.Selector.a, ID.Sounds.Block.Bell, null).GetCommandString());
            Assert.AreEqual("stopsound @a master", new StopSoundCommand(ID.Selector.a, null, ID.SoundSource.master).GetCommandString());
            Assert.AreEqual("stopsound @a", new StopSoundCommand(ID.Selector.a, null, null).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new StopSoundCommand(null, null, null));
        }

        [TestMethod]
        public void WeatherCommandTest()
        {
            Assert.AreEqual("weather clear 1000", new WeatherCommand(ID.WeatherType.clear, 1000).GetCommandString());
            Assert.AreEqual("weather clear", new WeatherCommand(ID.WeatherType.clear, null).GetCommandString());
        }
    }
}
