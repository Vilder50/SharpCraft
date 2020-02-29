using System;
using System.Collections.Generic;
using System.Text;
using SharpCraft.Data;

namespace SharpCraft
{
    /// <summary>
    /// Used for attributes for entities
    /// </summary>
    public class EntityAttribute : DataHolderBase
    {
        /// <summary>
        /// Intializes a new <see cref="EntityAttribute"/>
        /// </summary>
        /// <param name="attribute">The attribute</param>
        /// <param name="value">The value of the attribute</param>
        /// <param name="modifiers">Modifiers for the attribute</param>
        public EntityAttribute(ID.AttributeType attribute, double value, AttributeModifier?[]? modifiers = null)
        {
            Attribute = attribute;
            Base = value;
            Modifiers = modifiers;
        }

        /// <summary>
        /// Intializes a new empty <see cref="EntityAttribute"/>
        /// </summary>
        public EntityAttribute()
        {

        }

        /// <summary>
        /// The name of the attribute
        /// </summary>
        [DataTag("Name", ForceType = ID.NBTTagType.TagString)]
        public ID.AttributeType? Attribute { get; set; }

        /// <summary>
        /// The base value of the attribute
        /// </summary>
        [DataTag]
        public double? Base { get; set; }

        /// <summary>
        /// Modifiers for modifying the attribute
        /// </summary>
        [DataTag]
        public AttributeModifier?[]? Modifiers { get; set; }
    }
}
