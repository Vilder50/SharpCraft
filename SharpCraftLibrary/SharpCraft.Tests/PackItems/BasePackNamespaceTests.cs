using System;
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
                settings.Add(NamespaceSettings.GetSettings().FunctionGroupedCommands());

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

            public override string GetID(object getIdFor)
            {
                return "A-name";
            }
        }

        class BaseFileTestClass1 : BaseFile<TextWriter>
        {
            public BaseFileTestClass1(BasePackNamespace packNamespace, string fileName, WriteSetting setting) : base(packNamespace, fileName, setting, "test1")
            {
                FinishedConstructing();
            }

            protected override void FinishedConstructing()
            {
                PackNamespace.AddFile(this);
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

        class BaseFileTestClass2 : BaseFile<TextWriter>
        {
            public BaseFileTestClass2(BasePackNamespace packNamespace, string fileName, WriteSetting setting) : base(packNamespace, fileName, setting, "test2")
            {
                FinishedConstructing();
            }

            protected override void FinishedConstructing()
            {
                PackNamespace.AddFile(this);
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
            BasePackNamespace space = new NamespaceTestClass(datapack, "namespace");
            
            //test
            Assert.IsTrue(space.IsSetup);
            Assert.AreEqual(datapack, space.Datapack, "datapack is not getting set by the constructor");
            Assert.AreEqual("namespace", space.Name, "name is not getting set by the constructor");
            new BaseFileTestClass1(space, "file3", BaseFile.WriteSetting.Auto).Dispose();
            space.Dispose();

            //test none setup pack
            space = new NamespaceTestClass();
            //test
            Assert.IsFalse(space.IsSetup, "Empty constructor was called so it shouldn't have been setup already");
            Assert.ThrowsException<InvalidOperationException>(() => _ = space.Name, "Name didn't throw exception even though it isn't setup");
            Assert.ThrowsException<InvalidOperationException>(() => _ = space.Datapack, "Datapack didn't throw exception even though it isn't setup");

            space.Setup(datapack, "namespace2");
            Assert.IsTrue(space.IsSetup, "Setup has been called so it should have been setup");
            Assert.AreEqual(datapack, space.Datapack, "datapack is not getting set by bysetup");
            Assert.AreEqual("namespace2", space.Name, "name is not getting set by setup");
            space.Dispose();
        }

        [TestMethod]
        public void TestGetFile()
        {
            //setup
            BasePackNamespace pack = new NamespaceTestClass(new DatapackTestClass("a folder path", "pack"), "namespace");
            //test
            Assert.AreEqual("file1", pack.GetFile("test1", "file1")!.FileId, "GetFile failed to get the file with the correct name");
            Assert.AreEqual(BaseFile.WriteSetting.OnDispose, pack.GetFile("test1", "file1")!.Setting, "GetFile failed to get the file of the correct type");
            Assert.AreEqual("file2", pack.GetFile("test1", "file2")!.FileId, "GetFile failed to get the other file with the other name");
            Assert.AreEqual(BaseFile.WriteSetting.Auto, pack.GetFile("test2", "file1")!.Setting, "GetFile failed to get the file of the other type");

            //test exception on extra file with same name and same type
            Assert.ThrowsException<ArgumentException>(() => new BaseFileTestClass1(pack, "file1", BaseFile.WriteSetting.Auto), "Adding 2 files with the same name and same type should cast an exception");
            Assert.ThrowsException<InvalidOperationException>(() => pack.GetFile("test2", "file3"), "should not be able to get locked file");
            pack.Dispose();
        }

        [TestMethod]
        public void TestGetPath()
        {
            //setup
            BasePackNamespace pack = new NamespaceTestClass(new DatapackTestClass("a folder path", "pack"), "namespace");
            //test
            Assert.AreEqual("a folder path/pack/data/namespace/", pack.GetPath());
            pack.Dispose();
        }

        [TestMethod]
        public void TestIsSettingSet()
        {
            //setup
            BasePackNamespace pack = new NamespaceTestClass(new DatapackTestClass("a folder path", "pack"), "namespace");
            //test
            Assert.IsTrue(pack.IsSettingSet(NamespaceSettings.GetSettings().FunctionGroupedCommands()), "Failed to detect that the setting is set");
            Assert.IsFalse(pack.IsSettingSet(NamespaceSettings.GetSettings().GenerateNames()), "Failed to detect that the setting isn't set");
            pack.Dispose();
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
            Assert.IsFalse(space.GetFile("test2", "file1")!.Disposed, "namespace isn't disposed yet and the file shouldn't be disposed yet");
            space.Dispose();
            Assert.IsTrue(((NamespaceTestClass)space).RandomValue, "AfterDispose didn't run");
            Assert.IsTrue(space.Disposed, "namespace should have been disposed");
            Assert.IsTrue(space.GetFile("test2", "file1")!.Disposed, "namespace is disposed and the file should be disposed");

            Assert.ThrowsException<InvalidOperationException>(() => new BaseFileTestClass1(space, "afile", BaseFile.WriteSetting.Auto), "Shouldn't be able to add more files since namespace is disposed");
        }

        [TestMethod]
        public void TestEmptyNamespace()
        {
            Assert.AreEqual(MockNamespace.GetMinecraftNamespace(), MockNamespace.GetMinecraftNamespace(), "Getting minecraft namespace doesn't return the same object every time");
            Assert.AreEqual("minecraft", MockNamespace.GetMinecraftNamespace().Name, "Getting minecraft namespace returns wrong namespace");
            Assert.AreEqual(MockNamespace.GetNamespace("space"), MockNamespace.GetNamespace("space"), "Getting defined namespace doesn't return the same object every time");
            Assert.AreEqual("space", MockNamespace.GetNamespace("space").Name, "Getting defined namespace returns wrong namespace");
        }

        [TestMethod]
        public void TestFileAddListener()
        {
            using Datapack pack = new Datapack("a path", "name", ".", 4, new NoneFileCreator());
            bool fileAdded = false;
            PackNamespace space = pack.Namespace("space");
            space.Function("test1");
            space.AddNewFileListener((file) =>
            {
                fileAdded = true;
            });
            Assert.IsFalse(fileAdded, "file listener shouldn't have been called yet.");
            space.Function("test2");
            Assert.IsTrue(fileAdded, "file listener should have been called after file was added.");
        }

        class TestSetting : INamespaceSetting
        {
            public int ANumber;

            public TestSetting(int aNumber)
            {
                ANumber = aNumber;
            }
        }

        class OtherSetting : TestSetting
        {
            public OtherSetting(int aNumber) : base(aNumber)
            {
                
            }
        }

        [TestMethod] 
        public void TestGetSetting()
        {
            using Datapack pack = new Datapack("a path", "name", ".", 4, new NoneFileCreator());
            PackNamespace space = pack.Namespace("space");
            Assert.IsNull(space.GetSetting<TestSetting>(), "Setting isn't added yet and should return null");

            space.AddSetting(new OtherSetting(15));
            space.AddSetting(new TestSetting(10));
            TestSetting setting = (space.GetSetting<TestSetting>() as TestSetting)!;
            Assert.IsNotNull(space.GetSetting<TestSetting>(), "A setting should have been returned since there is one");
            Assert.AreEqual(10, setting.ANumber, "The wrong setting returned");
        }

        [TestMethod]
        public void TestDIsposeFileSetting()
        {
            using Datapack pack = new Datapack("a path", "name", ".", 4, new NoneFileCreator());
            PackNamespace space = pack.Namespace("space");
            space.AddSetting(NamespaceSettings.GetSettings().ForceDisposeWriteFiles());

            BaseFile file1 = space.Function("file1", BaseFile.WriteSetting.Auto);
            BaseFile file2 = space.Function("file2", BaseFile.WriteSetting.LockedAuto);
            BaseFile file3 = space.Function("file3", BaseFile.WriteSetting.LockedOnDispose);
            BaseFile file4 = space.Function("file4", BaseFile.WriteSetting.OnDispose);

            Assert.AreEqual(BaseFile.WriteSetting.OnDispose, file1.Setting, "Auto should have changed to OnDispose");
            Assert.AreEqual(BaseFile.WriteSetting.LockedOnDispose, file2.Setting, "LockedAuto should have changed to LockedOnDispose");
            Assert.AreEqual(BaseFile.WriteSetting.LockedOnDispose, file3.Setting, "LockedOnDispose shouldn't change");
            Assert.AreEqual(BaseFile.WriteSetting.OnDispose, file4.Setting, "OnDispose shouldn't change");
        }
    }
}

