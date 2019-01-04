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
            public Book(ID.Item? ItemID, int? Count = null, int? Slot = null) : base(ItemID, Count, Slot) { }

            /// <summary>
            /// Creates an item object which refereces to an item group
            /// </summary>
            /// <param name="ItemGroup">The item group to refere to</param>
            public Book(Group ItemGroup) : base(ItemGroup) { }

            /// <summary>
            /// The generation of the book.
            /// (How much it has been copied)
            /// </summary>
            [DataTag]
            public ID.BookGeneration? Generation { get; set; }
            /// <summary>
            /// The auther of the book
            /// </summary>
            [DataTag]
            public string Author { get; set; }
            /// <summary>
            /// The book's title
            /// </summary>
            [DataTag]
            public string Title { get; set; }
            /// <summary>
            /// The book's pages.
            /// Each index in the first array means a new page.
            /// </summary>
            [DataTag]
            public JSON[][] Pages { get; set; }
            /// <summary>
            /// The recipes unlocked when right clicking a recipe book
            /// </summary>
            [DataTag]
            public Recipe[] UnlockRecipes { get; set; }

            /// <summary>
            /// The stored enchantments the item has. These enchants does not effect anything. but can be extracted from the book with an anvil.
            /// </summary>
            [DataTag]
            public Enchantment[] StoredEnchants { get; set; }

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
                    if (Generation != null) { TempList.Add("generation:" + (int)Generation); }
                    if (Author != null) { TempList.Add("author:\"" + Author.Escape() + "\""); }
                    if (Title != null) { TempList.Add("title:\"" + Title.Escape() + "\""); }
                    if (Pages != null)
                    {
                        string TempString = "pages:[\"";
                        for (int i = 0; i < Pages.Length; i++)
                        {
                            if (Pages[i] != null)
                            {
                                if (i != 0) { TempString += "\",\""; }
                                TempString += "[";
                                for (int j = 0; j < Pages[i].Length; j++)
                                {
                                    if (j != 0) { TempString += ","; }
                                    if (Pages[i][j].BasicText == true)
                                    {
                                        TempString += Pages[i][j].Text.Escape();
                                    }
                                    else
                                    {
                                        TempString += Pages[i][j].ToString().Escape();
                                    }
                                }
                                TempString += "]";
                            }
                        }
                        TempList.Add(TempString + "\"]");
                    }
                    if (UnlockRecipes != null)
                    {
                        List<string> TempRecipeList = new List<string>();
                        for (int i = 0; i < UnlockRecipes.Length; i++)
                        {
                            TempRecipeList.Add("\"" + UnlockRecipes.ToString() + "\"");
                        }
                        TempList.Add("Recipes:[" + string.Join(",", TempRecipeList) + "]");
                    }
                    if (StoredEnchants != null)
                    {
                        string TempString = "StoredEnchantments:[";
                        for (int a = 0; a < StoredEnchants.Length; a++)
                        {
                            if (a != 0) { TempString += ","; }
                            TempString += "{" + StoredEnchants[a].EnchantDataString + "}";
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
