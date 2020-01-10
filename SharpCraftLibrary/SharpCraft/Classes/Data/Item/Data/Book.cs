using System.Collections.Generic;

namespace SharpCraft
{
    /// <summary>
    /// A object defining a Minecraft item
    /// </summary>
    public partial class Item
    {
        /// <summary>
        /// An object for book items
        /// </summary>
        public class Book : Item
        {
            /// <summary>
            /// Creates an item without an id or anything but which can have data
            /// This is used to test for item with data
            /// </summary>
            public Book() { }

            /// <summary>
            /// Creates a new item
            /// </summary>
            /// <param name="ItemID">The type of the item. If null the item has no type</param>
            /// <param name="Count">The amount of the item. If null the item has no amount</param>
            /// <param name="Slot">The slot the item is in. If null the item isn't in a slot</param>
            public Book(ItemType ItemID, sbyte? Count = null, sbyte? Slot = null) : base(ItemID, Count, Slot) { }

            /// <summary>
            /// The generation of the book.
            /// (How much it has been copied)
            /// </summary>
            [Data.DataTag("tag.generation", ForceType = SharpCraft.ID.NBTTagType.TagInt)]
            public ID.BookGeneration? Generation { get; set; }

            /// <summary>
            /// The auther of the book
            /// </summary>
            [Data.DataTag("tag.author")]
            public string Author { get; set; }

            /// <summary>
            /// The book's title
            /// </summary>
            [Data.DataTag("tag.title")]
            public string Title { get; set; }

            /// <summary>
            /// The book's pages.
            /// Each index in the first array means a new page.
            /// </summary>
            [Data.DataTag("tag.Pages", ForceType = SharpCraft.ID.NBTTagType.TagStringArray)]
            public JsonText[][] Pages { get; set; }

            /// <summary>
            /// The recipes unlocked when right clicking a recipe book
            /// </summary>
            [Data.DataTag("tag.Recipes", ForceType = SharpCraft.ID.NBTTagType.TagStringArray)]
            public IRecipe[] UnlockRecipes { get; set; }

            /// <summary>
            /// The stored enchantments the item has. These enchants does not effect anything. but can be extracted from the book with an anvil.
            /// </summary>
            [Data.DataTag("tag.StoredEnchantments")]
            public Enchantment[] StoredEnchants { get; set; }
        }
    }
}
