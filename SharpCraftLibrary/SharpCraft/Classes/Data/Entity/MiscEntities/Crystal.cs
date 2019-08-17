using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// An object for end crystal entities
        /// </summary>
        public class EndCrystal : EntityBasic
        {
            /// <summary>
            /// Creates a new end crystal
            /// </summary>
            /// <param name="type">the type of entity</param>
            public EndCrystal(ID.Entity? type = ID.Entity.end_crystal) : base(type) { }

            /// <summary>
            /// If the bedrock should be shown under the crystal
            /// </summary>
            [Data.DataTag]
            public bool? ShowBottom { get; set; }
            /// <summary>
            /// The coords the crystal's beam should point to
            /// </summary>
            [Data.CustomDataTag]
            public Coords BeamTarget { get; set; }

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
                    if (ShowBottom != null) { TempList.Add("ShowBottom:" + ShowBottom); }
                    if (BeamTarget != null) { TempList.Add("BeamTarget:{X:" + BeamTarget.X + ",Y:" + BeamTarget.Y + ",Z:" + BeamTarget.Z + "}"); }

                    return string.Join(",", TempList);
                }
            }
        }
    }
}
