﻿using System;
using SharpCraft.Data;

namespace SharpCraft
{
    /// <summary>
    /// An object used for tags
    /// </summary>
    public class Tag : IConvertableToDataTag
    {
        private string name = null!;
        /// <summary>
        /// Creates a new tag with the given name
        /// </summary>
        /// <param name="TagName">The name of the tag</param>
        public Tag(string TagName)
        {
            Name = TagName;
        }

        /// <summary>
        /// The name of the tag
        /// </summary>
        public string Name
        {
            get => name;
            protected set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Tag name may not be null or whitespace", nameof(Name));
                }
                if (!Validators.ValidateName(value, true, false, null))
                {
                    throw new ArgumentException("Tag name is invalid. Only accepts letters, numbers and -._");
                }
                name = value;
            }
        }

        /// <summary>
        /// Converts this tag into a <see cref="DataPartTag"/>
        /// </summary>
        /// <param name="extraConversionData">set to <see cref="ID.NBTTagType.TagString"/></param>
        /// <param name="asType">The type of tag</param>
        /// <returns>the made <see cref="DataPartTag"/></returns>
        public DataPartTag GetAsTag(ID.NBTTagType? asType, object?[] extraConversionData)
        {
            if (asType == ID.NBTTagType.TagString)
            {
                return new DataPartTag(Name);
            }
            else
            {
                throw new ArgumentException("Cannot convert the tag into a " + asType + " object");
            }
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

        /// <summary>
        /// Converts a <see cref="Tag"/> into a <see cref="Selector.EntityTag"/>
        /// </summary>
        /// <param name="tag">the <see cref="SharpCraft.Tag"/> to convert</param>
        public static implicit operator Selector.EntityTag(Tag tag)
        {
            return new Selector.EntityTag(tag);
        }

        /// <summary>
        /// Checks if the given tag doesn't exist on the selected entity.
        /// </summary>
        /// <param name="tag">The tag to check if doesn't exist</param>
        /// <returns><see cref="Selector.EntityTag"/> used for checking if the tag doesn't exist</returns>
        public static Selector.EntityTag operator !(Tag tag)
        {
            return new Selector.EntityTag(tag, false);
        }
    }
}
