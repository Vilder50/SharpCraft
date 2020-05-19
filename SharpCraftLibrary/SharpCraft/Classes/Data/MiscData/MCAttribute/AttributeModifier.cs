using System;
using System.Collections.Generic;
using System.Text;
using SharpCraft.Data;

namespace SharpCraft
{
    /// <summary>
    /// Used for modifying an attribute
    /// </summary>
    public class AttributeModifier : DataHolderBase
    {
        /// <summary>
        /// Intializes a new <see cref="AttributeModifier"/>
        /// </summary>
        /// <param name="name">The name of the modifier. Leave null to use UUID as name</param>
        /// <param name="amount">The amount to modify with</param>
        /// <param name="operation">The way to modify</param>
        /// <param name="uuid">The UUID of the modifier. Leave null to generate a UUID.</param>
        public AttributeModifier(double amount, ID.AttributeOperation operation, string? name = null, UUID? uuid = null)
        {
            Amount = amount;
            Operation = operation;
            UUID = uuid ?? new UUID();
            Name = name ?? UUID.UUIDString;
        }

        /// <summary>
        /// Intializes a new empty <see cref="AttributeModifier"/>
        /// </summary>
        public AttributeModifier()
        {

        }

        /// <summary>
        /// The name of the modifier
        /// </summary>
        [DataTag]
        public string? Name { get; set; }

        /// <summary>
        /// The amount to modify with
        /// </summary>
        [DataTag]
        public double? Amount { get; set; }

        /// <summary>
        /// The operation used for modifying
        /// </summary>
        [DataTag(ForceType = ID.NBTTagType.TagInt)]
        public ID.AttributeOperation? Operation { get; set; }

        /// <summary>
        /// The UUID of the modifier
        /// </summary>
        [DataTag((object)"UUIDMost", "UUIDLeast", Merge = true)]
        public UUID? UUID { get; set; }
    }
}
