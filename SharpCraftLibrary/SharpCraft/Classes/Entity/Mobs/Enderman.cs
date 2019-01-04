using System.Collections.Generic;
using System;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// Entity data for endermen
        /// </summary>
        public class Enderman : BaseMob
        {
            /// <summary>
            /// Creates a new enderman
            /// </summary>
            /// <param name="type">the type of entity</param>
            public Enderman(ID.Entity? type = ID.Entity.endermite) : base(type) { }

            /// <summary>
            /// The block the enderman is holding.
            /// Note: block data is not supported
            /// </summary>
            [DataTag]
            public Block Holding { get; set; }

            /// <summary>
            /// Gets the raw data from this entity
            /// </summary>
            public override string DataString
            {
                get
                {
                    List<string> TempList = new List<string>();

                    string NormalData = MobDataString;
                    if (NormalData.Length != 0) { TempList.Add(NormalData); }

                    if (Holding != null)
                    {
                        if (Holding.ID != null || Holding.HasState)
                        {
                            string blockState = "carriedBlockState:{";
                            if (Holding.ID != null) { blockState += "Name:\"minecraft:" + Holding.ID.ToString() + "\""; }
                            if (Holding.ID != null && Holding.HasState) { blockState += ","; }
                            if (Holding.HasState) { blockState += "Properties:{" + Holding.GetStateString().ToString().Replace("=", ":\"").Replace(",", "\",") + "\"}"; }
                            TempList.Add(blockState + "}");
                        }
                    }

                    return string.Join(",", TempList);
                }
            }
        }
    }
}
