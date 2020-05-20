using System.Collections.Generic;

namespace SharpCraft.Items
{
    /// <summary>
    /// An object for recipe book items
    /// </summary>
    public class RecipeBook : Item
    {
        /// <summary>
        /// Creates an item without an id or anything but which can have data
        /// This is used to test for item with data
        /// </summary>
        public RecipeBook() { }

        /// <summary>
        /// Creates a new item
        /// </summary>
        /// <param name="ItemID">The type of the item. If null the item has no type</param>
        /// <param name="Count">The amount of the item. If null the item has no amount</param>
        /// <param name="Slot">The slot the item is in. If null the item isn't in a slot</param>
        public RecipeBook(ItemType? ItemID, sbyte? Count = null, sbyte? Slot = null) : base(ItemID, Count, Slot) { }

        /// <summary>
        /// The recipes unlocked when right clicking a recipe book
        /// </summary>
        [Data.DataTag("tag.Recipes", ForceType = SharpCraft.ID.NBTTagType.TagStringArray)]
        public IRecipe[]? UnlockRecipes { get; set; }
    }
}
