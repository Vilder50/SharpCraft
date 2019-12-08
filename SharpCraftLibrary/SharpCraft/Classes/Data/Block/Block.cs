using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;
using SharpCraft.Data;

namespace SharpCraft
{
    public partial class Block : DataHolderBase, IConvertableToDataObject
    {
        private BlockType id;

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
        public Block(BlockType type)
        {
            ID = type;
        }

        /// <summary>
        /// Creates a new block which is the given type of block
        /// </summary>
        /// <param name="type">The block's ID/Type</param>
        public Block(ID.Block type)
        {
            ID = type;
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
        public BlockType ID
        {
            get => id;
            set
            {
                id = value;
            }
        }

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
                if (!(value is null))
                {
                    BlockStateAttribute attribute = (BlockStateAttribute)property.GetCustomAttribute(typeof(BlockStateAttribute));
                    states.Add(attribute.DataName + "=" + GetStateValue(property));
                }
            }

            return string.Join(",", states);
        }

        /// <summary>
        /// Returns the states value as a string
        /// </summary>
        /// <param name="stateProperty">the property holding the state</param>
        /// <returns></returns>
        public string GetStateValue(PropertyInfo stateProperty)
        {
            object value = stateProperty.GetValue(this);
            if (!(value is null))
            {
                BlockStateAttribute attribute = (BlockStateAttribute)stateProperty.GetCustomAttribute(typeof(BlockStateAttribute));
                if (attribute is null)
                {
                    throw new ArgumentException("The given property is not a state holding property", nameof(stateProperty));
                }
                if (stateProperty.PropertyType == typeof(bool?))
                {
                    return ((bool?)value).ToMinecraftBool();
                }
                else
                {
                    if (value != null)
                    {
                        if (attribute.ForceInt)
                        {
                            return Convert.ToInt32(value).ToString();
                        }
                        else
                        {
                            return value.ToString();
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the raw data used to set block this block
        /// </summary>
        /// <returns>Raw data used by Minecraft</returns>
        public override string ToString()
        {
            if (ID == null)
            {
                throw new ArgumentNullException(nameof(ID) + " has to have a value to convert the block to string");
            }

            string outputString = ID.Name;
            if (HasState) { outputString += "[" + GetStateString() + "]"; }
            if (HasData) { outputString += GetDataString(); }

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
            return false;
        }
#pragma warning restore IDE0060

        /// <summary>
        /// Converts a block id into a block
        /// </summary>
        /// <param name="type">The block id to convert</param>
        public static implicit operator Block(BlockType type)
        {
            return new Block(type);
        }

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
                    return (Block)Activator.CreateInstance(method.block, (BlockType)type);
                }
            }

            return new Block(type);
        }

        /// <summary>
        /// Converts this block into a <see cref="DataPartObject"/>
        /// </summary>
        /// <param name="conversionData">0: the path to the block id, 1: the path to the state holding <see cref="DataPartObject"/>, 2: if it should return in json format</param>
        /// <returns>the made <see cref="DataPartObject"/></returns>
        public DataPartObject GetAsDataObject(object[] conversionData)
        {
            if (conversionData.Length < 2 || conversionData.Length > 3)
            {
                throw new ArgumentException("There has to be exactly 2-3 conversion params to convert a block to a data object.");
            }

            bool isJson = false;
            if (conversionData.Length == 3)
            {
                isJson = (bool)conversionData[2];
            }

            if (conversionData[0] is string idPath && conversionData[1] is string statePath)
            {
                DataPartObject dataObject = new DataPartObject();
                if (!(ID is null))
                {
                    dataObject.AddValue(new DataPartPath(idPath, new DataPartTag(ID, SharpCraft.ID.NBTTagType.TagString, isJson), isJson));
                }
                if (HasState)
                {
                    DataPartObject states = new DataPartObject();
                    dataObject.AddValue(new DataPartPath(statePath, states, isJson));

                    //get state tags
                    PropertyInfo[] properties = GetStateProperties().ToArray();
                    foreach(PropertyInfo property in properties)
                    {
                        BlockStateAttribute attribute = (BlockStateAttribute)property.GetCustomAttribute(typeof(BlockStateAttribute));
                        states.AddValue(new DataPartPath(attribute.DataName, new DataPartTag(GetStateValue(property), isJson: true), isJson));
                    }
                }

                return dataObject;
            }
            else
            {
                throw new ArgumentException("The 2 conversion params has be strings to convert a block to a data object.");
            }
        }
    }
}
