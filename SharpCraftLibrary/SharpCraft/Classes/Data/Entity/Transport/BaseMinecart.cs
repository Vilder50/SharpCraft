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
            [Data.DataTag("DisplayState","Name","Properties")]
            public Block? DisplayBlock { get; set; }
            /// <summary>
            /// The y-offset the block is displayed with
            /// </summary>
            [Data.DataTag]
            public int? DisplayOffset { get; set; }
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
        }
    }
}
