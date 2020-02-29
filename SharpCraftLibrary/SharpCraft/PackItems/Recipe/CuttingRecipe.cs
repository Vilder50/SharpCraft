using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SharpCraft
{
    /// <summary>
    /// Class for stone cutter recipe files
    /// </summary>
    public class CuttingRecipe : BaseRecipe
    {
        private ID.Item result;
        private int count;
        private ItemType[] ingredients = null!;

        /// <summary>
        /// Intializes a new <see cref="CuttingRecipe"/>. Inherite from this constructor.
        /// </summary>
        /// <param name="packNamespace">The namespace the recipe is in</param>
        /// <param name="fileName">The name of the recipe file</param>
        /// <param name="writeSetting">The settings for how to write this file</param>
        /// <param name="group">The name of the recipe group the recipe is in. Leave null for no group.</param>
        /// <param name="ingredients">The different types of items which can be used in the recipe</param>
        /// <param name="count">The amount of the result item the recipe should output</param>
        /// <param name="result">The item to craft</param>
        /// <param name="_">Unused parameter used for specifing you want to use this constructor</param>
        public CuttingRecipe(bool _, BasePackNamespace packNamespace, string? fileName, ItemType[] ingredients, ID.Item result, int count = 1, string? group = null, WriteSetting writeSetting = WriteSetting.LockedAuto) : base(packNamespace, fileName, group, writeSetting, "stonecutting")
        {
            Ingredients = ingredients;
            Result = result;
            Count = count;
        }

        /// <summary>
        /// Intializes a new <see cref="CuttingRecipe"/>
        /// </summary>
        /// <param name="packNamespace">The namespace the recipe is in</param>
        /// <param name="fileName">The name of the recipe file</param>
        /// <param name="writeSetting">The settings for how to write this file</param>
        /// <param name="group">The name of the recipe group the recipe is in. Leave null for no group.</param>
        /// <param name="ingredients">The different types of items which can be used in the recipe</param>
        /// <param name="count">The amount of the result item the recipe should output</param>
        /// <param name="result">The item to craft</param>
        public CuttingRecipe(BasePackNamespace packNamespace, string? fileName, ItemType[] ingredients, ID.Item result, int count = 1, string? group = null, WriteSetting writeSetting = WriteSetting.LockedAuto) : this(true, packNamespace, fileName, ingredients, result, count, group, writeSetting)
        {
            FinishedConstructing();
        }

        /// <summary>
        /// Intializes a new <see cref="CuttingRecipe"/>
        /// </summary>
        /// <param name="packNamespace">The namespace the recipe is in</param>
        /// <param name="fileName">The name of the recipe file</param>
        /// <param name="writeSetting">The settings for how to write this file</param>
        /// <param name="group">The name of the recipe group the recipe is in. Leave null for no group.</param>
        /// <param name="ingredient">The item used in the recipe</param>
        /// <param name="count">The amount of the result item the recipe should output</param>
        /// <param name="result">The item to craft</param>
        public CuttingRecipe(BasePackNamespace packNamespace, string? fileName, ItemType ingredient, ID.Item result, int count = 1, string? group = null, WriteSetting writeSetting = WriteSetting.LockedAuto) : this(true, packNamespace, fileName, new ItemType[] { ingredient }, result, count, group, writeSetting)
        {
            FinishedConstructing();
        }

        /// <summary>
        /// The different types of items which can be used in the recipe
        /// </summary>
        public ItemType[] Ingredients
        {
            get => ingredients;
            set
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(Ingredients), "Ingredients may not be null");
                }
                if (value.Length < 1)
                {
                    throw new ArgumentOutOfRangeException(nameof(Ingredients), "Ingredients count has to be atleast 1");
                }
                if (value.Any(i => i is null))
                {
                    throw new ArgumentNullException(nameof(Ingredients), "Ingredients may not contain null");
                }
                if (value.Any(i => i.Name == "minecraft:air"))
                {
                    throw new ArgumentException(nameof(Ingredients), "Ingredients may not contain air");
                }
                ingredients = value;
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

            //write ingredients
            if (Ingredients.Length == 1)
            {
                stream.Write(",\"ingredient\":" + GetItemCompound(Ingredients[0]));
            }
            else
            {
                stream.Write(",\"ingredient\":[" + string.Join(",", Ingredients.Select(i => GetItemCompound(i))) + "]");
            }

            //write output item
            stream.Write(",\"result\":\"minecraft:" + Result.MinecraftValue() + "\"");
            stream.Write(",\"count\":" + Count);

            WriteFileEnd(stream);
        }

        /// <summary>
        /// Clears the things in the file.
        /// </summary>
        protected override void AfterDispose()
        {
            base.AfterDispose();
            ingredients = null!;
        }
    }
}
