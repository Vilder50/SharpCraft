using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Item
    {
        /// <summary>
        /// Creates an item without an id or anything but which can have data
        /// This is used to test for item with data
        /// </summary>
        public Item() { }

        /// <summary>
        /// Creates a new item
        /// </summary>
        /// <param name="itemID">The type of the item. If null the item has no type</param>
        /// <param name="count">The amount of the item. If null the item has no amount</param>
        /// <param name="slot">The slot the item is in. If null the item isn't in a slot</param>
        public Item(ID.Item? itemID, int? count = null, int? slot = null)
        {
            ID = itemID;
            Slot = slot;
            Count = count;
        }

        /// <summary>
        /// Creates an item object which refereces to an item group
        /// </summary>
        /// <param name="ItemGroup">The item group to refere to</param>
        public Item(Group ItemGroup)
        {
            Group = ItemGroup;
        }

        /// <summary>
        /// The name of the item group
        /// </summary>
        public Group Group;

        /// <summary>
        /// The count of this item.
        /// If null the item doesnt have a count
        /// </summary>
        public int? Count;

        /// <summary>
        /// The slot this item is in
        /// If null the item isnt in a slot
        /// </summary>
        public int? Slot;

        /// <summary>
        /// The item type
        /// If null the item isnt any item type
        /// </summary>
        public ID.Item? ID;


        /// <summary>
        /// An object used to define item enchantments
        /// </summary>
        public class Enchantment
        {
            /// <summary>
            /// The type of the enchantment
            /// </summary>
            public ID.Enchant? ID { get; set; }
            /// <summary>
            /// The level of the enchantment
            /// </summary>
            public int? LVL { get; set; }
            /// <summary>
            /// Creates a new enchantment
            /// </summary>
            /// <param name="ID">The enchantment type</param>
            /// <param name="Level">The enchantment level</param>
            public Enchantment(ID.Enchant? ID = null, int? Level = null)
            {
                this.ID = ID;
                LVL = Level;
            }

            /// <summary>
            /// Outputs the raw data used by the game
            /// </summary>
            public string EnchantDataString
            {
                get
                {
                    if (ID != null && LVL != null)
                    {
                        return "id:" + ID + ",lvl:" + LVL + "s";
                    }
                    else if (LVL != null)
                    {
                        return "lvl:" + LVL + "s";
                    }
                    return "id:" + ID;
                }
            }
        }

        /// <summary>
        /// An object used to define what flags on an item are hidden
        /// </summary>
        public class HideFlags
        {
            /// <summary>
            /// If enchantments should be hidden
            /// </summary>
            public bool Enchants { get; set; }
            /// <summary>
            /// If attributes should be hidden
            /// </summary>
            public bool Attributes { get; set; }
            /// <summary>
            /// If it should be hidden its unbreakabel
            /// </summary>
            public bool Unbreakable { get; set; }
            /// <summary>
            /// If the blocks it can destroy should be hidden
            /// </summary>
            public bool CanDestroy { get; set; }
            /// <summary>
            /// If the blocks it can place on should be hidden
            /// </summary>
            public bool CanPlaceOn { get; set; }
            /// <summary>
            /// If the lore should be hidden
            /// </summary>
            public bool NormalLore { get; set; }

            /// <summary>
            /// Gets the raw data used by the game to hide flags
            /// </summary>
            public int HideFlagsNumber
            {
                get
                {
                    int TempInt = 0;
                    if (Enchants) { TempInt += 1; }
                    if (Attributes) { TempInt += 2; }
                    if (Unbreakable) { TempInt += 4; }
                    if (CanDestroy) { TempInt += 8; }
                    if (CanPlaceOn) { TempInt += 16; }
                    if (NormalLore) { TempInt += 32; }
                    return TempInt;
                }
            }

        }

        /// <summary>
        /// Makes the item unbreakable
        /// </summary>
        [DataTag]
        public bool? Unbreakable { get; set; }
        /// <summary>
        /// A list of blocks the item can destroy in adventure mode
        /// </summary>
        [DataTag]
        public ID.Block[] CanDestroy { get; set; }
        /// <summary>
        /// A list of blocks the item can be placed on in adventure mode
        /// </summary>
        [DataTag]
        public ID.Block[] CanPlaceOn { get; set; }
        /// <summary>
        /// How much damage the item has taken
        /// </summary>
        [DataTag]
        public int? Damage { get; set; }
        /// <summary>
        /// The data the block will have when the item is placed
        /// </summary>
        [DataTag]
        public Block BlockData { get; set; }
        /// <summary>
        /// The data the entity will have when the item is placed
        /// </summary>
        [DataTag]
        public Entity.BaseEntity EntityTag { get; set; }
        /// <summary>
        /// The enchants the item has on
        /// </summary>
        [DataTag]
        public Enchantment[] Enchants { get; set; }
        /// <summary>
        /// Number of levels to add to the base levels when using an anvil
        /// </summary>
        [DataTag]
        public int? RepairCost { get; set; }
        /// <summary>
        /// The color of the leather armor
        /// </summary>
        [DataTag]
        public HexColor LeatherColor { get; set; }
        /// <summary>
        /// The color the map item has.
        /// (The small black text like things on the paper)
        /// </summary>
        [DataTag]
        public HexColor MapColor { get; set; }
        /// <summary>
        /// The item's shown name
        /// </summary>
        [DataTag]
        public JSON[] Name { get; set; }
        /// <summary>
        /// The item's lore.
        /// Each index in the first array means a new line.
        /// </summary>
        [DataTag]
        public JSON[][] Lore { get; set; }
        /// <summary>
        /// The things to hide on the item.
        /// </summary>
        [DataTag]
        public HideFlags HiddenFlags { get; set; }
        /// <summary>
        /// The attributes the item has
        /// </summary>
        [DataTag]
        public MCAttribute[] Attributes { get; set; }
        /// <summary>
        /// A fake tag. A place to write directly in the item's data.
        /// </summary>
        [DataTag]
        public string FakeTag { get; set; }
        /// <summary>
        /// The item's model ID
        /// </summary>
        [DataTag]
        public int? CustomModelData { get; set; }



        /// <summary>
        /// The item's raw data containing its slot, count and id
        /// Used for item objects in other objects
        /// </summary>
        public string DataString
        {
            get
            {
                List<string> TempList = new List<string>();

                if (Slot != null) { TempList.Add("Slot:" + Slot + "b"); }
                if (Count != null) { TempList.Add("Count:" + Count + "b"); }
                if (ID != null) { TempList.Add("id:\"minecraft:" + ID.MinecraftValue() + "\""); }

                string TempString = TagDataString;
                if (!string.IsNullOrEmpty(TempString))
                {
                    TempList.Add("tag:{" + TempString + "}");
                }
                return string.Join(",", TempList);
            }
        }

        /// <summary>
        /// The items raw data
        /// (The data inside the item tag)
        /// </summary>
        public virtual string TagDataString
        {
            get
            {
                List<string> TempList = new List<string>();
                List<string> DisplayTempList = new List<string>();

                if (CanDestroy != null)
                {
                    string TempString = "CanDestroy:[";
                    for (int a = 0; a < CanDestroy.Length; a++)
                    {
                        if (a != 0) { TempString += ","; }
                        TempString += "\"" + CanDestroy[a] + "\"";
                    }
                    TempString += "]";
                    TempList.Add(TempString);
                }
                if (CanPlaceOn != null)
                {
                    string TempString = "CanPlaceOn:[";
                    for (int a = 0; a < CanPlaceOn.Length; a++)
                    {
                        if (a != 0) { TempString += ","; }
                        TempString += "\"" + CanPlaceOn[a] + "\"";
                    }
                    TempString += "]";
                    TempList.Add(TempString);
                }
                if (Unbreakable != null) { TempList.Add("Unbreakable:" + Unbreakable); }
                if (Damage != null) { TempList.Add("Damage:" + Damage + "s"); }
                if (BlockData != null && BlockData.HasData) { TempList.Add("BlockEntityTag:{" + BlockData.GetDataString() + "}"); }
                if (BlockData != null && BlockData.HasState) { TempList.Add("BlockStateTag:{" + BlockData.GetStateString().Replace("=",":\"").Replace(",","\",") + "\"}"); }
                if (EntityTag != null) { TempList.Add("EntityTag:{" + EntityTag.DataWithID + "}"); }
                if (Enchants != null)
                {
                    string TempString = "Enchantments:[";
                    for (int a = 0; a < Enchants.Length; a++)
                    {
                        if (a != 0) { TempString += ","; }
                        TempString += "{" + Enchants[a].EnchantDataString + "}";
                    }
                    TempString += "]";
                    TempList.Add(TempString);
                }
                if (RepairCost != null) { TempList.Add("RepairCost:" + RepairCost); }
                if (LeatherColor != null) { DisplayTempList.Add("color:" + LeatherColor.ColorInt); }
                if (MapColor != null) { DisplayTempList.Add("MapColor:" + MapColor.ColorInt); }
                if (Name != null) { DisplayTempList.Add("Name:\"" + Name.GetString().Escape() + "\""); }
                if (Lore != null)
                {
                    string TempString = "Lore:[\"";
                    for (int a = 0; a < Lore.Length; a++)
                    {
                        if (a != 0) { TempString += "\",\""; }
                        TempString += Lore[a].GetString().Escape();
                    }
                    TempString += "\"]";
                    DisplayTempList.Add(TempString);
                }
                if (HiddenFlags != null) { TempList.Add("HideFlags:" + HiddenFlags.HideFlagsNumber); }
                if (CustomModelData != null) { TempList.Add("CustomModelData:" + CustomModelData); }
                if (Attributes != null)
                {
                    List<string> TempAtList = new List<string>();
                    for (int i = 0; i < Attributes.Length; i++)
                    {
                        TempAtList.Add(Attributes[i].ItemString());
                    }
                    TempList.Add("AttributeModifiers:[" + string.Join(",", TempAtList) + "]");
                }
                if (FakeTag != null) { TempList.Add(FakeTag); }

                if (DisplayTempList.Count != 0)
                {
                    TempList.Add("display:{" + string.Join(",", DisplayTempList) + "}");
                }
                return string.Join(",", TempList);
            }
        }

        /// <summary>
        /// The items raw data with id at the start
        /// Used for give item commands
        /// </summary>
        public string IDDataString
        {
            get
            {
                if (ID == null && Group == null)
                {
                    throw new ArgumentNullException(nameof(ID) + " or " + nameof(Group) + " has to have a value to convert the item into a giveable item");
                }

                string outputString = "";

                if (ID != null)
                {
                    outputString = ID.ToString();
                }
                else
                {
                    outputString = "#" + Group.ToString();
                }
                string tagData = TagDataString;
                if (!string.IsNullOrEmpty(TagDataString)) { outputString += "{" + tagData + "}"; }

                return outputString;
            }
        }

        /// <summary>
        /// Creates a clone of this item
        /// </summary>
        /// <returns>The cloned new item</returns>
        public Item Clone()
        {
            if (Group == null)
            {
                return DataTagAttribute.Clone((Item)Activator.CreateInstance(GetType(), new object[] { ID, Count, Slot }), this);
            }
            else
            {
                return DataTagAttribute.Clone((Item)Activator.CreateInstance(GetType(), new object[] { Group }), this);
            }
        }

        /// <summary>
        /// Converts an item id into a simple item
        /// </summary>
        /// <param name="item">The item id to convert</param>
        public static implicit operator Item(ID.Item item)
        {
            return new Item(item);
        }
    }
}
