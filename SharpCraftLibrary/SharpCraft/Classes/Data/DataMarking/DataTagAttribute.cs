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
        private ID.NBTTagType forceType;
        private object[] conversionParams;

        /// <summary>
        /// If the property this attrbite is marking is a <see cref="DataPartObject"/> and it's inside of a <see cref="DataHolderBase"/>. 
        /// Setting this to true will merge them together instead of adding the <see cref="DataPartObject"/> at the end of a path in <see cref="DataHolderBase"/>
        /// </summary>
        public bool Merge;

        /// <summary>
        /// Marks the property as a datatag holder
        /// </summary>
        /// <param name="conversionParams">Extra values used for converting the object correctly</param>
        public DataTagAttribute(params object[] conversionParams)
        {
            ConversionParams = conversionParams;
        }

        /// <summary>
        /// Marks the property as a datatag holder holding a tag with the given name
        /// </summary>
        /// <param name="dataTagName">the name of the data tag its holding</param>
        /// <param name="conversionParams">Extra values used for converting the object correctly</param>
        public DataTagAttribute(string dataTagName, params object[] conversionParams)
        {
            DataTagName = dataTagName;
            ConversionParams = conversionParams;
        }

        /// <summary>
        /// The name of this data tag
        /// </summary>
        public string DataTagName { get; private set; }

        /// <summary>
        /// The type this data actually is
        /// </summary>
        public ID.NBTTagType ForceType
        {
            get
            {
                return forceType;
            }
            set
            {
                forceType = value;
                UseForcedType = true;
            }
        }

        /// <summary>
        /// Extra parameters used for converting the object into the correct type
        /// </summary>
        public object[] ConversionParams
        {
            get
            {
                return conversionParams;
            }
            set
            {
                conversionParams = value ?? throw new ArgumentNullException(nameof(ConversionParams), "ConversionParams may not be null.");
            }
        }

        /// <summary>
        /// If <see cref="ForceType"/> should be used (this will be set if <see cref="ForceType"/> gets set)
        /// </summary>
        public bool UseForcedType { get; set; }

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
