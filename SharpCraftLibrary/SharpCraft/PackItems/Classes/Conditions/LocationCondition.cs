using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.Conditions
{
    /// <summary>
    /// Condition which returns true if the location is correct
    /// </summary>
    public class LocationCondition : BaseCondition
    {
        private JSONObjects.Location location;

        /// <summary>
        /// Intializes a new <see cref="LocationCondition"/>
        /// </summary>
        /// <param name="location">The location to test for</param>
        public LocationCondition(JSONObjects.Location location) : base("minecraft:location_check")
        {
            Location = location;
        }

        /// <summary>
        /// The location to test for
        /// </summary>
        [DataTag("predicate", JsonTag = true)]
        public JSONObjects.Location Location { get => location; set => location = value ?? throw new ArgumentNullException(nameof(Location), "Location may not be null"); }

        /// <summary>
        /// Offset to check at
        /// </summary>
        [DataTag((object)"offsetX", "offsetY", "offsetZ", ID.NBTTagType.TagInt,true, Merge = true, JsonTag = true)]
        public Coords Offset { get; set; }
    }
}
