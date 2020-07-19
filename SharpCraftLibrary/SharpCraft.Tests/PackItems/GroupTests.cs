using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpCraft;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using SharpCraft.Data;

namespace SharpCraft.Tests.PackItems
{
    #region Test classes
    class DatapackTestClass : BaseDatapack
    {
        public DatapackTestClass(string path, string name) : base(path, name)
        {

        }
    }

    class NamespaceTestClass : BasePackNamespace
    {
        public NamespaceTestClass(BaseDatapack datapack, string name) : base(datapack, name)
        {

        }

        public override string GetID(object getIdFor)
        {
            return "A name";
        }
    }

    class GroupItemClass : IGroupable
    {
        public GroupItemClass(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

        public bool IsAGroup => true;

        public DataPartObject GetAsDataObject(object?[] conversionData)
        {
            throw new NotImplementedException();
        }
    }

    class TestGroupClass : BaseGroup<GroupItemClass>
    {
        public static TextWriter WriterToUse = null!;
        public TestGroupClass(BasePackNamespace packNamespace, string fileName, List<GroupItemClass> items, bool appendGroup, WriteSetting writeSetting) : base(packNamespace, fileName, items, appendGroup, writeSetting, "test")
        {
            FinishedConstructing();
        }

        protected override TextWriter GetStream()
        {
            return WriterToUse;
        }
    }
    #endregion

    [TestClass]
    public class GroupTests
    {
        [TestMethod]
        public void TestGroup()
        {
            //setup
            TestGroupClass.WriterToUse = new StringWriter();
            using DatapackTestClass pack = new DatapackTestClass("path", "pack");
            NamespaceTestClass packNamespace = new NamespaceTestClass(pack, "namespace");
            List<GroupItemClass> items = new List<GroupItemClass>
                    {
                        new GroupItemClass("test"),
                        new GroupItemClass("test"),
                        new GroupItemClass("test2")
                    };
            bool append = true;

            //test
            TestGroupClass group = new TestGroupClass(packNamespace, "name", items, append, BaseFile.WriteSetting.LockedOnDispose);
            Assert.AreEqual(items, group.Items, "Items were not set correctly by the constructor");
            Assert.AreEqual(append, group.AppendGroup, "AppendGroup was not set correctly by the constructor");
        }

        [TestMethod]
        public void TestItems()
        {
            //setup
            using DatapackTestClass pack = new DatapackTestClass("path", "pack");
            NamespaceTestClass packNamespace = new NamespaceTestClass(pack, "namespace");
            //test
            TestGroupClass.WriterToUse = new StringWriter();
            TestGroupClass group = new TestGroupClass(packNamespace, "name1", new List<GroupItemClass> { new GroupItemClass("test") }, false, BaseFile.WriteSetting.LockedOnDispose);
            Assert.ThrowsException<ArgumentNullException>(() => group.Items = null!, "Items should not be able to be null");
            group.Items = new List<GroupItemClass>() { };
            group.Dispose();

            TestGroupClass.WriterToUse = new StringWriter();
            group = new TestGroupClass(packNamespace, "name2", new List<GroupItemClass> { new GroupItemClass("test") }, false, BaseFile.WriteSetting.LockedAuto);
            Assert.ThrowsException<InvalidOperationException>(() => group.Items = new List<GroupItemClass>() { }, "Items should not be changeable because the file is auto");
        }

        [TestMethod]
        public void TestAppendGroup()
        {
            //setup
            using DatapackTestClass pack = new DatapackTestClass("path", "pack");
            NamespaceTestClass packNamespace = new NamespaceTestClass(pack, "namespace");
            //test
            TestGroupClass.WriterToUse = new StringWriter();
            TestGroupClass group = new TestGroupClass(packNamespace, "name1", new List<GroupItemClass> { new GroupItemClass("test") }, false, BaseFile.WriteSetting.LockedOnDispose)
            {
                AppendGroup = true
            };
            group.Dispose();


            TestGroupClass.WriterToUse = new StringWriter();
            group = new TestGroupClass(packNamespace, "name2", new List<GroupItemClass> { new GroupItemClass("test") }, false, BaseFile.WriteSetting.LockedAuto);
            Assert.ThrowsException<InvalidOperationException>(() => group.AppendGroup = true, "AppendGroup should not be changeable because the file is auto");
            
        }

        [TestMethod]
        public void TestEmptyGroup()
        {
            Assert.AreEqual("#space:file", new FileMocks.MockGroup<IBlockType>(MockDatapack.GetPack().Namespace("space"), "file").GetNamespacedName(), "EmptyGroup doesn't return correct string");
        }

        [TestMethod]
        public void TestWriting()
        {
            //setup
            using DatapackTestClass pack = new DatapackTestClass("path", "pack");
            NamespaceTestClass packNamespace = new NamespaceTestClass(pack, "namespace");
            //test
            TestGroupClass.WriterToUse = new StringWriter();
            TestGroupClass group = new TestGroupClass(packNamespace, "name1", new List<GroupItemClass> { new GroupItemClass("test") }, true, BaseFile.WriteSetting.LockedOnDispose);
            group.Items.Add(new GroupItemClass("test2"));
            group.Items.Add(new GroupItemClass("test3"));
            Assert.AreEqual("", ((StringWriter)TestGroupClass.WriterToUse).GetStringBuilder().ToString(), "Group shouldn't have been written yet");
            group.Dispose();
            Assert.AreEqual("{\"values\":[\"test\",\"test2\",\"test3\"]}", ((StringWriter)TestGroupClass.WriterToUse).GetStringBuilder().ToString(), "Group wasn't written correctly");

            TestGroupClass.WriterToUse = new StringWriter();
            group = new TestGroupClass(packNamespace, "name2", new List<GroupItemClass> { new GroupItemClass("test") }, false, BaseFile.WriteSetting.LockedAuto);
            group.Dispose();
            Assert.IsTrue(group.Disposed);
            Assert.AreEqual("{\"replace\":true,\"values\":[\"test\"]}", ((StringWriter)TestGroupClass.WriterToUse).GetStringBuilder().ToString(), "Group didn't write AppendFile correctly");
            Assert.IsNull(group.Items, "Items wasn't cleared");
        }
    }
}
