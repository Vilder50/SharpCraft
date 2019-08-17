using System.Collections.Generic;

namespace SharpCraft
{
    public partial class Item
    {
        /// <summary>
        /// The data for potions
        /// </summary>
        public class Potion : Item
        {
            /// <summary>
            /// Creates an item without an id or anything but which can have data
            /// This is used to test for item with data
            /// </summary>
            public Potion() { }

            /// <summary>
            /// Creates a new item
            /// </summary>
            /// <param name="ItemID">The type of the item. If null the item has no type</param>
            /// <param name="Count">The amount of the item. If null the item has no amount</param>
            /// <param name="Slot">The slot the item is in. If null the item isn't in a slot</param>
            public Potion(ID.Item? ItemID, int? Count = null, int? Slot = null) : base(ItemID, Count, Slot) { }

            /// <summary>
            /// Creates an item object which refereces to an item group
            /// </summary>
            /// <param name="ItemGroup">The item group to refere to</param>
            public Potion(Group ItemGroup) : base(ItemGroup) { }

            /// <summary>
            /// The effects given by the potion
            /// </summary>
            [Data.DataTag("CustomPotionEffects")]
            public Effect[] PotionEffects { get; set; }
            /// <summary>
            /// The color of the potion
            /// </summary>
            [Data.DataTag("CustomPotionColor", ForceType = SharpCraft.ID.NBTTagType.TagInt)]
            public HexColor PotionColor { get; set; }
            /// <summary>
            /// The effect given by the potion using minecraft values.
            /// </summary>
            [Data.DataTag("Potion",ForceType = SharpCraft.ID.NBTTagType.TagString)]
            public ID.Potion? PotionType { get; set; }

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

                    if (PotionEffects != null)
                    {
                        string TempString = "CustomPotionEffects:[";
                        for (int a = 0; a < PotionEffects.Length; a++)
                        {
                            if (a != 0) { TempString += ","; }
                            TempString += "{" + PotionEffects[a] + "}";
                        }
                        TempString += "]";
                        TempList.Add(TempString);
                    }
                    if (PotionColor != null) { TempList.Add("CustomPotionColor:" + PotionColor.ColorInt); }
                    if (PotionType != null) { TempList.Add("Potion:\"minecraft:" + PotionType + "\""); }

                    return string.Join(",", TempList);
                }
            }
        }
    }
}
