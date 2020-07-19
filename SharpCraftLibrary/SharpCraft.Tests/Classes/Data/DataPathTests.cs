using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpCraft;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using SharpCraft.Data;

namespace SharpCraft.Tests.Data
{
    [TestClass]
    public class DataPathTests
    {
        [TestMethod]
        public void TestGetSimpleDataPath()
        {
            Assert.AreEqual("tag.Damage", Item.PathCreator.Make(i => i.Damage));
            Assert.AreEqual("tag.EntityTag.id", Item.PathCreator.Make(i => i.EntityTag!.EntityType));

            Assert.ThrowsException<PathCreatorException>(() => Item.PathCreator.Make(i => null));
        }

        [TestMethod]
        public void TestGetCheckedDataPath()
        {
            Assert.AreEqual("{tag:{Damage:1}}.Count", Item.PathCreator.Make((i,t) => t.CompoundCheck(i, new Item() { Damage = 1 }).Count));
            Assert.AreEqual("tag.EntityTag{id:\"minecraft:armor_stand\"}.CustomName", Item.PathCreator.Make((i, t) => t.CompoundCheck(i.EntityTag as Entities.Armorstand, new Entities.Armorstand(ID.Entity.armor_stand))!.CustomName));

            Assert.ThrowsException<PathCreatorException>(() => Item.PathCreator.Make((i, t) => t.CompoundCheck(t.CompoundCheck(i, new Item() { Count = 3 }), new Item() { Damage = 1 }).Count));
        }

        [TestMethod]
        public void TestGetIndexDataPath()
        {
            Assert.AreEqual("tag.Enchantments", Item.PathCreator.Make(i => i.Enchants));
            Assert.AreEqual("tag.Enchantments[10].lvl", Item.PathCreator.Make(i => i.Enchants![10]!.LVL));
            Assert.AreEqual("tag.Enchantments[{id:\"minecraft:aqua_infinity\"}].lvl", Item.PathCreator.Make((i, t) => i.Enchants![t.ArrayFilter(new Item.Enchantment(ID.Enchant.aqua_infinity, null))]!.LVL));
            Assert.AreEqual("tag.Enchantments[].lvl", Item.PathCreator.Make((i, t) => i.Enchants![t.ArrayFilter(null)]!.LVL));

            Assert.ThrowsException<PathCreatorException>(() => Item.PathCreator.Make((i, t) => t.CompoundCheck(i.Enchants![t.ArrayFilter(null)], new Item.Enchantment(ID.Enchant.aqua_infinity, null))!.LVL));
        }

        [TestMethod]
        public void TestIConvertableArrayPath()
        {
            Assert.AreEqual("Pos[0]", Entities.BasicEntity.PathCreator.Make(e => e.Coords!.PathArray()[0]));
            Assert.AreEqual("Pos[1]", Entities.BasicEntity.PathCreator.Make(e => e.Coords!.Y));
        }

        [TestMethod]
        public void TestIConvertableCompoundPath()
        {
            Assert.AreEqual("Target.Z", Entities.ShulkerBullet.PathCreator.Make(e => e.TargetCoords!.Z));
            Assert.AreEqual("TZD", Entities.ShulkerBullet.PathCreator.Make(e => e.OffsetTarget!.Z));
        }

        [TestMethod]
        public void TestGeneratorPath()
        {
            Assert.AreEqual("BlockState.Properties", Entities.FallingBlock.PathCreator.Make(f => f.TheBlock!.GetStatePath()));
            Assert.AreEqual("BlockState.Properties.powered", Entities.FallingBlock.PathCreator.Make(f => f.TheBlock!.GetStatePath<Blocks.Door>(d => d.SPowered)));
        }
    }
}