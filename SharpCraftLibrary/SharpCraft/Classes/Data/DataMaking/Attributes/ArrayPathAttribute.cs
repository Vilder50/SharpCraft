using System;

namespace SharpCraft.Data
{
    /// <summary>
    /// Marks a property as a datapath array indexer
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ArrayPathAttribute : DataConvertionAttribute
    {
        /// <summary>
        /// Marks the property as a datatag holder holding a tag with the given name
        /// </summary>
        /// <param name="index">The index the property is marking</param>
        /// <param name="conversionParams">Extra values used for converting the object correctly</param>
        public ArrayPathAttribute(int index, params object?[] conversionParams) : base(conversionParams)
        {
            Index = index;
        }

        /// <summary>
        /// The index the property is marking
        /// </summary>
        public int Index { get; private set; }
    }
}
