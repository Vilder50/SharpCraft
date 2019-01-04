using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// Entity data for chest minecarts
        /// </summary>
        public class MinecartChest : BaseMinecart
        {
            /// <summary>
            /// Creates a new chest minecart
            /// </summary>
            /// <param name="type">the type of entity</param>
            public MinecartChest(ID.Entity? type = ID.Entity.chest_minecart) : base(type) { }

            /// <summary>
            /// The chest's loottable
            /// </summary>
            [DataTag]
            public Loottable LootTable { get; set; }
            /// <summary>
            /// The seed used to generate the loot
            /// </summary>
            [DataTag]
            public long? LootTableSeed { get; set; }
            /// <summary>
            /// The items in the chest
            /// </summary>
            [DataTag]
            public SharpCraft.Item[] Items { get; set; }

            /// <summary>
            /// Gets the raw data from this entity
            /// </summary>
            public override string DataString
            {
                get
                {
                    List<string> TempList = new List<string>();

                    string NormalData = MinecartDataString;
                    if (NormalData.Length != 0) { TempList.Add(NormalData); }
                    if (LootTable != null) { TempList.Add("LootTable:\"" + LootTable + "\""); }
                    if (LootTableSeed != null) { TempList.Add("LootTableSeed:" + LootTableSeed + "L"); }
                    if (Items != null)
                    {
                        string TempString = "Items:[";
                        for (int a = 0; a < Items.Length; a++)
                        {
                            if (a != 0) { TempString += ","; }
                            TempString += "{" + Items[a].DataString + "}";
                        }
                        TempString += "]";
                        TempList.Add(TempString);
                    }

                    return string.Join(",", TempList);
                }
            }
        }
    }
}
