using System.Collections.Generic;

namespace SharpCraft
{
    /// <summary>
    /// An object for skins for player heads
    /// </summary>
    public class HeadTexture : Data.DataHolderBase
    {
        /// <summary>
        /// No idea
        /// </summary>
        [Data.DataTag]
        public string? Signature { get; set; }

        /// <summary>
        /// Data about the skin bas64 encoded.
        /// </summary>
        [Data.DataTag]
        public string? Value { get; set; }

        /// <summary>
        /// Converts a single <see cref="HeadTexture"/> into an array of textures.
        /// </summary>
        /// <param name="texture">The <see cref="HeadTexture"/> to convert</param>
        public static implicit operator HeadTexture[](HeadTexture texture)
        {
            return new HeadTexture[] { texture };
        }
    }
}
