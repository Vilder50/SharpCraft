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
    public class DataCommandsTests
    {
        [TestMethod]
        public void DataGetCommandTest()
        {
            Assert.AreEqual("data get block ~0 ~0 ~0 test.pineapple 10.5", new DataGetCommand(new BlockDataLocation(new Coords(), "test.pineapple"), 10.50).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new DataGetCommand(null, 10));
        }

        [TestMethod]
        public void DataMergeBlockCommandTest()
        {
            Assert.AreEqual("data merge block ~1 ~1 ~1 {test:1}", new DataMergeBlockCommand(new Coords(1,1,1), "{test:1}").GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new DataMergeBlockCommand(null, "{test:1}"));
            Assert.ThrowsException<ArgumentNullException>(() => new DataMergeBlockCommand(new Coords(1, 1, 1), null));
        }

        [TestMethod]
        public void DataMergeEntityCommandTest()
        {
            Assert.AreEqual("data merge entity @s {test:1}", new DataMergeEntityCommand(ID.Selector.s, "{test:1}").GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new DataMergeEntityCommand(null, "{test:1}"));
            Assert.ThrowsException<ArgumentNullException>(() => new DataMergeEntityCommand(ID.Selector.s, null));
            Assert.ThrowsException<ArgumentException>(() => new DataMergeEntityCommand(ID.Selector.e, "{test:1}"));
        }

        [TestMethod]
        public void DataModifyWithLocationCommandTest()
        {
            Assert.AreEqual("data modify entity @s test[0].cake merge from block ~0 ~0 ~0 test", new DataModifyWithLocationCommand(new EntityDataLocation(ID.Selector.s, "test[0].cake"), ID.EntityDataModifierType.merge, new BlockDataLocation(new Coords(), "test")).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new DataModifyWithLocationCommand(null, ID.EntityDataModifierType.merge, new BlockDataLocation(new Coords(), "test")));
            Assert.ThrowsException<ArgumentNullException>(() => new DataModifyWithLocationCommand(new BlockDataLocation(new Coords(), "test"), ID.EntityDataModifierType.merge, null));
        }

        [TestMethod]
        public void DataModifyWithDataCommandTest()
        {
            Assert.AreEqual("data modify entity @s test[0].cake merge value {test:1}", new DataModifyWithDataCommand(new EntityDataLocation(ID.Selector.s, "test[0].cake"), ID.EntityDataModifierType.merge, "{test:1}").GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new DataModifyWithDataCommand(null, ID.EntityDataModifierType.merge, "{test:1}"));
            Assert.ThrowsException<ArgumentNullException>(() => new DataModifyWithDataCommand(new EntityDataLocation(ID.Selector.s, "test[0].cake"), ID.EntityDataModifierType.merge , null));
        }

        [TestMethod]
        public void DataModifyInsertLocationCommandTest()
        {
            Assert.AreEqual("data modify entity @s test[0].cake insert 10 from block ~0 ~0 ~0 test", new DataModifyInsertLocationCommand(new EntityDataLocation(ID.Selector.s, "test[0].cake"), 10, new BlockDataLocation(new Coords(), "test")).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new DataModifyInsertLocationCommand(null, 10, new BlockDataLocation(new Coords(), "test")));
            Assert.ThrowsException<ArgumentNullException>(() => new DataModifyInsertLocationCommand(new BlockDataLocation(new Coords(), "test"), 10, null));
        }

        [TestMethod]
        public void DataModifyInsertDataCommandTest()
        {
            Assert.AreEqual("data modify entity @s test[0].cake insert 10 value {test:1}", new DataModifyInsertDataCommand(new EntityDataLocation(ID.Selector.s, "test[0].cake"), 10, "{test:1}").GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new DataModifyInsertDataCommand(null, 10, "{test:1}"));
            Assert.ThrowsException<ArgumentNullException>(() => new DataModifyInsertDataCommand(new EntityDataLocation(ID.Selector.s, "test[0].cake"), 10, null));
        }

        [TestMethod]
        public void DataDeleteCommandTest()
        {
            Assert.AreEqual("data remove entity @s test[0].cake", new DataDeleteCommand(new EntityDataLocation(ID.Selector.s, "test[0].cake")).GetCommandString());

            Assert.ThrowsException<ArgumentNullException>(() => new DataDeleteCommand(null));
        }
    }
}
