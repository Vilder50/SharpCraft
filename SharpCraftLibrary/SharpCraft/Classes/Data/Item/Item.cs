using System;
using System.Collections.Generic;
using System.Text;
using SharpCraft.Data;
using System.Linq;

namespace SharpCraft
{
    public partial class Item : DataHolderBase
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
        public Item(ItemType itemID, sbyte? count = null, sbyte? slot = null)
        {
            ID = itemID;
            Slot = slot;
            Count = count;
        }

        /// <summary>
        /// The count of this item.
        /// If null the item doesnt have a count
        /// </summary>
        [DataTag]
        public sbyte? Count { get; set; }

        /// <summary>
        /// The slot this item is in
        /// If null the item isnt in a slot
        /// </summary>
        [DataTag]
        public sbyte? Slot { get; set; }

        /// <summary>
        /// The item type
        /// If null the item isnt any item type
        /// </summary>
        [DataTag("id", ForceType = SharpCraft.ID.NBTTagType.TagString)]
        public ItemType ID { get; set; }

        /// <summary>
        /// An object used to define item enchantments
        /// </summary>
        public class Enchantment : DataHolderBase
        {
            /// <summary>
            /// The type of the enchantment
            /// </summary>
            [DataTag("id", ForceType = SharpCraft.ID.NBTTagType.TagNamespacedString)]
            public ID.Enchant? ID { get; set; }
            /// <summary>
            /// The level of the enchantment
            /// </summary>
            [DataTag("lvl", ForceType = SharpCraft.ID.NBTTagType.TagInt)]
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
        }

        /// <summary>
        /// An object used to define what flags on an item are hidden
        /// </summary>
        public class HideFlags : IConvertableToDataTag
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

            /// <summary>
            /// Converts this <see cref="HideFlags"/> object into a <see cref="DataPartTag"/>
            /// </summary>
            /// <param name="asType">Not used</param>
            /// <param name="extraConversionData">Not used</param>
            /// <returns>the made <see cref="DataPartTag"/></returns>
            public DataPartTag GetAsTag(ID.NBTTagType? asType, object[] extraConversionData)
            {
                return new DataPartTag(HideFlagsNumber);
            }
        }

        /// <summary>
        /// Makes the item unbreakable
        /// </summary>
        [DataTag("tag.Unbreakable")]
        public bool? Unbreakable { get; set; }
        /// <summary>
        /// A list of blocks the item can destroy in adventure mode
        /// </summary>
        [DataTag("tag.CanDestroy",ForceType = SharpCraft.ID.NBTTagType.TagStringArray)]
        public BlockType[] CanDestroy { get; set; }
        /// <summary>
        /// A list of blocks the item can be placed on in adventure mode
        /// </summary>
        [DataTag("tag.CanPlaceOn",ForceType = SharpCraft.ID.NBTTagType.TagStringArray)]
        public BlockType[] CanPlaceOn { get; set; }
        /// <summary>
        /// How much damage the item has taken
        /// </summary>
        [DataTag("tag.Damage")]
        public int? Damage { get; set; }
        /// <summary>
        /// The data the block will have when the item is placed
        /// </summary>
        [DataTag("tag.BlockEntityTag")]
        public Block BlockData { get; set; }
        /// <summary>
        /// The data the entity will have when the item is placed
        /// </summary>
        [DataTag("tag.EntityTag")]
        public Entity.BaseEntity EntityTag { get; set; }
        /// <summary>
        /// The enchants the item has on
        /// </summary>
        [DataTag("tag.Enchantments")]
        public Enchantment[] Enchants { get; set; }
        /// <summary>
        /// Number of levels to add to the base levels when using an anvil
        /// </summary>
        [DataTag("tag.RepairCost")]
        public int? RepairCost { get; set; }
        /// <summary>
        /// The color of the leather armor
        /// </summary>
        [DataTag("tag.display.color")]
        public RGBColor LeatherColor { get; set; }
        /// <summary>
        /// The color the map item has.
        /// (The small black text like things on the paper)
        /// </summary>
        [DataTag("tag.display.MapColor")]
        public RGBColor MapColor { get; set; }
        /// <summary>
        /// The item's shown name
        /// </summary>
        [DataTag("tag.display.Name", ForceType = SharpCraft.ID.NBTTagType.TagString)]
        public JsonText Name { get; set; }
        /// <summary>
        /// The item's lore.
        /// Each index in the first array means a new line.
        /// </summary>
        [DataTag("tag.display.Lore", ForceType = SharpCraft.ID.NBTTagType.TagStringArray)]
        public JsonText[] Lore { get; set; }
        /// <summary>
        /// The things to hide on the item.
        /// </summary>
        [DataTag("tag.HideFlags")]
        public HideFlags HiddenFlags { get; set; }
        /// <summary>
        /// The attributes the item has
        /// </summary>
        [DataTag("tag.AttributeModifiers")]
        public MCAttribute[] Attributes { get; set; }
        /// <summary>
        /// A fake tag. A place to write directly in the item's data.
        /// </summary>
        [DataTag("tag.Data", ForceType = SharpCraft.ID.NBTTagType.TagCompound)]
        public string FakeTag { get; set; }
        /// <summary>
        /// Extra data for this item to hold
        /// </summary>
        [DataTag("tag.Data")]
        public DataHolderBase ExtraTags { get; set; }
        /// <summary>
        /// The item's model ID
        /// </summary>
        [DataTag("tag.CustomModelData")]
        public int? CustomModelData { get; set; }

        /// <summary>
        /// Returns the item's data from the .tag tag
        /// </summary>
        /// <returns>the .tag data. Null if there is not data there</returns>
        public string GetItemTagString()
        {
            DataPartObject tree = GetDataTree();
            DataPartObject tagTag = (DataPartObject)tree.GetValues().SingleOrDefault(o => o.PathName == "tag")?.PathValue;
            if (tagTag is null)
            {
                return null;
            }
            else
            {
                return tagTag.GetDataString();
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
                if (ID == null)
                {
                    throw new ArgumentNullException(nameof(ID) + " has to have a value to convert the item into a giveable item");
                }

                string outputString = ID.Name;
                string tagData = GetItemTagString();
                if (!string.IsNullOrEmpty(tagData)) { outputString += tagData; }

                return outputString;
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
