namespace SharpCraft
{
    /// <summary>
    /// An object used to define an entity's spawner potential
    /// </summary>
    public class SpawnerPotential : Data.DataHolderBase
    {
        /// <summary>
        /// Creates a new <see cref="SpawnerPotential"/>
        /// </summary>
        public SpawnerPotential()
        {

        }

        /// <summary>
        /// Creates a new <see cref="SpawnerPotential"/> for the entity with the given weight
        /// </summary>
        /// <param name="Entity">The entity to spawn</param>
        /// <param name="Weight">The weight of the entity</param>
        public SpawnerPotential(Entity.BaseEntity Entity, int Weight)
        {
            this.Entity = Entity;
            this.Weight = Weight;
        }

        /// <summary>
        /// The entity this spawner potential is for
        /// </summary>
        [Data.DataTag]
        public Entity.BaseEntity? Entity { get; set; }

        /// <summary>
        /// The weight for this potential to be selected
        /// </summary>
        [Data.DataTag]
        public int? Weight { get; set; }
    }
}
