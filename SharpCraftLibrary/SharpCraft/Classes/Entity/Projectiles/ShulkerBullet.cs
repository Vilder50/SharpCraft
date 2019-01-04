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
            [DataTag]
            public UUID Owner { get; set; }
            /// <summary>
            /// The owner's location
            /// </summary>
            [DataTag]
            public Coords OwnerCoords { get; set; }
            /// <summary>
            /// The bullet's target
            /// </summary>
            [DataTag]
            public UUID Target { get; set; }
            /// <summary>
            /// The target's location
            /// </summary>
            [DataTag]
            public Coords TargetCoords { get; set; }
            /// <summary>
            /// The amount of steps it takes to get to the target
            /// </summary>
            [DataTag]
            public int? Steps { get; set; }
            /// <summary>
            /// The offset distance from the bullet to the target
            /// </summary>
            [DataTag]
            public Coords OffsetTarget { get; set; }

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
                    if (OwnerCoords != null || Owner != null)
                    {
                        List<string> OwnerTempList = new List<string>();

                        if (Owner != null) { OwnerTempList.Add("L:" + Owner.Least + ",M:" + Owner.Most); }
                        if (OwnerCoords != null) { OwnerTempList.Add("X:" + OwnerCoords.X + ",Y:" + OwnerCoords.Y + ",Z:" + OwnerCoords.Z); }

                        TempList.Add("Owner:{" + string.Join(",", OwnerTempList) + "}");
                    }
                    if (TargetCoords != null || Target != null)
                    {
                        List<string> TargetTempList = new List<string>();

                        if (Target != null) { TargetTempList.Add("L:" + Target.Least + ",M:" + Target.Most); }
                        if (TargetCoords != null) { TargetTempList.Add("X:" + TargetCoords.X + ",Y:" + TargetCoords.Y + ",Z:" + TargetCoords.Z); }

                        TempList.Add("Target:{" + string.Join(",", TargetTempList) + "}");
                    }
                    if (OffsetTarget != null) { TempList.Add("TXD:" + OffsetTarget.X + "f,TYD:" + OffsetTarget.Y + "f,TZD:" + OffsetTarget.Z + "f"); }
                    if (Steps != null) { TempList.Add("Steps:" + Steps); }

                    return string.Join(",", TempList);
                }
            }
        }
    }
}
