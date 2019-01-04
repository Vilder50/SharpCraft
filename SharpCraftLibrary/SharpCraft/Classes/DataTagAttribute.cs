using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace SharpCraft
{
    /// <summary>
    /// An attribute used to mark data.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DataTagAttribute : Attribute
    {
        /// <summary>
        /// Creates a new data marking attribute
        /// </summary>
        public DataTagAttribute()
        {

        }

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
}
