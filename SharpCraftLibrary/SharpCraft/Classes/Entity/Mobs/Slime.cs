using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// Entity data for slimes and magma cubes
        /// </summary>
        public class Slime : BaseMob
        {
            /// <summary>
            /// Creates a new slime or magma cube
            /// </summary>
            /// <param name="type">the type of entity</param>
            public Slime(ID.Entity? type = ID.Entity.slime) : base(type) { }

            /// <summary>
            /// The size of the slime
            /// </summary>
            [DataTag]
            public int? Size { get; set; }
            /// <summary>
            /// True if the slime touches the ground
            /// </summary>
            [DataTag]
            public bool? WasOnGround { get; set; }

            /// <summary>
            /// Gets the raw data from this entity
            /// </summary>
            public override string DataString
            {
                get
                {
                    List<string> TempList = new List<string>();

                    string NormalData = MobDataString;
                    if (NormalData.Length != 0) { TempList.Add(NormalData); }
                    if (Size != null) { TempList.Add("Size:" + Size); }
                    if (WasOnGround != null) { TempList.Add("wasOnGround:" + WasOnGround.ToMinecraftBool()); }

                    return string.Join(",", TempList);
                }
            }
        }
    }
}
