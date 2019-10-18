﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpCraft;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace SharpCraft.Tests.PackItems
{
    [TestClass]
    public class BaseDatapackTests
    {
        #region classes for testing
        class DatapackTestClass : BaseDatapack
        {
            public bool RandomValue;

            public DatapackTestClass(string path, string name) : base(path, name)
            {

            }

            protected override void AfterDispose()
            {
                RandomValue = true;
            }
        }

        class NamespaceTestClass : BasePackNamespace
        {
            public NamespaceTestClass() : base()
            {

            }

            public NamespaceTestClass(BaseDatapack datapack, string name) : base(datapack, name)
            {
                
            }
        }
        #endregion

        [TestMethod]
        public void TestBaseDatapack()
        {
            //setup
            using (BaseDatapack pack = new DatapackTestClass("a path", "nAme"))
            {
                //test
                Assert.AreEqual("a path", pack.Path, "Path is not getting set by the constructor");
                Assert.AreEqual("name", pack.Name, "Name is not getting set by the constructor");
            }
        }

        [TestMethod]
        public void TestNameValidation()
        {
            Assert.IsTrue(BaseDatapack.ValidateName("anAme"), "name with only letters should be valid");
            Assert.IsTrue(BaseDatapack.ValidateName("an4m3"), "name with numbers should be valid");
            Assert.IsTrue(BaseDatapack.ValidateName("an_4m_3"), "name with _ should be valid");

            Assert.IsFalse(BaseDatapack.ValidateName("a name"), "name with space should not be valid");
            Assert.IsFalse(BaseDatapack.ValidateName("a!name"), "name with symbol should not be valid");
            Assert.IsFalse(BaseDatapack.ValidateName("    "), "empty name should not be valid");
        }

        [TestMethod]
        public void TestPathValidation()
        {
            Assert.IsTrue(BaseDatapack.ValidatePath("anAhjfdkyg784 9y3uifdsme"), "path should be valid");
            Assert.IsFalse(BaseDatapack.ValidatePath("anAhjfdkyg784 9y3uifdsme/"), "path ends with / and should not be valid");
        }

        [TestMethod]
        public void TestNamespace()
        {
            //setup
            using (BaseDatapack pack = new DatapackTestClass("a path", "name"))
            {
                //test
                BasePackNamespace space1 = pack.Namespace<NamespaceTestClass>("namespace");
                BasePackNamespace space2 = pack.Namespace<NamespaceTestClass>("namespace");
                BasePackNamespace space3 = pack.Namespace<NamespaceTestClass>("potato");

                Assert.IsTrue(space1.IsSetup, "namespace was not setup");
                Assert.AreEqual("namespace", space1.Name, "Namespace did not get correct name");
                Assert.AreEqual(space1, space2, "namespace failed to find and return existing namespace");
                Assert.AreEqual("potato", space3.Name, "Namespace failed to add extra namespace");
                Assert.AreNotEqual(space1, space3, "Namespace failed to output correct namespace");

                Assert.ThrowsException<ArgumentException>(() => new NamespaceTestClass(pack, "namespace"), "Cannot have 2 namespaces with the same name");
            }
        }

        [TestMethod]
        public void TestDispose()
        {
            BaseDatapack pack = new DatapackTestClass("a path", "name");
            BasePackNamespace space = pack.Namespace<NamespaceTestClass>("namespace");

            Assert.IsFalse(pack.Disposed, "Pack wasn't disposed and shouldn't be disposed");
            Assert.IsFalse(space.Disposed, "namespace in pack wasn't disposed and shouldn't be disposed");

            pack.Dispose();
            Assert.IsTrue(((DatapackTestClass)pack).RandomValue, "AfterDispose didn't run");
            Assert.IsTrue(pack.Disposed, "Pack was disposed and should be disposed");
            Assert.IsTrue(space.Disposed, "namespace in pack was disposed and should be disposed since the pack is disposed");
            Assert.ThrowsException<InvalidOperationException>(() => pack.Namespace<NamespaceTestClass>("namespace"), "Shouldn't be able to get/create namespaces after pack has been disposed");
        }
    }
}