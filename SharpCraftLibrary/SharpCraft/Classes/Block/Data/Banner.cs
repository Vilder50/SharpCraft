using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for banner blocks
        /// </summary>
        public class Banner : Block, IBlock.IRotation, IBlock.IFacing
        {
            private int? _sRotation;

            /// <summary>
            /// Intilizes a new block object
            /// </summary>
            public Banner()
            {
                ID = null;
            }

            /// <summary>
            /// Creates a new banner block
            /// </summary>
            /// <param name="type">The type of block</param>
            public Banner(ID.Block? type) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public Banner(Group group) : base(group) { }

            /// <summary>
            /// Tests if the given block type fits this type of block object
            /// </summary>
            /// <param name="block">The block to test</param>
            /// <returns>true if the block fits</returns>
            public new static bool FitsBlock(ID.Block block)
            {
                string blockName = block.ToString();
                return (blockName.Contains("banner"));
            }

            /// <summary>
            /// The way the banner is rotated.
            /// (0-15. Rotation = X*22.5+South (goes south-west-north-east))
            /// (Used for standing banners)
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
            /// The way the banner is facing.
            /// (Used for banners on a wall)
            /// </summary>
            [BlockState("facing")]
            public ID.Facing? SFacing { get; set; }

            /// <summary>
            /// The banners name.
            /// This name is showed on maps which has clicked this banner.
            /// </summary>
            [Data.DataTag("CustomName", ForceType = SharpCraft.ID.NBTTagType.TagString)]
            public JSON[] DCustomName { get; set; }

            /// <summary>
            /// The banner's patterns
            /// </summary>
            [Data.DataTag("Patterns")]
            public BannerPattern[] DPatterns { get; set; }

            /// <summary>
            /// An object defining a banner pattern
            /// </summary>
            public class BannerPattern : Data.DataHolderBase
            {
                /// <summary>
                /// Creates a new banner pattern
                /// </summary>
                /// <param name="SetPattern">The pattern to use</param>
                /// <param name="SetColor">The color of the pattern</param>
                public BannerPattern(ID.BannerPattern SetPattern, ID.Color SetColor)
                {
                    Pattern = SetPattern;
                    Color = SetColor;
                }

                /// <summary>
                /// The pattern's color
                /// </summary>
                [Data.DataTag(ForceType = SharpCraft.ID.NBTTagType.TagInt)]
                public ID.Color Color { get; set; }

                /// <summary>
                /// The pattern
                /// </summary>
                [Data.DataTag(ForceType = SharpCraft.ID.NBTTagType.TagString)]
                public ID.BannerPattern Pattern { get; set; }
            }

            /// <summary>
            /// Gets the raw data for the data the block contains
            /// </summary>
            /// <returns>Raw data used by Minecraft</returns>
            public override string GetDataString()
            {
                base.GetDataString();

                List<string> TempList = new List<string>();

                if (DCustomName != null) { TempList.Add("CustomName:\"" + DCustomName.GetString().Escape() + "\""); }
                if (DPatterns != null)
                {
                    string TempString = "Patterns:[";
                    for (int i = 0; i < DPatterns.Length; i++)
                    {
                        if (i != 0) { TempString += ","; }
                        TempString += "{Color:" + (int)DPatterns[i].Color + ",Pattern:\"" + DPatterns[i].Pattern + "\"}";
                    }
                    TempString += "]";
                    TempList.Add(TempString);
                }

                return string.Join(",", TempList);
            }
        }
    }
}
