using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// Entity data for dolphins
    /// </summary>
    public class Dolphin : Mob
    {
        /// <summary>
        /// Creates a new dolphin
        /// </summary>
        /// <param name="type">the type of entity</param>
        public Dolphin(ID.Entity? type) : base(type) { }

        /// <summary>
        /// Creates a new entity
        /// </summary>
        public Dolphin() : base(SharpCraft.ID.Entity.dolphin) { }

        /// <summary>
        /// The location of the treasure the dolphin will lead the player to
        /// </summary>
        [Data.DataTag((object)"TreasurePosX", "TreasurePosY", "TreasurePosZ", Merge = true)]
        public IntVector? TreasureLocation { get; set; }

        /// <summary>
        /// If the dolphin has gotten any fish from the player
        /// </summary>
        [Data.DataTag]
        public bool? GotFish { get; set; }

        /// <summary>
        /// If the dolphin will lead the player to a treasure
        /// </summary>
        [Data.DataTag]
        public bool? CanFindTreasure { get; set; }
    }
}
