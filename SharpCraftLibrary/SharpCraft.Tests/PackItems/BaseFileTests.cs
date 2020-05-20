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
    public class BaseFileTests
    {
        #region classes for testing
        class DatapackTestClass : BaseDatapack
        {
            public DatapackTestClass(string path, string name) : base(path, name)
            {
                FinishedConstructing();
            }
        }

        class NamespaceTestClass : BasePackNamespace
        {
            private int nextId = 0;

            public NamespaceTestClass(BaseDatapack datapack, string name) : base(datapack, name)
            {

            }

            public override string GetID(object getIdFor)
            {
                nextId++;
                return nextId.ToString();
            }

            public void AddSetting(INamespaceSetting setting)
            {
                settings.Add(setting);
            }
        }

        class BaseFileTestClass : BaseFile
        {
            public static TextWriter WriterToUse;
            public bool RandomValue = false;

            public BaseFileTestClass(BasePackNamespace packNamespace, string fileName, WriteSetting setting) : base(packNamespace, fileName, setting, "filetest")
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
                return WriterToUse;
            }

            protected override void AfterDispose()
            {
                RandomValue = true;
            }
        }
        #endregion

        [TestMethod]
        public void TestBaseFile()
        {
            //setup
            BaseFileTestClass.WriterToUse = new StringWriter();
            NamespaceTestClass packNamespace = new NamespaceTestClass(new DatapackTestClass("pack", "path"), "namespace");
            BaseFile file = new BaseFileTestClass(packNamespace, "My/File", BaseFile.WriteSetting.Auto);
            //test
            Assert.AreEqual("my/file", file.FileId, "file name is not getting set by constructor");
            Assert.AreEqual(packNamespace, file.PackNamespace, "Packnamespace is not getting set by the constructor");
            Assert.AreEqual(file, packNamespace.GetFile("filetest", file.FileId), "Constructor is not adding the file to the namespace");
            file.Dispose();

            file = new BaseFileTestClass(packNamespace, null, BaseFile.WriteSetting.Auto);
            Assert.AreEqual("1", file.FileId, "file name wasn't generated correctly");
            file.Dispose();

            packNamespace.AddSetting(NamespaceSettings.GetSettings().GenerateNames());
            file = new BaseFileTestClass(packNamespace, "folder\\ignored-name", BaseFile.WriteSetting.Auto);
            Assert.AreEqual("2", file.WritePath, "writepath wasn't forced to be generated");
            Assert.AreEqual("folder/ignored-name", file.FileId, "filename wasn't kept after forced path generation");
            file.Dispose();
        }

        [TestMethod]
        public void TestValidateFileName()
        {
            Assert.IsTrue(BaseFile.ValidateFileName("MyFile"), "name only contains letters and should be valid");
            Assert.IsTrue(BaseFile.ValidateFileName("My1_2File/123"), "name only contains letters, numbers and / and \\ and should be valid");
            Assert.IsTrue(BaseFile.ValidateFileName("My1_2File/1\\fshjkfs"), "name has 2 pairs of / or \\ and should be valid");

            Assert.IsFalse(BaseFile.ValidateFileName("!nvalidname"), "name contains invalid symbol and shouldn't be valid");
            Assert.IsFalse(BaseFile.ValidateFileName("invalid name"), "name contains space and shouldn't be valid");
            Assert.IsFalse(BaseFile.ValidateFileName("invalidname/"), "name ends in a / and shouldn't be valid");
            Assert.IsFalse(BaseFile.ValidateFileName("invalid//name"), "name contains 2 / after each other and shouldn't be valid");
        }

        [TestMethod]
        public void TestDispose()
        {
            //setup
            BaseFileTestClass.WriterToUse = new StringWriter();
            BaseFile onDispose = new BaseFileTestClass(new NamespaceTestClass(new DatapackTestClass("pack", "path"), "namespace"), "MyFile", BaseFile.WriteSetting.OnDispose);
            BaseFile auto = new BaseFileTestClass(new NamespaceTestClass(new DatapackTestClass("pack", "path"), "namespace"), "MyFile", BaseFile.WriteSetting.Auto);

            //test (on dispose)
            Assert.AreEqual("", ((StringWriter)BaseFileTestClass.WriterToUse).GetStringBuilder().ToString(), "file isn't diposed yet and shouldn't have ran it's WriteFile method");
            Assert.IsFalse(onDispose.Disposed, "file isn't disposed and it should be false");
            onDispose.Dispose();
            Assert.IsTrue(((BaseFileTestClass)onDispose).RandomValue, "AfterDispose didn't run");
            Assert.IsTrue(onDispose.Disposed, "file is disposed and it should be true");
            Assert.AreEqual("Hello world", ((StringWriter)BaseFileTestClass.WriterToUse).GetStringBuilder().ToString(), "File is disposed but failed to write correctly");
            onDispose.Dispose();
            Assert.AreEqual("Hello world", ((StringWriter)BaseFileTestClass.WriterToUse).GetStringBuilder().ToString(), "File was already disposed and shouldn't change the thing it's writing");

            //(auto)
            BaseFileTestClass.WriterToUse = new StringWriter();
            auto.Dispose();
            Assert.AreEqual("", ((StringWriter)BaseFileTestClass.WriterToUse).GetStringBuilder().ToString(), "File is an Auto file and shouldn't write after being disposed");
        }

        [TestMethod]
        public void TestDisposeListener()
        {
            //test
            bool disposed = false;
            BaseFile onDispose = new BaseFileTestClass(new NamespaceTestClass(new DatapackTestClass("pack", "path"), "namespace"), "MyFile", BaseFile.WriteSetting.OnDispose);
            onDispose.AddDisposeListener(f => 
            {
                disposed = true;
            });
            Assert.IsFalse(disposed, "Dispose listener was called too early");
            onDispose.Dispose();
            Assert.IsTrue(disposed, "Dispose listener wasn't called when the file got disposed");

            //test exception
            Assert.ThrowsException<ArgumentNullException>(() => onDispose.AddDisposeListener(null));
        }
    }
}
