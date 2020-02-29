﻿using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// An object for firework entities
        /// </summary>
        public class Firework : EntityBasic
        {
            /// <summary>
            /// Creates a new firework rocket
            /// </summary>
            /// <param name="type">the type of entity</param>
            public Firework(ID.Entity? type = ID.Entity.firework_rocket) : base(type) { }

            /// <summary>
            /// Makes the firework stop flying upwards automatically
            /// </summary>
            [Data.DataTag("ShotAtAngle")]
            public bool? Angled { get; set; }

            /// <summary>
            /// The amount of time the firework has been flying
            /// </summary>
            [Data.DataTag(ForceType = ID.NBTTagType.TagInt)]
            public Time? Life { get; set; }

            /// <summary>
            /// The time before the firework blows up
            /// </summary>
            [Data.DataTag(ForceType = ID.NBTTagType.TagInt)]
            public Time? LifeTime { get; set; }

            /// <summary>
            /// The firework displayed when the rocket blows up.
            /// This also sets <see cref="ItemID"/> and <see cref="ItemCount"/>
            /// </summary>
            [Data.DataTag("FireworksItem.tag.Fireworks.Explosions")]
            public SharpCraft.Firework?[]? Fireworks { get; set; }

            /// <summary>
            /// The id of the firework item (is normally <see cref="ID.Item.firework_rocket"/>).
            /// This is automatically set by <see cref="Fireworks"/>
            /// </summary>
            [Data.DataTag("id", ForceType = ID.NBTTagType.TagString)]
            public ID.Item? ItemID { get; set; }

            /// <summary>
            /// The amount of firework items (is normally 1).
            /// This is automatically set by <see cref="Fireworks"/>
            /// </summary>
            [Data.DataTag("Count")]
            public sbyte? ItemCount { get; set; }

            /// <summary>
            /// The amount of time the firework will fly
            /// </summary>
            [Data.DataTag]
            public sbyte? Flight { get; set; }
        }
    }
}
