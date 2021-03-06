﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SharpCraft
{
    /// <summary>
    /// Class for shapeless recipe files
    /// </summary>
    public class ShapelessRecipe : BaseRecipe
    {
        private ID.Item result = null!;
        private int count;
        private IItemType[] ingredients = null!;

        /// <summary>
        /// Intializes a new <see cref="ShapelessRecipe"/>. Inherite from this constructor.
        /// </summary>
        /// <param name="packNamespace">The namespace the recipe is in</param>
        /// <param name="fileName">The name of the recipe file</param>
        /// <param name="writeSetting">The settings for how to write this file</param>
        /// <param name="group">The name of the recipe group the recipe is in. Leave null for no group.</param>
        /// <param name="ingredients">The items used in the recipe</param>
        /// <param name="count">The amount of the result item the recipe should output</param>
        /// <param name="result">The item to craft</param>
        /// <param name="_">Unused parameter used for specifing you want to use this constructor</param>
        protected ShapelessRecipe(bool _, BasePackNamespace packNamespace, string? fileName, IItemType[] ingredients, ID.Item result, int count = 1, string? group = null, WriteSetting writeSetting = WriteSetting.LockedAuto) : base(packNamespace, fileName, group, writeSetting, "minecraft:crafting_shapeless")
        {
            Ingredients = ingredients;
            Result = result;
            Count = count;
        }

        /// <summary>
        /// Intializes a new <see cref="ShapelessRecipe"/>
        /// </summary>
        /// <param name="packNamespace">The namespace the recipe is in</param>
        /// <param name="fileName">The name of the recipe file</param>
        /// <param name="writeSetting">The settings for how to write this file</param>
        /// <param name="group">The name of the recipe group the recipe is in. Leave null for no group.</param>
        /// <param name="ingredients">The items used in the recipe</param>
        /// <param name="count">The amount of the result item the recipe should output</param>
        /// <param name="result">The item to craft</param>
        public ShapelessRecipe(BasePackNamespace packNamespace, string? fileName, IItemType[] ingredients, ID.Item result, int count = 1, string? group = null, WriteSetting writeSetting = WriteSetting.LockedAuto) : this(true, packNamespace, fileName, ingredients, result, count, group, writeSetting)
        {
            FinishedConstructing();
        }

        /// <summary>
        /// The items used in the recipe
        /// </summary>
        public IItemType[] Ingredients
        {
            get => ingredients;
            set
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(Ingredients), "Ingredients may not be null");
                }
                if (value.Length < 1 || value.Length > 9)
                {
                    throw new ArgumentOutOfRangeException(nameof(Ingredients), "Ingredient count has to be between 1 and 9");
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
            stream.Write(",\"ingredients\":["+string.Join(",",Ingredients.Select(i => GetItemCompound(i)))+"]");

            //write output item
            stream.Write(",\"result\":{\"item\":\"" + Result + "\"");
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
            ingredients = null!;
        }
    }
}
