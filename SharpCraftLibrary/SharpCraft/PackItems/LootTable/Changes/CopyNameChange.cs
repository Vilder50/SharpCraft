using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.LootObjects
{
    /// <summary>
    /// Changes the loot name to the name of the broken block
    /// </summary>
    public class CopyNameChange : BaseChange
    {
        /// <summary>
        /// Intializes a new <see cref="CopyNameChange"/>
        /// </summary>
        public CopyNameChange() : base("copy_name")
        {
            
        }

        /// <summary>
        /// The place to copy the name from (Can only be block_entity)
        /// </summary>
        [DataTag("source", ForceType = ID.NBTTagType.TagString, JsonTag = true)]
        public ID.LootTarget Target { get; private set; } = ID.LootTarget.block_entity;
    }
}
