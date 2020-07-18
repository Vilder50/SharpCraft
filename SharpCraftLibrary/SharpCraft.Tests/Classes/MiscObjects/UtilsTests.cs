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
        public void TestToMinecraftBool()
        {
            bool value = true;
            Assert.AreEqual("true", value.ToMinecraftBool());

            bool? nullAble = false;
            Assert.AreEqual("false", nullAble.ToMinecraftBool());
        }

        [TestMethod]
        public void TestValidateName()
        {
            Assert.IsFalse(Validators.ValidateName(null!,true,true, null), "Null is not a valid name");
            Assert.IsTrue(Validators.ValidateName("...__---",false,false, null), "Name should accept / . _ and -");
            Assert.IsTrue(Validators.ValidateName("as0ds9az564xxcy12", false, false, null), "Name should accept letters and numbers");
            Assert.IsFalse(Validators.ValidateName("test:test", false, false, null), "Name should not accept :");
            Assert.IsFalse(Validators.ValidateName("", false, false, null), "Name may not be empty");
            Assert.IsFalse(Validators.ValidateName("ASD", false, false, null), "Name may not contain capitialized letters");
            Assert.IsTrue(Validators.ValidateName("ASD", true, false, null), "Name may contain capitialized letters if specified");
            Assert.IsFalse(Validators.ValidateName("/", false, false, null), "Name may not /");
            Assert.IsTrue(Validators.ValidateName("/", true, true, null), "Name may contain / if specified");
            Assert.IsTrue(Validators.ValidateName("123", true, true, 3), "Name is 3 chars long");
            Assert.IsFalse(Validators.ValidateName("1234", true, true, 3), "Name is 4 chars long and shouldn't be allowed");
        }

        [TestMethod]
        public void TestValidateSingleSelectSelector()
        {
            Validators.ValidateSingleSelectSelector(ID.Selector.s, "a", "b");
            Assert.ThrowsException<ArgumentException>(() => Validators.ValidateSingleSelectSelector(ID.Selector.a, "a", "b"));
            Assert.ThrowsException<ArgumentNullException>(() => Validators.ValidateSingleSelectSelector(null!, "a", "b"));
        }
    }
}
