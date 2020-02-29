using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.LootObjects
{
    /// <summary>
    /// Changes the loot nbt
    /// </summary>
    public class NBTChange : BaseChange
    {
        private SimpleDataHolder data = null!;

        /// <summary>
        /// Intializes a new <see cref="NBTChange"/>
        /// </summary>
        /// <param name="data">The NBT to use</param>
        public NBTChange(SimpleDataHolder data) : base("set_nbt")
        {
            Data = data;
        }

        /// <summary>
        /// The NBT to use
        /// </summary>
        [DataTag("tag", ForceType = ID.NBTTagType.TagString, JsonTag = true)]
        public SimpleDataHolder Data { get => data; set => data = value ?? throw new ArgumentNullException(nameof(Data), "Data may not be null"); }
    }
}
