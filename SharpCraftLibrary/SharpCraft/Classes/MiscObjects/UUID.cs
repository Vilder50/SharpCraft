using System;
using SharpCraft.Data;

namespace SharpCraft
{
    /// <summary>
    /// An object used for UUID's
    /// </summary>
    public class UUID : IConvertableToDataObject, IConvertableToDataTag
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
