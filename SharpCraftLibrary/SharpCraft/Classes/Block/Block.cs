using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// Creates a new block which is the given type of block
        /// </summary>
        /// <param name="type">The block's ID/Type</param>
        public Block(ID.Block? type)
        {
            ID = type;
        }

        /// <summary>
        /// Converts a group of blocks into a block object
        /// </summary>
        /// <param name="group"></param>
        public Block(Group group)
        {
            Group = group;
        }

        internal IEnumerable<PropertyInfo> GetProperties(BlockDataAttribute.DataType propertyType)
        {
            IEnumerable<PropertyInfo> properties = GetType().GetRuntimeProperties();
            foreach (PropertyInfo property in properties)
            {
                BlockDataAttribute attribute = (BlockDataAttribute)property.GetCustomAttribute(typeof(BlockDataAttribute));
                if (attribute != null && attribute.Type == propertyType)
                {
                    yield return property;
                }
            }
        }

        /// <summary>
        /// The block's ID
        /// </summary>
        public ID.Block? ID { get; set; }

        /// <summary>
        /// The name of the block group
        /// </summary>
        public Group Group { get; set; }

        /// <summary>
        /// Checks if the block has any block states defined
        /// </summary>
        public bool HasState
        {
            get
            {
                IEnumerable<PropertyInfo> properties = GetProperties(BlockDataAttribute.DataType.State);
                foreach(PropertyInfo property in properties)
                {
                    if (property.GetValue(this) != null)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        /// <summary>
        /// Checks if the block has any block data defined
        /// </summary>
        public bool HasData
        {
            get
            {
                IEnumerable<PropertyInfo> properties = GetProperties(BlockDataAttribute.DataType.Data);
                foreach (PropertyInfo property in properties)
                {
                    if (property.GetValue(this) != null)
                    {
                        return true;
                    }
                }

                return false;
            }
        }
        /// <summary>
        /// Clears the block's state
        /// </summary>
        public void ClearStates()
        {
            IEnumerable<PropertyInfo> properties = GetProperties(BlockDataAttribute.DataType.State);
            foreach (PropertyInfo property in properties)
            {
                property.SetValue(this, null);
            }
        }

        /// <summary>
        /// Clears the block's data
        /// </summary>
        public void ClearData()
        {
            IEnumerable<PropertyInfo> properties = GetProperties(BlockDataAttribute.DataType.Data);
            foreach (PropertyInfo property in properties)
            {
                property.SetValue(this, null);
            }
        }

        /// <summary>
        /// Gets the raw data for the data the block contains
        /// </summary>
        /// <returns>Raw data used by Minecraft</returns>
        public virtual string GetDataString()
        {
            if (!HasData)
            {
                throw new ArgumentException("The block has no data and can therefore not output it");
            }
            return "";
        }

        /// <summary>
        /// Gets the raw data for the states the block has
        /// </summary>
        /// <returns>Raw data used by Minecraft</returns>
        public string GetStateString()
        {
            if (!HasState)
            {
                throw new ArgumentException("The block has no states and can therefore not output it");
            }

            IEnumerable<PropertyInfo> properties = GetProperties(BlockDataAttribute.DataType.State);
            List<string> states = new List<string>();

            foreach (PropertyInfo property in properties)
            {
                object value = property.GetValue(this);
                if (value != null)
                {
                    BlockDataAttribute attribute = ((BlockDataAttribute)property.GetCustomAttribute(typeof(BlockDataAttribute)));
                    string state = attribute.DataName + "=";
                    if (property.PropertyType == typeof(bool?))
                    {
                        state += ((bool?)value).ToMinecraftBool();
                    }
                    else
                    {
                        if (value != null)
                        {
                            if (attribute.ForceInt)
                            {
                                state += Convert.ToInt32(value);
                            }
                            else
                            {
                                state += value.ToString();
                            }
                        }
                    }
                    states.Add(state);
                }
            }

            return string.Join(",", states);
        }
        /// <summary>
        /// Gets the raw data used to set block this block
        /// </summary>
        /// <returns>Raw data used by Minecraft</returns>
        public override string ToString()
        {
            if (ID == null && Group == null)
            {
                throw new ArgumentNullException(nameof(ID) + " or " + nameof(Group) + " has to have a value to convert the block to string");
            }

            string outputString = "";

            if (ID != null)
            {
                outputString = ID.ToString();
            }
            else
            {
                outputString = "#" + Group.ToString();
            }
            if (HasState) { outputString += "[" + GetStateString() + "]"; }
            if (HasData) { outputString += "{" + GetDataString() + "}"; }

            return outputString;
        }

        /// <summary>
        /// Creates a copy of this block
        /// </summary>
        /// <returns>The copied new block</returns>
        public Block Clone()
        {
            if (Group == null)
            {
                return DataTagAttribute.Clone((Block)Activator.CreateInstance(GetType(), new object[] { ID }), this);
            }
            else
            {
                return DataTagAttribute.Clone((Block)Activator.CreateInstance(GetType(), new object[] { Group }), this);
            }
        }

        /// <summary>
        /// Converts a block id into a block
        /// </summary>
        /// <param name="type">The block id to convert</param>
        public static implicit operator Block(ID.Block type)
        {
            return new Block(type);
        }
    }
}
