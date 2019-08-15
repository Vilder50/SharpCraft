﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for furnace blocks.
        /// </summary>
        public class Furnace : BaseInventory<Furnace>, IBlock.IFacing, IBlock.ILit
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
            [BlockData("facing")]
            public ID.Facing? SFacing { get; set; }
            /// <summary>
            /// If the furnace is lit or not
            /// </summary>
            [BlockData("lit")]
            public bool? SLit { get; set; }

            /// <summary>
            /// The amount of time till the used fuel item runs out
            /// </summary>
            [BlockData]
            public Time DBurnTime { get; set; }

            /// <summary>
            /// The amount of time the item has been smelting for
            /// </summary>
            [BlockData]
            public Time DCookTime { get; set; }

            /// <summary>
            /// The amount of time it will take for the item to smelt.
            /// </summary>
            [BlockData]
            public Time DCookTimeTotal { get; set; }

            /// <summary>
            /// The ID of the last new recipe smelted in the furnace
            /// </summary>
            [BlockData]
            public int? DLastNewRecipeID { get; set; }

            /// <summary>
            /// A list of all recipes the furnace has smelted.
            /// Each recipes' number shows how many of the recipe there has been smelted since the player last took out the xp.
            /// </summary>
            [BlockData]
            public SmeltedRecipe[] DSmeltedRecipes {get; set;}

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

                /// <summary>
                /// Gets the raw data used by the game
                /// </summary>
                /// <returns>Raw dat used by the game</returns>
                public string GetRawData()
                {
                    if(Recipe == null && TimesSmelted == null)
                    {
                        throw new ArgumentException(nameof(Recipe) + " and " + nameof(TimesSmelted) + " cannot both be null.");
                    }
                    if (Recipe == null)
                    {
                        return "RecipeAmount" + ID + ":" + TimesSmelted;
                    }
                    if (TimesSmelted == null)
                    {
                        return "RecipeLocation" + ID + ":\"" + Recipe + "\"";
                    }
                    return "RecipeLocation" + ID + ":\"" + Recipe + "\"," + "RecipeAmount" + ID + ":" + TimesSmelted;
                }
            }

            /// <summary>
            /// Gets the raw data for the data the block contains
            /// </summary>
            /// <returns>Raw data used by Minecraft</returns>
            public override string GetDataString()
            {
                List<string> TempList = new List<string>();

                string InventoryData = base.GetDataString();
                if (InventoryData.Length != 0) { TempList.Add(InventoryData); }
                if (DBurnTime != null) { TempList.Add("BurnTime:" + DBurnTime.AsTicks(Time.TimerType.Short) + "s"); }
                if (DCookTime != null) { TempList.Add("CookTime:" + DCookTime.AsTicks(Time.TimerType.Short) + "s"); }
                if (DCookTimeTotal != null) { TempList.Add("CookTimeTotal:" + DCookTimeTotal.AsTicks(Time.TimerType.Short) + "s"); }
                if (DLastNewRecipeID != null) { TempList.Add("RecipeUsedSize:" + DLastNewRecipeID + "s"); }
                if (DSmeltedRecipes != null) { TempList.Add(string.Join(",",DSmeltedRecipes.ToList())); }

                return string.Join(",", TempList);
            }
        }
    }
}
