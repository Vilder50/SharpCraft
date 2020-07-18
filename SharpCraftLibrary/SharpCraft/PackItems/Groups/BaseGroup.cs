using System;
using System.IO;
using System.Collections.Generic;
using SharpCraft.Data;

namespace SharpCraft
{
    /// <summary>
    /// Interface for groups of an item
    /// </summary>
    public interface IGroup<TItem> : IConvertableToDataTag, IGroupable where TItem : IGroupable
    {

    }

    /// <summary>
    /// Interface for items which can be in a Minecraft group
    /// </summary>
    public interface IGroupable: Data.IConvertableToDataObject
    {
        /// <summary>
        /// The name Minecraft uses for the item
        /// </summary>
        string Name { get; }

        /// <summary>
        /// If the object is a group object
        /// </summary>
        bool IsAGroup { get; }

        /// <summary>
        /// Converts this type into a <see cref="DataPartObject"/>
        /// </summary>
        /// <param name="conversionData">0: tag name if id. 1: tag name if group. 2: if json</param>
        /// <returns></returns>
        public DataPartObject GetGroupData(object?[] conversionData)
        {
            if (conversionData is null)
            {
                throw new ArgumentNullException(nameof(conversionData), "ConversionData may not be null");
            }

            if (conversionData.Length != 3)
            {
                throw new ArgumentOutOfRangeException(nameof(conversionData), "Need to get 3 conversion data.");
            }
            bool? json = conversionData[2] as bool?;
            if (json is null)
            {
                throw new ArgumentException(nameof(conversionData), "3rd conversion object has to be a bool");
            }

            DataPartObject returnObject = new DataPartObject();
            if (IsAGroup)
            {
                returnObject.AddValue(new DataPartPath(conversionData[1]!.ToString()!, new DataPartTag(Name.TrimStart('#'), isJson: json.Value), json.Value));
            }
            else
            {
                returnObject.AddValue(new DataPartPath(conversionData[0]!.ToString()!, new DataPartTag(Name, isJson: json.Value), json.Value));
            }
            return returnObject;
        }
    }

    /// <summary>
    /// Abstract class for Groups of an item.
    /// </summary>
    /// <typeparam name="TItem">The item the group is for</typeparam>
    public abstract class BaseGroup<TItem> : BaseFile<TextWriter>, IGroup<TItem> where TItem : IGroupable
    {
        private List<TItem> items = null!;
        private bool appendGroup;
        private readonly bool halfLockProperties;

        /// <summary>
        /// Intializes a new Group with the given parameters
        /// </summary>
        /// <param name="packNamespace">The namespace the group is in</param>
        /// <param name="fileName">The name of the group file</param>
        /// <param name="writeSetting">The settings for how to write this file</param>
        /// <param name="items">The items in this group</param>
        /// <param name="appendGroup">If this group should append other groups of the same type and same name from other datapacks</param>
        /// <param name="fileType">The type of group file</param>
        public BaseGroup(BasePackNamespace packNamespace, string? fileName, List<TItem> items, bool appendGroup, WriteSetting writeSetting, string fileType) : base(packNamespace, fileName, writeSetting, "group_" + fileType)
        {
            Items = items;
            AppendGroup = appendGroup;
            halfLockProperties = true;
        }

        /// <summary>
        /// The items in this group
        /// </summary>
        public List<TItem> Items
        {
            get
            {
                return items;
            }
            set
            {
                if (halfLockProperties)
                {
                    ThrowExceptionOnInvalidChange();
                }
                items = value ?? throw new ArgumentNullException(nameof(Items), "Items may not be null");
            }
        }

        /// <summary>
        /// Converts this type into a <see cref="DataPartObject"/>
        /// </summary>
        /// <param name="conversionData">0: tag name if id. 1: tag name if group. 2: if json</param>
        /// <returns></returns>
        public virtual DataPartObject GetAsDataObject(object?[] conversionData)
        {
            return (this as IGroupable).GetGroupData(conversionData);
        }

        /// <summary>
        /// If this group should append other groups of the same type and same name from other datapacks
        /// </summary>
        public bool AppendGroup
        {
            get
            {
                return appendGroup;
            }
            set
            {
                if (halfLockProperties)
                {
                    ThrowExceptionOnInvalidChange();
                }
                appendGroup = value;
            }
        }

        /// <summary>
        /// Returns <see cref="GetNamespacedName"/>
        /// </summary>
        [CompoundPath(1)]
        public string Name => GetNamespacedName();

        /// <summary>
        /// Marks this as being a group object
        /// </summary>
        public bool IsAGroup => true;

        /// <summary>
        /// Writes the file
        /// </summary>
        /// <param name="stream">The stream used for writing the file</param>
        protected override void WriteFile(TextWriter stream)
        {
            stream.Write("{");
            if (!AppendGroup)
            {
                stream.Write("\"replace\":true,");
            }

            List<string> names = new List<string>();
            foreach (TItem item in Items)
            {
                names.Add("\"" + item.Name + "\"");
            }
            stream.Write("\"values\":[" + string.Join(",",names) + "]}");
        }

        /// <summary>
        /// Returns the namespaced name of this file
        /// </summary>
        /// <returns>The namespaced name of this file</returns>
        public new string GetNamespacedName()
        {
            return "#" + PackNamespace.Name + ":" + WritePath;
        }

        /// <summary>
        /// Clears the things in the file.
        /// </summary>
        protected override void AfterDispose()
        {
            items = null!;
        }
    }
}
