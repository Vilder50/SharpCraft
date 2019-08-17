using System.Collections.Generic;

namespace SharpCraft
{
    public partial class Item
    {
        /// <summary>
        /// An object for map items
        /// </summary>
        public class Map : Item
        {
            /// <summary>
            /// Creates an item without an id or anything but which can have data
            /// This is used to test for item with data
            /// </summary>
            public Map() { }

            /// <summary>
            /// Creates a new item
            /// </summary>
            /// <param name="ItemID">The type of the item. If null the item has no type</param>
            /// <param name="Count">The amount of the item. If null the item has no amount</param>
            /// <param name="Slot">The slot the item is in. If null the item isn't in a slot</param>
            public Map(ID.Item? ItemID, int? Count = null, int? Slot = null) : base(ItemID, Count, Slot) { }

            /// <summary>
            /// Creates an item object which refereces to an item group
            /// </summary>
            /// <param name="ItemGroup">The item group to refere to</param>
            public Map(Group ItemGroup) : base(ItemGroup) { }

            /// <summary>
            /// A class used to define map icons
            /// </summary>
            public class Icon : Data.DataHolderBase
            {
                /// <summary>
                /// A random name for the marker
                /// </summary>
                [Data.DataTag("id")]
                public string ID { get; set; }
                /// <summary>
                /// The type of marker symbol
                /// </summary>
                [Data.DataTag(ForceType = SharpCraft.ID.NBTTagType.TagByte)]
                public ID.MapMarker? MarkerType { get; set; }
                /// <summary>
                /// The location to show it at.
                /// </summary>
                [Data.DataTag("x")]
                public double? X { get; set; }
                /// <summary>
                /// Tge location to show it at.
                /// </summary>
                [Data.DataTag("z")]
                public double? Z { get; set; }
                private double? _Rotation;
                /// <summary>
                /// The icon's rotation
                /// </summary>
                [Data.DataTag("rot")]
                public double? Rotation
                {
                    get
                    {
                        return _Rotation;
                    }
                    set
                    {
                        if (value > 360) { _Rotation = 360; }
                        else if (value < 0) { _Rotation = 0; }
                        else { _Rotation = value; }
                    }
                }

                /// <summary>
                /// The raw data used by the game to make icons on maps
                /// </summary>
                public string IconString
                {
                    get
                    {
                        List<string> TempList = new List<string>();

                        if (ID != null) { TempList.Add("id:\"" + ID.Escape() + "\""); }
                        if (Rotation != null) { TempList.Add("rot:" + Rotation.ToString().Replace(",", ".")); }
                        if (X != null) { TempList.Add("x:" + X); }
                        if (Z != null) { TempList.Add("x:" + Z); }
                        if (MarkerType != null) { TempList.Add("type:" + (int)MarkerType); }

                        return string.Join(",", TempList);
                    }
                }
            }

            /// <summary>
            /// The map's ID
            /// </summary>
            [Data.DataTag("map")]
            public int? MapID { get; set; }
            /// <summary>
            /// The icons displayed on the map.
            /// </summary>
            [Data.DataTag("Decorations")]
            public Icon[] Icons { get; set; }

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

                    if (MapID != null) { TempList.Add("map:" + MapID); }
                    if (Icons != null)
                    {
                        string TempString = "Decorations:[";
                        for (int a = 0; a < Icons.Length; a++)
                        {
                            if (a != 0) { TempString += ","; }
                            TempString += "{" + Icons[a].IconString + "}";
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
