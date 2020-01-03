﻿using System.Collections.Generic;
using SharpCraft.Data;
using System;
using SharpCraft.AdvancementObjects;

namespace SharpCraft
{
    /// <summary>
    /// <see cref="object"/>s used in <see cref="LootTable"/>s and <see cref="IAdvancement"/>s
    /// </summary>
    public static partial class JSONObjects
    {
        /// <summary>
        /// a <see cref="object"/> defining an <see cref="SharpCraft.Entity"/>
        /// </summary>
        public class Entity : DataHolderBase
        {
            /// <summary>
            /// The <see cref="SharpCraft.Entity"/> type
            /// </summary>
            [DataTag("type", JsonTag = true, ForceType = ID.NBTTagType.TagString)]
            public EntityType Type { get; set; }

            /// <summary>
            /// the <see cref="Distance"/> to the <see cref="SharpCraft.Entity"/>
            /// </summary>
            [DataTag("distance", JsonTag = true)]
            public Distance Distance { get; set; }

            /// <summary>
            /// the <see cref="Location"/> of the <see cref="SharpCraft.Entity"/>
            /// </summary>
            [DataTag("location", JsonTag = true)]
            public Location Location { get; set; }

            /// <summary>
            /// the <see cref="JSONObjects.Effects"/>s the <see cref="SharpCraft.Entity"/> should have
            /// </summary>
            [DataTag("effects", JsonTag = true)]
            public Effects Effects { get; set; }

            /// <summary>
            /// the nbt the <see cref="SharpCraft.Entity"/> should have
            /// </summary>
            [DataTag("nbt", JsonTag = true, ForceType = ID.NBTTagType.TagString)]
            public SimpleDataHolder NBT { get; set; }

            /// <summary>
            /// the amount of levels the player has
            /// </summary>
            [DataTag("player.level", "min", "max", ID.NBTTagType.TagInt, true, JsonTag = true)]
            public Range Levels { get; set; }

            /// <summary>
            /// The gamemode the player is in
            /// </summary>
            [DataTag("player.gamemode", JsonTag = true, ForceType = ID.NBTTagType.TagString)]
            public ID.Gamemode Gamemode { get; set; }

            /// <summary>
            /// Player recipes to check for
            /// </summary>
            [DataTag("player.recipes", JsonTag = true, ForceType = ID.NBTTagType.TagCompound)]
            public RecipeList Recipes { get; set; }

            /// <summary>
            /// Player advancements to check for
            /// </summary>
            [DataTag("player.advancements", JsonTag = true, ForceType = ID.NBTTagType.TagCompound)]
            public AdvancementList Advancements { get; set; }

            /// <summary>
            /// The entity's team
            /// </summary>
            [DataTag(JsonTag = true, ForceType = ID.NBTTagType.TagString)]
            public Team Team { get; set; }

            /// <summary>
            /// A list of <see cref="CheckRecipe"/>
            /// </summary>
            public class RecipeList : IConvertableToDataObject
            {
                private CheckRecipe[] recipes;

                /// <summary>
                /// The recipes
                /// </summary>
                public CheckRecipe[] Recipes { get => recipes; set => recipes = value ?? throw new ArgumentNullException(nameof(Recipes), "Recipes may not be null"); }

                /// <summary>
                /// Converts this object into a <see cref="DataPartObject"/>
                /// </summary>
                /// <param name="conversionData">Unused</param>
                /// <returns>This object as a <see cref="DataPartObject"/></returns>
                public DataPartObject GetAsDataObject(object[] conversionData)
                {
                    DataPartObject returnObject = new DataPartObject();

                    foreach(CheckRecipe recipe in Recipes)
                    {
                        returnObject.AddValue(new DataPartPath(recipe.Recipe.GetNamespacedName(), new DataPartTag(recipe.IsUnlocked, isJson: true), true));
                    }

                    return returnObject;
                }

                /// <summary>
                /// A recipe to check for
                /// </summary>
                public class CheckRecipe
                {
                    private IRecipe recipe;

                    /// <summary>
                    /// Intializes a new <see cref="CheckRecipe"/>
                    /// </summary>
                    /// <param name="recipe">The recipe to check for</param>
                    /// <param name="isUnlocked">True if the recipe should be unlocked</param>
                    public CheckRecipe(IRecipe recipe, bool isUnlocked = true)
                    {
                        Recipe = recipe;
                        IsUnlocked = isUnlocked;
                    }

                    /// <summary>
                    /// The recipe to check for
                    /// </summary>
                    public IRecipe Recipe { get => recipe; set => recipe = value ?? throw new ArgumentNullException(nameof(Recipe), "Recipe may not be null"); }

                    /// <summary>
                    /// True if the recipe should be unlocked
                    /// </summary>
                    public bool IsUnlocked { get; set; }
                }
            }

            /// <summary>
            /// A list of <see cref="BaseCheckAdvancement"/>
            /// </summary>
            public class AdvancementList : IConvertableToDataObject
            {
                private BaseCheckAdvancement[] advancements;

                /// <summary>
                /// The advancements
                /// </summary>
                public BaseCheckAdvancement[] Advancements { get => advancements; set => advancements = value ?? throw new ArgumentNullException(nameof(Advancements),"Advancements may not be null"); }

                /// <summary>
                /// Converts this object into a <see cref="DataPartObject"/>
                /// </summary>
                /// <param name="conversionData">Unused</param>
                /// <returns>This object as a <see cref="DataPartObject"/></returns>
                public DataPartObject GetAsDataObject(object[] conversionData)
                {
                    DataPartObject returnObject = new DataPartObject();

                    foreach (BaseCheckAdvancement advancement in Advancements)
                    {
                        returnObject.AddValue(new DataPartPath(advancement.Advancement.GetNamespacedName(), new DataPartTag(advancement.GetValue(), isJson: true), true));
                    }

                    return returnObject;
                }

                /// <summary>
                /// Interface for advancement checks
                /// </summary>
                public abstract class BaseCheckAdvancement
                {
                    private IAdvancement advancement;

                    /// <summary>
                    /// The advancement to check for
                    /// </summary>
                    public IAdvancement Advancement { get => advancement; set => advancement = value ?? throw new ArgumentNullException(nameof(Advancement), "Advancement may not be null"); }

                    /// <summary>
                    /// The value to check for
                    /// </summary>
                    /// <returns>The value to check for</returns>
                    public abstract object GetValue();
                }

                /// <summary>
                /// Checks if the player has the given advancement criteria
                /// </summary>
                public class CheckAdvancementCriteria : BaseCheckAdvancement
                {
                    private BaseTrigger criteria;

                    /// <summary>
                    /// Intializes a new <see cref="CheckAdvancementCriteria"/>
                    /// </summary>
                    /// <param name="advancement">The advancement to check for</param>
                    /// <param name="criteria">The criteria to check for</param>
                    public CheckAdvancementCriteria(IAdvancement advancement, BaseTrigger criteria)
                    {
                        Advancement = advancement;
                        Criteria = criteria;
                    }

                    /// <summary>
                    /// The criteria to check for
                    /// </summary>
                    public BaseTrigger Criteria { get => criteria; set => criteria = value; }

                    /// <summary>
                    /// The value to check for
                    /// </summary>
                    /// <returns>The value to check for</returns>
                    public override object GetValue()
                    {
                        return Criteria.Name;
                    }
                }

                /// <summary>
                /// Checks if the player has the advancement
                /// </summary>
                public class CheckAdvancement : BaseCheckAdvancement
                {
                    /// <summary>
                    /// Intializes a new <see cref="CheckAdvancement"/>
                    /// </summary>
                    /// <param name="advancement">The advancement to check for</param>
                    /// <param name="isCompleted">True if the advancement is completed</param>
                    public CheckAdvancement(IAdvancement advancement, bool isCompleted)
                    {
                        Advancement = advancement;
                        IsCompleted = isCompleted;
                    }

                    /// <summary>
                    /// True if the advancement is completed
                    /// </summary>
                    public bool IsCompleted { get; set; }

                    /// <summary>
                    /// The value to check for
                    /// </summary>
                    /// <returns>The value to check for</returns>
                    public override object GetValue()
                    {
                        return IsCompleted;
                    }
                }
            }

            /// <summary>
            /// Implicit converts <see cref="EntityType"/> into an <see cref="Entity"/> object
            /// </summary>
            /// <param name="type">the <see cref="EntityType"/> to convert</param>
            public static implicit operator Entity(EntityType type)
            {
                return new Entity() { Type = type };
            }

            /// <summary>
            /// Implicit converts <see cref="ID.Entity"/> into an <see cref="Entity"/> object
            /// </summary>
            /// <param name="type">the <see cref="ID.Entity"/> to convert</param>
            public static implicit operator Entity(ID.Entity type)
            {
                return new Entity() { Type = type };
            }

            /// <summary>
            /// Converts a <see cref="SharpCraft.Entity.BaseEntity"/> into an <see cref="Entity"/> object
            /// </summary>
            /// <param name="entity">The <see cref="SharpCraft.Entity.BaseEntity"/> to convert</param>
            public static implicit operator Entity(SharpCraft.Entity.BaseEntity entity)
            {
                return new Entity() { Type = entity.EntityType, NBT = entity };
            }
        }
    }
}
