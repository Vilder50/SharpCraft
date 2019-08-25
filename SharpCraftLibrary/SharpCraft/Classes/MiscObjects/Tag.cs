using System;
using SharpCraft.Data;

namespace SharpCraft
{
    /// <summary>
    /// An object used for tags
    /// </summary>
    public class Tag : IConvertableToDataTag
    {
        readonly string Name;
        /// <summary>
        /// Creates a new tag with the given name
        /// </summary>
        /// <param name="TagName">The name of the tag</param>
        public Tag(string TagName)
        {
            Name = TagName;
        }

        /// <summary>
        /// Converts this tag into a <see cref="DataPartTag"/>
        /// </summary>
        /// <param name="extraConversionData">Not used</param>
        /// <param name="asType">The type of tag</param>
        /// <returns>the made <see cref="DataPartTag"/></returns>
        public DataPartTag GetAsTag(ID.NBTTagType? asType, object[] extraConversionData)
        {
            if (asType == ID.NBTTagType.TagString)
            {
                return new DataPartTag(ToString());
            }
            else
            {
                throw new ArgumentException("Cannot convert the tag into a " + asType + " object");
            }
        }

        /// <summary>
        /// Returns the name of the tag
        /// </summary>
        /// <returns>The name of the tag</returns>
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// Converts a string into a <see cref="Tag"/>
        /// </summary>
        /// <param name="tag">the string to convert</param>
        public static implicit operator Tag(string tag)
        {
            return new Tag(tag);
        }

        /// <summary>
        /// Converts a <see cref="Tag"/> into an <see cref="Tag"/> array containing only that one <see cref="Tag"/>
        /// </summary>
        /// <param name="tag">the <see cref="Tag"/> to convert</param>
        public static implicit operator Tag[](Tag tag)
        {
            return new Tag[] { tag };
        }
    }
}
