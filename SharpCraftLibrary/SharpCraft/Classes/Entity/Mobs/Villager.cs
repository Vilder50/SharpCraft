using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class Entity
    {
        //TODO update to 1.14 Villagers
        /// <summary>
        /// Entity data for villagers
        /// </summary>
        public class Villager :BaseBreedable
        {
            /// <summary>
            /// Creates a new villager
            /// </summary>
            /// <param name="type">the type of entity</param>
            public Villager(ID.Entity? type = ID.Entity.villager) : base(type) { }


            /// <summary>
            /// The amount of emeralds given to the villager
            /// </summary>
            [DataTag]
            public int? Riches { get; set; }
            /// <summary>
            /// The type of villager
            /// </summary>
            [DataTag]
            public ID.Villager? VillagerType { get; set; }
            /// <summary>
            /// The current level of trading.
            /// (This number is used to chose what trades are open / are going to open next)
            /// </summary>
            [DataTag]
            public int? CareerLevel { get; set; }
            /// <summary>
            /// If the villager is willing to mate
            /// </summary>
            [DataTag]
            public bool? Willing { get; set; }
            /// <summary>
            /// The villager's inventory
            /// </summary>
            [DataTag]
            public Item[] Inventory { get; set; }
            /// <summary>
            /// The trades the villager has
            /// </summary>
            [DataTag]
            public Trade[] Trades { get; set; }

            /// <summary>
            /// An object used to define trades
            /// </summary>
            public class Trade
            {
                /// <summary>
                /// True if the villager gives xp when traded with
                /// </summary>
                public bool? RewardExp { get; set; }
                /// <summary>
                /// The maximum number of times the trade can be traded before closing
                /// </summary>
                public int? MaxUses { get; set; }
                /// <summary>
                /// The amount of times the trade has been used
                /// </summary>
                public int? Uses { get; set; }
                /// <summary>
                /// The first item the villager is buying in this trade
                /// </summary>
                public Item BuyItem1 { get; set; }
                /// <summary>
                /// The second item the villager is buying in this trade
                /// </summary>
                public Item BuyItem2 { get; set; }
                /// <summary>
                /// The item the villager is selling in this trade
                /// </summary>
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
                    if (Riches != null) { TempList.Add("Riches:" + Riches); }
                    if (VillagerType != null)
                    {
                        switch ((int)VillagerType)
                        {
                            case 0:
                            case 1:
                            case 2:
                            case 3:
                                TempList.Add("Career:" + ((int)VillagerType + 1));
                                TempList.Add("Profession:0");
                                break;
                            case 4:
                            case 5:
                                TempList.Add("Career:" + ((int)VillagerType - 3));
                                TempList.Add("Profession:1");
                                break;
                            case 6:
                                TempList.Add("Career:1");
                                TempList.Add("Profession:2");
                                break;
                            case 7:
                            case 8:
                            case 9:
                                TempList.Add("Career:" + ((int)VillagerType - 6));
                                TempList.Add("Profession:3");
                                break;
                            case 10:
                            case 11:
                                TempList.Add("Career:" + ((int)VillagerType - 9));
                                TempList.Add("Profession:4");
                                break;
                            case 12:
                                TempList.Add("Career:1");
                                TempList.Add("Profession:5");
                                break;
                        }
                    }
                    if (CareerLevel != null) { TempList.Add("CareerLevel:" + CareerLevel); }
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


                    return string.Join(",", TempList);
                }
            }
        }
    }
}
