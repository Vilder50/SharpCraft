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
    public class TitleCommandsTests
    {
        [TestMethod]
        public void TitleClearCommandTest()
        {
            Assert.AreEqual("title @a clear", new TitleClearCommand(ID.Selector.a).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new TitleClearCommand(null!));
        }

        [TestMethod]
        public void TitleResetCommandTest()
        {
            Assert.AreEqual("title @a reset", new TitleResetCommand(ID.Selector.a).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new TitleResetCommand(null!));
        }

        [TestMethod]
        public void TitleCommandTest()
        {
            Assert.AreEqual("title @a title {\"text\":\"Hello\"}", new TitleCommand(ID.Selector.a, new JsonText.Text("Hello")).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new TitleCommand(null!, new JsonText.Text("hello")));
            Assert.ThrowsException<ArgumentNullException>(() => new TitleCommand(ID.Selector.a, null!));
        }

        [TestMethod]
        public void TitleSubtitleCommandTest()
        {
            Assert.AreEqual("title @a subtitle {\"text\":\"Hello\"}", new TitleSubtitleCommand(ID.Selector.a, new JsonText.Text("Hello")).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new TitleSubtitleCommand(null!, new JsonText.Text("hello")));
            Assert.ThrowsException<ArgumentNullException>(() => new TitleSubtitleCommand(ID.Selector.a, null!));
        }

        [TestMethod]
        public void TitleActionbarCommandTest()
        {
            Assert.AreEqual("title @a actionbar {\"text\":\"Hello\"}", new TitleActionbarCommand(ID.Selector.a, new JsonText.Text("Hello")).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new TitleActionbarCommand(null!, new JsonText.Text("hello")));
            Assert.ThrowsException<ArgumentNullException>(() => new TitleActionbarCommand(ID.Selector.a, null!));
        }

        [TestMethod]
        public void TitleTimesCommandTest()
        {
            Assert.AreEqual("title @a times 20 40 60", new TitleTimesCommand(ID.Selector.a, new NoneNegativeTime<int>(1, ID.TimeType.seconds), new NoneNegativeTime<int>(2, ID.TimeType.seconds), new NoneNegativeTime<int>(3, ID.TimeType.seconds)).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new TitleTimesCommand(null!, new NoneNegativeTime<int>(1, ID.TimeType.seconds), new NoneNegativeTime<int>(2, ID.TimeType.seconds), new NoneNegativeTime<int>(3, ID.TimeType.seconds)));
            Assert.ThrowsException<ArgumentNullException>(() => new TitleTimesCommand(ID.Selector.a, null!, new NoneNegativeTime<int>(2, ID.TimeType.seconds), new NoneNegativeTime<int>(3, ID.TimeType.seconds)));
            Assert.ThrowsException<ArgumentNullException>(() => new TitleTimesCommand(ID.Selector.a, new NoneNegativeTime<int>(1, ID.TimeType.seconds), null!, new NoneNegativeTime<int>(3, ID.TimeType.seconds)));
            Assert.ThrowsException<ArgumentNullException>(() => new TitleTimesCommand(ID.Selector.a, new NoneNegativeTime<int>(1, ID.TimeType.seconds), new NoneNegativeTime<int>(2, ID.TimeType.seconds), null!));
        }
    }
}
