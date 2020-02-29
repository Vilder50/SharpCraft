using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// An object for area effect cloud entities
    /// </summary>
    public class AreaCloud : BasicEntity
    {
        /// <summary>
        /// Creates a new area effect cloud
        /// </summary>
        /// <param name="type">the type of entity</param>
        public AreaCloud(ID.Entity? type = ID.Entity.area_effect_cloud) : base(type) { }

        /// <summary>
        /// The amount of time before the cloud disapears after the <see cref="WaitTime"/> is over
        /// </summary>
        [Data.DataTag(ForceType = ID.NBTTagType.TagInt)]
        public Time? Duration { get; set; }

        /// <summary>
        /// The color of the particles it displays
        /// </summary>
        [Data.DataTag(ForceType = ID.NBTTagType.TagInt)]
        public RGBColor? Color { get; set; }

        /// <summary>
        /// The amount of time the cloud has existed.
        /// </summary>
        [Data.DataTag(ForceType = ID.NBTTagType.TagInt)]
        public Time? Age { get; set; }

        /// <summary>
        /// The time before the cloud will show up.
        /// (Time before the <see cref="Radius"/> will be used)
        /// </summary>
        [Data.DataTag(ForceType = ID.NBTTagType.TagInt)]
        public Time? WaitTime { get; set; }

        /// <summary>
        /// The time before the cloud's effect will be given out to the entities inside again.
        /// </summary>
        [Data.DataTag(ForceType = ID.NBTTagType.TagInt)]
        public Time? ReapplicationDealy { get; set; }

        /// <summary>
        /// The UUID of the entity who made the cloud
        /// </summary>
        [Data.DataTag((object)"OwnerUUIDMost", "OwnerUUIDLeast")]
        public UUID? OwnerUUID { get; set; }

        /// <summary>
        /// The amount of time to remove from the duration every time the cloud gives out its effect
        /// </summary>
        [Data.DataTag(ForceType = ID.NBTTagType.TagInt)]
        public Time? DurationOnUse { get; set; }

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
                return Duration.AsTicks() == int.MaxValue;
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
