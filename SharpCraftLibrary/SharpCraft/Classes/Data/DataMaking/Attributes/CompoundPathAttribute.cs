using System;

namespace SharpCraft.Data
{
    /// <summary>
    /// Marks a property as a datapath
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class CompoundPathAttribute : DataConvertionAttribute
    {
        /// <summary>
        /// Marks the property as a datatag holder holding a tag with the given name
        /// </summary>
        /// <param name="dataTagName">the name of the datapath to the value</param>
        /// <param name="conversionParams">Extra values used for converting the object correctly</param>
        public CompoundPathAttribute(string dataTagName, params object?[] conversionParams) : base(conversionParams)
        {
            DataTagName = dataTagName;
        }

        /// <summary>
        /// Marks the property as a datatag holder holding a tag with the given name
        /// </summary>
        /// <param name="index">An index in the parents conversionparams to use for the datapath</param>
        /// <param name="conversionParams">Extra values used for converting the object correctly</param>
        public CompoundPathAttribute(int index, params object?[] conversionParams) : base(conversionParams)
        {
            ConversionIndex = index;
        }

        /// <summary>
        /// The name of this data tag
        /// </summary>
        public string? DataTagName { get; private set; }

        /// <summary>
        /// An index in the parents conversionparams to use for the datapath
        /// </summary>
        public int? ConversionIndex { get; private set; }
    }
}
