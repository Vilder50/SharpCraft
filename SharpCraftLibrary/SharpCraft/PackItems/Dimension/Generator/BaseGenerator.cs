using System;
using System.Collections.Generic;
using System.Text;
using SharpCraft.Data;

namespace SharpCraft.DimensionObjects
{
    /// <summary>
    /// Base object for terrain generators
    /// </summary>
    public abstract class BaseGenerator : DataHolderBase
    {
        /// <summary>
        /// Intializes a new generator
        /// </summary>
        /// <param name="generatorType">The type of generator</param>
        public BaseGenerator(string generatorType)
        {
            GeneratorType = generatorType;
        }

        /// <summary>
        /// The type of generator
        /// </summary>
        [DataTag("type", ForceType = ID.NBTTagType.TagNamespacedString, JsonTag = true)]
        public string GeneratorType { get; private set; }
    }
}
