using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// Entity data for chickens
        /// </summary>
        public class Chicken : BaseBreedable
        {
            /// <summary>
            /// Creates a new chicken
            /// </summary>
            /// <param name="type">the type of entity</param>
            public Chicken(ID.Entity? type = ID.Entity.chicken) : base(type) { }

            /// <summary>
            /// Makes the chicken despawnable and drop 10 xp
            /// </summary>
            [DataTag]
            public bool? IsChickenJockey { get; set; }
            /// <summary>
            /// The time till the chicken lays another egg
            /// </summary>
            [DataTag]
            public Time EggLayTime { get; set; }

            /// <summary>
            /// Gets the raw data from this entity
            /// </summary>
            public override string DataString
            {
                get
                {
                    List<string> TempList = new List<string>();

                    string NormalData = BreedDataString;
                    if (NormalData.Length != 0) { TempList.Add(NormalData); }
                    if (IsChickenJockey != null) { TempList.Add("IsChickenJockey:" + IsChickenJockey); }
                    if (EggLayTime != null) { TempList.Add("EggLayTime:" + EggLayTime.AsTicks()); }

                    return string.Join(",", TempList);
                }
            }
        }
    }
}
