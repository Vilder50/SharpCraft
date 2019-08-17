using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for coral plants (not blocks)
        /// </summary>
        public class Coral : Block, IBlock.IWaterLogged
        {
            /// <summary>
            /// Intilizes a new block object
            /// </summary>
            public Coral()
            {
                ID = null;
            }

            /// <summary>
            /// Creates a new coral plant (not block)
            /// </summary>
            /// <param name="type">The type of plant</param>
            public Coral(ID.Block? type) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public Coral(Group group) : base(group) { }

            /// <summary>
            /// Tests if the given block type fits this type of block object
            /// </summary>
            /// <param name="block">The block to test</param>
            /// <returns>true if the block fits</returns>
            public new static bool FitsBlock(ID.Block block)
            {
                string blockName = block.ToString();
                return (blockName.Contains("coral") && !blockName.Contains("wall") && !blockName.Contains("block"));
            }

            /// <summary>
            /// If the coral is water logged
            /// </summary>
            [BlockState("waterlogged")]
            public bool? SWaterLogged { get; set; }
        }

        /// <summary>
        /// An object for coral plants wall fans
        /// </summary>
        public class CoralFan : Coral, IBlock.IFacing
        {
            /// <summary>
            /// Intilizes a new block object
            /// </summary>
            public CoralFan()
            {
                ID = null;
            }

            /// <summary>
            /// Creates a new coral wall plant
            /// </summary>
            /// <param name="type">The type of plant</param>
            public CoralFan(ID.Block? type) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public CoralFan(Group group) : base(group) { }

            /// <summary>
            /// Tests if the given block type fits this type of block object
            /// </summary>
            /// <param name="block">The block to test</param>
            /// <returns>true if the block fits</returns>
            public new static bool FitsBlock(ID.Block block)
            {
                string blockName = block.ToString();
                return (blockName.Contains("coral") && blockName.Contains("wall"));
            }

            /// <summary>
            /// The way the coral fan is facing
            /// Note: this is only for coral wall fans
            /// </summary>
            [BlockState("facing")]
            public ID.Facing? SFacing { get; set; }
        }
    }
}
