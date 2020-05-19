using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.LootObjects
{
    /// <summary>
    /// Base class for objects used for changing a loot table item
    /// </summary>
    public abstract class BaseChange : DataHolderBase
    {
        /// <summary>
        /// Intializes a new <see cref="BaseChange"/>
        /// </summary>
        /// <param name="name">The name of the change</param>
        public BaseChange(string name)
        {
            ChangeName = name;
        }

        /// <summary>
        /// The name of the change
        /// </summary>
        [DataTag("function", ForceType = ID.NBTTagType.TagNamespacedString, JsonTag = true)]
        public string ChangeName { get; private set; }

        /// <summary>
        /// An array conditions which has to be true for the change to take place
        /// </summary>
        [DataTag("conditions", JsonTag = true)]
        public Conditions.BaseCondition[]? Conditions { get; set; }

        /// <summary>
        /// Converts a single change into an array
        /// </summary>
        /// <param name="change">The change to convert</param>
        public static implicit operator BaseChange[] (BaseChange change)
        {
            return new BaseChange[] { change };
        }
    }
}
