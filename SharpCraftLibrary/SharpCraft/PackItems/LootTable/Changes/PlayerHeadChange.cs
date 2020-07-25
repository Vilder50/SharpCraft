using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.LootObjects
{
    /// <summary>
    /// Changes the skin on a player head
    /// </summary>
    public class PlayerHeadChange : BaseChange
    {
        /// <summary>
        /// Intializes a new <see cref="PlayerHeadChange"/>
        /// </summary>
        public PlayerHeadChange() : base("minecraft:copy_name")
        {
            
        }

        /// <summary>
        /// The entity to get the skull for
        /// </summary>
        [DataTag("source", ForceType = ID.NBTTagType.TagString, JsonTag = true)]
        public ID.LootTarget Target { get; set; }
    }
}
