﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;

namespace SharpCraft
{
    public partial class Block : Data.DataHolderBase
    {
        private ID.Block? id;

        /// <summary>
        /// Intilizes a new block object
        /// </summary>
        public Block()
        {
            ID = null;
        }

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

        /// <summary>
        /// Gets a list of this block's state properties
        /// </summary>
        /// <returns>A list of all the state properties for this block</returns>
        public IEnumerable<PropertyInfo> GetStateProperties()
        {
            IEnumerable<PropertyInfo> properties = GetType().GetRuntimeProperties();
            foreach (PropertyInfo property in properties)
            {
                BlockStateAttribute attribute = (BlockStateAttribute)property.GetCustomAttribute(typeof(BlockStateAttribute));
                if (attribute != null)
                {
                    yield return property;
                }
            }
        }

        /// <summary>
        /// The block's ID
        /// </summary>
        public ID.Block? ID
        {
            get => id;
            set
            {
                if (!(value is null) && !FitsBlock(value.Value))
                {
                    throw new ArgumentException("The given block type doesn't fit this block object", nameof(ID));
                }
                id = value;
            }
        }

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
                return GetStateProperties().Any(p => !(p.GetValue(this) is null));
            }
        }

        /// <summary>
        /// Clears the block's state
        /// </summary>
        public void ClearStates()
        {
            IEnumerable<PropertyInfo> properties = GetStateProperties();
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

            IEnumerable<PropertyInfo> properties = GetStateProperties();
            List<string> states = new List<string>();

            foreach (PropertyInfo property in properties)
            {
                object value = property.GetValue(this);
                if (value != null)
                {
                    BlockStateAttribute attribute = ((BlockStateAttribute)property.GetCustomAttribute(typeof(BlockStateAttribute)));
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

            string outputString;
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
        /// Creates a clone of this block with all its data, states and its ID
        /// </summary>
        /// <returns>The new block cloned</returns>
        public Block FullClone()
        {
            Block baseClone = (Block)base.Clone();
            baseClone.ID = ID;

            //add states
            IEnumerable<PropertyInfo> properties = GetType().GetRuntimeProperties();
            foreach (PropertyInfo property in properties)
            {
                BlockStateAttribute attribute = (BlockStateAttribute)property.GetCustomAttribute(typeof(BlockStateAttribute));
                if (!(attribute is null))
                {
                    object value = property.GetValue(this);
                    property.SetValue(baseClone, value);
                }
            }

            return baseClone;
        }

#pragma warning disable IDE0060
        /// <summary>
        /// Tests if the given block type fits this type of block object
        /// </summary>
        /// <param name="block">The block to test</param>
        /// <returns>true if the block fits</returns>
        public static bool FitsBlock(ID.Block block)
        {
            return true;
        }
#pragma warning restore IDE0060

        /// <summary>
        /// Converts a block id into a block
        /// </summary>
        /// <param name="type">The block id to convert</param>
        public static implicit operator Block(ID.Block type)
        {
            return new Block(type);
        }

        private static List<(MethodInfo method, Type block)> fitBlockMethods;

        /// <summary>
        /// Converts a block id into the correct block for the given id
        /// </summary>
        /// <param name="type">the id to convert into a block</param>
        /// <returns>The block</returns>
        public static Block GetFullBlock(ID.Block type)
        {
            if (fitBlockMethods is null)
            {
                fitBlockMethods = new List<(MethodInfo method, Type block)>();
                Type blockType = typeof(Block);
                Type[] types = Assembly.GetAssembly(typeof(Block)).GetTypes().Where(t => t.IsSubclassOf(blockType) && !t.IsAbstract).ToArray();
                foreach (Type classType in types)
                {
                    MethodInfo fitBlockMethod = classType.GetMethod("FitsBlock", BindingFlags.Public | BindingFlags.Static);
                    if (!(fitBlockMethod is null))
                    {
                        fitBlockMethods.Add((fitBlockMethod, classType));
                    }
                }
            }

            foreach((MethodInfo method, Type block) method in fitBlockMethods)
            {
                if ((bool)method.method.Invoke(null, new object[] { type }))
                {
                    return (Block)Activator.CreateInstance(method.block, type);
                }
            }

            return new Block(type);
        }
    }
}