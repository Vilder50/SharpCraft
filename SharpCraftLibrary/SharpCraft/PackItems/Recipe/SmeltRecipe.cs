using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCraft
{
    /// <summary>
    /// Class for smeltable recipe files
    /// </summary>
    public class SmeltRecipe : BaseRecipe
    {
        private ItemType[] ingredients = null!;
        private ID.Item result;
        private double experience;
        private NoneNegativeTime<int>? cookingTime;

        /// <summary>
        /// Smelting recipe type
        /// </summary>
        public enum SmeltType
        {
            /// <summary>
            /// Blast furnace recipe
            /// </summary>
            blasting,

            /// <summary>
            /// Campfire recipe
            /// </summary>
            campfire_cooking,

            /// <summary>
            /// Furnace recipe
            /// </summary>
            smelting,

            /// <summary>
            /// Smoker recipe
            /// </summary>
            smoking
        }

        /// <summary>
        /// Intializes a new <see cref="SmeltRecipe"/>. Inherite from this constructor.
        /// </summary>
        /// <param name="packNamespace">The namespace the recipe is in</param>
        /// <param name="fileName">The name of the recipe file</param>
        /// <param name="writeSetting">The settings for how to write this file</param>
        /// <param name="group">The name of the recipe group the recipe is in. Leave null for no group.</param>
        /// <param name="recipeType">The type of smelting recipe</param>
        /// <param name="ingredients">The different types of items which can be used in the recipe</param>
        /// <param name="result">The result from the recipe</param>
        /// <param name="experience">The amount of experience to get for smelting the item</param>
        /// <param name="cookingTime">The amount of time it takes to cook the item</param>
        /// <param name="_">Unused parameter used for specifing you want to use this constructor</param>
        protected SmeltRecipe(bool _, BasePackNamespace packNamespace, string? fileName, SmeltType recipeType, ItemType[] ingredients, ID.Item result, double experience, NoneNegativeTime<int>? cookingTime = null, string? group = null, WriteSetting writeSetting = WriteSetting.LockedAuto) : base(packNamespace, fileName, group, writeSetting, recipeType.ToString())
        {
            Ingredients = ingredients;
            Result = result;
            Experience = experience;
            CookingTime = cookingTime;
        }

        /// <summary>
        /// Intializes a new <see cref="SmeltRecipe"/>
        /// </summary>
        /// <param name="packNamespace">The namespace the recipe is in</param>
        /// <param name="fileName">The name of the recipe file</param>
        /// <param name="writeSetting">The settings for how to write this file</param>
        /// <param name="group">The name of the recipe group the recipe is in. Leave null for no group.</param>
        /// <param name="recipeType">The type of smelting recipe</param>
        /// <param name="ingredients">The different types of items which can be used in the recipe</param>
        /// <param name="result">The result from the recipe</param>
        /// <param name="experience">The amount of experience to get for smelting the item</param>
        /// <param name="cookingTime">The amount of time it takes to cook the item</param>
        public SmeltRecipe(BasePackNamespace packNamespace, string? fileName, SmeltType recipeType, ItemType[] ingredients, ID.Item result, double experience, NoneNegativeTime<int>? cookingTime = null, string? group = null, WriteSetting writeSetting = WriteSetting.LockedAuto) : this(true, packNamespace, fileName, recipeType, ingredients, result, experience, cookingTime, group, writeSetting)
        {
            FinishedConstructing();
        }

        /// <summary>
        /// Intializes a new <see cref="SmeltRecipe"/>
        /// </summary>
        /// <param name="packNamespace">The namespace the recipe is in</param>
        /// <param name="fileName">The name of the recipe file</param>
        /// <param name="writeSetting">The settings for how to write this file</param>
        /// <param name="group">The name of the recipe group the recipe is in. Leave null for no group.</param>
        /// <param name="recipeType">The type of smelting recipe</param>
        /// <param name="ingredient">The item to smelt</param>
        /// <param name="result">The result from the recipe</param>
        /// <param name="experience">The amount of experience to get for smelting the item</param>
        /// <param name="cookingTime">The amount of time in ticks it takes to cook the item</param>
        public SmeltRecipe(BasePackNamespace packNamespace, string? fileName, SmeltType recipeType, ItemType ingredient, ID.Item result, double experience, NoneNegativeTime<int>? cookingTime = null, string? group = null, WriteSetting writeSetting = WriteSetting.LockedAuto) : this(true, packNamespace, fileName, recipeType, new ItemType[] { ingredient }, result, experience, cookingTime, group, writeSetting)
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
                    throw new ArgumentOutOfRangeException(nameof(Ingredients), "Ingredient count has to be atleast 1");
                }
                if (value.Length > 9)
                {
                    throw new ArgumentOutOfRangeException(nameof(Ingredients), "Ingredient count has to be less than or equal to 9");
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
        /// The amount of experience to get for smelting the item
        /// </summary>
        public double Experience
        {
            get => experience;
            set 
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(Experience), "Experience may not be less than 0");
                }
                experience = value;
            }
        }

        /// <summary>
        /// The amount of time in ticks it takes to cook the item
        /// </summary>
        public NoneNegativeTime<int>? CookingTime
        {
            get => cookingTime;
            set 
            { 
                cookingTime = value; 
            }
        }

        /// <summary>
        /// Writes the file
        /// </summary>
        /// <param name="stream">The stream used for writing the file</param>
        protected override void WriteFile(TextWriter stream)
        {
            WriteFileStart(stream);

            stream.Write(",\"ingredient\":");
            if (Ingredients.Length == 1)
            {
                stream.Write(GetItemCompound(Ingredients[0]) + "");
            }
            else
            {
                stream.Write("[" + string.Join(",", Ingredients.Select(i => GetItemCompound(i))) + "]");
            }

            stream.Write(",\"result\":\"minecraft:" + Result.MinecraftValue() + "\"");
            stream.Write(",\"experience\":" + Experience.ToMinecraftDouble());
            if (!(CookingTime is null))
            {
                stream.Write(",\"cookingtime\":" + CookingTime.GetAsTicks());
            }

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
