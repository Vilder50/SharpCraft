using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// Entity data for shulker
        /// </summary>
        public class Shulker : BaseMob
        {
            /// <summary>
            /// Creates a new shulker
            /// </summary>
            /// <param name="type">the type of entity</param>
            public Shulker(ID.Entity? type = ID.Entity.shulker) : base(type) { }

            /// <summary>
            /// The direction of the block the shulker is placed on
            /// </summary>
            [Data.DataTag("AttachFace", ForceType = ID.NBTTagType.TagByte)]
            public ID.ShulkerDirection? PlacedOn { get; set; }
            /// <summary>
            /// The shulker's color.
            /// Setting this to (ID.Color)16 makes it the normal shulker color
            /// </summary>
            [Data.DataTag(ForceType = ID.NBTTagType.TagByte)]
            public ID.Color? Color { get; set; }
            /// <summary>
            /// The height of the shulker peek
            /// </summary>
            [Data.DataTag]
            public byte? Peek { get; set; }
            /// <summary>
            /// The approximate location of the shulker
            /// </summary>
            [Data.DataTag((object)"APX","APY","APZ", ID.NBTTagType.TagInt, Merge = true)]
            public Coords ApproxCoords { get; set; }
        }
    }
}
