using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// An object for shulker bullets
        /// </summary>
        public class ShulkerBullet : EntityBasic
        {
            /// <summary>
            /// Creates a new shulker bullet
            /// </summary>
            /// <param name="type">the type of entity</param>
            public ShulkerBullet(ID.Entity? type = ID.Entity.shulker_bullet) : base(type) { }

            /// <summary>
            /// The owner of the bullet
            /// </summary>
            [Data.DataTag((object)"M","L")]
            public UUID Owner { get; set; }
            /// <summary>
            /// The owner's location
            /// </summary>
            [Data.DataTag("Owner","X", "Y", "Z", ID.NBTTagType.TagInt)]
            public Coords OwnerCoords { get; set; }
            /// <summary>
            /// The bullet's target
            /// </summary>
            [Data.DataTag((object)"M", "L")]
            public UUID Target { get; set; }
            /// <summary>
            /// The target's location
            /// </summary>
            [Data.DataTag("Target", "X", "Y", "Z", ID.NBTTagType.TagInt)]
            public Coords TargetCoords { get; set; }
            /// <summary>
            /// The amount of steps it takes to get to the target
            /// </summary>
            [Data.DataTag]
            public int? Steps { get; set; }
            /// <summary>
            /// The offset distance from the bullet to the target
            /// </summary>
            [Data.DataTag((object)"TXD", "TYD", "TZD", ID.NBTTagType.TagInt, Merge = true)]
            public Coords OffsetTarget { get; set; }
        }
    }
}
