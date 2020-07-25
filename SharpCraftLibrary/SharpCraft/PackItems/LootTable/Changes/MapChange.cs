using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.LootObjects
{
    /// <summary>
    /// Changes map item destination
    /// </summary>
    public class MapChange : BaseChange
    {
        /// <summary>
        /// Intializes a new <see cref="MapChange"/>
        /// </summary>
        public MapChange(ID.Structure structure) : base("minecraft:exploration_map")
        {
            Structure = structure;
        }

        /// <summary>
        /// The destination of the map
        /// </summary>
        [DataTag("destination", JsonTag = true)]
        public ID.Structure Structure { get; set; }

        /// <summary>
        /// The icon used for marking the destination on the map
        /// </summary>
        [DataTag("decoration", ForceType = ID.NBTTagType.TagString, JsonTag = true)]
        public ID.MapMarker Marker { get; set; }

        /// <summary>
        /// The zoom level on the map
        /// </summary>
        [DataTag("zoom", JsonTag = true)]
        public int? ZoomLevel { get; set; }

        /// <summary>
        /// The radius to search for the structure in.
        /// </summary>
        [DataTag("search_radius", JsonTag = true)]
        public int? SearchRadius { get; set; }

        /// <summary>
        /// If the map shouldn't be able to find already found structures
        /// </summary>
        [DataTag("skip_existing_chunks", JsonTag = true)]
        public bool SkipExistingChunks { get; set; }
    }
}
