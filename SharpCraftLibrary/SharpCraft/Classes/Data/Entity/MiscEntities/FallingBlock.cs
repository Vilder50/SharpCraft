using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// An object for falling block entities
    /// </summary>
    public class FallingBlock : BasicEntity
    {
        /// <summary>
        /// Returns a object which can be used for creating data paths
        /// </summary>
        /// <returns>Object used for making data paths</returns>
        public new static Data.DataPathCreator<FallingBlock> PathCreator => new Data.DataPathCreator<FallingBlock>();

        /// <summary>
        /// Creates a new falling block
        /// </summary>
        /// <param name="type">the type of entity</param>
        public FallingBlock(ID.Entity? type) : base(type) { }

        /// <summary>
        /// Creates a new entity
        /// </summary>
        public FallingBlock() : base(SharpCraft.ID.Entity.falling_block) { }

        /// <summary>
        /// The time the entity has been falling.
        /// If this number is 0 the entity will dissapear the next tick
        /// </summary>
        [Data.DataTag]
        public Time<int>? Time { get; set; }

        /// <summary>
        /// The falling block
        /// </summary>
        [Data.DataTag("BlockState", "Name", "Properties")]
        public Block? TheBlock { get; set; }

        /// <summary>
        /// If the block should be dropped if the falling block is destroyed
        /// </summary>
        [Data.DataTag]
        public bool? DropItem { get; set; }

        /// <summary>
        /// If the block should damage entities it lands on
        /// </summary>
        [Data.DataTag]
        public bool? HurtEntities { get; set; }

        /// <summary>
        /// The maximum amount of damage it can cause
        /// </summary>
        [Data.DataTag("FallHurtMax")]
        public int? MaxDamage { get; set; }

        /// <summary>
        /// The amount of damage it should cause per block fallen
        /// </summary>
        [Data.DataTag("FallHurtAmount")]
        public float? DamageAmount { get; set; }
    }
}