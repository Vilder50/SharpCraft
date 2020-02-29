using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// The basic entity data for mobs
        /// </summary>
        public abstract class BaseMob : EntityBasic
        {
            /// <summary>
            /// Creates a new entity
            /// </summary>
            /// <param name="type">the type of entity</param>
            public BaseMob(ID.Entity? type) : base(type) { }

            /// <summary>
            /// The amount of health the mob has
            /// </summary>
            [Data.DataTag]
            public float? Health { get; set; }
            /// <summary>
            /// The amount of extra health the mob has
            /// </summary>
            [Data.DataTag]
            public double? AbsorptionAmount { get; set; }
            /// <summary>
            /// Makes the entity turn red for the given time
            /// </summary>
            [Data.DataTag(ForceType = ID.NBTTagType.TagShort)]
            public Time? HurtTime { get; set; }
            /// <summary>
            /// The time since the mob last was hit
            /// </summary>
            [Data.DataTag(ForceType = ID.NBTTagType.TagInt)]
            public Time? HurtByTimestamp { get; set; }
            /// <summary>
            /// The time the mob has been dead for.
            /// (0 = alive)
            /// </summary>
            [Data.DataTag(ForceType = ID.NBTTagType.TagShort)]
            public Time? DeathTime { get; set; }
            /// <summary>
            /// Makes the mob fly when falling if it has an elytra on.
            /// </summary>
            [Data.DataTag]
            public bool? FallFlying { get; set; }
            /// <summary>
            /// The loot table the mob drops on death
            /// </summary>
            [Data.DataTag(ForceType = ID.NBTTagType.TagString)]
            public LootTable? DeathLootTable { get; set; }
            /// <summary>
            /// The seed to use when dropping the <see cref="DeathLootTable"/>
            /// </summary>
            [Data.DataTag]
            public long? DeathLootTableSeed { get; set; }
            /// <summary>
            /// If the mob can pick up armor and items from the ground
            /// </summary>
            [Data.DataTag]
            public bool? CanPickUpLoot { get; set; }
            /// <summary>
            /// If the mob doesn't have an AI
            /// </summary>
            [Data.DataTag]
            public bool? NoAI { get; set; }
            /// <summary>
            /// If the mob shouldn't despawn
            /// </summary>
            [Data.DataTag]
            public bool? PersistenceRequired { get; set; }
            /// <summary>
            /// If the mob's main hand is its left hand
            /// </summary>
            [Data.DataTag]
            public bool? LeftHanded { get; set; }
            /// <summary>
            /// The team the mob is on
            /// </summary>
            [Data.DataTag(ForceType = ID.NBTTagType.TagString)]
            public Team? Team { get; set; }
            /// <summary>
            /// The location the mob is leashed to
            /// </summary>
            [Data.DataTag("Leash","X","Y","Z")]
            public IntVector? LeashCoords { get; set; }
            /// <summary>
            /// The <see cref="UUID"/> of the leash
            /// </summary>
            [Data.DataTag("Least","UUIDMost", "UUIDLeast")]
            public UUID? LeashUUID { get; set; }
            /// <summary>
            /// The items there is in the mob's hands.
            /// 0: main hand. 1: off hand.
            /// </summary>
            [Data.DataTag]
            public Item?[]? HandItems { get; set; }
            /// <summary>
            /// The items the mob has on
            /// 0: boots. 1: leggings. 2: chestplate. 3: helmet
            /// </summary>
            [Data.DataTag]
            public Item?[]? ArmorItems { get; set; }
            /// <summary>
            /// The chance that the mob will drop its hand items when killed (number between 0-1)
            /// 0: main hand. 1: off hand
            /// </summary>
            [Data.DataTag]
            public float?[]? HandDropChances { get; set; }
            /// <summary>
            /// The chance that the mob will drop its armor items when killed (number between 0-1)
            /// 0: boots. 1: leggings. 2: chestplate. 3: helmet
            /// </summary>
            [Data.DataTag]
            public float?[]? ArmorDropChances { get; set; }
            /// <summary>
            /// The <see cref="Effect"/>s the mob has
            /// </summary>
            [Data.DataTag]
            public Effect?[]? ActiveEffects { get; set; }
            /// <summary>
            /// The attributes the mob has
            /// </summary>
            [Data.DataTag]
            public EntityAttribute?[]? Attributes { get; set; }
        }

        /// <summary>
        /// Entity data for mobs
        /// </summary>
        public class Mob : BaseMob
        {
            /// <summary>
            /// Creates a new mob
            /// </summary>
            /// <param name="type">the type of entity</param>
            public Mob(ID.Entity? type) : base(type) { }
        }
    }
}
