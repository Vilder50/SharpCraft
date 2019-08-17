using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// Entity data for hopper minecarts
        /// </summary>
        public class MinecartHopper : BaseMinecart
        {
            /// <summary>
            /// Creates a new hopper minecart
            /// </summary>
            /// <param name="type">the type of entity</param>
            public MinecartHopper(ID.Entity? type = ID.Entity.hopper_minecart) : base(type) { }

            /// <summary>
            /// The hopper's loottable
            /// </summary>
            [Data.DataTag(ForceType = ID.NBTTagType.TagString)]
            public Loottable LootTable { get; set; }
            /// <summary>
            /// The seed used to generate the loot
            /// </summary>
            [Data.DataTag]
            public long? LootTableSeed { get; set; }
            /// <summary>
            /// The items in the hopper
            /// </summary>
            [Data.DataTag]
            public SharpCraft.Item[] Items { get; set; }
            /// <summary>
            /// If the hopper is enabled
            /// </summary>
            [Data.DataTag]
            public bool? Enabled { get; set; }
            /// <summary>
            /// Time until it transfer another item
            /// </summary>
            [Data.DataTag(ForceType = ID.NBTTagType.TagInt)]
            public Time TransferCooldown { get; set; }

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
                    if (Enabled != null) { TempList.Add("Enabled:" + Enabled.ToMinecraftBool()); }
                    if (TransferCooldown != null) { TempList.Add("TransferCooldown:" + TransferCooldown.AsTicks()); }
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
