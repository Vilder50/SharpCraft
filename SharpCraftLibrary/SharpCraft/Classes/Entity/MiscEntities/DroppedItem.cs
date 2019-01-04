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
            [DataTag]
            public Time Age { get; set; }
            /// <summary>
            /// The health of the item. despawns when at 0
            /// </summary>
            [DataTag]
            public float? Health { get; set; }
            /// <summary>
            /// The delay before the item can be picked up in ticks
            /// </summary>
            [DataTag]
            public Time PickupDelay { get; set; }
            /// <summary>
            /// The <see cref="UUID"/> of the entity who can pick up the item
            /// </summary>
            [DataTag]
            public UUID Owner { get; set; }
            /// <summary>
            /// The <see cref="UUID"/> of the entity who threw the item
            /// </summary>
            [DataTag]
            public UUID Thrower { get; set; }
            /// <summary>
            /// The item itself
            /// </summary>
            [DataTag]
            public SharpCraft.Item ItemData { get; set; }

            /// <summary>
            /// Makes the item unpickable
            /// (This overwrites <see cref="PickupDelay"/>)
            /// </summary>
            public bool Unpickable
            {
                get
                {
                    if (PickupDelay.AsTicks() != 32767)
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
                    if (Age.AsTicks() != -32768)
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

            /// <summary>
            /// Gets the raw data from this entity
            /// </summary>
            public override string DataString
            {
                get
                {
                    List<string> TempList = new List<string>();

                    string NormalData = BasicDataString;
                    if (NormalData.Length != 0) { TempList.Add(NormalData); }
                    if (Age != null) { TempList.Add("Age:" + Age.AsTicks(Time.TimerType.Short) + "s"); }
                    if (Health != null) { TempList.Add("Health:" + Health + "s"); }
                    if (PickupDelay != null) { TempList.Add("PickupDelay:" + PickupDelay.AsTicks(Time.TimerType.Short) + "s"); }
                    if (Owner != null) { TempList.Add("Owner:{L:\"" + Owner.Least + "\",M:\"" + Owner.Most + "\"}"); }
                    if (Thrower != null) { TempList.Add("Thrower:{L:\"" + Thrower.Least + "\",M:\"" + Thrower.Most + "\"}"); }
                    if (ItemData != null) { TempList.Add("Item:{" + ItemData.DataString + "}"); }

                    return string.Join(",", TempList);
                }
            }
        }
    }
}
