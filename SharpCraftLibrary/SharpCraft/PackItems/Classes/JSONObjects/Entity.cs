using System.Collections.Generic;
using SharpCraft.Data;

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
