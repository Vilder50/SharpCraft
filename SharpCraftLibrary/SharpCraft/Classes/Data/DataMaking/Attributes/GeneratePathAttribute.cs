using System;

namespace SharpCraft.Data
{
    /// <summary>
    /// Marks a property or method as a datapath generator
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method)]
    public class GeneratePathAttribute : DataConvertionAttribute
    {
        private string generatorName = null!;

        /// <summary>
        /// Marks the property as a datatag holder holding a tag with the given name
        /// </summary>
        /// <param name="generatorName">The name of the static method used for generating the datapath</param>
        /// <param name="forType">The type of tag this generator should generate</param>
        /// <param name="conversionParams">Extra values used for converting the object correctly</param>
        public GeneratePathAttribute(string generatorName, ID.SimpleNBTTagType forType, params object?[] conversionParams) : base(conversionParams)
        {
            GeneratorName = generatorName;
            ForType = forType;
        }

        /// <summary>
        /// The name of the static method used for generating the datapath
        /// </summary>
        public string GeneratorName 
        { 
            get => generatorName;
            private set 
            { 
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Generator name may not be null or empty");
                }
                generatorName = value; 
            }
        }

        /// <summary>
        /// The type of tag this generator should generate
        /// </summary>
        public ID.SimpleNBTTagType ForType { get; set; }
    }
}
