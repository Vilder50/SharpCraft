using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.LootObjects
{
    /// <summary>
    /// Changes the state in the loot item to the state of the broken block
    /// </summary>
    public class CopyStateChange : BaseChange
    {
        private BlockType id;
        private string[] states;

        /// <summary>
        /// Intializes a new <see cref="CopyStateChange"/>
        /// </summary>
        /// <param name="id">The block to copy the state from</param>
        /// <param name="states">The states to copy</param>
        public CopyStateChange(BlockType id, string[] states) : base("copy_state")
        {
            Id = id;
            States = states;
        }

        /// <summary>
        /// The block to copy the state from
        /// </summary>
        [DataTag("block", ForceType = ID.NBTTagType.TagString, JsonTag = true)]
        public BlockType Id { get => id; set => id = value ?? throw new ArgumentNullException(nameof(Id), "Id may not be null"); }

        /// <summary>
        /// The states to copy
        /// </summary>
        [DataTag("properties", JsonTag = true)]
        public string[] States { get => states; set => states = value ?? throw new ArgumentNullException(nameof(States), "States may not be null"); }
    }
}
