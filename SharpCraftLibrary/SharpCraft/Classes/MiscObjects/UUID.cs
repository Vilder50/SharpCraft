using System;
using SharpCraft.Data;

namespace SharpCraft
{
    /// <summary>
    /// An object used for UUID's
    /// </summary>
    public class UUID :  IConvertableToDataArray
    {
        #region random
        private static Random random = new Random(0);

        /// <summary>
        /// Sets the seed for the randomiser whcih generates random UUID's
        /// </summary>
        public static void SetRandomSeed(int seed)
        {
            random = new Random(seed);
        }

        /// <summary>
        /// Intializes a new random <see cref="UUID"/>
        /// </summary>
        public UUID()
        {
            Least = random.Next();
            Most = random.Next();
            UUIDString = CreateUUIDString(Most, Least);
        }
        #endregion

        /// <summary>
        /// Creates a UUID Out of a UUIDLeast and a UUIDMost
        /// </summary>
        /// <param name="least">the UUIDLeast</param>
        /// <param name="most">the UUIDMost</param>
        public UUID(long most, long least)
        {
            Least = least;
            Most = most;
            UUIDString = CreateUUIDString(most, least);
        }

        /// <summary>
        /// Creates a uuid object out of a UUID in a string format
        /// </summary>
        /// <param name="uuid">the string to convert</param>
        public UUID(string uuid)
        {
            if (uuid is null)
            {
                throw new ArgumentNullException("uuid may not be null", nameof(uuid));
            }
            try
            {
                Most = long.Parse(uuid.Replace("-", "").Substring(0, 16), System.Globalization.NumberStyles.HexNumber);
                Least = long.Parse(uuid.Replace("-", "").Substring(16,16), System.Globalization.NumberStyles.HexNumber);
                UUIDString = CreateUUIDString(Most, Least);
            }
            catch
            {
                throw new Exception("Unable to convert string to an UUID");
            }
        }

        /// <summary>
        /// Intializes a new <see cref="UUID"/> from 4 ints
        /// </summary>
        /// <param name="a">int part</param>
        /// <param name="b">int part</param>
        /// <param name="c">int part</param>
        /// <param name="d">int part</param>
        public UUID(int a, int b, int c, int d)
        {
            Most = a << 32 & b;
            Least = c << 32 & d;
        }

        /// <summary>
        /// the UUIDLeast
        /// </summary>
        public long Least { get; protected set; }

        /// <summary>
        /// the UUIDMost
        /// </summary>
        public long Most { get; protected set; }

        /// <summary>
        /// The UUID string
        /// </summary>
        public string UUIDString { get; protected set; }

        private string CreateUUIDString(long most, long least)
        {
            string mostString = ("0000000000000000" + most.ToString("x"))[^16..];
            string leastString = ("0000000000000000" + least.ToString("x"))[^16..];

            string uuid = mostString + leastString;
            uuid = uuid.Insert(8, "-").Insert(13, "-").Insert(18, "-").Insert(23, "-");

            return uuid;
        }

        /// <summary>
        /// Returns this uuid as an array
        /// </summary>
        /// <returns>This uuid as an array</returns>
        public int[] GetUUIDAsInts()
        {
            return new int[]
            {
                (int)(Most >> 32),
                (int)(Most),
                (int)(Least >> 32),
                (int)(Least)
            };
        }

        /// <summary>
        /// Converts this uuid into a <see cref="DataPartArray"/>
        /// </summary>
        /// <param name="asType">Not used</param>
        /// <param name="extraConversionData">Not used</param>
        /// <returns>This uuid as an int array</returns>
        public DataPartArray GetAsArray(ID.NBTTagType? asType, object[] extraConversionData)
        {
            return new DataPartArray(GetUUIDAsInts(),null,extraConversionData);
        }
    }
}
