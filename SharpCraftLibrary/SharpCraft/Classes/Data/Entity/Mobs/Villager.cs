﻿using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// Entity data for villagers
        /// </summary>
        public class Villager : BaseBreedable
        {
            /// <summary>
            /// Creates a new villager
            /// </summary>
            /// <param name="type">the type of entity</param>
            public Villager(ID.Entity? type = ID.Entity.villager) : base(type) { }

            /// <summary>
            /// If the villager is willing to mate
            /// </summary>
            [Data.DataTag]
            public bool? Willing { get; set; }
            /// <summary>
            /// The villager's inventory
            /// </summary>
            [Data.DataTag]
            public Item[] Inventory { get; set; }
            /// <summary>
            /// The trades the villager has
            /// </summary>
            [Data.DataTag("Offers")]
            public Trade[] Trades { get; set; }

            /// <summary>
            /// The villager's proffession
            /// </summary>
            [Data.CustomDataTag]
            public int? Level { get; set; }

            /// <summary>
            /// The villager's proffession
            /// </summary>
            [Data.CustomDataTag]
            public ID.VillagerProffession? Proffession {get; set;}

            /// <summary>
            /// The type of villager
            /// </summary>
            [Data.CustomDataTag]
            public ID.VillagerType? Type {get; set;}

            /// <summary>
            /// An object used to define trades
            /// </summary>
            public class Trade : Data.DataHolderBase
            {
                /// <summary>
                /// True if the villager gives xp when traded with
                /// </summary>
                [Data.DataTag("rewardExp")]
                public bool? RewardExp { get; set; }
                /// <summary>
                /// The maximum number of times the trade can be traded before closing
                /// </summary>
                [Data.DataTag("maxUses")]
                public int? MaxUses { get; set; }
                /// <summary>
                /// The amount of times the trade has been used
                /// </summary>
                [Data.DataTag("uses")]
                public int? Uses { get; set; }
                /// <summary>
                /// The first item the villager is buying in this trade
                /// </summary>
                [Data.DataTag("buy")]
                public Item BuyItem1 { get; set; }
                /// <summary>
                /// The second item the villager is buying in this trade
                /// </summary>
                [Data.DataTag("buyB")]
                public Item BuyItem2 { get; set; }
                /// <summary>
                /// The item the villager is selling in this trade
                /// </summary>
                [Data.DataTag("sell")]
                public Item SellItem { get; set; }

                /// <summary>
                /// Gets the raw data from this trade
                /// </summary>
                /// <returns>Raw data used by Minecraft</returns>
                public override string ToString()
                {
                    List<string> TempList = new List<string>();

                    if (RewardExp != null) { TempList.Add("rewardExp:" + RewardExp.ToMinecraftBool()); }
                    if (MaxUses != null) { TempList.Add("maxUses:" + MaxUses); }
                    if (Uses != null) { TempList.Add("uses:" + Uses); }
                    if (BuyItem1 != null) { TempList.Add("buy:{" + BuyItem1.DataString + "}"); }
                    if (BuyItem2 != null) { TempList.Add("buyB:{" + BuyItem2.DataString + "}"); }
                    if (SellItem != null) { TempList.Add("sell:{" + SellItem.DataString + "}"); }

                    return string.Join(",", TempList);
                }
            }

            /// <summary>
            /// Gets the raw data from this entity
            /// </summary>
            public override string DataString
            {
                get
                {
                    List<string> TempList = new List<string>();

                    string NormalData = BreedDataString;
                    if (NormalData.Length != 0) { TempList.Add(NormalData); }
                    if (Willing != null) { TempList.Add("Willing:" + Willing.ToMinecraftBool()); }
                    if (Inventory != null)
                    {
                        string TempString = "Inventory:[";
                        for (int a = 0; a < Inventory.Length; a++)
                        {
                            if (a != 0) { TempString += ","; }
                            TempString += "{" + Inventory[a].DataString + "}";
                        }
                        TempString += "]";
                        TempList.Add(TempString);
                    }
                    if (Trades != null)
                    {
                        string TempString = "Offers:{Recipes:[";
                        for (int a = 0; a < Trades.Length; a++)
                        {
                            if (a != 0) { TempString += ","; }
                            TempString += "{" + Trades[a] + "}";
                        }
                        TempString += "]}";
                        TempList.Add(TempString);
                    }
                    if (Level != null || Proffession != null || Type != null) 
                    {
                        List<string> typeList = new List<string>();
                        if (Level != null) {typeList.Add("level:" + Level);}
                        if (Proffession != null) {typeList.Add("proffesion:\"" + Proffession + "\"");}
                        if (Type != null) {typeList.Add("type:\"" + Type + "\"");}
                        TempList.Add("VillagerData:{" + string.Join(",", typeList) + "}");
                    }


                    return string.Join(",", TempList);
                }
            }
        }
    }
}