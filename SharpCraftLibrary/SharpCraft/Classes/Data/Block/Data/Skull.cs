using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.Blocks
{
    /// <summary>
    /// An object for skull / head blocks
    /// </summary>
    public abstract class BaseSkull : Block
    {
        /// <summary>
        /// Creates a new skull / head block
        /// </summary>
        /// <param name="type">The type of block</param>
        public BaseSkull(BlockType? type) : base(type) { }

        /// <summary>
        /// The name of the player whose skin to display
        /// </summary>
        [Data.DataTag("SkullOwner")]
        public string? DPlayerSkin { get; set; }

        /// <summary>
        /// The raw data for a skin.
        /// (Starting from the Owner tag... so Owner:[value] (remember the {}))
        /// </summary>
        [Data.DataTag("Owner", ForceType = SharpCraft.ID.NBTTagType.TagCompound)]
        public string? DDataSkin { get; set; }
    }

    /// <summary>
    /// An object for skull / head blocks
    /// </summary>
    public class GroundSkull : BaseSkull, Interfaces.IRotation
    {
        private int? _sRotation;

        /// <summary>
        /// Creates a new skull / head block
        /// </summary>
        /// <param name="type">The type of block</param>
        public GroundSkull(BlockType? type) : base(type) { }

        /// <summary>
        /// Tests if the given block type fits this type of block object
        /// </summary>
        /// <param name="block">The block to test</param>
        /// <returns>true if the block fits</returns>
        public new static bool FitsBlock(ID.Block block)
        {
            string blockName = block.ToString();
            return (blockName.Contains("skull") && !blockName.Contains("wall"));
        }

        /// <summary>
        /// The way the skull / head is rotated.
        /// (0-15. Rotation = X*22.5+South (goes south-west-north-east))
        /// (Used for standing skulls / heads)
        /// </summary>
        [BlockState("rotation")]
        [BlockIntStateRange(0, 15)]
        public int? SRotation
        {
            get => _sRotation;
            set
            {
                if (value != null && (value < 0 || value > 15))
                {
                    throw new ArgumentException(nameof(SRotation) + " has to be equel to or between 0 and 15");
                }
                _sRotation = value;
            }
        }
    }

    /// <summary>
    /// An object for skull / head blocks
    /// </summary>
    public class WallSkull : BaseSkull, Interfaces.IFacing
    {
        /// <summary>
        /// Creates a new skull / head block
        /// </summary>
        /// <param name="type">The type of block</param>
        public WallSkull(BlockType? type) : base(type) { }

        /// <summary>
        /// Tests if the given block type fits this type of block object
        /// </summary>
        /// <param name="block">The block to test</param>
        /// <returns>true if the block fits</returns>
        public new static bool FitsBlock(ID.Block block)
        {
            string blockName = block.ToString();
            return (blockName.Contains("skull") && blockName.Contains("wall"));
        }

        /// <summary>
        /// The way the skull / head is facing.
        /// </summary>
        [BlockState("facing")]
        public ID.Facing? SFacing { get; set; }
    }
}
