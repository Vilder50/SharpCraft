using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.LootObjects
{
    /// <summary>
    /// Changes the enchants on the item by enchanting the item with the given amount of levels
    /// </summary>
    public class EnchantRandomlyChange : BaseChange
    {
        private Range levels;

        /// <summary>
        /// Intializes a new <see cref="EnchantRandomlyChange"/>
        /// </summary>
        public EnchantRandomlyChange(Range levels, bool treasure) : base("enchant_with_levels")
        {
            Treasure = treasure;
            Levels = levels;
        }

        /// <summary>
        /// If treasure enchants should be get-able
        /// </summary>
        [DataTag("treasure", JsonTag = true)]
        public bool Treasure { get; set; }

        /// <summary>
        /// The amount of levels to enchant with
        /// </summary>
        [DataTag("levels", "min", "max", ID.NBTTagType.TagDouble, true, JsonTag = true)]
        public Range Levels { get => levels; set => levels = value ?? throw new ArgumentNullException(nameof(Levels), "Levels may not be null"); }
    }
}
