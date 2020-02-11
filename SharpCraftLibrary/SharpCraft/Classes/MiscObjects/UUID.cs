using System;
using SharpCraft.Data;

namespace SharpCraft
{
    /// <summary>
    /// An object used for UUID's
    /// </summary>
    public class UUID : IConvertableToDataObject, IConvertableToDataTag
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
        /// Converts this UUID into a <see cref="DataPartObject"/>
        /// </summary>
        /// <param name="conversionData">0: UUIDMost path, 1: UUIDLeast path</param>
        /// <returns>the made <see cref="DataPartObject"/></returns>
        public DataPartObject GetAsDataObject(object[] conversionData)
        {
            if (conversionData.Length != 2)
            {
                throw new ArgumentException("There has to be exacly 2 conversion params to convert a UUID to a data object.");
            }

            if (conversionData[0] is string most && conversionData[1] is string least)
            {
                DataPartObject dataObject = new DataPartObject();
                dataObject.AddValue(new DataPartPath(most, new DataPartTag(Most)));
                dataObject.AddValue(new DataPartPath(least, new DataPartTag(Least)));


                return dataObject;
            }
            else
            {
                throw new ArgumentException("The 2 conversion params has be strings to convert a UUID to a data object.");
            }
        }

        /// <summary>
        /// Converts this UUID into a <see cref="DataPartTag"/>
        /// </summary>
        /// <param name="asType">The type to convert the uuid into</param>
        /// <param name="extraConversionData">Not used</param>
        /// <returns>the made <see cref="DataPartTag"/></returns>
        public DataPartTag GetAsTag(ID.NBTTagType? asType, object[] extraConversionData)
        {
            if (asType == ID.NBTTagType.TagString)
            {
                return new DataPartTag(UUIDString);
            }
            else
            {
                throw new ArgumentException("Cannot convert the UUID into " + asType);
            }
        }
    }
}
