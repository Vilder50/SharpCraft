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
    public class DataTagAttribute : DataConvertionAttribute
    {
        /// <summary>
        /// Marks the property as a datatag holder
        /// </summary>
        /// <param name="conversionParams">Extra values used for converting the object correctly</param>
        public DataTagAttribute(params object?[] conversionParams) : base(conversionParams)
        {
            
        }

        /// <summary>
        /// Marks the property as a datatag holder holding a tag with the given name
        /// </summary>
        /// <param name="dataTagName">the name of the data tag its holding</param>
        /// <param name="conversionParams">Extra values used for converting the object correctly</param>
        public DataTagAttribute(string? dataTagName, params object?[] conversionParams) : base(conversionParams)
        {
            DataTagName = dataTagName;
        }

        /// <summary>
        /// The name of this data tag
        /// </summary>
        public string? DataTagName { get; private set; }

        /// <summary>
        /// True if the path name should be encapsulated in "'s. (Default) False if it shouldn't
        /// </summary>
        public bool JsonTag { get; set; }

        /// <summary>
        /// Clones all properties with a <see cref="DataTagAttribute"/> from one object to another
        /// </summary>
        /// <typeparam name="T">The type of object to clone</typeparam>
        /// <param name="emptyCopy">An empty object to clone the properties to</param>
        /// <param name="copy">The object to clone</param>
        /// <returns><paramref name="emptyCopy"/></returns>
        public static T Clone<T>(T emptyCopy, T copy)
        {
            if (copy is null)
            {
                throw new ArgumentNullException(nameof(copy), "Copy may not be null");
            }

            IEnumerable<PropertyInfo> properties = copy.GetType().GetRuntimeProperties();
            foreach (PropertyInfo property in properties)
            {
                DataTagAttribute? attribute = (DataTagAttribute?)property.GetCustomAttribute(typeof(DataTagAttribute));
                if (attribute != null)
                {
                    property.SetValue(emptyCopy, property.GetValue(copy));
                }
            }

            return emptyCopy;
        }
    }
}
