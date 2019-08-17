using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// The basic entity data for illagers
        /// </summary>
        public abstract class BaseIllager : BaseMob
        {
            /// <summary>
            /// Creates a new entity
            /// </summary>
            /// <param name="type">the type of entity</param>
            public BaseIllager(ID.Entity? type) : base(type) { }

            /// <summary>
            /// If the illager has a raid goal
            /// </summary>
            [Data.DataTag("HadRaidGoal")]
            public bool? HasGoal { get; set; }

            /// <summary>
            /// If the illager is patrolling
            /// </summary>
            [Data.DataTag]
            public bool? Patrolling { get; set; }

            /// <summary>
            /// If the illager is the leader
            /// </summary>
            [Data.DataTag("PatrolLeader")]
            public bool? Leader { get; set; }

            /// <summary>
            /// The place the illager is patrolling to
            /// </summary>
            [Data.CustomDataTag]
            public Coords PatrolTarget { get; set; }

            /// <summary>
            /// The id of the raid the illager is in
            /// </summary>
            [Data.DataTag]
            public int? RaidID { get; set; }

            /// <summary>
            /// the wave number the illager is in
            /// </summary>
            [Data.DataTag]
            public int? Wave { get; set; }

            /// <summary>
            /// Gets the raw basic data for illagers
            /// </summary>
            public string IllagerDataString
            {
                get
                {
                    List<string> TempList = new List<string>();

                    string MobData = MobDataString;
                    if (MobData.Length != 0) { TempList.Add(MobData); }
                    if (HasGoal != null) { TempList.Add("HasRaidGoal:" + HasGoal.ToMinecraftBool()); }
                    if (Patrolling != null) { TempList.Add("Patrolling:" + Patrolling.ToMinecraftBool()); }
                    if (Leader != null) { TempList.Add("PatrolLeader:" + Leader.ToMinecraftBool()); }
                    if (PatrolTarget != null) { TempList.Add("PatrolTarget:{X:" + System.Math.Round(PatrolTarget.X) + ",Y:" + System.Math.Round(PatrolTarget.Y) + ",Z:" + System.Math.Round(PatrolTarget.Z) + "}"); }
                    if (RaidID != null) { TempList.Add("RaidId:" + RaidID); }
                    if (Wave != null) { TempList.Add("Wave:" + Wave); }

                    return string.Join(",", TempList);
                }
            }
        }

        /// <summary>
        /// Entity data for illagers
        /// </summary>
        public class Illager : BaseIllager
        {
            /// <summary>
            /// Creates a new illager
            /// </summary>
            /// <param name="type">the type of entity</param>
            public Illager(ID.Entity? type = ID.Entity.pillager) : base(type) { }

            /// <summary>
            /// Gets the raw data from this entity
            /// </summary>
            public override string DataString
            {
                get
                {
                    return IllagerDataString;
                }
            }
        }
    }
}
