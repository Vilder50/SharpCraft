using System.Collections.Generic;
using System;
using SharpCraft.Data;

namespace SharpCraft.Entities
{
    /// <summary>
    /// An object for armor stand entities
    /// </summary>
    public class Armorstand : BasicEntity
    {
        /// <summary>
        /// Creates a new armor stand entity
        /// </summary>
        /// <param name="type">the type of entity</param>
        public Armorstand(ID.Entity? type) : base(type) { }

        /// <summary>
        /// Creates a new entity
        /// </summary>
        public Armorstand() : base(SharpCraft.ID.Entity.armor_stand) { }

        /// <summary>
        /// Choses which slots are locked
        /// </summary>
        [DataTag]
        public LockedSlots? SlotRules { get; set; }

        /// <summary>
        /// Used to define what slots on an armor stand are locked
        /// </summary>
        public class LockedSlots : IConvertableToDataTag
        {
            /// <summary>
            /// Locks the boots item 100%
            /// </summary>
            /// <returns>Itself</returns>
            public LockedSlots LockHand()
            {
                LockAddingHand = true;
                LockRemovingHand = true;
                LockChangingHand = true;
                return this;
            }

            /// <summary>
            /// Locks the boots item 100%
            /// </summary>
            /// <returns>Itself</returns>
            public LockedSlots LockBoots()
            {
                LockAddingBoots = true;
                LockRemovingBoots = true;
                LockChangingBoots = true;
                return this;
            }

            /// <summary>
            /// Locks the leggings item 100%
            /// </summary>
            /// <returns>Itself</returns>
            public LockedSlots LockLeggings()
            {
                LockAddingLeggings = true;
                LockRemovingLeggings = true;
                LockChangingLeggings = true;
                return this;
            }

            /// <summary>
            /// Locks the chestplate item 100%
            /// </summary>
            /// <returns>Itself</returns>
            public LockedSlots LockChestplate()
            {
                LockAddingChestplate = true;
                LockRemovingChestplate = true;
                LockChangingChestplate = true;
                return this;
            }

            /// <summary>
            /// Locks the helmet item 100%
            /// </summary>
            /// <returns>Itself</returns>
            public LockedSlots LockHelmet()
            {
                LockAddingHelmet = true;
                LockRemovingHelmet = true;
                LockChangingHelmet = true;
                return this;
            }

            /// <summary>
            /// Locks all slots 100%
            /// </summary>
            /// <returns>Itself</returns>
            public LockedSlots LockAll()
            {
                LockHand();
                LockBoots();
                LockLeggings();
                LockChestplate();
                LockHelmet();
                return this;
            }

            /// <summary>
            /// Makes it impossible to remove the hand item from the armor stand
            /// </summary>
            public bool LockRemovingHand;
            /// <summary>
            /// Makes it impossible to remove the boots item from the armor stand
            /// </summary>
            public bool LockRemovingBoots;
            /// <summary>
            /// Makes it impossible to remove the leggings item from the armor stand
            /// </summary>
            public bool LockRemovingLeggings;
            /// <summary>
            /// Makes it impossible to remove the chestplate item from the armor stand
            /// </summary>
            public bool LockRemovingChestplate;
            /// <summary>
            /// Makes it impossible to remove the helmet item from the armor stand
            /// </summary>
            public bool LockRemovingHelmet;

            /// <summary>
            /// Makes it impossible to change the hand item from the armor stand
            /// </summary>
            public bool LockChangingHand;
            /// <summary>
            /// Makes it impossible to change the boots item from the armor stand
            /// </summary>
            public bool LockChangingBoots;
            /// <summary>
            /// Makes it impossible to change the leggings item from the armor stand
            /// </summary>
            public bool LockChangingLeggings;
            /// <summary>
            /// Makes it impossible to change the chestplate item from the armor stand
            /// </summary>
            public bool LockChangingChestplate;
            /// <summary>
            /// Makes it impossible to change the helmet item from the armor stand
            /// </summary>
            public bool LockChangingHelmet;

            /// <summary>
            /// Makes it impossible to add the hand item from the armor stand
            /// </summary>
            public bool LockAddingHand;
            /// <summary>
            /// Makes it impossible to add the boots item from the armor stand
            /// </summary>
            public bool LockAddingBoots;
            /// <summary>
            /// Makes it impossible to add the leggings item from the armor stand
            /// </summary>
            public bool LockAddingLeggings;
            /// <summary>
            /// Makes it impossible to add the chestplate item from the armor stand
            /// </summary>
            public bool LockAddingChestplate;
            /// <summary>
            /// Makes it impossible to add the helmet item from the armor stand
            /// </summary>
            public bool LockAddingHelmet;

            /// <summary>
            /// Gets the value Minecraft uses to disables slots
            /// </summary>
            /// <returns>Raw data used by Minecarft</returns>
            public int GetValue()
            {
                int returnValue = 0;
                if (LockRemovingHand) { returnValue += (int)Math.Pow(2, 0); }
                if (LockRemovingBoots) { returnValue += (int)Math.Pow(2, 1); }
                if (LockRemovingLeggings) { returnValue += (int)Math.Pow(2, 2); }
                if (LockRemovingChestplate) { returnValue += (int)Math.Pow(2, 3); }
                if (LockRemovingHelmet) { returnValue += (int)Math.Pow(2, 4); }
                if (LockChangingHand) { returnValue += (int)Math.Pow(2, 8); }
                if (LockChangingBoots) { returnValue += (int)Math.Pow(2, 9); }
                if (LockChangingLeggings) { returnValue += (int)Math.Pow(2, 10); }
                if (LockChangingChestplate) { returnValue += (int)Math.Pow(2, 11); }
                if (LockChangingHelmet) { returnValue += (int)Math.Pow(2, 12); }
                if (LockAddingHand) { returnValue += (int)Math.Pow(2, 16); }
                if (LockAddingBoots) { returnValue += (int)Math.Pow(2, 17); }
                if (LockAddingLeggings) { returnValue += (int)Math.Pow(2, 18); }
                if (LockAddingChestplate) { returnValue += (int)Math.Pow(2, 19); }
                if (LockAddingHelmet) { returnValue += (int)Math.Pow(2, 20); }

                return returnValue;
            }

            /// <summary>
            /// Converts this <see cref="LockedSlots"/> object into a <see cref="DataPartTag"/>
            /// </summary>
            /// <param name="extraConversionData">Not used</param>
            /// <param name="asType">Not used</param>
            /// <returns>the made <see cref="DataPartTag"/></returns>
            public DataPartTag GetAsTag(ID.NBTTagType? asType, object?[] extraConversionData)
            {
                return new DataPartTag(GetValue());
            }
        }

        /// <summary>
        /// Makes the armor stand a marker armor stand.
        /// The armor stand wont have a hitbox.
        /// </summary>
        [DataTag]
        public bool? Marker { get; set; }
        /// <summary>
        /// The items there is in the armor stand's hands.
        /// 0: main hand. 1: off hand.
        /// </summary>
        [DataTag]
        public Item[]? HandItems { get; set; }
        /// <summary>
        /// The items the armor stand has on
        /// 0: boots. 1: leggings. 2: chestplate. 3: helmet
        /// </summary>
        [DataTag]
        public Item[]? ArmorItems { get; set; }
        /// <summary>
        /// Makes the armor stand invisible
        /// </summary>
        [DataTag]
        public bool? Invisible { get; set; }
        /// <summary>
        /// Removes the armor stand's stone plate
        /// </summary>
        [DataTag]
        public bool? NoBasePlate { get; set; }
        /// <summary>
        /// Makes the armor stand fly when falling if it has an elytra on.
        /// </summary>
        [DataTag]
        public bool? FallFlying { get; set; }
        /// <summary>
        /// If the armor stand should show its arms
        /// </summary>
        [DataTag]
        public bool? ShowArms { get; set; }
        /// <summary>
        /// If the armor stand is a small armor stand
        /// </summary>
        [DataTag]
        public bool? Small { get; set; }
        /// <summary>
        /// Rotates the armor stand's body
        /// 0: x. 1: y. 2: z.
        /// </summary>
        [DataTag("Pose.Body")]
        public float[]? BodyRotation { get; set; }
        /// <summary>
        /// Rotates the armor stand's left arm
        /// 0: x. 1: y. 2: z.
        /// </summary>
        [DataTag("Pose.LeftArm")]
        public float[]? ArmLeftRotation { get; set; }
        /// <summary>
        /// Rotates the armor stand's right arm
        /// 0: x. 1: y. 2: z.
        /// </summary>
        [DataTag("Pose.RightArm")]
        public float[]? ArmRightRotation { get; set; }
        /// <summary>
        /// Rotates the armor stand's left leg
        /// 0: x. 1: y. 2: z.
        /// </summary>
        [DataTag("Pose.LeftLeg")]
        public float[]? LegLeftRotation { get; set; }
        /// <summary>
        /// Rotates the armor stand's right leg
        /// 0: x. 1: y. 2: z.
        /// </summary>
        [DataTag("Pose.RightLeg")]
        public float[]? LegRightRotation { get; set; }
        /// <summary>
        /// Rotates the armor stand's head
        /// 0: x. 1: y. 2: z.
        /// </summary>
        [DataTag("Pose.Head")]
        public float[]? HeadRotation { get; set; }
    }
}
