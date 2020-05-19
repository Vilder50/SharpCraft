using System;
using System.Collections.Generic;
using System.Text;
using SharpCraft.Data;

namespace SharpCraft
{
    /// <summary>
    /// Used for attributes for items
    /// </summary>
    public class ItemAttribute : AttributeModifier
    {
        /// <summary>
        /// Intializes a new <see cref="ItemAttribute"/>
        /// </summary>
        /// <param name="attribute">The attribute the item should modify</param>
        /// <param name="slot">The slot the item has to be in for the modifier to work</param>
        /// <param name="name">The name of the modifier. Leave null to use UUID as name</param>
        /// <param name="amount">The amount to modify with</param>
        /// <param name="operation">The way to modify</param>
        /// <param name="uuid">The UUID of the modifier. Leave null to generate a UUID.</param>
        public ItemAttribute(ID.AttributeType attribute, ID.AttributeSlot slot, double amount, ID.AttributeOperation operation, string? name = null, UUID? uuid = null)
        {
            Attribute = attribute;
            Slot = slot;
            Amount = amount;
            Operation = operation;
            UUID = uuid ?? new UUID();
            Name = name ?? UUID.UUIDString;
        }

        /// <summary>
        /// Intializes a new empty <see cref="ItemAttribute"/>
        /// </summary>
        public ItemAttribute()
        {

        }

        /// <summary>
        /// The attribute the item should modify
        /// </summary>
        [DataTag("AttributeName", ForceType = ID.NBTTagType.TagString)]
        public ID.AttributeType? Attribute { get; set; }

        /// <summary>
        /// The slot the item has to be in for the modifier to work
        /// </summary>
        [DataTag("Slot", ForceType = ID.NBTTagType.TagString)]
        public ID.AttributeSlot? Slot { get; set; }
    }
}
