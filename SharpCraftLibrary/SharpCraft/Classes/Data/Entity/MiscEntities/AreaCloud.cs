﻿using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// An object for area effect cloud entities
    /// </summary>
    public class AreaCloud : BasicEntity
    {
        /// <summary>
        /// Returns a object which can be used for creating data paths
        /// </summary>
        /// <returns>Object used for making data paths</returns>
        public new static Data.DataPathCreator<AreaCloud> PathCreator => new Data.DataPathCreator<AreaCloud>();

        /// <summary>
        /// Creates a new area effect cloud
        /// </summary>
        /// <param name="type">the type of entity</param>
        public AreaCloud(ID.Entity? type) : base(type) { }

        /// <summary>
        /// Creates a new entity
        /// </summary>
        public AreaCloud() : base(SharpCraft.ID.Entity.area_effect_cloud) { }

        /// <summary>
        /// The amount of time before the cloud disapears after the <see cref="WaitTime"/> is over
        /// </summary>
        [Data.DataTag]
        public Time<int>? Duration { get; set; }

        /// <summary>
        /// The color of the particles it displays
        /// </summary>
        [Data.DataTag(ForceType = ID.NBTTagType.TagInt)]
        public RGBColor? Color { get; set; }

        /// <summary>
        /// The amount of time the cloud has existed.
        /// </summary>
        [Data.DataTag]
        public Time<int>? Age { get; set; }

        /// <summary>
        /// The time before the cloud will show up.
        /// (Time before the <see cref="Radius"/> will be used)
        /// </summary>
        [Data.DataTag]
        public Time<int>? WaitTime { get; set; }

        /// <summary>
        /// The time before the cloud's effect will be given out to the entities inside again.
        /// </summary>
        [Data.DataTag]
        public Time<int>? ReapplicationDealy { get; set; }

        /// <summary>
        /// The UUID of the entity who made the cloud
        /// </summary>
        [Data.DataTag("Owner",ForceType = ID.NBTTagType.TagIntArray)]
        public UUID? OwnerUUID { get; set; }

        /// <summary>
        /// The amount of time to remove from the duration every time the cloud gives out its effect
        /// </summary>
        [Data.DataTag]
        public Time<int>? DurationOnUse { get; set; }

        /// <summary>
        /// The radius of the cloud
        /// </summary>
        [Data.DataTag]
        public float? Radius { get; set; }

        /// <summary>
        /// The amount of radius to remove every time the cloud gives out its effect
        /// </summary>
        [Data.DataTag]
        public float? RadiusOnUse { get; set; }

        /// <summary>
        /// The amount of radius to remove each tick.
        /// </summary>
        [Data.DataTag]
        public float? RadiusPerTick { get; set; }

        /// <summary>
        /// The particle type the cloud displays
        /// </summary>
        [Data.DataTag(ForceType = ID.NBTTagType.TagString)]
        public ID.Particle? Particle { get; set; }

        /// <summary>
        /// The effect the cloud gives
        /// </summary>
        [Data.DataTag]
        public Effect?[]? Effects { get; set; }

        /// <summary>
        /// If the cloud shouldn't despawn
        /// (Sets <see cref="Duration"/> to its max value)
        /// </summary>
        public bool Unspawnable
        {
            get
            {
                if (Duration is null)
                {
                    return false;
                }
                return Duration.GetAsTicks() == int.MaxValue;
            }
            set
            {
                if (value)
                {
                    Duration = int.MaxValue;
                }
                else
                {
                    Duration = null;
                }
            }
        }
    }
}
