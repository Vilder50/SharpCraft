using System.Collections.Generic;

namespace SharpCraft
{
    public partial class Item
    {
        /// <summary>
        /// An object for firework items
        /// </summary>
        public class Firework : Item
        {
            /// <summary>
            /// Creates an item without an id or anything but which can have data
            /// This is used to test for item with data
            /// </summary>
            public Firework() { }

            /// <summary>
            /// Creates a new item
            /// </summary>
            /// <param name="ItemID">The type of the item. If null the item has no type</param>
            /// <param name="Count">The amount of the item. If null the item has no amount</param>
            /// <param name="Slot">The slot the item is in. If null the item isn't in a slot</param>
            public Firework(ID.Item? ItemID, int? Count = null, int? Slot = null) : base(ItemID, Count, Slot) { }

            /// <summary>
            /// Creates an item object which refereces to an item group
            /// </summary>
            /// <param name="ItemGroup">The item group to refere to</param>
            public Firework(Group ItemGroup) : base(ItemGroup) { }

            /// <summary>
            /// The data for a single firework star item
            /// </summary>
            [Data.DataTag("Explosion")]
            public SharpCraft.Firework FireworkStar { get; set; }
            /// <summary>
            /// The data for a firework rocket
            /// </summary>
            [Data.CustomDataTag]
            public SharpCraft.Firework[] FireworkRocket { get; set; }
            /// <summary>
            /// How many seconds the rocket will fly for
            /// </summary>
            [Data.CustomDataTag]
            public int? RocketFlight { get; set; }

            /// <summary>
            /// The items raw data
            /// (The data inside the item tag)
            /// </summary>
            public override string TagDataString
            {
                get
                {
                    List<string> TempList = new List<string>();
                    string NormalData = base.TagDataString;
                    if (NormalData.Length != 0) { TempList.Add(NormalData); }

                    if (FireworkStar != null) { TempList.Add("Explosion:{" + FireworkStar + "}"); }
                    string TempString = "";
                    if (FireworkRocket != null || RocketFlight != null)
                    {
                        TempString = "Explosions:[";
                    }
                    if (RocketFlight != null)
                    {
                        TempString += "Flight:" + RocketFlight;
                        if (FireworkRocket != null) { TempString += ","; }
                    }
                    if (FireworkRocket != null)
                    {
                        for (int a = 0; a < FireworkRocket.Length; a++)
                        {
                            if (a != 0) { TempString += ","; }
                            TempString += "{" + FireworkRocket[a] + "}";
                        }
                        TempString += "]";
                    }
                    if (TempString != "")
                    {
                        TempList.Add(TempString);
                    }

                    return string.Join(",", TempList);
                }
            }
        }
    }
}
