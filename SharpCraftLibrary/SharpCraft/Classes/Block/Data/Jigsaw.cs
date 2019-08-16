using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for jigsaw blocks
        /// </summary>
        public class Jigsaw : Block, IBlock.IFacingFull
        {
            /// <summary>
            /// Creates a new jigsaw block
            /// </summary>
            /// <param name="type">The type of block</param>
            public Jigsaw(ID.Block? type = SharpCraft.ID.Block.jigsaw) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public Jigsaw(Group group) : base(group) { }

            /// <summary>
            /// Tests if the given block type fits this type of block object
            /// </summary>
            /// <param name="block">The block to test</param>
            /// <returns>true if the block fits</returns>
            public new static bool FitsBlock(ID.Block block)
            {
                return block == SharpCraft.ID.Block.jigsaw;
            }

            /// <summary>
            /// The way the jigsaw is facing
            /// </summary>
            [BlockData("facing")]
            public ID.FacingFull? SFacing { get; set; }

            /// <summary>
            /// The name of the structure pool to use
            /// </summary>
            [BlockData]
            public string DPool { get; set; }
            /// <summary>
            /// The block the jigsaw should transform into when done
            /// </summary>
            [BlockData]
            public ID.Block? DFinishedBlock { get; set; }
            /// <summary>
            /// The name of the jigsaw block.
            /// (Jigsaws only connects to other jigsaws with the same name)
            /// </summary>
            [BlockData]
            public string DName { get; set; }

            /// <summary>
            /// Gets the raw data for the data the block contains
            /// </summary>
            /// <returns>Raw data used by Minecraft</returns>
            public override string GetDataString()
            {
                base.GetDataString();

                List<string> TempList = new List<string>();

                if (DPool != null) { TempList.Add("target_pool:\"" + DPool.Escape() + "\""); }
                if (DFinishedBlock != null) { TempList.Add("final_state:\"" + DFinishedBlock.ToString().Escape() + "\""); }
                if (DName != null) { TempList.Add("attachment_type:\"" + DName.Escape() + "\""); }

                return string.Join(",", TempList);
            }
        }
    }
}
