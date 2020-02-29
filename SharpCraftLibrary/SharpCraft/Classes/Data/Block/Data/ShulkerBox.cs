using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for shulker box and barral blocks
        /// </summary>
        public class ShulkerBox : BaseContainer, IBlock.IFacingFull
        {
            private Item?[]? _dItems;

            /// <summary>
            /// Creates a new shulker box
            /// </summary>
            /// <param name="type">The type of block</param>
            public ShulkerBox(BlockType? type) : base(type) { }

            /// <summary>
            /// Tests if the given block type fits this type of block object
            /// </summary>
            /// <param name="block">The block to test</param>
            /// <returns>true if the block fits</returns>
            public new static bool FitsBlock(ID.Block block)
            {
                string blockName = block.ToString();
                return (blockName.Contains("shulker"));
            }

            /// <summary>
            /// The direction the shulker box is facing (the way it opens out into )
            /// </summary>
            [BlockState("facing")]
            public ID.FacingFull? SFacing { get; set; }

            /// <summary>
            /// The item's inside the shulker box.
            /// (0-26)
            /// </summary>
            [Data.DataTag("Items")]
            public override Item?[]? DItems
            {
                get => _dItems;
                set
                {
                    if (DItems != null && DItems.Length > 27)
                    {
                        throw new ArgumentException("Too many slots specified");
                    }
                    _dItems = value;
                }
            }
        }
    }
}
