using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// Entity data for ravagers
    /// </summary>
    public class Ravager : BaseIllager
    {
        /// <summary>
        /// Creates a new ravager
        /// </summary>
        /// <param name="type">the type of entity</param>
        public Ravager(ID.Entity? type = ID.Entity.ravager) : base(type) { }

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
