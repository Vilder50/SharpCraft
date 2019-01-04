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
        public class MovedByPiston : CloneBlock<MovedByPiston>
        {
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
            /// The way the block is being pushed
            /// </summary>
            [BlockData("facing")]
            public ID.FacingFull? SPushing { get; set; }

            /// <summary>
            /// The type of piston base used
            /// </summary>
            [BlockData("type")]
            public ID.PistonType? SPistonBase { get; set; }

            /// <summary>
            /// If the piston arm is short
            /// </summary>
            [BlockData("short")]
            public bool? SShort { get; set; }

            /// <summary>
            /// The block being moved
            /// Note: block data is not supported
            /// </summary>
            [BlockData]
            public Block DMovingBlock { get; set; }

            /// <summary>
            /// The way the block is being pushed / pulled from
            /// </summary>
            [BlockData]
            public ID.FacingFull? DDirection { get; set; }

            /// <summary>
            /// How far the black has been moved
            /// </summary>
            [BlockData]
            public double? DProgress { get; set; }

            /// <summary>
            /// If the block is pushed or pulled
            /// </summary>
            [BlockData]
            public bool? Pushed { get; set; }

            /// <summary>
            /// If the block actually is the piston's piston head
            /// </summary>
            [BlockData]
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
