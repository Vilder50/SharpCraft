using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// The basic entity data for tameable
        /// </summary>
        public abstract class BaseTameable : BaseBreedable
        {
            /// <summary>
            /// Creates a new entity
            /// </summary>
            /// <param name="type">the type of entity</param>
            public BaseTameable(ID.Entity? type) : base(type) { }

            /// <summary>
            /// If the mob is sitting (wont follow / tp to its owner)
            /// </summary>
            [DataTag]
            public bool? Sitting { get; set; }
            /// <summary>
            /// the <see cref="UUID"/> of the owner
            /// </summary>
            [DataTag]
            public UUID OwnerUUID { get; set; }

            /// <summary>
            /// Gets the raw basic data for tameable mobs
            /// </summary>
            public string TameDataString
            {
                get
                {
                    List<string> TempList = new List<string>();

                    string BreedData = BreedDataString;
                    if (BreedData.Length != 0) { TempList.Add(BreedData); }
                    if (Sitting != null) { TempList.Add("Sitting:" + Sitting); }
                    if (OwnerUUID != null) { TempList.Add("OwnerUUID:" + OwnerUUID); }

                    return string.Join(",", TempList);
                }
            }
        }

        /// <summary>
        /// Entity data for tameable mobs
        /// </summary>
        public class Tameable : BaseTameable
        {
            /// <summary>
            /// Creates a new tameable mob
            /// </summary>
            /// <param name="type">the type of entity</param>
            public Tameable(ID.Entity? type) : base(type) { }
            /// <summary>
            /// Gets the raw data from this entity
            /// </summary>
            public override string DataString
            {
                get
                {
                    return TameDataString;
                }
            }
        }
    }
}
