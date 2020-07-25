using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// Entity data for ravagers
    /// </summary>
    public class Ravager : BaseIllager
    {
        /// <summary>
        /// Returns a object which can be used for creating data paths
        /// </summary>
        /// <returns>Object used for making data paths</returns>
        public new static Data.DataPathCreator<Ravager> PathCreator => new Data.DataPathCreator<Ravager>();

        /// <summary>
        /// Creates a new ravager
        /// </summary>
        /// <param name="type">the type of entity</param>
        public Ravager(ID.Entity? type) : base(type) { }

        /// <summary>
        /// Creates a new entity
        /// </summary>
        public Ravager() : base(SharpCraft.ID.Entity.ravager) { }

        /// <summary>
        /// Cooldown till it can attack again
        /// </summary>
        [Data.DataTag("AttackTick")]
        public Time<int>? Attack { get; set; }
        /// <summary>
        /// Cooldown till it can roar again
        /// </summary>
        [Data.DataTag("RoarTick")]
        public Time<int>? Roar { get; set; }
        /// <summary>
        /// Cooldown till it can stun again
        /// </summary>
        [Data.DataTag("StunTick")]
        public Time<int>? Stun { get; set; }
    }
}
