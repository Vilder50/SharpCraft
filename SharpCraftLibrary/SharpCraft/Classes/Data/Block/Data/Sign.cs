using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for sign blocks
        /// </summary>
        public class Sign : Block, IBlock.IRotation, IBlock.IWaterLogged
        {
            private int? _sRotation;

            /// <summary>
            /// Creates a new sign block
            /// </summary>
            /// <param name="type">The type of block</param>
            public Sign(ID.Block? type = SharpCraft.ID.Block.oak_sign) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public Sign(Group group) : base(group) { }

            /// <summary>
            /// Tests if the given block type fits this type of block object
            /// </summary>
            /// <param name="block">The block to test</param>
            /// <returns>true if the block fits</returns>
            public new static bool FitsBlock(ID.Block block)
            {
                string blockName = block.ToString();
                return (blockName.Contains("sign"));
            }

            /// <summary>
            /// If the sign is water logged
            /// </summary>
            [BlockState("waterlogged")]
            public bool? SWaterLogged { get; set; }

            /// <summary>
            /// The way the sign is rotated.
            /// (0-15. Rotation = X*22.5+South (goes south-west-north-east))
            /// (Used for standing signs)
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
            /// <summary>
            /// The way the sign is facing.
            /// (Used for signs on a wall)
            /// </summary>
            [BlockState("facing")]
            public ID.Facing? SFacing { get; set; }

            /// <summary>
            /// The color of the text on the sign
            /// </summary>
            [Data.DataTag("Color", ForceType = SharpCraft.ID.NBTTagType.TagString)]
            public ID.MinecraftColor? DColor { get; set; }

            /// <summary>
            /// The text on line 1
            /// </summary>
            [Data.DataTag("Text1", ForceType = SharpCraft.ID.NBTTagType.TagString)]
            public JSON[] DText1 { get; set; }

            /// <summary>
            /// The text on line 2
            /// </summary>
            [Data.DataTag("Text2", ForceType = SharpCraft.ID.NBTTagType.TagString)]
            public JSON[] DText2 { get; set; }

            /// <summary>
            /// The text on line 3
            /// </summary>
            [Data.DataTag("Text3", ForceType = SharpCraft.ID.NBTTagType.TagString)]
            public JSON[] DText3 { get; set; }

            /// <summary>
            /// The text on line 4
            /// </summary>
            [Data.DataTag("Text4", ForceType = SharpCraft.ID.NBTTagType.TagString)]
            public JSON[] DText4 { get; set; }

            /// <summary>
            /// Gets the raw data for the data the block contains
            /// </summary>
            /// <returns>Raw data used by Minecraft</returns>
            public override string GetDataString()
            {
                base.GetDataString();

                List<string> TempList = new List<string>();

                if (DColor != null) { TempList.Add("Color:\"" + DColor.ToString() + "\""); }
                if (DText1 != null) { TempList.Add("Text1:\"" + DText1.GetString().Escape() + "\""); }
                if (DText2 != null) { TempList.Add("Text2:\"" + DText2.GetString().Escape() + "\""); }
                if (DText3 != null) { TempList.Add("Text3:\"" + DText3.GetString().Escape() + "\""); }
                if (DText4 != null) { TempList.Add("Text4:\"" + DText4.GetString().Escape() + "\""); }

                return string.Join(",", TempList);
            }
        }
    }
}
