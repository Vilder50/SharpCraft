using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class JSONObjects
    {
        /// <summary>
        /// a <see cref="object"/> defining an <see cref="SharpCraft.Item"/>
        /// </summary>
        public class Item
        {
            /// <summary>
            /// the <see cref="SharpCraft.Item"/>'s id
            /// </summary>
            public ItemType Id;

            /// <summary>
            /// the <see cref="SharpCraft.Item"/>'s durability
            /// </summary>
            public Range Durability;

            /// <summary>
            /// the <see cref="SharpCraft.Item"/>'s count
            /// </summary>
            public Range Count;

            /// <summary>
            /// the <see cref="SharpCraft.Item"/>'s potion
            /// </summary>
            public ID.Potion? Potion;

            /// <summary>
            /// the <see cref="SharpCraft.Item"/>'s <see cref="Enchantment"/>s
            /// </summary>
            public Enchantment[] Enchantments;

            /// <summary>
            /// the <see cref="SharpCraft.Item"/>'s nbt data
            /// </summary>
            public SharpCraft.Item NBT;

            /// <summary>
            /// a <see cref="object"/> defining an enchantment
            /// </summary>
            public class Enchantment
            {
                /// <summary>
                /// the enchantment id
                /// </summary>
                public ID.Enchant? Enchant;

                /// <summary>
                /// the level of the enchantment
                /// </summary>
                public Range Level;

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

                if (Id != null) { TempList.Add("\"item\": \"" + Id.Name + "\""); }
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
            /// Converts an <see cref="SharpCraft.Item"/> object into a <see cref="Item"/> object
            /// </summary>
            /// <param name="item">The <see cref="SharpCraft.Item"/> to convert</param>
            public static implicit operator Item(SharpCraft.Item item)
            {
                Item returnItem = new Item() {Id = item.ID, Count = item.Count };
                if (string.IsNullOrEmpty(item.GetItemTagString()))
                {
                    returnItem.NBT = item;
                }
                return returnItem;
            }

            /// <summary>
            /// Converts an item id ento a <see cref="Item"/> object
            /// </summary>
            /// <param name="item">the item id to convert</param>
            public static implicit operator Item(ID.Item item)
            {
                return new Item { Id = item};
            }
        }
    }
}
