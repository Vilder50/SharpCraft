using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.Commands
{
    /// <summary>
    /// Command which gets the value of an attribute
    /// </summary>
    public class AttributeGetCommand : BaseCommand
    {
        private BaseSelector selector = null!;

        /// <summary>
        /// Intializes a new <see cref="AttributeGetCommand"/>
        /// </summary>
        /// <param name="selector">Selector which selects the entity to get the attribute for</param>
        /// <param name="attribute">The attribute to get</param>
        /// <param name="scale">A value to multiply the attribute with before outputting</param>
        public AttributeGetCommand(BaseSelector selector, ID.AttributeType attribute, double scale)
        {
            Selector = selector;
            Attribute = attribute;
            Scale = scale;
        }

        /// <summary>
        /// Selector which selects the entity to get the attribute for
        /// </summary>
        public BaseSelector Selector
        {
            get => selector;
            set
            {
                if (!(value ?? throw new ArgumentNullException(nameof(Selector), "Selector may not be null.")).IsLimited())
                {
                    throw new ArgumentException("Command doesn't allow selectors which selects multiple entities", nameof(BaseSelector));
                }
                selector = value;
            }
        }

        /// <summary>
        /// The attribute to get
        /// </summary>
        public ID.AttributeType Attribute { get; set; }

        /// <summary>
        /// A value to multiply the attribute with before outputting
        /// </summary>
        public double Scale { get; set; }

        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>attribute [Selector] [Attribute] get [Scale]</returns>
        public override string GetCommandString()
        {
            if (Scale == 1)
            {
                return $"attribute {Selector.GetSelectorString()} {Attribute} get";
            } 
            else
            {
                return $"attribute {Selector.GetSelectorString()} {Attribute} get {Scale.ToMinecraftDouble()}";
            }
        }
    }

    /// <summary>
    /// Command which gets the base value of an attribute
    /// </summary>
    public class AttributeGetBaseCommand : BaseCommand
    {
        private BaseSelector selector = null!;

        /// <summary>
        /// Intializes a new <see cref="AttributeGetBaseCommand"/>
        /// </summary>
        /// <param name="selector">Selector which selects the entity to get the attribute base for</param>
        /// <param name="attribute">The attribute to get the base for</param>
        /// <param name="scale">A value to multiply the attribute base with before outputting</param>
        public AttributeGetBaseCommand(BaseSelector selector, ID.AttributeType attribute, double scale)
        {
            Selector = selector;
            Attribute = attribute;
            Scale = scale;
        }

        /// <summary>
        /// Selector which selects the entity to get the attribute base for
        /// </summary>
        public BaseSelector Selector
        {
            get => selector;
            set
            {
                if (!(value ?? throw new ArgumentNullException(nameof(Selector), "Selector may not be null.")).IsLimited())
                {
                    throw new ArgumentException("Command doesn't allow selectors which selects multiple entities", nameof(BaseSelector));
                }
                selector = value;
            }
        }

        /// <summary>
        /// The attribute to get the base for
        /// </summary>
        public ID.AttributeType Attribute { get; set; }

        /// <summary>
        /// A value to multiply the attribute base with before outputting
        /// </summary>
        public double Scale { get; set; }

        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>attribute [Selector] [Attribute] base get [Scale]</returns>
        public override string GetCommandString()
        {
            if (Scale == 1)
            {
                return $"attribute {Selector.GetSelectorString()} {Attribute} base get";
            }
            else
            {
                return $"attribute {Selector.GetSelectorString()} {Attribute} base get {Scale.ToMinecraftDouble()}";
            }
        }
    }

    /// <summary>
    /// Command which sets the base value of an attribute
    /// </summary>
    public class AttributeSetBaseCommand : BaseCommand
    {
        private BaseSelector selector = null!;

        /// <summary>
        /// Intializes a new <see cref="AttributeSetBaseCommand"/>
        /// </summary>
        /// <param name="selector">Selector which selects the entity to set the attribute base for</param>
        /// <param name="attribute">The attribute base to get</param>
        /// <param name="value">The value to set the base to</param>
        public AttributeSetBaseCommand(BaseSelector selector, ID.AttributeType attribute, double value)
        {
            Selector = selector;
            Attribute = attribute;
            Value = value;
        }

        /// <summary>
        /// Selector which selects the entity to set the attribute base for
        /// </summary>
        public BaseSelector Selector
        {
            get => selector;
            set
            {
                if (!(value ?? throw new ArgumentNullException(nameof(Selector), "Selector may not be null.")).IsLimited())
                {
                    throw new ArgumentException("Command doesn't allow selectors which selects multiple entities", nameof(BaseSelector));
                }
                selector = value;
            }
        }

        /// <summary>
        /// The attribute base to get
        /// </summary>
        public ID.AttributeType Attribute { get; set; }

        /// <summary>
        /// The value to set the base to
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>attribute [Selector] [Attribute] base set [Value]</returns>
        public override string GetCommandString()
        {
            return $"attribute {Selector.GetSelectorString()} {Attribute} base set {Value.ToMinecraftDouble()}";
        }
    }

    /// <summary>
    /// Command which adds an attribute modifier to an entity
    /// </summary>
    public class AttributeAddModifierCommand : BaseCommand
    {
        private BaseSelector selector = null!;
        private UUID uUID = null!;
        private string name = null!;

        /// <summary>
        /// Intializes a new <see cref="AttributeAddModifierCommand"/>
        /// </summary>
        /// <param name="selector">Selector which selects the entity to add the attribute modifier to</param>
        /// <param name="attribute">The attribute to add the modifier to</param>
        /// <param name="name">The name of the modifier</param>
        /// <param name="operation">The modifier's operation</param>
        /// <param name="uuid">The UUID of the modifier</param>
        /// <param name="value">The value of the modifier</param>
        public AttributeAddModifierCommand(BaseSelector selector, ID.AttributeType attribute, UUID uuid, string name, double value, ID.AttributeOperation operation)
        {
            Selector = selector;
            Attribute = attribute;
            UUID = uuid;
            Name = name;
            Value = value;
            Operation = operation;
        }

        /// <summary>
        /// Selector which selects the entity to add the attribute modifier to
        /// </summary>
        public BaseSelector Selector
        {
            get => selector;
            set
            {
                if (!(value ?? throw new ArgumentNullException(nameof(Selector), "Selector may not be null.")).IsLimited())
                {
                    throw new ArgumentException("Command doesn't allow selectors which selects multiple entities", nameof(BaseSelector));
                }
                selector = value;
            }
        }

        /// <summary>
        /// The attribute to add the modifier to
        /// </summary>
        public ID.AttributeType Attribute { get; set; }

        /// <summary>
        /// The UUID of the modifier
        /// </summary>
        public UUID UUID { get => uUID; set => uUID = value ?? throw new ArgumentNullException(nameof(UUID), "UUID may not be null."); }

        /// <summary>
        /// The name of the modifier
        /// </summary>
        public string Name 
        { 
            get => name;
            set 
            {
                if (!Validators.ValidateName(value, true, false, null))
                {
                    throw new ArgumentException("Name contains invalid characters. Only allows letters, numbers and .-_", nameof(Name));
                }
                name = value;
            }
        }

        /// <summary>
        /// The value of the modifier
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// The modifier's operation
        /// </summary>
        public ID.AttributeOperation Operation { get; set; }


        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>attribute [Selector] [Attribute] modifier add [UUID] [Name] [Value] [Operation]</returns>
        public override string GetCommandString()
        {
            if (Operation == ID.AttributeOperation.multiply_total)
            {
                return $"attribute {Selector.GetSelectorString()} {Attribute} modifier add {UUID.UUIDString} {Name} {Value.ToMinecraftDouble()} multiply";
            } 
            else
            {
                return $"attribute {Selector.GetSelectorString()} {Attribute} modifier add {UUID.UUIDString} {Name} {Value.ToMinecraftDouble()} {Operation}";
            }
        }
    }

    /// <summary>
    /// Command which removes an attribute modifier from an entity
    /// </summary>
    public class AttributeRemoveModifierCommand : BaseCommand
    {
        private BaseSelector selector = null!;
        private UUID uUID = null!;

        /// <summary>
        /// Intializes a new <see cref="AttributeRemoveModifierCommand"/>
        /// </summary>
        /// <param name="selector">Selector which selects the entity to remove the attribute modifier from</param>
        /// <param name="attribute">The attribute to remove the modifier from</param>
        /// <param name="uuid">The UUID of the modifier</param>
        public AttributeRemoveModifierCommand(BaseSelector selector, ID.AttributeType attribute, UUID uuid)
        {
            Selector = selector;
            Attribute = attribute;
            UUID = uuid;
        }

        /// <summary>
        /// Selector which selects the entity to remove the attribute modifier from
        /// </summary>
        public BaseSelector Selector
        {
            get => selector;
            set
            {
                if (!(value ?? throw new ArgumentNullException(nameof(Selector), "Selector may not be null.")).IsLimited())
                {
                    throw new ArgumentException("Command doesn't allow selectors which selects multiple entities", nameof(BaseSelector));
                }
                selector = value;
            }
        }

        /// <summary>
        /// The attribute to remove the modifier from
        /// </summary>
        public ID.AttributeType Attribute { get; set; }

        /// <summary>
        /// The UUID of the modifier
        /// </summary>
        public UUID UUID { get => uUID; set => uUID = value ?? throw new ArgumentNullException(nameof(UUID), "UUID may not be null."); }


        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>attribute [Selector] [Attribute] modifier remove [UUID]</returns>
        public override string GetCommandString()
        {
            return $"attribute {Selector.GetSelectorString()} {Attribute} modifier remove {UUID.UUIDString}";
        }
    }

    /// <summary>
    /// Command which gets an attribute modifier from an entity
    /// </summary>
    public class AttributeGetModifierCommand : BaseCommand
    {
        private BaseSelector selector = null!;
        private UUID uUID = null!;

        /// <summary>
        /// Intializes a new <see cref="AttributeGetModifierCommand"/>
        /// </summary>
        /// <param name="selector">Selector which selects the entity to get the attribute modifier from</param>
        /// <param name="attribute">The attribute to get the modifier from</param>
        /// <param name="uuid">The UUID of the modifier</param>
        /// <param name="scale">A value to multiply the attribute modifier with before outputting</param>
        public AttributeGetModifierCommand(BaseSelector selector, ID.AttributeType attribute, UUID uuid, double scale)
        {
            Selector = selector;
            Attribute = attribute;
            UUID = uuid;
            Scale = scale;
        }

        /// <summary>
        /// Selector which selects the entity to get the attribute modifier from
        /// </summary>
        public BaseSelector Selector
        {
            get => selector;
            set
            {
                if (!(value ?? throw new ArgumentNullException(nameof(Selector), "Selector may not be null.")).IsLimited())
                {
                    throw new ArgumentException("Command doesn't allow selectors which selects multiple entities", nameof(BaseSelector));
                }
                selector = value;
            }
        }

        /// <summary>
        /// The attribute to get the modifier from
        /// </summary>
        public ID.AttributeType Attribute { get; set; }

        /// <summary>
        /// The UUID of the modifier
        /// </summary>
        public UUID UUID { get => uUID; set => uUID = value ?? throw new ArgumentNullException(nameof(UUID), "UUID may not be null."); }

        /// <summary>
        /// A value to multiply the attribute modifier with before outputting
        /// </summary>
        public double Scale { get; set; }


        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>attribute [Selector] [Attribute] modifier get [UUID] [Scale]</returns>
        public override string GetCommandString()
        {
            if (Scale == 1)
            {
                return $"attribute {Selector.GetSelectorString()} {Attribute} modifier value get {UUID.UUIDString}";
            }
            else
            {
                return $"attribute {Selector.GetSelectorString()} {Attribute} modifier value get {UUID.UUIDString} {Scale.ToMinecraftDouble()}";
            }
        }
    }
}
