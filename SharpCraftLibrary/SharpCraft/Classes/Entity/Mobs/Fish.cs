using System.Collections.Generic;
using System;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// Entity data for fishs
        /// </summary>
        public class Fish : BaseMob
        {
            /// <summary>
            /// Creates a new fish
            /// </summary>
            /// <param name="type">the type of entity</param>
            public Fish(ID.Entity? type) : base(type) { }

            /// <summary>
            /// Fow the tropical fish looks
            /// </summary>
            [Data.CustomDataTag]
            public Variant FishVariant { get; set; }

            /// <summary>
            /// A object used to define how a fish looks
            /// </summary>
            public class Variant
            {
                /// <summary>
                /// The size of the fish
                /// </summary>
                public ID.FishSize Size { get; set; }
                /// <summary>
                /// The fish's pattern
                /// </summary>
                public ID.FishPattern Pattern { get; set; }
                /// <summary>
                /// The color of the body of the fish
                /// </summary>
                public ID.Color BodyColor { get; set; }
                /// <summary>
                /// The color of the pattern on the fish
                /// </summary>
                public ID.Color PatternColor { get; set; }

                /// <summary>
                /// Gets the value Minecraft uses
                /// </summary>
                /// <returns>Gets raw data used by Minecraft</returns>
                public int GetValue()
                {
                    byte[] asBytes = new byte[] { (byte)PatternColor, (byte)BodyColor, (byte)Pattern, (byte)Size };
                    if (BitConverter.IsLittleEndian)
                    {
                        Array.Reverse(asBytes);
                    }

                    return BitConverter.ToInt32(asBytes, 0);
                }
            }
            /// <summary>
            /// If the fish comes from a bucket.
            /// It wont despawn.
            /// </summary>
            [Data.DataTag]
            public bool? FromBucket { get; set; }
            /// <summary>
            /// The puff state for pufferfish.
            /// 0 = deflated. 1 = halfway puffed-up. 2 = puffed-up.
            /// </summary>
            [Data.DataTag]
            public int? PuffState { get; set; }
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
                    if (FishVariant != null) { TempList.Add("Variant:" + FishVariant.GetValue()); }
                    if (PuffState != null) { TempList.Add("PuffState:" + PuffState); }
                    if (FromBucket != null) { TempList.Add("FromBucket:" + FromBucket + "b"); }
                    return string.Join(",", TempList);
                }
            }
        }
    }
}
