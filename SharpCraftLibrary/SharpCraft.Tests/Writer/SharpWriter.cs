using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpCraft;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using SharpCraft.Writer;

namespace SharpCraft.Tests.Writer
{
    #region classes and interfaces
    interface IMyWriter : ISharpWriterNormal
    {

    }

    interface IMyOtherWriter : ISharpWriterNormal
    {

    }

    class Writer1 : IMyWriter
    {
        public static string Calls = "";

        public int Index => 1;

        public void Write()
        {
            Calls += "1";
        }
    }

    class Writer2 : IMyWriter, IMyOtherWriter
    {
        public int Index => 0;

        public void Write()
        {
            Writer1.Calls += "2";
        }
    }

    class Writer3 : ISharpWriterNamespace
    {
        public PackNamespace Namespace { get; set; } = null!;

        public string NamespaceName => "namespace";

        public int Index => 0;

        public void Write()
        {
            Writer1.Calls += Namespace.Name;
        }
    }
    #endregion

    [TestClass]
    public class SharpWriterTests
    {
        [TestMethod]
        public void TestNormalWrite()
        {
            Writer1.Calls = "";
            SharpWriter.RunNormalWriters<IMyWriter>();
            Assert.AreEqual("21",Writer1.Calls, "Writers wasn't sorted correctly");
            SharpWriter.RunNormalWriters<IMyOtherWriter>();
            Assert.AreEqual("212", Writer1.Calls, "Writers with multiply interface doesn't get run correctly");
            SharpWriter.RunNormalWriters<ISharpWriterNormal>();
            Assert.AreEqual("212", Writer1.Calls, "Writers shouldn't have been called since the interface is in a different assembly");
            SharpWriter.RunNormalWriters<ISharpWriterNormal>(true);
            Assert.AreEqual("21221", Writer1.Calls, "Writers inheriting from writers aren't run correctly");
        }

        [TestMethod]
        public void TestNamespaceWrite()
        {
            //setup
            Writer1.Calls = "";
            using Datapack pack = new Datapack("datapacks", "pack", "a pack", 0, new NoneFileCreator());

            //test
            SharpWriter.RunNamespaceWriters<ISharpWriterNamespace>(pack);
            Assert.AreEqual("", Writer1.Calls, "Namespace shouldn't have been called since it's in a different assembly");
            SharpWriter.RunNamespaceWriters<ISharpWriterNamespace>(pack, true);
            Assert.AreEqual("namespace", Writer1.Calls, "Writers doesn't get namespaces correctly");
        }
    }
}
