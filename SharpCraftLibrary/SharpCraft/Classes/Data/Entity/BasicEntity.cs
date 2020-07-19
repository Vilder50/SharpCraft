using System;
using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// The basic entity data
    /// </summary>
    public class BasicEntity : Entity
    {
        /// <summary>
        /// Returns a object which can be used for creating data paths
        /// </summary>
        /// <returns>Object used for making data paths</returns>
        public new static Data.DataPathCreator<BasicEntity> PathCreator => new Data.DataPathCreator<BasicEntity>();

        /// <summary>
        /// Creates a new entity
        /// </summary>
        /// <param name="type">the type of entity</param>
        public BasicEntity(ID.Entity? type) : base(type) { }

        /// <summary>
        /// The entity's motion
        /// </summary>
        [Data.DataTag]
        public Vector? Motion { get; set; }

        /// <summary>
        /// The entity's rotation
        /// </summary>
        [Data.DataTag(ForceType = ID.NBTTagType.TagFloatArray)]
        public Rotation? Rotation { get; set; }

        /// <summary>
        /// The entity's location
        /// </summary>
        [Data.DataTag("Pos", ForceType = ID.NBTTagType.TagDoubleArray)]
        public Vector? Coords { get; set; }

        /// <summary>
        /// The distance the entity has fallen
        /// </summary>
        [Data.DataTag]
        public float? FallDistance { get; set; }
        /// <summary>
        /// The team the entity is on
        /// </summary>
        [Data.DataTag(ForceType = ID.NBTTagType.TagString)]
        public Team? Team { get; set; }
        /// <summary>
        /// The time before the fire on the entity goes out.
        /// Negative value means how long it takes for the entity to turn on fire.
        /// </summary>
        [Data.DataTag]
        public Time<short>? Fire { get; set; }

        /// <summary>
        /// How much air the entity has left.
        /// (Being 0 under water will make the entity drown)
        /// </summary>
        [Data.DataTag]
        public Time<short>? Air { get; set; }

        /// <summary>
        /// If the entity is on the ground or not
        /// </summary>
        [Data.DataTag]
        public bool? OnGround { get; set; }

        /// <summary>
        /// If the entity shouldn't be effected by gravity
        /// </summary>
        [Data.DataTag]
        public bool? NoGravity { get; set; }
        /// <summary>
        /// The dimension the entity is in
        /// </summary>
        [Data.DataTag]
        public DimensionObjects.IDimension? Dimension { get; set; }

        /// <summary>
        /// If the entity is Invulnerable.
        /// (Can't be killed)
        /// </summary>
        [Data.DataTag]
        public bool? Invulnerable { get; set; }

        /// <summary>
        /// The amount of time before the entity can go through a portal again.
        /// </summary>
        [Data.DataTag]
        public Time<int>? PortalCooldown { get; set; }

        /// <summary>
        /// The entity's UUID
        /// </summary>
        [Data.DataTag(ForceType = ID.NBTTagType.TagIntArray)]
        public UUID? UUID { get; set; }

        /// <summary>
        /// The entity's shown name
        /// </summary>
        [Data.DataTag]
        public BaseJsonText? CustomName { get; set; }

        /// <summary>
        /// If the entity's name should be shown always
        /// </summary>
        [Data.DataTag]
        public bool? CustomNameVisible { get; set; }

        /// <summary>
        /// If the entity should be silent and not make any sounds
        /// </summary>
        [Data.DataTag]
        public bool? Silent { get; set; }

        /// <summary>
        /// The entities riding on the entity
        /// </summary>
        [Data.DataTag]
        public Entity[]? Passengers { get; set; }

        /// <summary>
        /// If the entity should glow
        /// </summary>
        [Data.DataTag]
        public bool? Glowing { get; set; }

        /// <summary>
        /// The entity's tags
        /// </summary>
        [Data.DataTag(ForceType = ID.NBTTagType.TagStringArray)]
        public Tag[]? Tags { get; set; }
    }
}
