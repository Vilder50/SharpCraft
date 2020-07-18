using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.LootObjects
{
    /// <summary>
    /// Entry for dropping an item
    /// </summary>
    public class ItemEntry : BaseEntry
    {
        private IItemType item = null!;

        /// <summary>
        /// Intializes a new <see cref="ItemEntry"/>
        /// </summary>
        /// <param name="item">The item the entry drops</param>
        public ItemEntry(IItemType item) : base(ID.LootEntryType.item)
        {
            Item = item;
        }

        /// <summary>
        /// The item the entry drops
        /// </summary>
        [DataTag("name", ForceType = ID.NBTTagType.TagString, JsonTag = true)]
        public IItemType Item { get => item; set => item = value ?? throw new ArgumentNullException(nameof(Item), "Item may not be null"); }

        /// <summary>
        /// Changes to make to the entry
        /// </summary>
        [DataTag("functions", JsonTag = true)]
        public BaseChange[]? Changes { get; set; }
    }
}
