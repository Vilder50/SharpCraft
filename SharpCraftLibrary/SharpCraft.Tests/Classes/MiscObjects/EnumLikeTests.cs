using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpCraft;

namespace SharpCraft.Tests.MiscObjects
{
    [TestClass]
    public class EnumLikeTests
    {
        class NormalTestEnum : EnumLike<string>
        {
            public NormalTestEnum(string value) : base(value)
            {
            }

            public static readonly NormalTestEnum Value1 = new NormalTestEnum("v1");
            public static readonly NormalTestEnum Value2 = new NormalTestEnum("v2");
            public static readonly NormalTestEnum Value3 = new NormalTestEnum("v3");
        }

        class NamespacedTestEnum : NamespacedEnumLike<string>
        {
            public NamespacedTestEnum(string value, BasePackNamespace? @namespace = null) : base(value, @namespace)
            {
            }

            public static readonly NamespacedTestEnum Value1 = new NamespacedTestEnum("v1");
            public static readonly NamespacedTestEnum Value2 = new NamespacedTestEnum("v2", MockNamespace.GetNamespace("test"));
            public static readonly NamespacedTestEnum Value3 = new NamespacedTestEnum("v3", MockNamespace.GetNamespace("other"));
        }

        [TestMethod]
        public void ToStringTest()
        {
            Assert.AreEqual("v1", NormalTestEnum.Value1.ToString());
            Assert.AreEqual("v2", NormalTestEnum.Value2.ToString());
            Assert.AreEqual("v3", NormalTestEnum.Value3.ToString());

            Assert.AreEqual("minecraft:v1", NamespacedTestEnum.Value1.ToString());
            Assert.AreEqual("test:v2", NamespacedTestEnum.Value2.ToString());
            Assert.AreEqual("other:v3", NamespacedTestEnum.Value3.ToString());

            Assert.AreEqual(NormalTestEnum.Value1.ToString(), NormalTestEnum.Value1.Value);
            Assert.AreEqual(NormalTestEnum.Value2.ToString(), NormalTestEnum.Value2.Value);
            Assert.AreEqual(NormalTestEnum.Value3.ToString(), NormalTestEnum.Value3.Value);
        }

        [TestMethod]
        public void EqualTest()
        {
            Assert.AreEqual(NormalTestEnum.Value1, NormalTestEnum.Value1);
            Assert.AreEqual(NormalTestEnum.Value2, NormalTestEnum.Value2);
            Assert.AreEqual(NormalTestEnum.Value3, NormalTestEnum.Value3);

            Assert.AreNotEqual(NormalTestEnum.Value1, NormalTestEnum.Value2);
            Assert.AreNotEqual(NormalTestEnum.Value2, NormalTestEnum.Value3);
            Assert.AreNotEqual(NormalTestEnum.Value1, NormalTestEnum.Value3);

            Assert.AreNotEqual(NormalTestEnum.Value1, NamespacedTestEnum.Value1);
        }

        [TestMethod]
        public void ToDataTag()
        {
            Assert.AreEqual(NormalTestEnum.Value1.Value, NormalTestEnum.Value1.GetAsTag(ID.NBTTagType.TagString, new object[] { }).Value);
            Assert.AreEqual(NormalTestEnum.Value2.Value, NormalTestEnum.Value2.GetAsTag(null, new object[] { }).Value);
            Assert.AreEqual(NormalTestEnum.Value3.Value, NormalTestEnum.Value3.GetAsTag(ID.NBTTagType.TagString, new object[] { }).Value);

            Assert.AreEqual(NamespacedTestEnum.Value1.ToString(), NamespacedTestEnum.Value1.GetAsTag(ID.NBTTagType.TagString, new object[] { }).Value);

            Assert.ThrowsException<ArgumentException>(() => NormalTestEnum.Value1.GetAsTag(ID.NBTTagType.TagInt, new object[] { }), "Shouldnt be able to convert to other than string");
            Assert.ThrowsException<ArgumentException>(() => NormalTestEnum.Value1.GetAsTag(ID.NBTTagType.TagStringArray, new object[] { }), "Shouldnt be able to convert to other than string");
            Assert.ThrowsException<ArgumentException>(() => NormalTestEnum.Value1.GetAsTag(ID.NBTTagType.TagCompound, new object[] { }), "Shouldnt be able to convert to other than string");
        }

        [TestMethod]
        public void GetValuesFromEnumHolder()
        {
            NormalTestEnum[] items = NormalTestEnum.GetValuesFromEnumHolder<NormalTestEnum>().ToArray();

            Assert.AreEqual(3, items.Length);
            Assert.IsFalse(items.SingleOrDefault(i => i == NormalTestEnum.Value1) is null);
            Assert.IsFalse(items.SingleOrDefault(i => i == NormalTestEnum.Value2) is null);
            Assert.IsFalse(items.SingleOrDefault(i => i == NormalTestEnum.Value3) is null);
        }
    }
}
