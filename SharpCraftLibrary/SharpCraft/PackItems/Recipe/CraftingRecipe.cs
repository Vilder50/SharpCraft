using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCraft
{
    /// <summary>
    /// Class for normal crafting recipes
    /// </summary>
    public class CraftingRecipe : BaseRecipe
    {
        private ItemType[,] recipe = null!;
        private ID.Item result;
        private int count;

        /// <summary>
        /// Intializes a new <see cref="CraftingRecipe"/>. Inherite from this constructor.
        /// </summary>
        /// <param name="packNamespace">The namespace the recipe is in</param>
        /// <param name="fileName">The name of the recipe file</param>
        /// <param name="writeSetting">The settings for how to write this file</param>
        /// <param name="group">The name of the recipe group the recipe is in. Leave null for no group.</param>
        /// <param name="recipe">The recipe for crafting the item. Use <see cref="ID.Item.air"/> or null for empty slots</param>
        /// <param name="count">The amount of the result item the recipe should output</param>
        /// <param name="result">The item to craft</param>
        /// <param name="_">Unused parameter used for specifing you want to use this constructor</param>
        protected CraftingRecipe(bool _, BasePackNamespace packNamespace, string? fileName, ItemType[,] recipe, ID.Item result, int count = 1, string? group = null, WriteSetting writeSetting = WriteSetting.LockedAuto) : base(packNamespace, fileName, group, writeSetting, "crafting_shaped")
        {
            Recipe = recipe;
            Result = result;
            Count = count;
        }

        /// <summary>
        /// Intializes a new <see cref="CraftingRecipe"/>
        /// </summary>
        /// <param name="packNamespace">The namespace the recipe is in</param>
        /// <param name="fileName">The name of the recipe file</param>
        /// <param name="writeSetting">The settings for how to write this file</param>
        /// <param name="group">The name of the recipe group the recipe is in. Leave null for no group.</param>
        /// <param name="recipe">The recipe for crafting the item. Use <see cref="ID.Item.air"/> or null for empty slots</param>
        /// <param name="count">The amount of the result item the recipe should output</param>
        /// <param name="result">The item to craft</param>
        public CraftingRecipe(BasePackNamespace packNamespace, string? fileName, ItemType[,] recipe, ID.Item result, int count = 1, string? group = null, WriteSetting writeSetting = WriteSetting.LockedAuto) : this(true, packNamespace, fileName, recipe, result, count, group, writeSetting)
        {
            FinishedConstructing();
        }

        /// <summary>
        /// The recipe for crafting the item. Use <see cref="ID.Item.air"/> or null for empty slots
        /// </summary>
        public ItemType[,] Recipe
        {
            get => recipe;
            set
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(Recipe), "Recipe may not be null");
                }
                if (value.GetLength(0) > 3 || value.GetLength(1) > 3)
                {
                    throw new ArgumentOutOfRangeException(nameof(Recipe), "Recipe may not be bigger than 3x3");
                }
                if (value.GetLength(0) < 1 || value.GetLength(1) < 1)
                {
                    throw new ArgumentOutOfRangeException(nameof(Recipe), "Recipe may not be smaller than 1x1");
                }
                if ((from ItemType item in value select item).All(i => i is null || i.Name == "minecraft:air"))
                {
                    throw new ArgumentException(nameof(Recipe), "Recipe may not only contain empty slots");
                }
                recipe = value;
            }
        }

        /// <summary>
        /// The item to craft
        /// </summary>
        public ID.Item Result 
        { 
            get => result; 
            set
            {
                if (value == ID.Item.air)
                {
                    throw new ArgumentException(nameof(Result), "Result may not be null");
                }
                result = value;
            }
        }

        /// <summary>
        /// The amount of the result item the recipe should output
        /// </summary>
        public int Count 
        { 
            get => count;
            set
            {
                if (value < 1)
                {
                    throw new ArgumentOutOfRangeException(nameof(Count), "Count may not be less than 0");
                }
                if (value > 64)
                {
                    throw new ArgumentOutOfRangeException(nameof(Count), "Count may not be more than 64");
                }
                count = value;
            }
        }


        /// <summary>
        /// Writes the file
        /// </summary>
        /// <param name="stream">The stream used for writing the file</param>
        protected override void WriteFile(TextWriter stream)
        {
            WriteFileStart(stream);

            //get keys and Write pattern
            stream.Write(",\"pattern\":");
            Dictionary<string, (int key, ItemType item)> keys = new Dictionary<string, (int key, ItemType item)>();
            List<string> recipeLines = new List<string>();

            for (int y = 0; y < Recipe.GetLength(0); y++)
            {
                string row = "";
                for (int x = 0; x < Recipe.GetLength(1); x++)
                {
                    string useKey;
                    ItemType item = Recipe[y, x];
                    if (item is null || item.Name == "minecraft:air")
                    {
                        useKey = " ";
                    }
                    else if (!keys.ContainsKey(item.Name))
                    {
                        useKey = keys.Keys.Count.ToString();
                        keys.Add(item.Name, (keys.Keys.Count, item));
                    }
                    else
                    {
                        useKey = keys[item.Name].key.ToString();
                    }
                    row += useKey;
                }
                recipeLines.Add(row);
            }
            stream.Write("[\"" + string.Join("\",\"", recipeLines) + "\"]");

            //Write keys
            List<string> keyLines = new List<string>();
            foreach (KeyValuePair<string, (int key, ItemType item)> key in keys)
            {
                keyLines.Add("\"" + key.Value.key + "\":" + GetItemCompound(key.Value.item));
            }
            stream.Write(",\"key\":{" + string.Join(",", keyLines) + "}");

            //write output item
            stream.Write(",\"result\":{\"item\":\"minecraft:" + Result.FixEnum() + "\"");
            if (Count != 1)
            {
                stream.Write(",\"count\":" + Count);
            }
            stream.Write("}");

            WriteFileEnd(stream);
        }

        /// <summary>
        /// Clears the things in the file.
        /// </summary>
        protected override void AfterDispose()
        {
            base.AfterDispose();
            recipe = null!;
        }
    }
}
