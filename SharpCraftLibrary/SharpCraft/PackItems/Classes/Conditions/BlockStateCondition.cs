using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.Conditions
{
    /// <summary>
    /// Condition which returns true if the block is of the given type and has the properties
    /// </summary>
    public class BlockStateCondition : BaseCondition
    {
        private Block block= null!;

        /// <summary>
        /// Intializes a new <see cref="BlockStateCondition"/>
        /// </summary>
        /// <param name="block">The block to search for</param>
        public BlockStateCondition(Block block) : base("minecraft:block_state_property")
        {
            Block = block;
        }

        /// <summary>
        /// The block to search for.
        /// Note: block data is not supported.
        /// </summary>
        [DataTag("block", "block", "properties", true, JsonTag = true, Merge = true)]
        public Block Block { get => block; set => block = value ?? throw new ArgumentNullException(nameof(Block), "Block may not be null"); }
    }
}
