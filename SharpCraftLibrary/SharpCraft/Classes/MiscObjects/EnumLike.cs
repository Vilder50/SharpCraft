using System;
using System.Collections.Generic;
using System.Text;
using SharpCraft.Data;

namespace SharpCraft
{
    /// <summary>
    /// Base class for objects which are like enum values
    /// </summary>
    /// <typeparam name="T">The type of enum</typeparam>
    public abstract class EnumLike<T> : IConvertableToDataTag
    {
        /// <summary>
        /// Intializes a new <see cref="EnumLike{T}"/>
        /// </summary>
        /// <param name="value">The enums value</param>
        public EnumLike(T value)
        {
            Value = value;
        }

        /// <summary>
        /// The value
        /// </summary>
        public virtual T Value { get; private set; }

        /// <summary>
        /// Converts the enum object into a data tag
        /// </summary>
        /// <param name="asType">The type to convert to</param>
        /// <param name="extraConversionData">Extra information on how to convert</param>
        /// <returns>The enum object as a data tag</returns>
        public virtual DataPartTag GetAsTag(ID.NBTTagType? asType, object?[] extraConversionData)
        {
            if (asType == ID.NBTTagType.TagString || asType is null)
            {
                return new DataPartTag(ToString());
            } 
            else
            {
                throw new ArgumentException("Cannot convert " + GetType() + " into a data tag of type " + asType.ToString() + ".");
            }
        }

        /// <summary>
        /// Returns a string that represents the current object
        /// </summary>
        /// <returns>A string that represents the current object</returns>
        public override string ToString()
        {
            return Value?.ToString() ?? "";
        }

        /// <summary>
        /// Gets all the enum like values from a static class holding values
        /// </summary>
        /// <typeparam name="J">The enum holder to get values from</typeparam>
        /// <returns>All the enum like values in the class</returns>
        public static IEnumerable<J> GetValuesFromEnumHolder<J>() where J : EnumLike<T>
        {
            Type getEnumOfType = typeof(J);
            foreach(var fieldInfo in getEnumOfType.GetFields())
            {
                if (getEnumOfType.IsAssignableFrom(fieldInfo.FieldType) && fieldInfo.IsStatic)
                {
                    yield return (fieldInfo.GetValue(null) as J)!;
                }
            }
        }

        /// <summary>
        /// Checks if 2 enum likes are the same. (Checks if same type and same tostring output)
        /// </summary>
        /// <param name="obj">The object to check</param>
        /// <returns>True if the are equal</returns>
        public override bool Equals(object? obj)
        {
            if (obj is null)
            {
                return false;
            }

            Type thisType = GetType();
            Type otherType = obj.GetType();
            return thisType.IsAssignableFrom(otherType) && otherType.IsAssignableFrom(thisType) && ToString() == obj.ToString();
        }

        /// <summary>
        /// Returns the hash code for the object
        /// </summary>
        /// <returns>The hash code</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(ToString());
        }
    }

    /// <summary>
    /// Base class for objects which are like enum values but also contains a namespace
    /// </summary>
    /// <typeparam name="T">The type of enum</typeparam>
    public abstract class NamespacedEnumLike<T> : EnumLike<T>
    {
        /// <summary>
        /// Intializes a new <see cref="NamespacedEnumLike{T}"/>
        /// </summary>
        /// <param name="namespace">The namespace its in. If null its in the default minecraft namespace</param>
        /// <param name="value">The enums value</param>
        public NamespacedEnumLike(T value, BasePackNamespace? @namespace = null) : base(value)
        {
            Namespace = @namespace;
        }

        /// <summary>
        /// The namespace its in. If null its in the default minecraft namespace
        /// </summary>
        public virtual BasePackNamespace? Namespace { get; private set; }

        /// <summary>
        /// Returns a string that represents the current object
        /// </summary>
        /// <returns>A string that represents the current object</returns>
        public override string ToString()
        {
            string namespaceString;
            if (Namespace is null)
            {
                namespaceString = "minecraft:";
            }
            else
            {
                namespaceString = Namespace.Name + ":";
            }
            if (Value is null)
            {
                return namespaceString;
            }
            else
            {
                return namespaceString + Value.ToString();
            }
        }
    }
}
