using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// The basic entity data for breedable mobs
        /// </summary>
        public abstract class BaseBreedable : BaseMob
        {
            /// <summary>
            /// Creates a new entity
            /// </summary>
            /// <param name="type">the type of entity</param>
            public BaseBreedable(ID.Entity? type) : base(type) { }

            /// <summary>
            /// The amount of time the mob will be in love
            /// </summary>
            [DataTag]
            public Time InLove { get; set; }
            /// <summary>
            /// When negative it's the time till the mob turns into an adult
            /// When positive it's the time till the mob can breed again
            /// </summary>
            [DataTag]
            public Time Age { get; set; }
            /// <summary>
            /// A age which will be given to the mob when it has grown up.
            /// </summary>
            [DataTag]
            public Time ForcedAge { get; set; }
            /// <summary>
            /// The <see cref="UUID"/> of the entity who fed the mob
            /// </summary>
            [DataTag]
            public UUID LoveCause { get; set; }

            /// <summary>
            /// Gets the raw basic data for breedable mobs
            /// </summary>
            public string BreedDataString
            {
                get
                {
                    List<string> TempList = new List<string>();

                    string MobData = MobDataString;

                    if (MobData.Length != 0) { TempList.Add(MobData); }
                    if (InLove != null) { TempList.Add("InLove:" + InLove.AsTicks()); }
                    if (Age != null) { TempList.Add("Age:" + Age.AsTicks()); }
                    if (ForcedAge != null) { TempList.Add("ForcedAge:" + ForcedAge.AsTicks()); }
                    if (LoveCause != null) { TempList.Add("LoveCauseLeast:" + LoveCause.Least + "L,LoveCauseMost:" + LoveCause.Most + "L"); }

                    return string.Join(",", TempList);
                }
            }
        }

        /// <summary>
        /// Entity data for breedable mobs
        /// </summary>
        public class Breedable : BaseBreedable
        {
            /// <summary>
            /// Creates a new breedable mob
            /// </summary>
            /// <param name="type">the type of entity</param>
            public Breedable(ID.Entity? type) : base(type) { }

            /// <summary>
            /// Gets the raw data from this entity
            /// </summary>
            public override string DataString
            {
                get
                {
                    return BreedDataString;
                }
            }
        }

    }
}
