using System;
using System.Collections.Generic;
using System.Text;
using SharpCraft.Data;

namespace SharpCraft.DimensionObjects
{
    /// <summary>
    /// Class for dimension types
    /// </summary>
    public class DimensionTypeObject : DataHolderBase, IDimensionType
    {
        /// <summary>
        /// Makes everything hot as nether (water evaporates and lava spreads further)
        /// </summary>
        [DataTag("ultrawarm", JsonTag = true)]
        public bool UltraWarm { get; set; }

        /// <summary>
        /// If false makes compasses and clocks spin randomly
        /// </summary>
        [DataTag("natural", JsonTag = true)]
        public bool Natural { get; set; }

        /// <summary>
        /// If true 1 block = 8 blocks (like in the nether)
        /// </summary>
        [DataTag("shrunk", JsonTag = true)]
        public bool Shrunk { get; set; }

        /// <summary>
        /// If the sun makes light
        /// </summary>
        [DataTag("has_skylight", JsonTag = true)]
        public bool HasSkylight { get; set; }

        /// <summary>
        /// If lava should spread faster. Has nothing to do with ceilings...
        /// </summary>
        [DataTag("has_ceiling", JsonTag = true)]
        public bool HasCeiling { get; set; }

        /// <summary>
        /// The amount of ambient lighting in the dimension
        /// </summary>
        [DataTag("ambient_light", JsonTag = true)]
        public float AbmbientLight { get; set; }

        /// <summary>
        /// Makes the time of day fixed to specific time if set.
        /// </summary>
        public int? FixedTime { get; set; }

        /// <summary>
        /// If an ender dragon should spawn in the dimension
        /// </summary>
        [DataTag("ender_dragon", JsonTag = true)]
        public bool? HasEnderDragon { get; set; }

        [DataTag("biome_zoomer", JsonTag = true, ForceType = ID.NBTTagType.TagString)]
        public ID.BiomeZoomer? BiomeZoomer { get; set; }

        /// <summary>
        /// Returns the data in this object as a data part object
        /// </summary>
        /// <returns>The data converted</returns>
        public override DataPartObject GetDataTree()
        {
            var baseTree = base.GetDataTree();

            if (FixedTime is null)
            {
                baseTree.AddValue(new DataPartPath("fixed_time", new DataPartTag(false,null,true), true));
            } 
            else
            {
                baseTree.AddValue(new DataPartPath("fixed_time", new DataPartTag(FixedTime,null,true), true));
            }

            return baseTree;
        }

        /// <summary>
        /// Returns the string used for chosen this dimension type
        /// </summary>
        /// <returns>String for chosing this</returns>
        public string GetDimensionTypeString()
        {
            return GetDataTree().GetDataString();
        }
    }
}
