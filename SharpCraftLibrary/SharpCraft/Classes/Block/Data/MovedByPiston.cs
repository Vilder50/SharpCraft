using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for blocks being moved by a piston
        /// </summary>
        public class MovedByPiston : Block, IBlock.IFacingFull
        {
            /// <summary>
            /// Intilizes a new block object
            /// </summary>
            public MovedByPiston()
            {
                ID = null;
            }

            /// <summary>
            /// Creates a new block being moved by a piston
            /// </summary>
            /// <param name="type">The type of block</param>
            public MovedByPiston(ID.Block? type = SharpCraft.ID.Block.piston) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public MovedByPiston(Group group) : base(group) { }

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
            [Data.CustomDataTag]
            public Block DMovingBlock { get; set; }

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

            /// <summary>
            /// Gets the raw data for the data the block contains
            /// </summary>
            /// <returns>Raw data used by Minecraft</returns>
            public override string GetDataString()
            {
                base.GetDataString();

                List<string> TempList = new List<string>();

                if (DMovingBlock != null && (DMovingBlock.ID != null || DMovingBlock.HasState))
                {
                    string blockState = "BlockState:{";
                    if (DMovingBlock.ID != null) { blockState += "Name:\"minecraft:" + DMovingBlock.ID.ToString() + "\""; }
                    if (DMovingBlock.ID != null && DMovingBlock.HasState) { blockState += ","; }
                    if (DMovingBlock.HasState) { blockState += "Properties:{" + DMovingBlock.GetStateString().ToString().Replace("=", ":\"").Replace(",", "\",") + "\"}"; }
                    TempList.Add(blockState + "}");
                }

                if (DDirection != null) { TempList.Add("facing:" + (int)DDirection); }
                if (DProgress != null) { TempList.Add("progress:" + DProgress + "f"); }
                if (Pushed != null) { TempList.Add("extending:" + Pushed.ToMinecraftBool()); }
                if (PistonHead != null) { TempList.Add("source:" + PistonHead.ToMinecraftBool()); }

                return string.Join(",", TempList);
            }
        }
    }
}
