﻿using System;
using System.IO;
using System.Collections.Generic;

namespace SharpCraft
{
    /// <summary>
    /// Interface for groups of an item
    /// </summary>
    public interface IGroup<TItem> where TItem : IGroupable
    {

    }

    /// <summary>
    /// Abstract class for Groups of an item.
    /// </summary>
    /// <typeparam name="TItem">The item the group is for</typeparam>
    public abstract class BaseGroup<TItem> : BaseFile, IGroup<TItem> where TItem : IGroupable
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

    /// <summary>
    /// Interface for items which can be in a Minecraft group
    /// </summary>
    public interface IGroupable
    {
        /// <summary>
        /// The name Minecraft uses for the item
        /// </summary>
        string Name { get; }
    }
}
