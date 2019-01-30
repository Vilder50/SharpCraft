﻿using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class Entity
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
            [DataTag]
            public Time Attack { get; set; }
            /// <summary>
            /// Cooldown till it can roar again
            /// </summary>
            [DataTag]
            public Time Roar { get; set; }
            /// <summary>
            /// Cooldown till it can stun again
            /// </summary>
            [DataTag]
            public Time Stun { get; set; }

            /// <summary>
            /// Gets the raw data from this entity
            /// </summary>
            public override string DataString
            {
                get
                {
                    List<string> TempList = new List<string>();

                    string NormalData = IllagerDataString;
                    if (NormalData.Length != 0) { TempList.Add(NormalData); }
                    if (Attack != null) { TempList.Add("AttackTick:" + Attack.AsTicks()); }
                    if (Roar != null) { TempList.Add("RoarTick:" + Roar.AsTicks()); }
                    if (Stun != null) { TempList.Add("StunTick:" + Stun.AsTicks()); }

                    return string.Join(",", TempList);
                }
            }
        }
    }
}