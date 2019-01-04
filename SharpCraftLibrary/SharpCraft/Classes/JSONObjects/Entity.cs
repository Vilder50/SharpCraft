using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class JSONObjects
    {
        /// <summary>
        /// a <see cref="object"/> defining an <see cref="SharpCraft.Entity"/>
        /// </summary>
        public class Entity
        {
            /// <summary>
            /// The <see cref="SharpCraft.Entity"/> type
            /// </summary>
            public ID.Entity? Type;

            /// <summary>
            /// the <see cref="Distance"/> to the <see cref="SharpCraft.Entity"/>
            /// </summary>
            public Distance Distance;

            /// <summary>
            /// the <see cref="Location"/> of the <see cref="SharpCraft.Entity"/>
            /// </summary>
            public Location Location;

            /// <summary>
            /// the <see cref="Effect"/>s the <see cref="SharpCraft.Entity"/> has
            /// </summary>
            public Effect[] Effects;

            /// <summary>
            /// the nbt the <see cref="SharpCraft.Entity"/> has
            /// </summary>
            public SharpCraft.Entity.BaseEntity NBT;

            /// <summary>
            /// Outputs this <see cref="Entity"/> data in string format
            /// </summary>
            /// <returns>this <see cref="Entity"/>'s data</returns>
            public override string ToString()
            {
                List<string> TempList = new List<string>();

                if (Type != null) { TempList.Add("\"type\": \"" + Type + "\""); }
                if (Distance != null) { TempList.Add("\"distance\":" + Distance + ""); }
                if (Location != null) { TempList.Add("\"location\":{" + Location + "}"); }
                if (Effects != null)
                {
                    List<string> TempEffectList = new List<string>();
                    for (int i = 0; i < Effects.Length; i++)
                    {
                        TempEffectList.Add(Effects[i].ToString());
                    }
                    TempList.Add("\"effects\": {" + string.Join(",", TempEffectList) + "}");
                }
                if (NBT != null) { TempList.Add("\"nbt\":\"{" + NBT.DataWithID.Escape() + "}\""); }

                return "{" + string.Join(",", TempList) + "}";
            }
        }
    }
}
