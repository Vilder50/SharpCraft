using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpCraft;

namespace SharpCraft.Tests.MiscObjects
{
    [TestClass]
    public class UtilsTests
    {
        [TestMethod]
        public void TestEscape()
        {
            Assert.AreEqual("\\\",\\\\,\\\\\\\"", "\",\\,\\\"".Escape());
        }

        [TestMethod]
        public void TestToMinecraftDouble()
        {
            double value = 0.3;
            Assert.AreEqual("0.3", value.ToMinecraftDouble());

            double? nullAble = 0.953;
            Assert.AreEqual("0.953", nullAble.ToMinecraftDouble());
        }

        [TestMethod]
        public void TestToMinecraftFloat()
        {
            float value = 0.3f;
            Assert.AreEqual("0.3", value.ToMinecraftFloat());

            float? nullAble = 0.953f;
            Assert.AreEqual("0.953", nullAble.ToMinecraftFloat());
        }

        [TestMethod]
        public void TestMinecraftValue()
        {
            ID.Item value = ID.Item.String;
            Assert.AreEqual("string", value.MinecraftValue());

            ID.Item? nullAble = ID.Item.structure_void;
            Assert.AreEqual("structure_void", nullAble.MinecraftValue());
        }

        [TestMethod]
        public void TestToMinecraftBool()
        {
            bool value = true;
            Assert.AreEqual("true", value.ToMinecraftBool());

            bool? nullAble = false;
            Assert.AreEqual("false", nullAble.ToMinecraftBool());
        }

        [TestMethod]
        public void TestConvertToBlock()
        {
            //setup
            ID.Item value = ID.Item.dirt;
            ID.Item? nullAble = ID.Item.stone;
            ID.Item? isNull = null;
            ID.Item noneConvertable = ID.Item.stick;

            //test
            Assert.AreEqual(ID.Block.dirt, value.ConvertToBlock(), "Item to block convert didn't work correctly");
            Assert.AreEqual(ID.Block.stone, nullAble.ConvertToBlock(), "Nullable item to block convert didn't work correctly");
            Assert.IsNull(isNull.ConvertToBlock(), "Null wasn't converted");
            Assert.ThrowsException<InvalidCastException>(() => noneConvertable.ConvertToBlock(), "Unconvertable item should throw exception");
        }

        [TestMethod]
        public void TestConvertToItem()
        {
            //setup
            ID.Block value = ID.Block.dirt;
            ID.Block? nullAble = ID.Block.stone;
            ID.Block? isNull = null;
            ID.Block noneConvertable = ID.Block.fire;

            //test
            Assert.AreEqual(ID.Item.dirt, value.ConvertToItem(), "Block to item convert didn't work correctly");
            Assert.AreEqual(ID.Item.stone, nullAble.ConvertToItem(), "Nullable block to item convert didn't work correctly");
            Assert.IsNull(isNull.ConvertToItem(), "Null wasn't converted");
            Assert.ThrowsException<InvalidCastException>(() => noneConvertable.ConvertToItem(), "Unconvertable block should throw exception");
        }

        [TestMethod]
        public void TestValidateName()
        {
            Assert.IsFalse(Utils.ValidateName(null,true,true), "Null is not a valid name");
            Assert.IsTrue(Utils.ValidateName("...__---",false,false), "Name should accept / . _ and -");
            Assert.IsTrue(Utils.ValidateName("as0ds9az564xxcy12", false, false), "Name should accept letters and numbers");
            Assert.IsFalse(Utils.ValidateName("test:test", false, false), "Name should not accept :");
            Assert.IsFalse(Utils.ValidateName("", false, false), "Name may not be empty");
            Assert.IsFalse(Utils.ValidateName("ASD", false, false), "Name may not contain capitialized letters");
            Assert.IsTrue(Utils.ValidateName("ASD", true, false), "Name may contain capitialized letters if specified");
            Assert.IsFalse(Utils.ValidateName("/", false, false), "Name may not /");
            Assert.IsTrue(Utils.ValidateName("/", true, true), "Name may contain / if specified");
        }
    }
}
