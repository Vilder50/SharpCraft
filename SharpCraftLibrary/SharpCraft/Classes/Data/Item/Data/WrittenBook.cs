using System.Collections.Generic;

namespace SharpCraft.Items
{
    /// <summary>
    /// An object for written book items
    /// </summary>
    public class WrittenBook : Item
    {
        /// <summary>
        /// Creates an item without an id or anything but which can have data
        /// This is used to test for item with data
        /// </summary>
        public WrittenBook() { }

        /// <summary>
        /// Creates a new item
        /// </summary>
        /// <param name="ItemID">The type of the item. If null the item has no type</param>
        /// <param name="Count">The amount of the item. If null the item has no amount</param>
        /// <param name="Slot">The slot the item is in. If null the item isn't in a slot</param>
        public WrittenBook(ItemType? ItemID, sbyte? Count = null, sbyte? Slot = null) : base(ItemID, Count, Slot) { }

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
        public string? Author { get; set; }

        /// <summary>
        /// The book's title
        /// </summary>
        [Data.DataTag("tag.title")]
        public string? Title { get; set; }

        /// <summary>
        /// The book's pages.
        /// Each index in the first array means a new page.
        /// </summary>
        [Data.DataTag("tag.Pages", ForceType = SharpCraft.ID.NBTTagType.TagStringArray)]
        public BaseJsonText[]? Pages { get; set; }
    }
}
