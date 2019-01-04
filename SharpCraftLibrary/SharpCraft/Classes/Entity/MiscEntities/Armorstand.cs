using System.Collections.Generic;
using System;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// An object for armor stand entities
        /// </summary>
        public class Armorstand : EntityBasic
        {
            /// <summary>
            /// Creates a new armor stand entity
            /// </summary>
            /// <param name="type">the type of entity</param>
            public Armorstand(ID.Entity? type = ID.Entity.armor_stand) : base(type) { }


            /// <summary>
            /// Choses which slots are locked
            /// </summary>
            [DataTag]
            public LockedSlots SlotRules { get; set; }

            /// <summary>
            /// Used to define what slots on an armor stand are locked
            /// </summary>
            public class LockedSlots
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
            public Item[] HandItems { get; set; }
            /// <summary>
            /// The items the armor stand has on
            /// 0: boots. 1: leggings. 2: chestplate. 3: helmet
            /// </summary>
            [DataTag]
            public Item[] ArmorItems { get; set; }
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
            [DataTag]
            public double?[] BodyRotation { get; set; }
            /// <summary>
            /// Rotates the armor stand's left arm
            /// 0: x. 1: y. 2: z.
            /// </summary>
            [DataTag]
            public double?[] ArmLeftRotation { get; set; }
            /// <summary>
            /// Rotates the armor stand's right arm
            /// 0: x. 1: y. 2: z.
            /// </summary>
            [DataTag]
            public double?[] ArmRightRotation { get; set; }
            /// <summary>
            /// Rotates the armor stand's left leg
            /// 0: x. 1: y. 2: z.
            /// </summary>
            [DataTag]
            public double?[] LegLeftRotation { get; set; }
            /// <summary>
            /// Rotates the armor stand's right leg
            /// 0: x. 1: y. 2: z.
            /// </summary>
            [DataTag]
            public double?[] LegRightRotation { get; set; }
            /// <summary>
            /// Rotates the armor stand's head
            /// 0: x. 1: y. 2: z.
            /// </summary>
            [DataTag]
            public double?[] HeadRotation { get; set; }

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
                    if (Marker != null) { TempList.Add("Marker:" + Marker); }
                    if (HandItems != null)
                    {
                        string TempString = "HandItems:[";
                        for (int a = 0; a < HandItems.Length; a++)
                        {
                            if (a != 0) { TempString += ","; }
                            TempString += "{" + HandItems[a].DataString + "}";
                        }
                        TempString += "]";
                        TempList.Add(TempString);
                    }
                    if (ArmorItems != null)
                    {
                        string TempString = "ArmorItems:[";
                        for (int a = 0; a < ArmorItems.Length; a++)
                        {
                            if (a != 0) { TempString += ","; }
                            TempString += "{" + ArmorItems[a].DataString + "}";
                        }
                        TempString += "]";
                        TempList.Add(TempString);
                    }
                    if (Invisible != null) { TempList.Add("Invisible:" + Invisible); }
                    if (NoBasePlate != null) { TempList.Add("NoBasePlate:" + NoBasePlate); }
                    if (FallFlying != null) { TempList.Add("FallFlying:" + Marker); }
                    if (ShowArms != null) { TempList.Add("ShowArms:" + ShowArms); }
                    if (Small != null) { TempList.Add("Small:" + Small); }
                    if (BodyRotation != null || ArmLeftRotation != null || ArmRightRotation != null || LegLeftRotation != null || LegRightRotation != null || HeadRotation != null)
                    {
                        List<string> TempListRotation = new List<string>();
                        if (BodyRotation != null) { TempListRotation.Add("Body:[" + BodyRotation[0].ToString().Replace(",", ".") + "f," + BodyRotation[1].ToString().Replace(",", ".") + "f," + BodyRotation[2].ToString().Replace(",", ".") + "f]"); }
                        if (ArmLeftRotation != null) { TempListRotation.Add("LeftArm:[" + ArmLeftRotation[0].ToString().Replace(",", ".") + "f," + ArmLeftRotation[1].ToString().Replace(",", ".") + "f," + ArmLeftRotation[2].ToString().Replace(",", ".") + "f]"); }
                        if (ArmRightRotation != null) { TempListRotation.Add("RightArm:[" + ArmRightRotation[0].ToString().Replace(",", ".") + "f," + ArmRightRotation[1].ToString().Replace(",", ".") + "f," + ArmRightRotation[2].ToString().Replace(",", ".") + "f]"); }
                        if (LegLeftRotation != null) { TempListRotation.Add("LeftLeg:[" + LegLeftRotation[0].ToString().Replace(",", ".") + "f," + LegLeftRotation[1].ToString().Replace(",", ".") + "f," + LegLeftRotation[2].ToString().Replace(",", ".") + "f]"); }
                        if (LegRightRotation != null) { TempListRotation.Add("RightLeg:[" + LegRightRotation[0].ToString().Replace(",", ".") + "f," + LegRightRotation[1].ToString().Replace(",", ".") + "f," + LegRightRotation[2].ToString().Replace(",", ".") + "f]"); }
                        if (HeadRotation != null) { TempListRotation.Add("Head:[" + HeadRotation[0].ToString().Replace(",", ".") + "f," + HeadRotation[1].ToString().Replace(",", ".") + "f," + HeadRotation[2].ToString().Replace(",", ".") + "f]"); }

                        TempList.Add("Pose:{" + string.Join(",", TempListRotation) + "}");
                    }
                    if (SlotRules != null)
                    {
                        TempList.Add("DisabledSlots:" + SlotRules.GetValue());
                    }

                    return string.Join(",", TempList);
                }
            }
        }
    }
}
