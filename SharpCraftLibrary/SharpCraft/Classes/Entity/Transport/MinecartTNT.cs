using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// Entity data for tnt minecarts
        /// </summary>
        public class MinecartTNT : BaseMinecart
        {
            /// <summary>
            /// Creates a new tnt minecart
            /// </summary>
            /// <param name="type">the type of entity</param>
            public MinecartTNT(ID.Entity? type = ID.Entity.tnt_minecart) : base(type) { }

            /// <summary>
            /// Time till it explodes
            /// (-1 ticks = not exploding)
            /// </summary>
            [Data.DataTag(ForceType = ID.NBTTagType.TagInt)]
            public Time TNTFuse { get; set; }

            /// <summary>
            /// Gets the raw data from this entity
            /// </summary>
            public override string DataString
            {
                get
                {
                    List<string> TempList = new List<string>();

                    string NormalData = MinecartDataString;
                    if (NormalData.Length != 0) { TempList.Add(NormalData); }
                    if (TNTFuse != null) { TempList.Add("TNTFuse:" + TNTFuse.AsTicks()); }

                    return string.Join(",", TempList);
                }
            }
        }
    }
}
