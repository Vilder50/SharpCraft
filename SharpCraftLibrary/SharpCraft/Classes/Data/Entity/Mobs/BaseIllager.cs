using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// The basic entity data for illagers
    /// </summary>
    public abstract class BaseIllager : Mob
    {
        /// <summary>
        /// Creates a new entity
        /// </summary>
        /// <param name="type">the type of entity</param>
        public BaseIllager(ID.Entity? type) : base(type) { }

        /// <summary>
        /// If the illager has a raid goal
        /// </summary>
        [Data.DataTag("HadRaidGoal")]
        public bool? HasGoal { get; set; }

        /// <summary>
        /// If the illager is patrolling
        /// </summary>
        [Data.DataTag]
        public bool? Patrolling { get; set; }

        /// <summary>
        /// If the illager is the leader
        /// </summary>
        [Data.DataTag("PatrolLeader")]
        public bool? Leader { get; set; }

        /// <summary>
        /// The place the illager is patrolling to
        /// </summary>
        [Data.DataTag((object)"X", "Y", "Z")]
        public IntVector? PatrolTarget { get; set; }

        /// <summary>
        /// The id of the raid the illager is in
        /// </summary>
        [Data.DataTag]
        public int? RaidID { get; set; }

        /// <summary>
        /// the wave number the illager is in
        /// </summary>
        [Data.DataTag]
        public int? Wave { get; set; }
    }

    /// <summary>
    /// Entity data for illagers
    /// </summary>
    public class Illager : BaseIllager
    {
        /// <summary>
        /// Creates a new illager
        /// </summary>
        /// <param name="type">the type of entity</param>
        public Illager(ID.Entity? type = ID.Entity.pillager) : base(type) { }
    }
}
