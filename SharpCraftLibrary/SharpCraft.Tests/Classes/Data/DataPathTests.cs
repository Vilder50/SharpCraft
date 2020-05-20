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
            Assert.AreEqual("tag.Damage", DataPathCreator.GetPath<Item>(i => i.Damage));
            Assert.AreEqual("tag.EntityTag.id", DataPathCreator.GetPath<Item>(i => i.EntityTag.EntityType));

            Assert.ThrowsException<PathCreatorException>(() => DataPathCreator.GetPath<Item>(i => null));
        }

        [TestMethod]
        public void TestGetCheckedDataPath()
        {
            Assert.AreEqual("{tag:{Damage:1}}.Count", DataPathCreator.GetPath<Item>(i => DataPathCreator.AddCompoundCheck(i, new Item() { Damage = 1 }).Count));
            Assert.AreEqual("tag.EntityTag{id:\"minecraft:armor_stand\"}.CustomName", DataPathCreator.GetPath<Item>(i => DataPathCreator.AddCompoundCheck(i.EntityTag as Entities.Armorstand, new Entities.Armorstand(ID.Entity.armor_stand)).CustomName));

            Assert.ThrowsException<PathCreatorException>(() => DataPathCreator.GetPath<Item>(i => DataPathCreator.AddCompoundCheck(DataPathCreator.AddCompoundCheck(i, new Item() { Count = 3 }), new Item() { Damage = 1 }).Count));
        }

        [TestMethod]
        public void TestGetIndexDataPath()
        {
            Assert.AreEqual("tag.Enchantments", DataPathCreator.GetPath<Item>(i => i.Enchants));
            Assert.AreEqual("tag.Enchantments[10].lvl", DataPathCreator.GetPath<Item>(i => i.Enchants[10].LVL));
            Assert.AreEqual("tag.Enchantments[{id:\"minecraft:aqua_infinity\"}].lvl", DataPathCreator.GetPath<Item>(i => i.Enchants[DataPathCreator.AddArrayFilter(new Item.Enchantment(ID.Enchant.aqua_infinity, null))].LVL));
            Assert.AreEqual("tag.Enchantments[].lvl", DataPathCreator.GetPath<Item>(i => i.Enchants[DataPathCreator.AddArrayFilter(null)].LVL));

            Assert.ThrowsException<PathCreatorException>(() => DataPathCreator.GetPath<Item>(i => DataPathCreator.AddCompoundCheck(i.Enchants[DataPathCreator.AddArrayFilter(null)], new Item.Enchantment(ID.Enchant.aqua_infinity, null)).LVL));
        }

        [TestMethod]
        public void TestIConvertableArrayPath()
        {
            Assert.AreEqual("Pos[0]", DataPathCreator.GetPath<Entities.BasicEntity>(e => e.Coords.PathArray()[0]));
            Assert.AreEqual("Pos[1]", DataPathCreator.GetPath<Entities.BasicEntity>(e => e.Coords.Y));
        }

        [TestMethod]
        public void TestIConvertableCompoundPath()
        {
            Assert.AreEqual("Target.Z", DataPathCreator.GetPath<Entities.ShulkerBullet>(e => e.TargetCoords.Z));
            Assert.AreEqual("TZD", DataPathCreator.GetPath<Entities.ShulkerBullet>(e => e.OffsetTarget.Z));
        }

        [TestMethod]
        public void TestGeneratorPath()
        {
            Assert.AreEqual("BlockState.Properties", DataPathCreator.GetPath<Entities.FallingBlock>(f => f.TheBlock.GetStatePath()));
            Assert.AreEqual("BlockState.Properties.powered", DataPathCreator.GetPath<Entities.FallingBlock>(f => f.TheBlock.GetStatePath<Blocks.Door>(d => d.SPowered)));
        }
    }
}