using System;
using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// Entity data for players
        /// </summary>
        public class Player : BaseMob
        {
            /// <summary>
            /// Creates a new player
            /// </summary>
            /// <param name="type">the type of entity</param>
            public Player(ID.Entity? type) : base(type) { }

            /// <summary>
            /// The ID of the Minecraft version the player is in
            /// </summary>
            [DataTag]
            public int? Version { get; set; }
            /// <summary>
            /// The player's gamemode
            /// </summary>
            [DataTag]
            public ID.Gamemode? Gamemode { get; set; }
            /// <summary>
            /// The score displayed on death
            /// </summary>
            [DataTag]
            public int? DeathScore { get; set; }
            /// <summary>
            /// The slot the player has selected
            /// (0-8)
            /// </summary>
            [DataTag]
            public int? SelectedSlot { get; set; }
            /// <summary>
            /// The item the player has selected
            /// </summary>
            [DataTag]
            public SharpCraft.Item SelectedItem { get; set; }
            /// <summary>
            /// The player's spawnpoint
            /// </summary>
            [DataTag]
            public Coords Spawn { get; set; }
            /// <summary>
            /// If the player should spawn at the given <see cref="Spawn"/> even if there is no bed
            /// </summary>
            [DataTag]
            public bool? ForceSpawn { get; set; }
            /// <summary>
            /// If the player is sleeping
            /// </summary>
            [DataTag]
            public bool? Sleeping { get; set; }
            /// <summary>
            /// The amount of time the player has been sleeping
            /// </summary>
            [DataTag]
            public Time SleepTime { get; set; }
            /// <summary>
            /// How much food the player has
            /// (0-20)
            /// </summary>
            [DataTag]
            public int? Food { get; set; }
            /// <summary>
            /// How close a food bar is from dissapearing
            /// (4=lose one bar)
            /// </summary>
            [DataTag]
            public double? FoodExhaustion { get; set; }
            /// <summary>
            /// How much saturation the player has
            /// </summary>
            [DataTag]
            public double? FoodSaturation { get; set; }
            /// <summary>
            /// When this hits 80 ticks and the player has enough food, they will be healed
            /// </summary>
            [DataTag]
            public Time FoodTimer { get; set; }
            /// <summary>
            /// The level the player has
            /// </summary>
            [DataTag]
            public int? Level { get; set; }
            /// <summary>
            /// How far the player is to hit next level
            /// </summary>
            [DataTag]
            public double? XPPogress { get; set; }
            /// <summary>
            /// The total amount of xp the player has picked up since last death
            /// </summary>
            [DataTag]
            public int? Totalxp { get; set; }
            /// <summary>
            /// The seed used to determine which enchantments should show up for the player
            /// </summary>
            [DataTag]
            public int? EnchantSeed { get; set; }
            /// <summary>
            /// The player's inventory.
            /// (Slot 0-8 = hotbar left to right)
            /// (Slot 9-35 = inventory left top to right bottom)
            /// (Slot 100 = boots, 101 = leggings, 102 = chestplate, 103 = helmet)
            /// (Slot -106 = off hand)
            /// </summary>
            [DataTag]
            public Item[] Inventory { get; set; }
            /// <summary>
            /// The items in the player's enderchest
            /// (Slot 0-26 = left top to right bottom slots)
            /// </summary>
            [DataTag]
            public Item[] Enderchest { get; set; }
            /// <summary>
            /// The entity the player is riding
            /// </summary>
            [DataTag]
            public BaseEntity Riding { get; set; }
            /// <summary>
            /// The entity on the player's left shoulder
            /// </summary>
            [DataTag]
            public BaseEntity ShoulderEntityLeft { get; set; }
            /// <summary>
            /// The entity on the player's right shoulder
            /// </summary>
            [DataTag]
            public BaseEntity ShoulderEntityRight { get; set; }
            /// <summary>
            /// True if the player has seen the end to overworld credits
            /// </summary>
            [DataTag]
            public bool? SeenCredits { get; set; }
            /// <summary>
            /// True if the player only sees the recipes they have unlocked
            /// </summary>
            [DataTag]
            public bool? RecipeBookFiltered { get; set; }
            /// <summary>
            /// True if the player has the book open
            /// </summary>
            [DataTag]
            public bool? RecipeBookOpen { get; set; }
            /// <summary>
            /// A list of recipes the player has unlocked
            /// </summary>
            [DataTag]
            public Recipe[] UnlockedRecipes { get; set; }
            /// <summary>
            /// A list of recipes the player has unlocked, but still haven't seen in the crafting table.
            /// </summary>
            [DataTag]
            public Recipe[] NotSeenRecipes { get; set; }

            /// <summary>
            /// Gets the raw data from this entity
            /// </summary>
            public override string DataString
            {
                get
                {
                    List<string> TempList = new List<string>();

                    string NormalData = MobDataString;
                    if (NormalData.Length != 0) { TempList.Add(NormalData); }
                    if (Version != null) { TempList.Add("DataVersion:" + Version); }
                    if (Gamemode != null) { TempList.Add("playerGameType:" + (int)Gamemode); }
                    if (DeathScore != null) { TempList.Add("Score:" + DeathScore); }
                    if (SelectedSlot != null) { TempList.Add("SelectedItemSlot:" + SelectedSlot); }
                    if (SelectedItem != null) { TempList.Add("SelectedItem:{" + SelectedItem.DataString + "}"); }
                    if (Spawn != null) { TempList.Add("SpawnX:" + Spawn.X + ",SpawnY:" + Spawn.Y + ",SpawnZ:" + Spawn.Z); }
                    if (ForceSpawn != null) { TempList.Add("SpawnForced:" + ForceSpawn); }
                    if (Sleeping != null) { TempList.Add("Sleeping:" + Sleeping); }
                    if (SleepTime != null) { TempList.Add("SleepTimer:" + SleepTime.AsTicks(Time.TimerType.Short) + "s"); }
                    if (Food != null) { TempList.Add("foodLevel:" + Food); }
                    if (FoodExhaustion != null) { TempList.Add("foodExhaustionLevel:" + FoodExhaustion.ToMinecraftDouble() + "f"); }
                    if (FoodSaturation != null) { TempList.Add("foodSaturationLevel:" + FoodSaturation.ToMinecraftDouble() + "f"); }
                    if (FoodTimer != null) { TempList.Add("foodTickTimer:" + FoodTimer.AsTicks()); }
                    if (Level != null) { TempList.Add("XpLevel:" + Level); }
                    if (XPPogress != null) { TempList.Add("XpP:" + XPPogress.ToMinecraftDouble() + "f"); }
                    if (Totalxp != null) { TempList.Add("XpTotal:" + Totalxp); }
                    if (EnchantSeed != null) { TempList.Add("XpSeed:" + EnchantSeed); }
                    if (Inventory != null)
                    {
                        List<string> TempItemList = new List<string>();
                        for (int i = 0; i < Inventory.Length; i++)
                        {
                            TempItemList.Add("{" + Inventory[i].DataString + "}");
                        }
                        TempList.Add("Inventory:[" + String.Join(",", TempItemList) + "]");
                    }
                    if (Enderchest != null)
                    {
                        List<string> TempItemList = new List<string>();
                        for (int i = 0; i < Enderchest.Length; i++)
                        {
                            TempItemList.Add("{" + Enderchest[i].DataString + "}");
                        }
                        TempList.Add("Inventory:[" + String.Join(",", TempItemList) + "]");
                    }
                    if (Riding != null) { TempList.Add("RootVehicle:{Entity:{" + Riding.DataWithID + "}}"); }
                    if (ShoulderEntityLeft != null) { TempList.Add("ShoulderEntityLeft:{" + ShoulderEntityLeft.DataWithID + "}"); }
                    if (ShoulderEntityRight != null) { TempList.Add("ShoulderEntityRight:{" + ShoulderEntityRight.DataWithID + "}"); }
                    if (SeenCredits != null) { TempList.Add("seenCredits:" + SeenCredits); }
                    if (RecipeBookFiltered != null || RecipeBookOpen != null || UnlockedRecipes != null || NotSeenRecipes != null)
                    {
                        List<string> TempRecipeList = new List<string>();
                        if (RecipeBookFiltered != null) { TempRecipeList.Add("isFilteringCraftable:" + RecipeBookFiltered); }
                        if (RecipeBookOpen != null) { TempRecipeList.Add("isGuiOpen:" + RecipeBookOpen); }

                        if (UnlockedRecipes != null) { TempRecipeList.Add("recipes:{" + string.Join(",", new List<Recipe>(UnlockedRecipes)) + "}"); }
                        if (NotSeenRecipes != null) { TempRecipeList.Add("toBeDisplayed:{" + string.Join(",", new List<Recipe>(UnlockedRecipes)) + "}"); }

                        TempList.Add("recipeBook:{" + string.Join(",", TempRecipeList) + "}");
                    }

                    return string.Join(",", TempList);
                }
            }
        }
    }
}
