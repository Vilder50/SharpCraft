using System;
using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// Entity data for players
    /// </summary>
    public class Player : Mob
    {
        /// <summary>
        /// Creates a new player
        /// </summary>
        /// <param name="type">the type of entity</param>
        public Player(ID.Entity? type) : base(type) { }

        /// <summary>
        /// The ID of the Minecraft version the player is in
        /// </summary>
        [Data.DataTag("DataVersion")]
        public int? Version { get; set; }

        /// <summary>
        /// The player's gamemode
        /// </summary>
        [Data.DataTag("playerGameType", ForceType = ID.NBTTagType.TagInt)]
        public ID.Gamemode? Gamemode { get; set; }

        /// <summary>
        /// The score displayed on death
        /// </summary>
        [Data.DataTag("Score")]
        public int? DeathScore { get; set; }

        /// <summary>
        /// The slot the player has selected
        /// (0-8)
        /// </summary>
        [Data.DataTag("SelectedItemSlot")]
        public int? SelectedSlot { get; set; }

        /// <summary>
        /// The item the player has selected
        /// </summary>
        [Data.DataTag]
        public Item? SelectedItem { get; set; }

        /// <summary>
        /// The player's spawnpoint
        /// </summary>
        [Data.DataTag((object)"SpawnX", "SpawnY", "SpawnZ", Merge = true)]
        public IntVector? Spawn { get; set; }

        /// <summary>
        /// If the player should spawn at the given <see cref="Spawn"/> even if there is no bed
        /// </summary>
        [Data.DataTag("SpawnForced")]
        public bool? ForceSpawn { get; set; }

        /// <summary>
        /// If the player is sleeping
        /// </summary>
        [Data.DataTag]
        public bool? Sleeping { get; set; }

        /// <summary>
        /// The amount of time the player has been sleeping
        /// </summary>
        [Data.DataTag("SleepTimer", ForceType = ID.NBTTagType.TagShort)]
        public Time? SleepTime { get; set; }

        /// <summary>
        /// How much food the player has
        /// (0-20)
        /// </summary>
        [Data.DataTag("foodLevel")]
        public int? Food { get; set; }

        /// <summary>
        /// How close a food bar is from dissapearing
        /// (4=lose one bar)
        /// </summary>
        [Data.DataTag("foodExhaustionLevel")]
        public float? FoodExhaustion { get; set; }

        /// <summary>
        /// How much saturation the player has
        /// </summary>
        [Data.DataTag("FoodSaturationLevel")]
        public float? FoodSaturation { get; set; }

        /// <summary>
        /// When this hits 80 ticks and the player has enough food, they will be healed
        /// </summary>
        [Data.DataTag("foodTickTimer", ForceType = ID.NBTTagType.TagInt)]
        public Time? FoodTimer { get; set; }

        /// <summary>
        /// The level the player has
        /// </summary>
        [Data.DataTag("XpLevel")]
        public int? Level { get; set; }

        /// <summary>
        /// How far the player is to hit next level
        /// </summary>
        [Data.DataTag("XpP")]
        public float? XPPogress { get; set; }

        /// <summary>
        /// The total amount of xp the player has picked up since last death
        /// </summary>
        [Data.DataTag("XpTotal")]
        public int? Totalxp { get; set; }

        /// <summary>
        /// The seed used to determine which enchantments should show up for the player
        /// </summary>
        [Data.DataTag("XpSeed")]
        public int? EnchantSeed { get; set; }

        /// <summary>
        /// The player's inventory.
        /// (Slot 0-8 = hotbar left to right)
        /// (Slot 9-35 = inventory left top to right bottom)
        /// (Slot 100 = boots, 101 = leggings, 102 = chestplate, 103 = helmet)
        /// (Slot -106 = off hand)
        /// </summary>
        [Data.DataTag]
        public Item[]? Inventory { get; set; }
        /// <summary>
        /// The items in the player's enderchest
        /// (Slot 0-26 = left top to right bottom slots)
        /// </summary>
        [Data.DataTag("EnderItems")]
        public Item[]? Enderchest { get; set; }
        /// <summary>
        /// The entity the player is riding
        /// </summary>
        [Data.DataTag("RootVehicle.Entity")]
        public Entity? Riding { get; set; }
        /// <summary>
        /// The entity on the player's left shoulder
        /// </summary>
        [Data.DataTag]
        public Entity? ShoulderEntityLeft { get; set; }
        /// <summary>
        /// The entity on the player's right shoulder
        /// </summary>
        [Data.DataTag]
        public Entity? ShoulderEntityRight { get; set; }
        /// <summary>
        /// True if the player has seen the end to overworld credits
        /// </summary>
        [Data.DataTag("seenCredits")]
        public bool? SeenCredits { get; set; }
        /// <summary>
        /// True if the player only sees the crafting recipes they have unlocked
        /// </summary>
        [Data.DataTag("recipeBook.isFilteringCraftable")]
        public bool? RecipeCraftingBookFiltered { get; set; }
        /// <summary>
        /// True if the player only sees the furnace recipes they have unlocked
        /// </summary>
        [Data.DataTag("recipeBook.isFurnaceFilteringCraftable")]
        public bool? RecipeSmeltBookFiltered { get; set; }
        /// <summary>
        /// True if the player has the crafting book open
        /// </summary>
        [Data.DataTag("recipeBook.isGuiOpen")]
        public bool? RecipeCraftingBookOpen { get; set; }
        /// <summary>
        /// True if the player has the crafting book open
        /// </summary>
        [Data.DataTag("recipeBook.isFurnaceGuiOpen")]
        public bool? RecipeSmeltBookOpen { get; set; }
        /// <summary>
        /// A list of recipes the player has unlocked
        /// </summary>
        [Data.DataTag("recipeBook.recipes")]
        public IRecipe[]? UnlockedRecipes { get; set; }
        /// <summary>
        /// A list of recipes the player has unlocked, but still haven't seen in the recipe book.
        /// </summary>
        [Data.DataTag("recipeBook.toBeDisplayed")]
        public IRecipe[]? NotSeenRecipes { get; set; }
    }
}
