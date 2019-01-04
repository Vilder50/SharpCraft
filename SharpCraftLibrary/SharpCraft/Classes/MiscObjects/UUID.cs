using System;

namespace SharpCraft
{
    /// <summary>
    /// An object used for UUID's
    /// </summary>
    public class UUID
    {
        /// <summary>
        /// the UUIDLeast
        /// </summary>
        public readonly long Least;

        /// <summary>
        /// the UUIDMost
        /// </summary>
        public readonly long Most;
        private readonly string UUIDString;

        /// <summary>
        /// Creates a UUID Out of a UUIDLeast and a UUIDMost
        /// </summary>
        /// <param name="Least">the UUIDLeast</param>
        /// <param name="Most">the UUIDMost</param>
        public UUID(long Least, long Most)
        {
            this.Least = Least;
            this.Most = Most;
            string MostString = ("0000000000000000" + Most.ToString("x"));
            string LeastString = ("0000000000000000" + Least.ToString("x"));
            UUIDString = MostString.Substring(MostString.Length - 16, 16) + LeastString.Substring(MostString.Length - 16, 16);
            UUIDString = UUIDString.Insert(8, "-").Insert(13, "-").Insert(18, "-").Insert(23, "-");
        }

        /// <summary>
        /// Creates a uuid object out of a UUID in a string format
        /// </summary>
        /// <param name="UUID">the string to convert</param>
        public UUID(string UUID)
        {
            try
            {
                Least = long.Parse(UUID.Substring(19).Replace("-", ""), System.Globalization.NumberStyles.HexNumber);
                Most = long.Parse(UUID.Substring(0, 18).Replace("-", ""), System.Globalization.NumberStyles.HexNumber);
                UUIDString = UUID;
            }
            catch
            {
                throw new Exception("Unable to convert string to an UUID");
            }
        }

        /// <summary>
        /// Returns the uuid in string format
        /// </summary>
        /// <returns>The uuid as a string</returns>
        public override string ToString()
        {
            return UUIDString;
        }
    }
}
