using System;

namespace SharpCraft.Data
{
    /// <summary>
    /// Base class for datapath related attributes
    /// </summary>
    public abstract class DataConvertionAttribute : Attribute
    {
        /// <summary>
        /// Types of datatags
        /// </summary>
        public enum TagType
        {
            /// <summary>
            /// Array tag
            /// </summary>
            Array,
            /// <summary>
            /// Compound tag
            /// </summary>
            Compound,
            /// <summary>
            /// Normal tag
            /// </summary>
            Tag,
            /// <summary>
            /// Unknown tag type
            /// </summary>
            Unknown,
        }

        private ID.NBTTagType forceType;
        private object?[] conversionParams = null!;

        /// <summary>
        /// If the property this attrbite is marking is a <see cref="DataPartObject"/> and it's inside of a <see cref="DataHolderBase"/>. 
        /// Setting this to true will merge them together instead of adding the <see cref="DataPartObject"/> at the end of a path in <see cref="DataHolderBase"/>
        /// </summary>
        public bool Merge;

        /// <summary>
        /// Intializes a new <see cref="DataConvertionAttribute"/>
        /// </summary>
        /// <param name="conversionParams">Extra values used for converting the object correctly</param>
        protected DataConvertionAttribute(params object?[] conversionParams)
        {
            ConversionParams = conversionParams;
        }

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
        public object?[] ConversionParams
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
        /// The type of <see cref="ForceType"/>.
        /// </summary>
        /// <returns>The type of forced type</returns>
        public TagType ConvertType()
        {
            if (!UseForcedType)
            {
                return TagType.Unknown;
            }
            if (ForceType == ID.NBTTagType.TagCompound)
            {
                return TagType.Compound;
            }
            else if ((int)ForceType >= 100)
            {
                return TagType.Array;
            }
            else
            {
                return TagType.Tag;
            }
        }
    }
}
