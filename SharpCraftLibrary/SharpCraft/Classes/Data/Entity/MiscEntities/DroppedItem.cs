using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// An object for item entities
        /// </summary>
        public class DroppedItem : EntityBasic
        {
            /// <summary>
            /// Creates a new item
            /// </summary>
            /// <param name="type">the type of entity</param>
            public DroppedItem(ID.Entity? type = ID.Entity.item) : base(type) { }

            /// <summary>
            /// The age of the item in ticks. When it hits 6000 it despawns
            /// </summary>
            [Data.DataTag(ForceType = ID.NBTTagType.TagShort)]
            public Time? Age { get; set; }

            /// <summary>
            /// The health of the item. despawns when at 0
            /// </summary>
            [Data.DataTag]
            public float? Health { get; set; }

            /// <summary>
            /// The delay before the item can be picked up in ticks
            /// </summary>
            [Data.DataTag(ForceType = ID.NBTTagType.TagShort)]
            public Time? PickupDelay { get; set; }

            /// <summary>
            /// The <see cref="UUID"/> of the entity who can pick up the item
            /// </summary>
            [Data.DataTag((object)"M","L")]
            public UUID? Owner { get; set; }

            /// <summary>
            /// The <see cref="UUID"/> of the entity who threw the item
            /// </summary>
            [Data.DataTag((object)"M", "L")]
            public UUID? Thrower { get; set; }

            /// <summary>
            /// The item itself
            /// </summary>
            [Data.DataTag("Item")]
            public SharpCraft.Item? ItemData { get; set; }

            /// <summary>
            /// Makes the item unpickable
            /// (This overwrites <see cref="PickupDelay"/>)
            /// </summary>
            public bool Unpickable
            {
                get
                {
                    if (PickupDelay is null || PickupDelay.AsTicks() != 32767)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                set
                {
                    if (value)
                    {
                        PickupDelay = 32767;
                    }
                    else
                    {
                        PickupDelay = null;
                    }
                }
            }
            /// <summary>
            /// Makes the item not despawn
            /// (This overwrites <see cref="Age"/>)
            /// </summary>
            public bool IgnoreAge
            {
                get
                {
                    if (Age is null || Age.AsTicks() != -32768)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                set
                {
                    if (value)
                    {
                        Age = -32768;
                    }
                    else
                    {
                        Age = null;
                    }
                }
            }
        }
    }
}
