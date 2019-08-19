using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for skull / head blocks
        /// </summary>
        public abstract class BaseSkull : Block
        {
            private string _dDataSkin;

            /// <summary>
            /// Creates a new skull / head block
            /// </summary>
            /// <param name="type">The type of block</param>
            public BaseSkull(ID.Block? type = SharpCraft.ID.Block.player_head) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public BaseSkull(Group group) : base(group) { }

            /// <summary>
            /// The name of the player whose skin to display
            /// </summary>
            [Data.DataTag("SkullOwner")]
            public string DPlayerSkin { get; set; }

            /// <summary>
            /// The raw data for a skin.
            /// (Starting from the Owner tag)
            /// </summary>
            [Data.CustomDataTag]
            public string DDataSkin
            {
                get => _dDataSkin;
                set
                {
                    if (value.StartsWith("{"))
                    {
                        value = value.Substring(1,value.Length - 1);
                    }
                    if (value.EndsWith("}"))
                    {
                        value = value.Substring(0, value.Length - 1);
                    }
                    if (!value.StartsWith("Owner:{"))
                    {
                        throw new FormatException("The raw data is invalid for a player head");
                    }

                    _dDataSkin = value;
                }
            }

            /// <summary>
            /// Gets the raw data for the data the block contains
            /// </summary>
            /// <returns>Raw data used by Minecraft</returns>
            public override string GetDataString()
            {
                base.GetDataString();

                List<string> TempList = new List<string>();

                if (DPlayerSkin != null) { TempList.Add("SkullOwner:\"" + DPlayerSkin.Escape() + "\""); }
                if (DDataSkin != null) { TempList.Add(DDataSkin.Escape()); }

                return string.Join(",", TempList);
            }
        }

        /// <summary>
        /// An object for skull / head blocks
        /// </summary>
        public class GroundSkull : BaseSkull, IBlock.IRotation
        {
            private int? _sRotation;

            /// <summary>
            /// Intilizes a new block object
            /// </summary>
            public GroundSkull()
            {
                ID = null;
            }

            /// <summary>
            /// Creates a new skull / head block
            /// </summary>
            /// <param name="type">The type of block</param>
            public GroundSkull(ID.Block? type = SharpCraft.ID.Block.player_head) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public GroundSkull(Group group) : base(group) { }

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
        public class WallSkull : BaseSkull, IBlock.IFacing
        {
            /// <summary>
            /// Intilizes a new block object
            /// </summary>
            public WallSkull()
            {
                ID = null;
            }

            /// <summary>
            /// Creates a new skull / head block
            /// </summary>
            /// <param name="type">The type of block</param>
            public WallSkull(ID.Block? type = SharpCraft.ID.Block.player_head) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public WallSkull(Group group) : base(group) { }

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
}
