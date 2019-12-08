using System.Collections.Generic;
using SharpCraft.Data;

namespace SharpCraft
{
    public static partial class JSONObjects
    {
        /// <summary>
        /// a <see cref="object"/> defining an <see cref="SharpCraft.Item"/>
        /// </summary>
        public class Item : DataHolderBase
        {
            /// <summary>
            /// the <see cref="SharpCraft.Item"/>'s id
            /// </summary>
            [DataTag("item", JsonTag = true, ForceType = ID.NBTTagType.TagString)]
            public ID.Item? Id { get; set; }

            /// <summary>
            /// the <see cref="SharpCraft.Item"/>'s id
            /// </summary>
            [DataTag("tag", JsonTag = true, ForceType = ID.NBTTagType.TagString)]
            public ItemGroup Group { get; set; }

            /// <summary>
            /// the <see cref="SharpCraft.Item"/>'s durability
            /// </summary>
            [DataTag("durability", JsonTag = true)]
            public Range Durability { get; set; }

            /// <summary>
            /// the <see cref="SharpCraft.Item"/>'s count
            /// </summary>
            [DataTag("count", JsonTag = true)]
            public Range Count { get; set; }

            /// <summary>
            /// the <see cref="SharpCraft.Item"/>'s potion
            /// </summary>
            [DataTag("potion", JsonTag = true, ForceType = ID.NBTTagType.TagString)]
            public ID.Potion? Potion { get; set; }

            /// <summary>
            /// the <see cref="SharpCraft.Item"/>'s <see cref="Enchantment"/>s
            /// </summary>
            [DataTag("enchantments", JsonTag = true)]
            public Enchantment[] Enchantments { get; set; }

            /// <summary>
            /// the <see cref="SharpCraft.Item"/>'s nbt data
            /// </summary>
            [DataTag("nbt", JsonTag = true, ForceType = ID.NBTTagType.TagString)]
            public SharpCraft.Item NBT { get; set; }

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
                [DataTag("levels","min","max",ID.NBTTagType.TagDouble,true, JsonTag = true, ForceType = ID.NBTTagType.TagInt)]
                public Range Level { get; set; }

                /// <summary>
                /// Outputs this <see cref="Enchantment"/> data in string format
                /// </summary>
                /// <returns>this <see cref="Enchantment"/>'s data</returns>
                public override string ToString()
                {
                    List<string> TempList = new List<string>();
                    if (Enchant != null) { TempList.Add("\"enchantment\": \"" + Enchant + "\""); }
                    if (Level != null) { TempList.Add(Level.JSONString("levels")); }
                    return "{" + string.Join(",", TempList) + "}";
                }
            }

            /// <summary>
            /// Outputs this <see cref="Item"/> data in string format
            /// </summary>
            /// <returns>this <see cref="Item"/>'s data</returns>
            public override string ToString()
            {
                List<string> TempList = new List<string>();

                if (Id != null) { TempList.Add("\"item\":\"minecraft:" + Id.ToString() + "\""); }
                if (Durability != null) { TempList.Add(Durability.JSONString("durability")); }
                if (Count != null) { TempList.Add(Count.JSONString("count")); }
                if (Potion != null) { TempList.Add("\"potion\": \"" + Potion + "\""); }
                if (Enchantments != null)
                {
                    List<string> TempEnchantList = new List<string>();
                    for (int i = 0; i < Enchantments.Length; i++)
                    {
                        TempEnchantList.Add(Enchantments[i].ToString());
                    }
                    TempList.Add("\"enchantments\": [" + string.Join(",", TempEnchantList) + "]");
                }
                if (NBT != null) { TempList.Add("\"nbt\":\"" + NBT.GetItemTagString().Escape() + "\""); }

                return "{" + string.Join(",", TempList) + "}";
            }

            /// <summary>
            /// Converts an item id into a <see cref="Item"/> object
            /// </summary>
            /// <param name="item">the item id to convert</param>
            public static implicit operator Item(ID.Item item)
            {
                return new Item { Id = item};
            }

            /// <summary>
            /// Converts an item group into a <see cref="Item"/> object
            /// </summary>
            /// <param name="group">the item group to convert</param>
            public static implicit operator Item(ItemGroup group)
            {
                return new Item { Group = group };
            }
        }
    }
}
