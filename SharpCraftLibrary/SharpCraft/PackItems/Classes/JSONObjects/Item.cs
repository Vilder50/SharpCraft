using System.Collections.Generic;
using SharpCraft.Data;

namespace SharpCraft.JsonObjects
{
    /// <summary>
    /// a <see cref="object"/> defining an <see cref="SharpCraft.Item"/>
    /// </summary>
    public class Item : DataHolderBase
    {
        /// <summary>
        /// the <see cref="SharpCraft.Item"/>'s id
        /// </summary>
        [DataTag((object)"item", "tag", true, JsonTag = true, ForceType = ID.NBTTagType.TagCompound, Merge = true)]
        public ItemType? Id { get; set; }

        /// <summary>
        /// the <see cref="SharpCraft.Item"/>'s durability
        /// </summary>
        [DataTag("durability", JsonTag = true)]
        public MCRange? Durability { get; set; }

        /// <summary>
        /// the <see cref="SharpCraft.Item"/>'s count
        /// </summary>
        [DataTag("count", JsonTag = true)]
        public MCRange? Count { get; set; }

        /// <summary>
        /// the <see cref="SharpCraft.Item"/>'s potion
        /// </summary>
        [DataTag("potion", JsonTag = true, ForceType = ID.NBTTagType.TagString)]
        public ID.Potion? Potion { get; set; }

        /// <summary>
        /// the <see cref="SharpCraft.Item"/>'s <see cref="Enchantment"/>s
        /// </summary>
        [DataTag("enchantments", JsonTag = true)]
        public Enchantment[]? Enchantments { get; set; }

        /// <summary>
        /// the <see cref="SharpCraft.Item"/>'s stored <see cref="Enchantment"/>s
        /// </summary>
        [DataTag("stored_enchantments", JsonTag = true)]
        public Enchantment[]? StoredEnchantments { get; set; }

        /// <summary>
        /// the <see cref="SharpCraft.Item"/>'s nbt data
        /// </summary>
        [DataTag("nbt", JsonTag = true, ForceType = ID.NBTTagType.TagString)]
        public SharpCraft.Item? NBT { get; set; }

        /// <summary>
        /// a <see cref="object"/> defining an enchantment
        /// </summary>
        public class Enchantment : DataHolderBase
        {
            /// <summary>
            /// the enchantment id
            /// </summary>
            [DataTag("enchantment", JsonTag = true, ForceType = ID.NBTTagType.TagNamespacedString)]
            public ID.Enchant? Enchant { get; set; }

            /// <summary>
            /// the level of the enchantment
            /// </summary>
            [DataTag("levels", "min", "max", ID.NBTTagType.TagDouble, true, JsonTag = true, ForceType = ID.NBTTagType.TagInt)]
            public MCRange? Level { get; set; }
        }

        /// <summary>
        /// Converts an item id into a <see cref="Item"/> object
        /// </summary>
        /// <param name="item">the item id to convert</param>
        public static implicit operator Item(ID.Item item)
        {
            return new Item { Id = item };
        }

        /// <summary>
        /// Converts an item group into a <see cref="Item"/> object
        /// </summary>
        /// <param name="group">the item group to convert</param>
        public static implicit operator Item(ItemGroup group)
        {
            return new Item { Id = group };
        }

        /// <summary>
        /// Converts an item into a <see cref="Item"/> object
        /// </summary>
        /// <param name="item">The item to convert</param>
        public static implicit operator Item(SharpCraft.Item item)
        {
            return new Item() { Id = item.ID, NBT = item };
        }
    }
}
