using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.Blocks
{
    /// <summary>
    /// An object for blocks being moved by a piston
    /// </summary>
    public class MovedByPiston : BaseBlockEntity, Interfaces.IFacingFull
    {
        /// <summary>
        /// Creates a new block being moved by a piston
        /// </summary>
        /// <param name="type">The type of block</param>
        public MovedByPiston(BlockType? type) : base(type) { }

        /// <summary>
        /// Tests if the given block type fits this type of block object
        /// </summary>
        /// <param name="block">The block to test</param>
        /// <returns>true if the block fits</returns>
        public new static bool FitsBlock(ID.Block block)
        {
            return block == SharpCraft.ID.Block.moving_piston || block == SharpCraft.ID.Block.piston_head;
        }

        /// <summary>
        /// The way the block is being pushed
        /// </summary>
        [BlockState("facing")]
        public ID.FacingFull? SFacing { get; set; }

        /// <summary>
        /// The type of piston base used
        /// </summary>
        [BlockState("type")]
        public ID.PistonType? SPistonBase { get; set; }

        /// <summary>
        /// If the piston arm is short
        /// </summary>
        [BlockState("short")]
        public bool? SShort { get; set; }

        /// <summary>
        /// The block being moved
        /// Note: block data is not supported
        /// </summary>
        [Data.DataTag("blockState", "Name", "Properties")]
        public Block? DMovingBlock { get; set; }

        /// <summary>
        /// The way the block is being pushed / pulled from
        /// </summary>
        [Data.DataTag("facing", ForceType = SharpCraft.ID.NBTTagType.TagInt)]
        public ID.FacingFull? DDirection { get; set; }

        /// <summary>
        /// How far the black has been moved
        /// </summary>
        [Data.DataTag("progress")]
        public float? DProgress { get; set; }

        /// <summary>
        /// If the block is pushed or pulled
        /// </summary>
        [Data.DataTag("extending")]
        public bool? Pushed { get; set; }

        /// <summary>
        /// If the block actually is the piston's piston head
        /// </summary>
        [Data.DataTag("source")]
        public bool? PistonHead { get; set; }
    }
}
