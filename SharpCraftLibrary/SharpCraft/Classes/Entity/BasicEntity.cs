using System;
using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// The basic entity data
        /// </summary>
        public abstract class EntityBasic : BaseEntity
        {
            /// <summary>
            /// Creates a new entity
            /// </summary>
            /// <param name="type">the type of entity</param>
            public EntityBasic(ID.Entity? type) : base(type) { }

            /// <summary>
            /// The entity's motion
            /// </summary>
            [DataTag]
            public Coords Motion { get; set; }
            /// <summary>
            /// The entity's rotation
            /// </summary>
            [DataTag]
            public Rotation Rotation { get; set; }
            /// <summary>
            /// The entity's location
            /// </summary>
            [DataTag]
            public Coords Coords { get; set; }
            /// <summary>
            /// The distance the entity has fallen
            /// </summary>
            [DataTag]
            public float? FallDistance { get; set; }
            /// <summary>
            /// The time before the fire on the entity goes out.
            /// Negative value means how long it takes for the entity to turn on fire.
            /// </summary>
            [DataTag]
            public Time Fire { get; set; }
            /// <summary>
            /// How much air the entity has left.
            /// (Being 0 under water will make the entity drown)
            /// </summary>
            [DataTag]
            public Time Air { get; set; }
            /// <summary>
            /// If the entity is on the ground or not
            /// </summary>
            [DataTag]
            public bool? OnGround { get; set; }
            /// <summary>
            /// If the entity shouldn't be effected by gravity
            /// </summary>
            [DataTag]
            public bool? NoGravity { get; set; }
            /// <summary>
            /// The dimension the entity is in
            /// </summary>
            [DataTag]
            public ID.Dimension? Dimension { get; set; }
            /// <summary>
            /// If the entity is Invulnerable.
            /// (Can't be killed)
            /// </summary>
            [DataTag]
            public bool? Invulnerable { get; set; }
            /// <summary>
            /// The amount of time before the entity can go through a portal again.
            /// </summary>
            [DataTag]
            public Time PortalCooldown { get; set; }
            /// <summary>
            /// The entity's UUID
            /// </summary>
            [DataTag]
            public UUID UUID { get; set; }
            /// <summary>
            /// The entity's shown name
            /// </summary>
            [DataTag]
            public JSON[] CustomName { get; set; }
            /// <summary>
            /// If the entity's name should be shown always
            /// </summary>
            [DataTag]
            public bool? CustomNameVisible { get; set; }
            /// <summary>
            /// If the entity should be silent and not make any sounds
            /// </summary>
            [DataTag]
            public bool? Silent { get; set; }
            /// <summary>
            /// The entities riding on the entity
            /// </summary>
            [DataTag]
            public BaseEntity[] Passengers { get; set; }
            /// <summary>
            /// If the entity should glow
            /// </summary>
            [DataTag]
            public bool? Glowing { get; set; }
            /// <summary>
            /// The entity's tags
            /// </summary>
            [DataTag]
            public Tag[] Tags { get; set; }

            /// <summary>
            /// Gets the raw basic data for this entity
            /// </summary>
            public string BasicDataString
            {
                get
                {
                    List<string> TempList = new List<string>();
                    if (Motion != null) { TempList.Add("Motion:[" + Motion.X.ToString().Replace(",", ".") + "d," + Motion.Y.ToString().Replace(",", ".") + "d," + Motion.Z.ToString().Replace(",", ".") + "d]"); }
                    if (Rotation != null) { TempList.Add("Rotation:[" + Rotation.XRot + "f," + Rotation.YRot + "f]"); }
                    if (FallDistance != null) { TempList.Add("FallDistance:" + FallDistance.ToString().Replace(",", ".") + "f"); }
                    if (Fire != null) { TempList.Add("Fire:" + Fire.AsTicks(Time.TimerType.Short) + "s"); }
                    if (Air != null) { TempList.Add("Air:" + Air.AsTicks(Time.TimerType.Short) + "s"); }
                    if (OnGround != null) { TempList.Add("OnGround:" + OnGround); }
                    if (NoGravity != null) { TempList.Add("NoGravity:" + NoGravity); }
                    if (Dimension != null) { TempList.Add("Dimension:" + (int)Dimension); }
                    if (Invulnerable != null) { TempList.Add("Invulnerable:" + Invulnerable); }
                    if (PortalCooldown != null) { TempList.Add("PortalCooldown:" + PortalCooldown.AsTicks()); }
                    if (UUID != null) { TempList.Add("UUIDMost:" + UUID.Least + "L,UUIDLeast:" + UUID.Least + "L"); }
                    if (CustomName != null) { TempList.Add("CustomName:\"" + CustomName.GetString().Escape() + "\""); }
                    if (CustomNameVisible != null) { TempList.Add("CustomNameVisible:" + CustomNameVisible); }
                    if (Silent != null) { TempList.Add("Silent:" + Silent); }
                    if (Glowing != null) { TempList.Add("Glowing:" + Glowing); }
                    if (Coords != null) { TempList.Add("Pos:[" + Coords.X + "," + Coords.Y + "," + Coords.Z + "]"); }

                    if (Passengers != null)
                    {
                        string TempString = "Passengers:[";
                        for (int i = 0; i < Passengers.Length; i++)
                        {
                            if (i != 0) { TempString += ","; }
                            TempString += "{" + Passengers[i].DataWithID + "}";
                        }
                        TempList.Add(TempString + "]");
                    }
                    if (Tags != null)
                    {
                        List<string> TempTagList = new List<string>();
                        for (int i = 0; i < Tags.Length; i++)
                        {
                            TempTagList.Add("\"" + Tags[i] + "\"");
                        }
                        TempList.Add("Tags:[" + string.Join(",", TempTagList) + "]");
                    }

                    return string.Join(",", TempList);
                }
            }
        }
    }
}
