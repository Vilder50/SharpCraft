using System.Collections.Generic;
using SharpCraft.Data;

namespace SharpCraft.JsonObjects
{
    /// <summary>
    /// A <see cref="object"/> defining a location
    /// </summary>
    public class Location : DataHolderBase
    {
        /// <summary>
        /// The biome to detect
        /// </summary>
        [DataTag("biome", JsonTag = true)]
        public ID.Biome? Biome { get; set; }

        /// <summary>
        /// the dimension to detect
        /// </summary>
        [DataTag("dimension", JsonTag = true)]
        public DimensionObjects.IDimension? Dimension { get; set; }

        /// <summary>
        /// the structure to detect
        /// </summary>
        [DataTag("feature", JsonTag = true)]
        public ID.Structure? Structure { get; set; }

        /// <summary>
        /// If the location should be smokey from a fireplace
        /// </summary>
        [DataTag("smokey", JsonTag = true)]
        public bool? Smokey { get; set; }

        /// <summary>
        /// the x coordinate to detect
        /// </summary>
        [DataTag("position.x", "min", "max", ID.NBTTagType.TagDouble, true, JsonTag = true)]
        public MCRange? X { get; set; }

        /// <summary>
        /// the y coordinate to detect
        /// </summary>
        [DataTag("position.y", "min", "max", ID.NBTTagType.TagDouble, true, JsonTag = true)]
        public MCRange? Y { get; set; }

        /// <summary>
        /// the z coordinate to detect
        /// </summary>
        [DataTag("position.z", "min", "max", ID.NBTTagType.TagDouble, true, JsonTag = true)]
        public MCRange? Z { get; set; }

        /// <summary>
        /// the light level to check for
        /// </summary>
        [DataTag("light.light", "min", "max", ID.NBTTagType.TagInt, true, JsonTag = true)]
        public MCRange? Light { get; set; }

        /// <summary>
        /// The block at the location
        /// </summary>
        [DataTag("block", "block", "tag", "nbt", "state", true, JsonTag = true)]
        public Block? Block { get; set; }

        /// <summary>
        /// The liquid at the location
        /// </summary>
        [DataTag("fluid", "fluid", "tag", "nbt", "state", true, JsonTag = true)]
        public Block? Liquid { get; set; }

        /// <summary>
        /// Converts a <see cref="ID.Biome"/> into a <see cref="Location"/>
        /// </summary>
        /// <param name="biome">The <see cref="ID.Biome"/> to convert</param>
        public static implicit operator Location(ID.Biome biome)
        {
            return new Location() { Biome = biome };
        }

        /// <summary>
        /// Converts a <see cref="ID.Structure"/> into a <see cref="Location"/>
        /// </summary>
        /// <param name="structure">The <see cref="ID.Structure"/> to convert</param>
        public static implicit operator Location(ID.Structure structure)
        {
            return new Location() { Structure = structure };
        }

        /// <summary>
        /// Converts a <see cref="SharpCraft.Dimension"/> into a <see cref="Location"/>
        /// </summary>
        /// <param name="dimension">The <see cref="SharpCraft.Dimension"/> to convert</param>
        public static implicit operator Location(Dimension dimension)
        {
            return new Location() { Dimension = dimension };
        }

        /// <summary>
        /// Converts a <see cref="FileMocks.MockDimension"/> into a <see cref="Location"/>
        /// </summary>
        /// <param name="dimension">The <see cref="FileMocks.MockDimension"/> to convert</param>
        public static implicit operator Location(FileMocks.MockDimension dimension)
        {
            return new Location() { Dimension = dimension };
        }
    }
}