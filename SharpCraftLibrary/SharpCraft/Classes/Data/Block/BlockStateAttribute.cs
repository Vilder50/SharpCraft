using System;

namespace SharpCraft.Blocks
{
    /// <summary>
    /// An attribute used to mark a block's state properties
    /// </summary>
    public class BlockStateAttribute : Attribute
    {
        /// <summary>
        /// The name of the state the attribute is being used by
        /// </summary>
        public readonly string DataName;
        /// <summary>
        /// If true converts it to an int instead of a string
        /// </summary>
        public readonly bool ForceInt;

        /// <summary>
        /// Creates an attribute marking a block's state
        /// </summary>
        /// <param name="stateName">The Minecraft name of the state</param>
        /// <param name="toInt">If it should convert the state into an int</param>
        public BlockStateAttribute(string stateName, bool toInt = false)
        {
            DataName = stateName;
            ForceInt = toInt;
        }
    }

    /// <summary>
    /// An attribute used for marking the range a block state can be in
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class BlockIntStateRange : Attribute
    {
        /// <summary>
        /// Intializes a new <see cref="BlockIntStateRange"/> for defining the range of the state
        /// </summary>
        /// <param name="intRange">the range the int can be in</param>
        public BlockIntStateRange(MCRange intRange)
        {
            IntRange = intRange;
        }

        /// <summary>
        /// Intializes a new <see cref="BlockIntStateRange"/> for defining the range of the state
        /// </summary>
        /// <param name="min">the minimum the number can be</param>
        /// <param name="max">the maximum the number can be</param>
        public BlockIntStateRange(int min, int max)
        {
            IntRange = new MCRange(min, max);
        }

        /// <summary>
        /// the range the int can be in
        /// </summary>
        public MCRange IntRange { get; set; }
    }
}
