using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.LootObjects
{
    /// <summary>
    /// Changes the loot nbt to nbt from another place
    /// </summary>
    public class CopyNBTChange : BaseChange
    {
        private CopyOperation[] operations;

        /// <summary>
        /// Intializes a new <see cref="CopyNBTChange"/>
        /// </summary>
        /// <param name="operations">The copy operations to run</param>
        /// <param name="target">The target to copy from</param>
        public CopyNBTChange(ID.LootTarget target, CopyOperation[] operations) : base("copy_nbt")
        {
            Target = target;
            Operations = operations;
        }

        /// <summary>
        /// The target to copy from
        /// </summary>
        [DataTag("source", ForceType = ID.NBTTagType.TagString, JsonTag = true)]
        public ID.LootTarget Target { get; set; }

        /// <summary>
        /// The copy operations to run
        /// </summary>
        [DataTag("ops", JsonTag = true)]
        public CopyOperation[] Operations { get => operations; set => operations = value ?? throw new ArgumentNullException(nameof(Operations), "Operations may not be null"); }

        /// <summary>
        /// Class for making nbt copy operations
        /// </summary>
        public class CopyOperation : DataHolderBase
        {
            private string toDataPath;
            private string fromDataPath;

            /// <summary>
            /// Intializes a new <see cref="CopyOperation"/>
            /// </summary>
            /// <param name="fromDataPath">The datapath to copy from</param>
            /// <param name="toDataPath">The datapath on the item to copy to. Starting from the Item's "Tag" tag</param>
            /// <param name="copyType">The way to copy the data</param>
            public CopyOperation(string fromDataPath, string toDataPath, ID.EntityDataModifierType copyType)
            {
                FromDataPath = fromDataPath;
                ToDataPath = toDataPath;
                CopyType = copyType;
            }

            /// <summary>
            /// The datapath to copy from
            /// </summary>
            [DataTag("source", JsonTag = true)]
            public string FromDataPath { get => fromDataPath; set => fromDataPath = value ?? throw new ArgumentNullException(nameof(FromDataPath), "FromDataPath may not be null"); }

            /// <summary>
            /// The datapath on the item to copy to. Starting from the Item's "Tag" tag
            /// </summary>
            [DataTag("target", JsonTag = true)]
            public string ToDataPath { get => toDataPath; set => toDataPath = value ?? throw new ArgumentNullException(nameof(ToDataPath), "ToDataPath may not be null"); }

            /// <summary>
            /// The way to copy the data
            /// </summary>
            [DataTag("source", ForceType = ID.NBTTagType.TagString, JsonTag = true)]
            public ID.EntityDataModifierType CopyType { get; set; }
        }
    }
}
