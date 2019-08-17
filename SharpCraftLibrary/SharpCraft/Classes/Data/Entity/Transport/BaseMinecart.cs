using System.Collections.Generic;
using System;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// The basic entity data for minecarts
        /// </summary>
        public abstract class BaseMinecart : EntityBasic
        {
            /// <summary>
            /// Creates a new entity
            /// </summary>
            /// <param name="type">the type of entity</param>
            public BaseMinecart(ID.Entity? type) : base(type) { }

            /// <summary>
            /// If <see cref="DisplayBlock"/> should be displayed
            /// </summary>
            [Data.DataTag]
            public bool? CustomDisplayTile { get; set; }
            /// <summary>
            /// The block to display
            /// Note: block data is not supported
            /// </summary>
            [Data.CustomDataTag]
            public Block DisplayBlock { get; set; }
            /// <summary>
            /// The y-offset the block is displayed with
            /// </summary>
            [Data.DataTag]
            public int? DisplayOffset { get; set; }

            /// <summary>
            /// Gets the raw basic data for minecarts
            /// </summary>
            public string MinecartDataString
            {
                get
                {
                    List<string> TempList = new List<string>();

                    string basicData = BasicDataString;
                    if (basicData.Length != 0) { TempList.Add(basicData); }
                    if (CustomDisplayTile != null) { TempList.Add("CustomDisplayTile:" + CustomDisplayTile.ToMinecraftBool()); }

                    if (DisplayBlock != null && (DisplayBlock.ID != null || DisplayBlock.HasState))
                    {
                        string blockState = "BlockState:{";
                        if (DisplayBlock.ID != null) { blockState += "Name:\"minecraft:" + DisplayBlock.ID.ToString() + "\""; }
                        if (DisplayBlock.ID != null && DisplayBlock.HasState) { blockState += ","; }
                        if (DisplayBlock.HasState) { blockState += "Properties:{" + DisplayBlock.GetStateString().ToString().Replace("=", ":\"").Replace(",", "\",") + "\"}"; }
                        TempList.Add(blockState + "}");
                    }

                    if (DisplayOffset != null) { TempList.Add("DisplayOffset:" + DisplayOffset); }

                    return string.Join(",", TempList);
                }
            }
        }

        /// <summary>
        /// Entity data for minecarts
        /// </summary>
        public class Minecart : BaseMinecart
        {
            /// <summary>
            /// Creates a new minecart
            /// </summary>
            /// <param name="type">the type of entity</param>
            public Minecart(ID.Entity? type = ID.Entity.minecart) : base(type) { }

            /// <summary>
            /// Gets the raw data from this entity
            /// </summary>
            public override string DataString
            {
                get
                {
                    return MinecartDataString;
                }
            }
        }
    }
}
