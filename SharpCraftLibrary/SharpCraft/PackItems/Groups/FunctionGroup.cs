﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCraft
{
    /// <summary>
    /// Class for <see cref="IFunction"/> groups files
    /// </summary>
    public class FunctionGroup : BaseGroup<IFunction>, IFunction
    {
        /// <summary>
        /// Intializes a new Group with the given parameters. Inherite from this constructor.
        /// </summary>
        /// <param name="packNamespace">The namespace the group is in</param>
        /// <param name="fileName">The name of the group file</param>
        /// <param name="writeSetting">The settings for how to write this file</param>
        /// <param name="items">The items in this group</param>
        /// <param name="appendGroup">If this group should append other groups of the same type and same name from other datapacks</param>
        /// <param name="_">Unused parameter used for specifing you want to use this constructor</param>
        protected FunctionGroup(bool _, BasePackNamespace packNamespace, string? fileName, List<IFunction> items, bool appendGroup, WriteSetting writeSetting) : base(packNamespace, fileName, items, appendGroup, writeSetting, "function")
        {
            
        }

        /// <summary>
        /// Intializes a new Group with the given parameters
        /// </summary>
        /// <param name="packNamespace">The namespace the group is in</param>
        /// <param name="fileName">The name of the group file</param>
        /// <param name="writeSetting">The settings for how to write this file</param>
        /// <param name="items">The items in this group</param>
        /// <param name="appendGroup">If this group should append other groups of the same type and same name from other datapacks</param>
        public FunctionGroup(BasePackNamespace packNamespace, string? fileName, List<IFunction> items, bool appendGroup, WriteSetting writeSetting) : this(true, packNamespace, fileName, items, appendGroup, writeSetting)
        {
            FinishedConstructing();
        }

        /// <summary>
        /// Returns the stream this file is going to use for writing it's file
        /// </summary>
        /// <returns>The stream for this file</returns>
        protected override TextWriter GetStream()
        {
            CreateDirectory("tags/functions");
            return PackNamespace.Datapack.FileCreator.CreateWriter(PackNamespace.GetPath() + "tags/functions/" + WritePath + ".json");
        }
    }
}
