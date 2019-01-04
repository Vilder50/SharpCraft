using System;

namespace SharpCraft
{
    /// <summary>
    /// An attribute used to mark a block's state properties and data properties
    /// </summary>
    public class BlockDataAttribute : DataTagAttribute
    {
        /// <summary>
        /// The type of data the attribute can be used by
        /// </summary>
        public enum DataType
        {
            /// <summary>
            /// It's block data
            /// </summary>
            Data,
            /// <summary>
            /// It's a block state
            /// </summary>
            State
        }
        /// <summary>
        /// The type of data the attribute is being used by
        /// </summary>
        public readonly DataType Type;
        /// <summary>
        /// The name of the state the attribute is being used by
        /// </summary>
        public readonly string DataName;
        /// <summary>
        /// If true converts it to an int instead of a string
        /// </summary>
        public readonly bool ForceInt;
        /// <summary>
        /// Creates an attribute marking a block's data 
        /// </summary>
        public BlockDataAttribute()
        {
            Type = DataType.Data;
        }

        /// <summary>
        /// Creates an attribute marking a block's state
        /// </summary>
        /// <param name="stateName">The Minecraft name of the state</param>
        /// <param name="toInt">If it should convert the state into an int</param>
        public BlockDataAttribute(string stateName, bool toInt = false)
        {
            Type = DataType.State;
            DataName = stateName;
            ForceInt = toInt;
        }
    }
}
