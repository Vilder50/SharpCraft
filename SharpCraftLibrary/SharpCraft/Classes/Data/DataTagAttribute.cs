using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace SharpCraft.Data
{
    /// <summary>
    /// An attribute used to mark NBT data tags.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DataTagAttribute : Attribute
    {
        /// <summary>
        /// Marks the property as a datatag holder
        /// </summary>
        public DataTagAttribute()
        {

        }

        /// <summary>
        /// Marks the property as a datatag holder holding a tag with the given name
        /// </summary>
        /// <param name="dataTagName">the name of the data tag its holding</param>
        public DataTagAttribute(string dataTagName)
        {
            DataTagName = dataTagName;
        }

        /// <summary>
        /// The name of this data tag
        /// </summary>
        public string DataTagName { get; }

        /// <summary>
        /// The type this data actually is
        /// </summary>
        public ID.NBTTagType ForceType { get; set; }

        /// <summary>
        /// Clones all properties with a <see cref="DataTagAttribute"/> from one object to another
        /// </summary>
        /// <typeparam name="T">The type of object to clone</typeparam>
        /// <param name="emptyCopy">An empty object to clone the properties to</param>
        /// <param name="copy">The object to clone</param>
        /// <returns></returns>
        public static T Clone<T>(T emptyCopy, T copy)
        {
            IEnumerable<PropertyInfo> properties = copy.GetType().GetRuntimeProperties();
            foreach (PropertyInfo property in properties)
            {
                DataTagAttribute attribute = (DataTagAttribute)property.GetCustomAttribute(typeof(DataTagAttribute));
                if (attribute != null)
                {
                    property.SetValue(emptyCopy, property.GetValue(copy));
                }
            }

            return emptyCopy;
        }
    }

    /// <summary>
    /// An attribute used to mark customly made NBT data tags
    /// </summary>
    public class CustomDataTagAttribute : DataTagAttribute
    {

    }
}
