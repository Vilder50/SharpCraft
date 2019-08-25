using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using SharpCraft.Data;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for furnace blocks.
        /// </summary>
        public class Furnace : BaseInventory, IBlock.IFacing, IBlock.ILit
        {
            private Item[] _dItems;

            /// <summary>
            /// Creates a new furnace block
            /// </summary>
            /// <param name="type">The type of block</param>
            public Furnace(ID.Block? type = SharpCraft.ID.Block.furnace) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public Furnace(Group group) : base(group) { }

            /// <summary>
            /// Tests if the given block type fits this type of block object
            /// </summary>
            /// <param name="block">The block to test</param>
            /// <returns>true if the block fits</returns>
            public new static bool FitsBlock(ID.Block block)
            {
                return block == SharpCraft.ID.Block.furnace || block == SharpCraft.ID.Block.blast_furnace || block == SharpCraft.ID.Block.smoker;
            }

            /// <summary>
            /// The item's inside the brewing stand.
            /// 0 = Smelting item slot. 1 = Fuel slot. 2 = Result slot.
            /// </summary>
            public override Item[] DItems
            {
                get => _dItems;
                set
                {
                    if (DItems != null && DItems.Length > 3)
                    {
                        throw new ArgumentException("Too many slots specified");
                    }
                    _dItems = value;
                }
            }

            /// <summary>
            /// The direction the furnace is facing.
            /// </summary>
            [BlockState("facing")]
            public ID.Facing? SFacing { get; set; }
            /// <summary>
            /// If the furnace is lit or not
            /// </summary>
            [BlockState("lit")]
            public bool? SLit { get; set; }

            /// <summary>
            /// The amount of time till the used fuel item runs out
            /// </summary>
            [DataTag("BurnTime", ForceType = SharpCraft.ID.NBTTagType.TagShort)]
            public Time DBurnTime { get; set; }

            /// <summary>
            /// The amount of time the item has been smelting for
            /// </summary>
            [DataTag("CookTime", ForceType = SharpCraft.ID.NBTTagType.TagShort)]
            public Time DCookTime { get; set; }

            /// <summary>
            /// The amount of time it will take for the item to smelt.
            /// </summary>
            [DataTag("CookTimeTotal", ForceType = SharpCraft.ID.NBTTagType.TagShort)]
            public Time DCookTimeTotal { get; set; }

            /// <summary>
            /// The number of used recipes in this furnace
            /// </summary>
            [DataTag("RecipesUsedSize")]
            public short? DRecipesUsed { get; set; }

            /// <summary>
            /// A list of all recipes the furnace has smelted.
            /// Each recipes' number shows how many of the recipe there has been smelted since the player last took out the xp.
            /// </summary>
            [DataTag(Merge = true)]
            public SmeltedRecipes DSmeltedRecipes {get; set;}

            /// <summary>
            /// Holds a list of smelted recipes
            /// </summary>
            public class SmeltedRecipes : IConvertableToDataObject
            {
                /// <summary>
                /// Intializes a new <see cref="SmeltedRecipes"/> with the given recipes
                /// </summary>
                /// <param name="recipes">The smelted recipes</param>
                public SmeltedRecipes(SmeltedRecipe[] recipes)
                {
                    if (recipes is null)
                    {
                        throw new ArgumentNullException(nameof(recipes), "Recipes may not be null");
                    }
                    Recipes = recipes;
                }

                /// <summary>
                /// The smelted recipes
                /// </summary>
                public SmeltedRecipe[] Recipes { get; private set; }

                /// <summary>
                /// Converts this <see cref="SmeltedRecipes"/> object into a <see cref="DataPartObject"/>
                /// </summary>
                /// <param name="conversionData">Not in use</param>
                /// <returns>This object as a <see cref="DataPartObject"/></returns>
                public DataPartObject GetAsDataObject(object[] conversionData)
                {
                    DataPartObject returnObject = new DataPartObject();
                    foreach(SmeltedRecipe recipe in Recipes)
                    {
                        if (!(recipe is null))
                        {
                            if (!(recipe.Recipe is null))
                            {
                                returnObject.AddValue(new DataPartPath("RecipeLocation" + recipe.ID, new DataPartTag(recipe.Recipe.ToString())));
                            }
                            if (!(recipe.TimesSmelted is null))
                            {
                                returnObject.AddValue(new DataPartPath("RecipeAmount" + recipe.ID, new DataPartTag((int)recipe.TimesSmelted)));
                            }
                        }
                    }

                    return returnObject;
                }
            }

            /// <summary>
            /// An object used to define how many times a recipe has been smelted
            /// </summary>
            public class SmeltedRecipe
            {
                /// <summary>
                /// An object used to define how many times a recipe has been smelted
                /// </summary>
                /// <param name="id">The recipe's unique ID</param>
                /// <param name="recipe">the recipe</param>
                /// <param name="timesSmelted">the amount of times the recipe has been smelted</param>
                public SmeltedRecipe(int id, Recipe recipe, int? timesSmelted)
                {
                    Recipe = recipe;
                    TimesSmelted = timesSmelted;
                    ID = id;
                }

                /// <summary>
                /// The recipe
                /// </summary>
                public Recipe Recipe { get; set; }
                /// <summary>
                /// The amount of times the recipe has been smelted
                /// </summary>
                public int? TimesSmelted { get; set; }

                /// <summary>
                /// The recipe's unique ID
                /// </summary>
                public int ID { get; set; }
            }
        }
    }
}
