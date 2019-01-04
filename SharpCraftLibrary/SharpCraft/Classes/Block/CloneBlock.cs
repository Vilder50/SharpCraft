using System;
using System.Collections.Generic;
using System.Reflection;

namespace SharpCraft
{
    /// <summary>
    /// The base class for a block which data/states can be copied seperatly
    /// </summary>
    /// <typeparam name="T">The type of the class inheriting this class</typeparam>
    public abstract class CloneBlock<T> : Block where T : Block
    {
        /// <summary>
        /// Creates a new block which is the given type of block
        /// </summary>
        /// <param name="type">The block's ID/Type</param>
        public CloneBlock(ID.Block? type) : base(type) { }

        /// <summary>
        /// Converts a group of blocks into a block object
        /// </summary>
        /// <param name="group"></param>
        public CloneBlock(Group group) : base(group) { }

        private T Clone(BlockDataAttribute.DataType cloneType)
        {
            T clonedBlock = (T)Activator.CreateInstance(typeof(T), new object[] { ID });

            IEnumerable<PropertyInfo> properties = GetProperties(cloneType);
            foreach (PropertyInfo property in properties)
            {
                property.SetValue(clonedBlock, property.GetValue(this));
            }

            return clonedBlock;
        }

        /// <summary>
        /// Creates a new block with this block's states
        /// </summary>
        /// <returns>The new block</returns>
        public T CloneState()
        {
            return Clone( BlockDataAttribute.DataType.State);
        }

        /// <summary>
        /// Creates a new block with this block's data
        /// </summary>
        /// <returns>The new block</returns>
        public T CloneData()
        {
            return Clone(BlockDataAttribute.DataType.Data);
        }
    }
}
