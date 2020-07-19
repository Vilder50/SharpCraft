using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// Entity data for shulker
    /// </summary>
    public class Shulker : Mob
    {
        /// <summary>
        /// Returns a object which can be used for creating data paths
        /// </summary>
        /// <returns>Object used for making data paths</returns>
        public new static Data.DataPathCreator<Shulker> PathCreator => new Data.DataPathCreator<Shulker>();

        /// <summary>
        /// Creates a new shulker
        /// </summary>
        /// <param name="type">the type of entity</param>
        public Shulker(ID.Entity? type) : base(type) { }

        /// <summary>
        /// Creates a new entity
        /// </summary>
        public Shulker() : base(SharpCraft.ID.Entity.shulker) { }

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
        [Data.DataTag((object)"APX", "APY", "APZ", Merge = true)]
        public IntVector? ApproxCoords { get; set; }
    }
}
