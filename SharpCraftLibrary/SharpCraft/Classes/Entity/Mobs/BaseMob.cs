using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// The basic entity data for mobs
        /// </summary>
        public abstract class BaseMob : EntityBasic
        {
            /// <summary>
            /// Creates a new entity
            /// </summary>
            /// <param name="type">the type of entity</param>
            public BaseMob(ID.Entity? type) : base(type) { }

            /// <summary>
            /// The amount of health the mob has
            /// </summary>
            [Data.DataTag]
            public float? Health { get; set; }
            /// <summary>
            /// The amount of extra health the mob has
            /// </summary>
            [Data.DataTag]
            public double? AbsorptionAmount { get; set; }
            /// <summary>
            /// Makes the entity turn red for the given time
            /// </summary>
            [Data.DataTag(ForceType = ID.NBTTagType.TagShort)]
            public Time HurtTime { get; set; }
            /// <summary>
            /// The time since the mob last was hit
            /// </summary>
            [Data.DataTag(ForceType = ID.NBTTagType.TagInt)]
            public Time HurtByTimestamp { get; set; }
            /// <summary>
            /// The time the mob has been dead for.
            /// (0 = alive)
            /// </summary>
            [Data.DataTag(ForceType = ID.NBTTagType.TagShort)]
            public Time DeathTime { get; set; }
            /// <summary>
            /// Makes the mob fly when falling if it has an elytra on.
            /// </summary>
            [Data.DataTag]
            public bool? FallFlying { get; set; }
            /// <summary>
            /// The loot table the mob drops on death
            /// </summary>
            [Data.DataTag(ForceType = ID.NBTTagType.TagString)]
            public Loottable DeathLootTable { get; set; }
            /// <summary>
            /// The seed to use when dropping the <see cref="DeathLootTable"/>
            /// </summary>
            [Data.DataTag]
            public long? DeathLootTableSeed { get; set; }
            /// <summary>
            /// If the mob can pick up armor and items from the ground
            /// </summary>
            [Data.DataTag]
            public bool? CanPickUpLoot { get; set; }
            /// <summary>
            /// If the mob doesn't have an AI
            /// </summary>
            [Data.DataTag]
            public bool? NoAI { get; set; }
            /// <summary>
            /// If the mob shouldn't despawn
            /// </summary>
            [Data.DataTag]
            public bool? PersistenceRequired { get; set; }
            /// <summary>
            /// If the mob's main hand is its left hand
            /// </summary>
            [Data.DataTag]
            public bool? LeftHanded { get; set; }
            /// <summary>
            /// The team the mob is on
            /// </summary>
            [Data.DataTag(ForceType = ID.NBTTagType.TagString)]
            public Team Team { get; set; }
            /// <summary>
            /// The location the mob is leashed to
            /// </summary>
            [Data.CustomDataTag]
            public Coords LeashCoords { get; set; }
            /// <summary>
            /// The <see cref="UUID"/> of the leash
            /// </summary>
            [Data.CustomDataTag]
            public UUID LeashUUID { get; set; }
            /// <summary>
            /// The items there is in the mob's hands.
            /// 0: main hand. 1: off hand.
            /// </summary>
            [Data.DataTag]
            public Item[] HandItems { get; set; }
            /// <summary>
            /// The items the mob has on
            /// 0: boots. 1: leggings. 2: chestplate. 3: helmet
            /// </summary>
            [Data.DataTag]
            public Item[] ArmorItems { get; set; }
            /// <summary>
            /// The chance that the mob will drop its hand items when killed (number between 0-1)
            /// 0: main hand. 1: off hand
            /// </summary>
            [Data.DataTag]
            public float[] HandDropChances { get; set; }
            /// <summary>
            /// The chance that the mob will drop its armor items when killed (number between 0-1)
            /// 0: boots. 1: leggings. 2: chestplate. 3: helmet
            /// </summary>
            [Data.DataTag]
            public float[] ArmorDropChances { get; set; }
            /// <summary>
            /// The <see cref="Effect"/>s the mob has
            /// </summary>
            [Data.DataTag]
            public Effect[] ActiveEffects { get; set; }
            /// <summary>
            /// The <see cref="MCAttribute"/>s the mob has
            /// </summary>
            [Data.DataTag]
            public MCAttribute[] Attributes { get; set; }

            /// <summary>
            /// Gets the raw basic data for mobs
            /// </summary>
            public string MobDataString
            {
                get
                {
                    List<string> TempList = new List<string>();

                    string basicData = BasicDataString;
                    if (basicData.Length != 0) { TempList.Add(basicData); }
                    if (Health != null) { TempList.Add("Health:" + Health.ToString().Replace(",", ".") + "f"); }
                    if (AbsorptionAmount != null) { TempList.Add("AbsorptionAmount:" + AbsorptionAmount.ToString().Replace(",", ".") + "f"); }
                    if (HurtTime != null) { TempList.Add("HurtTime:" + HurtTime.AsTicks(Time.TimerType.Short) + "s"); }
                    if (HurtByTimestamp != null) { TempList.Add("HurtByTimestamp:" + HurtByTimestamp.AsTicks()); }
                    if (DeathTime != null) { TempList.Add("DeathTime:" + DeathTime.AsTicks(Time.TimerType.Short) + "s"); }
                    if (FallFlying != null) { TempList.Add("FallFlying:" + FallFlying); }
                    if (DeathLootTable != null) { TempList.Add("DeathLootTable:\"" + DeathLootTable + "\""); }
                    if (DeathLootTableSeed != null) { TempList.Add("DeathLootTableSeed:" + DeathLootTableSeed); }
                    if (CanPickUpLoot != null) { TempList.Add("CanPickupLoot:" + CanPickUpLoot); }
                    if (NoAI != null) { TempList.Add("NoAI:" + NoAI); }
                    if (PersistenceRequired != null) { TempList.Add("PersistenceRequired:" + PersistenceRequired); }
                    if (LeftHanded != null) { TempList.Add("LeftHanded:" + LeftHanded); }
                    if (Team != null) { TempList.Add("Team:" + Team); }
                    if (LeashCoords != null || LeashUUID != null)
                    {
                        TempList.Add("Leashed:1b");
                        List <string> TempLeashList = new List<string>();

                        if (LeashUUID != null) { TempLeashList.Add("LeashUUIDLeast:" + LeashUUID.Least + "L,LeashUUIDMost:" + LeashUUID.Most + "L"); }
                        if (LeashCoords != null) { TempLeashList.Add("X:" + LeashCoords.X + ",Y:" + LeashCoords.Y + ",Z" + LeashCoords.Z); }

                        TempList.Add("Leash:{" + string.Join(",", TempLeashList) + "}");
                    }
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
                    if (HandDropChances != null) { TempList.Add("HandDropChances:[" + HandDropChances[0].ToString().Replace(",", ".") + "f," + HandDropChances[1].ToString().Replace(",", ".") + "f]"); }
                    if (ArmorDropChances != null) { TempList.Add("ArmorDropChances:[" + ArmorDropChances[0].ToString().Replace(",", ".") + "f," + ArmorDropChances[1].ToString().Replace(",", ".") + "f," + ArmorDropChances[2].ToString().Replace(",", ".") + "f," + ArmorDropChances[3].ToString().Replace(",", ".") + "f]"); }
                    if (ActiveEffects != null)
                    {
                        string TempString = "ActiveEffects:[";
                        for (int a = 0; a < ActiveEffects.Length; a++)
                        {
                            if (a != 0) { TempString += ","; }
                            TempString += "{" + ActiveEffects[a] + "}";
                        }
                        TempString += "]";
                        TempList.Add(TempString);
                    }
                    if (Attributes != null)
                    {
                        List<string> TempAtList = new List<string>();
                        for (int i = 0; i < Attributes.Length; i++)
                        {
                            TempAtList.Add(Attributes[i].EntityString());
                        }
                        TempList.Add("Attributes:[" + string.Join(",", TempAtList) + "]");
                    }

                    return string.Join(",", TempList);
                }
            }
        }

        /// <summary>
        /// Entity data for mobs
        /// </summary>
        public class Mob : BaseMob
        {
            /// <summary>
            /// Creates a new mob
            /// </summary>
            /// <param name="type">the type of entity</param>
            public Mob(ID.Entity? type) : base(type) { }

            /// <summary>
            /// Gets the raw data from this entity
            /// </summary>
            public override string DataString
            {
                get
                {
                    return MobDataString;
                }
            }
        }
    }
}
