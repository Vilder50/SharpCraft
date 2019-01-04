using System.Collections.Generic;
using System;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// The base of all projectile entities
        /// </summary>
        public abstract class BaseProjectile : EntityBasic
        {
            /// <summary>
            /// Creates a new entity
            /// </summary>
            /// <param name="type">the type of entity</param>
            public BaseProjectile(ID.Entity? type) : base(type) { }

            /// <summary>
            /// The coords of the block
            /// </summary>
            [DataTag]
            public Coords TileCoords { get; set; }

            /// <summary>
            /// The block this projectile is in
            /// </summary>
            [DataTag]
            public Block InBlock { get; set; }

            /// <summary>
            /// Gets the raw data from this entity
            /// </summary>
            /// <returns>raw data Minecraft uses</returns>
            public string ProjectileDataString
            {
                get
                {
                    List<string> TempList = new List<string>();

                    if (InBlock != null && (InBlock.ID != null || InBlock.HasState))
                    {
                        string blockState = "BlockState:{";
                        if (InBlock.ID != null) { blockState += "Name:\"minecraft:" + InBlock.ID.ToString() + "\""; }
                        if (InBlock.ID != null && InBlock.HasState) { blockState += ","; }
                        if (InBlock.HasState) { blockState += "Properties:{" + InBlock.GetStateString().ToString().Replace("=", ":\"").Replace(",", "\",") + "\"}"; }
                        TempList.Add(blockState + "}");
                    }

                    return string.Join(",", TempList);
                }
            }
        }
    }
}
