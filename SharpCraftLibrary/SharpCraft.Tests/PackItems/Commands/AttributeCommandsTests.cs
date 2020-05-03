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
    public class AttributeCommandsTests
    {
        [TestMethod]
        public void AttributeGet()
        {
            Assert.AreEqual("attribute @s generic.armor_toughness get 2.3", new AttributeGetCommand(ID.Selector.s, ID.AttributeType.generic_armor_toughness, 2.3).GetCommandString());
            Assert.AreEqual("attribute @s generic.armor_toughness get", new AttributeGetCommand(ID.Selector.s, ID.AttributeType.generic_armor_toughness, 1).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new AttributeGetCommand(null, ID.AttributeType.generic_armor_toughness, 1).GetCommandString());
            Assert.ThrowsException<ArgumentException>(() => new AttributeGetCommand(ID.Selector.a, ID.AttributeType.generic_armor_toughness, 1).GetCommandString());
        }

        [TestMethod]
        public void AttributeGetBase()
        {
            Assert.AreEqual("attribute @s generic.armor_toughness base get 2.3", new AttributeGetBaseCommand(ID.Selector.s, ID.AttributeType.generic_armor_toughness, 2.3).GetCommandString());
            Assert.AreEqual("attribute @s generic.armor_toughness base get", new AttributeGetBaseCommand(ID.Selector.s, ID.AttributeType.generic_armor_toughness, 1).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new AttributeGetBaseCommand(null, ID.AttributeType.generic_armor_toughness, 1).GetCommandString());
            Assert.ThrowsException<ArgumentException>(() => new AttributeGetBaseCommand(ID.Selector.a, ID.AttributeType.generic_armor_toughness, 1).GetCommandString());
        }

        [TestMethod]
        public void AttributeSetBase()
        {
            Assert.AreEqual("attribute @s generic.armor_toughness base set 1", new AttributeSetBaseCommand(ID.Selector.s, ID.AttributeType.generic_armor_toughness, 1).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new AttributeSetBaseCommand(null, ID.AttributeType.generic_armor_toughness, 1).GetCommandString());
            Assert.ThrowsException<ArgumentException>(() => new AttributeSetBaseCommand(ID.Selector.a, ID.AttributeType.generic_armor_toughness, 1).GetCommandString());
        }

        [TestMethod]
        public void AttributeAddModifier()
        {
            Assert.AreEqual("attribute @s generic.armor_toughness modifier add 00000000-0000-0000-0000-000000000000 test 1.1 multiply", new AttributeAddModifierCommand(ID.Selector.s, ID.AttributeType.generic_armor_toughness, new UUID(0, 0), "test", 1.1, ID.AttributeOperation.multiply_total).GetCommandString());
            Assert.AreEqual("attribute @s generic.armor_toughness modifier add 00000000-0000-0001-0000-000000000001 test 1 addition", new AttributeAddModifierCommand(ID.Selector.s, ID.AttributeType.generic_armor_toughness, new UUID(1, 1), "test", 1, ID.AttributeOperation.addition).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new AttributeAddModifierCommand(null, ID.AttributeType.generic_armor_toughness, new UUID(0, 0), "test", 1.1, ID.AttributeOperation.multiply_total).GetCommandString());
            Assert.ThrowsException<ArgumentException>(() => new AttributeAddModifierCommand(ID.Selector.a, ID.AttributeType.generic_armor_toughness, new UUID(0, 0), "test", 1.1, ID.AttributeOperation.multiply_total).GetCommandString());
            Assert.ThrowsException<ArgumentNullException>(() => new AttributeAddModifierCommand(ID.Selector.s, ID.AttributeType.generic_armor_toughness, null, "test", 1.1, ID.AttributeOperation.multiply_total).GetCommandString());
        }

        [TestMethod]
        public void AttributeRemoveModifier()
        {
            Assert.AreEqual("attribute @s generic.armor_toughness modifier remove 00000000-0000-0001-0000-000000000001", new AttributeRemoveModifierCommand(ID.Selector.s, ID.AttributeType.generic_armor_toughness, new UUID(1, 1)).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new AttributeRemoveModifierCommand(null, ID.AttributeType.generic_armor_toughness, new UUID(0, 0)).GetCommandString());
            Assert.ThrowsException<ArgumentException>(() => new AttributeRemoveModifierCommand(ID.Selector.a, ID.AttributeType.generic_armor_toughness, new UUID(0, 0)).GetCommandString());
            Assert.ThrowsException<ArgumentNullException>(() => new AttributeRemoveModifierCommand(ID.Selector.s, ID.AttributeType.generic_armor_toughness, null).GetCommandString());
        }

        [TestMethod]
        public void AttributeGetModifier()
        {
            Assert.AreEqual("attribute @s generic.armor_toughness modifier value get 00000000-0000-0001-0000-000000000001 1.1", new AttributeGetModifierCommand(ID.Selector.s, ID.AttributeType.generic_armor_toughness, new UUID(1, 1), 1.1).GetCommandString());
            Assert.AreEqual("attribute @s generic.armor_toughness modifier value get 00000000-0000-0001-0000-000000000001", new AttributeGetModifierCommand(ID.Selector.s, ID.AttributeType.generic_armor_toughness, new UUID(1, 1), 1).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new AttributeGetModifierCommand(null, ID.AttributeType.generic_armor_toughness, new UUID(0, 0), 1).GetCommandString());
            Assert.ThrowsException<ArgumentException>(() => new AttributeGetModifierCommand(ID.Selector.a, ID.AttributeType.generic_armor_toughness, new UUID(0, 0), 1).GetCommandString());
            Assert.ThrowsException<ArgumentNullException>(() => new AttributeGetModifierCommand(ID.Selector.s, ID.AttributeType.generic_armor_toughness, null, 1).GetCommandString());
        }
    }
}
