using System.Collections.Generic;
using SharpCraft.Data;

namespace SharpCraft
{
    public static partial class JSONObjects
    {
        /// <summary>
        /// a <see cref="object"/> defining damage
        /// </summary>
        public class Damage : DataHolderBase
        {
            /// <summary>
            /// The amount of damage actually dealt
            /// </summary>
            [DataTag("dealt", "min", "max", ID.NBTTagType.TagDouble, true, JsonTag = true)]
            public Range Dealt { get; set; }

            /// <summary>
            /// The amount of damage taken
            /// </summary>
            [DataTag("taken", "min", "max", ID.NBTTagType.TagDouble, true, JsonTag = true)]
            public Range Taken { get; set; }

            /// <summary>
            /// if the damage was blocked or not
            /// </summary>
            [DataTag("blocked", JsonTag = true)]
            public bool? Blocked { get; set; }

            /// <summary>
            /// the type of damage
            /// </summary>
            [DataTag("type", JsonTag = true)]
            public DamageFlags Type { get; set; }

            /// <summary>
            /// the damage source entity
            /// </summary>
            [DataTag("source_entity", JsonTag = true)]
            public Entity SourceEntity { get; set; }

            /// <summary>
            /// Outputs this <see cref="Damage"/> data in string format
            /// </summary>
            /// <returns>this <see cref="Damage"/>'s data</returns>
            public override string ToString()
            {
                List<string> TempList = new List<string>();

                if (Dealt != null) { TempList.Add(Dealt.JSONString("dealt")); }
                if (Taken != null) { TempList.Add(Taken.JSONString("taken")); }
                if (Blocked != null) { TempList.Add("\"blocked\":" + Blocked); }
                if (Type != null) { TempList.Add("\"type\":" + Type); }

                return "{" + string.Join(",", TempList) + "}";
            }
        }
    }
}
