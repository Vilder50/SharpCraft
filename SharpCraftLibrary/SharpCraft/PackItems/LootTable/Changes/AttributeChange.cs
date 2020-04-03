using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.LootObjects
{
    /// <summary>
    /// Changes the attributes on the item
    /// </summary>
    public class AttributeChange : BaseChange
    {
        private Attribute[] attributes;

        /// <summary>
        /// Intializes a new <see cref="DecayChange"/>
        /// </summary>
        /// <param name="attributes">An array of attributes the item should get</param>
        public AttributeChange(Attribute[] attributes) : base("set_attributes")
        {
            Attributes = attributes;
        }

        /// <summary>
        /// An array of attributes the item should get
        /// </summary>
        [DataTag("modifiers", JsonTag = true)]
        public Attribute[] Attributes { get => attributes; set => attributes = value ?? throw new ArgumentNullException(nameof(Attributes), "Attributes may not be null"); }

        /// <summary>
        /// Class for attribute an item can get
        /// </summary>
        public class Attribute : DataHolderBase
        {
            private MCRange valueRange;

            /// <summary>
            /// Intializes a new <see cref="Attribute"/>
            /// </summary>
            /// <param name="type">The type of attribute</param>
            /// <param name="operation">The attribute operation to use</param>
            /// <param name="value">The value to use in the operation</param>
            /// <param name="slot">Choses a random slot from the array the attribute effect works in</param>
            public Attribute(ID.AttributeType type, ID.AttributeOperation operation, MCRange value, ID.AttributeSlot[] slot)
            {
                Type = type;
                Operation = operation;
                Value = value;
                Slot = slot;
            }

            /// <summary>
            /// Intializes a new <see cref="Attribute"/>
            /// </summary>
            /// <param name="type">The type of attribute</param>
            /// <param name="operation">The attribute operation to use</param>
            /// <param name="value">The value to use in the operation</param>
            /// <param name="slot">The slot the attribute effect works in</param>
            public Attribute(ID.AttributeType type, ID.AttributeOperation operation, MCRange value, ID.AttributeSlot slot)
            {
                Type = type;
                Operation = operation;
                Value = value;
                Slot = new ID.AttributeSlot[] { slot };
            }

            /// <summary>
            /// The type of attribute
            /// </summary>
            [DataTag("attribute", ForceType = ID.NBTTagType.TagString, JsonTag = true)]
            public ID.AttributeType Type { get; set; }

            /// <summary>
            /// The name of the attribute
            /// </summary>
            [DataTag("name", ForceType = ID.NBTTagType.TagString, JsonTag = true)]
            public ID.AttributeType Name { get => Type; }

            /// <summary>
            /// The attribute operation to use
            /// </summary>
            [DataTag("operation", ForceType = ID.NBTTagType.TagString, JsonTag = true)]
            public ID.AttributeOperation Operation { get; set; }

            /// <summary>
            /// The value to use in the operation
            /// </summary>
            [DataTag("amount", "min", "max", ID.NBTTagType.TagDouble, true, JsonTag = true)]
            public MCRange Value { get => valueRange; set => valueRange = value ?? throw new ArgumentNullException(nameof(Value), "Value may not be null"); }

            /// <summary>
            /// The UUID of the attribute. Leave empty to generate random one
            /// </summary>
            [Data.DataTag("UUID", ForceType = ID.NBTTagType.TagIntArray)]
            public UUID UUID { get; set; }

            /// <summary>
            /// Choses a random slot from the array the attribute effect works in
            /// </summary>
            [DataTag("slot", ForceType = ID.NBTTagType.TagStringArray, JsonTag = true)]
            public ID.AttributeSlot[] Slot { get; set; }
        }
    }
}
