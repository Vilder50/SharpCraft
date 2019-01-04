namespace SharpCraft
{
    /// <summary>
    /// An object used to define an entity's spawner potential
    /// </summary>
    public class SpawnerPotential
    {
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
        private readonly Entity.BaseEntity Entity;
        private readonly int Weight;

        /// <summary>
        /// Returns the raw data for this object as a string
        /// </summary>
        /// <returns>Raw data used by the game</returns>
        public override string ToString()
        {
            return "{Entity:{" + Entity.DataWithID + "},Weight:" + Weight + "}";
        }
    }
}
