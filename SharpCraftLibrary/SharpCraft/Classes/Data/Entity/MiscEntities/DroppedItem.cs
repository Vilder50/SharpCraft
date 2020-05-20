using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// An object for item entities
    /// </summary>
    public class DroppedItem : BasicEntity
    {
        /// <summary>
        /// Creates a new item
        /// </summary>
        /// <param name="type">the type of entity</param>
        public DroppedItem(ID.Entity? type = ID.Entity.item) : base(type) { }

        /// <summary>
        /// The age of the item in ticks. When it hits 6000 it despawns
        /// </summary>
        [Data.DataTag]
        public Time<short>? Age { get; set; }

        /// <summary>
        /// The health of the item. despawns when at 0
        /// </summary>
        [Data.DataTag]
        public float? Health { get; set; }

        /// <summary>
        /// The delay before the item can be picked up in ticks
        /// </summary>
        [Data.DataTag]
        public Time<short>? PickupDelay { get; set; }

        /// <summary>
        /// The <see cref="UUID"/> of the entity who can pick up the item
        /// </summary>
        [Data.DataTag("Owner", ForceType = ID.NBTTagType.TagIntArray)]
        public UUID? Owner { get; set; }

        /// <summary>
        /// The <see cref="UUID"/> of the entity who threw the item
        /// </summary>
        [Data.DataTag("Thrower", ForceType = ID.NBTTagType.TagIntArray)]
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
                if (PickupDelay is null || PickupDelay.GetAsTicks() != short.MaxValue)
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
                    PickupDelay = short.MaxValue;
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
                if (Age is null || Age.GetAsTicks() != short.MinValue)
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
                    Age = short.MinValue;
                }
                else
                {
                    Age = null;
                }
            }
        }
    }
}
