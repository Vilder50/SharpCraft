﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpCraft;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace SharpCraft.Tests.PackItems
{
    [TestClass]
    public class BasePackNamespaceTests
    {
        #region classes for testing
        class DatapackTestClass : BaseDatapack
        {
            public DatapackTestClass(string path, string name) : base(path, name)
            {

            }
        }

        class NamespaceTestClass : BasePackNamespace
        {
            public bool RandomValue;
            public NamespaceTestClass() : base()
            {

            }

            public NamespaceTestClass(BaseDatapack datapack, string name) : base(datapack, name)
            {
                settings.Add(Settings.WriteFunctionCalls());

                #pragma warning disable IDE0067
                new BaseFileTestClass1(this, "file1", BaseFile.WriteSetting.OnDispose);
                new BaseFileTestClass1(this, "file2", BaseFile.WriteSetting.Auto);
                new BaseFileTestClass2(this, "file1", BaseFile.WriteSetting.Auto);
                new BaseFileTestClass2(this, "file2", BaseFile.WriteSetting.Auto);
                new BaseFileTestClass2(this, "file3", BaseFile.WriteSetting.LockedAuto);
                #pragma warning restore IDE0067
            }

            protected override void AfterDispose()
            {
                RandomValue = true;
            }
        }

        class BaseFileTestClass1 : BaseFile
        {
            public BaseFileTestClass1(BasePackNamespace packNamespace, string fileName, WriteSetting setting) : base(packNamespace, fileName, setting)
            {

            }

            protected override void WriteFile(TextWriter stream)
            {
                stream.Write("Hello world");
            }

            protected override TextWriter GetStream()
            {
                return new StringWriter();
            }
        }

        class BaseFileTestClass2 : BaseFile
        {
            public BaseFileTestClass2(BasePackNamespace packNamespace, string fileName, WriteSetting setting) : base(packNamespace, fileName, setting)
            {

            }

            protected override void WriteFile(TextWriter stream)
            {
                stream.Write("Hello world");
            }

            protected override TextWriter GetStream()
            {
                return new StringWriter();
            }
        }
        #endregion

        [TestMethod]
        public void TestBasePackNamespace()
        {
            //setup
            BaseDatapack datapack = new DatapackTestClass("a folder path", "pack");
            using (BasePackNamespace space = new NamespaceTestClass(datapack, "namespace"))
            {
                //test
                Assert.IsTrue(space.IsSetup);
                Assert.AreEqual(datapack, space.Datapack, "datapack is not getting set by the constructor");
                Assert.AreEqual("namespace", space.Name, "name is not getting set by the constructor");
                new BaseFileTestClass1(space, "file3", BaseFile.WriteSetting.Auto).Dispose();
            }

            //test none setup pack
            using (BasePackNamespace space = new NamespaceTestClass())
            {
                //test
                Assert.IsFalse(space.IsSetup, "Empty constructor was called so it shouldn't have been setup already");
                Assert.ThrowsException<InvalidOperationException>(() => _ = space.Name, "Name didn't throw exception even though it isn't setup");
                Assert.ThrowsException<InvalidOperationException>(() => _ = space.Datapack, "Datapack didn't throw exception even though it isn't setup");

                space.Setup(datapack, "namespace2");
                Assert.IsTrue(space.IsSetup, "Setup has been called so it should have been setup");
                Assert.AreEqual(datapack, space.Datapack, "datapack is not getting set by bysetup");
                Assert.AreEqual("namespace2", space.Name, "name is not getting set by setup");
            }
        }

        [TestMethod]
        public void TestGetFile()
        {
            //setup
            using (BasePackNamespace pack = new NamespaceTestClass(new DatapackTestClass("a folder path", "pack"), "namespace"))
            {
                //test
                Assert.AreEqual("file1", pack.GetFile<BaseFileTestClass1>("file1").FileName, "GetFile failed to get the file with the correct name");
                Assert.AreEqual(BaseFile.WriteSetting.OnDispose, pack.GetFile<BaseFileTestClass1>("file1").Setting, "GetFile failed to get the file of the correct type");
                Assert.AreEqual("file2", pack.GetFile<BaseFileTestClass1>("file2").FileName, "GetFile failed to get the other file with the other name");
                Assert.AreEqual(BaseFile.WriteSetting.Auto, pack.GetFile<BaseFileTestClass2>("file1").Setting, "GetFile failed to get the file of the other type");

                //test exception on extra file with same name and same type
                Assert.ThrowsException<ArgumentException>(() => new BaseFileTestClass1(pack, "file1", BaseFile.WriteSetting.Auto), "Adding 2 files with the same name and same type should cast an exception");
                Assert.ThrowsException<InvalidOperationException>(() => pack.GetFile<BaseFileTestClass2>("file3"), "should not be able to get locked file");
            }
        }

        [TestMethod]
        public void TestGetPath()
        {
            //setup
            using (BasePackNamespace pack = new NamespaceTestClass(new DatapackTestClass("a folder path", "pack"), "namespace"))
            {
                //test
                Assert.AreEqual("a folder path\\pack\\data\\namespace\\", pack.GetPath());
            }
        }

        [TestMethod]
        public void TestIsSettingSet()
        {
            //setup
            using (BasePackNamespace pack = new NamespaceTestClass(new DatapackTestClass("a folder path", "pack"), "namespace"))
            {
                //test
                Assert.IsTrue(pack.IsSettingSet(BasePackNamespace.Settings.WriteFunctionCalls()), "Failed to detect that the setting is set");
                Assert.IsFalse(pack.IsSettingSet(BasePackNamespace.Settings.ShortNames()), "Failed to detect that the setting isn't set");
            }
        }

        [TestMethod]
        public void TestName()
        {
            new NamespaceTestClass(new DatapackTestClass("a folder path", "pack"), "namespace").Dispose();
            new NamespaceTestClass(new DatapackTestClass("a folder path", "pack"), "name_space").Dispose();
            new NamespaceTestClass(new DatapackTestClass("a folder path", "pack"), "namesp4c3").Dispose();

            Assert.ThrowsException<ArgumentException>(() => new NamespaceTestClass(new DatapackTestClass("a folder path", "pack"), "name space").Dispose(), "name with space shouldn't be allowed");
            Assert.ThrowsException<ArgumentException>(() => new NamespaceTestClass(new DatapackTestClass("a folder path", "pack"), "name/space").Dispose(), "name with symbol shouldn't be allowed");
            Assert.ThrowsException<ArgumentException>(() => new NamespaceTestClass(new DatapackTestClass("a folder path", "pack"), "    ").Dispose(), "Empty name shouldn't be allowed");
        }

        [TestMethod]
        public void TestDispose()
        {
            BasePackNamespace space = new NamespaceTestClass(new DatapackTestClass("a folder path", "pack"), "namespace");

            Assert.IsFalse(space.Disposed, "namespace shouldn't have been disposed yet");
            Assert.IsFalse(space.GetFile<BaseFileTestClass2>("file1").Disposed, "namespace isn't disposed yet and the file shouldn't be disposed yet");
            space.Dispose();
            Assert.IsTrue(((NamespaceTestClass)space).RandomValue, "AfterDispose didn't run");
            Assert.IsTrue(space.Disposed, "namespace should have been disposed");
            Assert.IsTrue(space.GetFile<BaseFileTestClass2>("file1").Disposed, "namespace is disposed and the file should be disposed");

            Assert.ThrowsException<InvalidOperationException>(() => new BaseFileTestClass1(space, "afile", BaseFile.WriteSetting.Auto), "Shouldn't be able to add more files since namespace is disposed");
        }
    }
}
