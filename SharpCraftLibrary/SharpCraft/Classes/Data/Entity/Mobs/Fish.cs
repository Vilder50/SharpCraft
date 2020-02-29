using System.Collections.Generic;
using System;
using SharpCraft.Data;

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
            [DataTag("Variant")]
            public Variant? FishVariant { get; set; }

            /// <summary>
            /// A object used to define how a fish looks
            /// </summary>
            public class Variant : IConvertableToDataTag
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
                /// Converts this <see cref="Variant"/> object into a <see cref="DataPartTag"/>
                /// </summary>
                /// <param name="asType">Not used</param>
                /// <param name="extraConversionData">Not used</param>
                /// <returns>the made <see cref="DataPartTag"/></returns>
                public DataPartTag GetAsTag(ID.NBTTagType? asType, object?[]? extraConversionData)
                {
                    return new DataPartTag(GetValue());
                }

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
            [DataTag]
            public bool? FromBucket { get; set; }
            /// <summary>
            /// The puff state for pufferfish.
            /// 0 = deflated. 1 = halfway puffed-up. 2 = puffed-up.
            /// </summary>
            [DataTag]
            public int? PuffState { get; set; }
        }
    }
}
