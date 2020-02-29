using System.Collections.Generic;
using SharpCraft.Data;

namespace SharpCraft.Entities
{
    /// <summary>
    /// Entity data for Bees
    /// </summary>
    public class Bee : BreedableMob
    {
        /// <summary>
        /// Creates a new bee
        /// </summary>
        /// <param name="type">the type of entity</param>
        public Bee(ID.Entity? type = ID.Entity.bee) : base(type) { }

        /// <summary>
        /// The location of the bee's hive
        /// </summary>
        [DataTag("HivePos", "X", "Y", "Z")]
        public IntVector? HivePosition { get; set; }

        /// <summary>
        /// The location of the flower the bee should fly around
        /// </summary>
        [DataTag("FlowerPos", "X", "Y", "Z")]
        public IntVector? FlowerPosition { get; set; }

        /// <summary>
        /// If the bee has pollen
        /// </summary>
        [DataTag]
        public bool? HasNectar { get; set; }

        /// <summary>
        /// If the bee has stung
        /// </summary>
        [DataTag]
        public bool? HasStung { get; set; }

        /// <summary>
        /// Time since the bee last pollinated
        /// </summary>
        [DataTag("TicksSincePollination", ForceType = ID.NBTTagType.TagInt)]
        public Time? LastPollination { get; set; }

        /// <summary>
        /// Time till the bee is allowed to enter a hive
        /// </summary>
        [DataTag("CannotEnterHiveTicks", ForceType = ID.NBTTagType.TagInt)]
        public Time? ToEnterHive { get; set; }

        /// <summary>
        /// The amount of crops the bee has grown since it last pollinated
        /// </summary>
        [DataTag]
        public int? CropsGrownSincePollination { get; set; }

        /// <summary>
        /// The amount of time the bee is angry for
        /// </summary>
        [DataTag(ForceType = ID.NBTTagType.TagInt)]
        public Time? Anger { get; set; }

        /// <summary>
        /// The <see cref="UUID"/> of the entity this bee is angry on
        /// </summary>
        [DataTag("HurtBy", ForceType = ID.NBTTagType.TagString)]
        public UUID? AngryOn { get; set; }
    }
}
