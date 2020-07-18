using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft
{
    /// <summary>
    /// Class for <see cref="IItemType"/> groups files
    /// </summary>
    public class ItemGroup : BaseGroup<IItemType>, IItemType
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
        protected ItemGroup(bool _, BasePackNamespace packNamespace, string? fileName, List<IItemType> items, bool appendGroup, WriteSetting writeSetting) : base(packNamespace, fileName, items, appendGroup, writeSetting, "item")
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
        public ItemGroup(BasePackNamespace packNamespace, string? fileName, List<IItemType> items, bool appendGroup, WriteSetting writeSetting) : this(true, packNamespace, fileName, items, appendGroup, writeSetting)
        {
            FinishedConstructing();
        }

        /// <summary>
        /// Returns the stream this file is going to use for writing it's file
        /// </summary>
        /// <returns>The stream for this file</returns>
        protected override TextWriter GetStream()
        {
            CreateDirectory("tags/items");
            return PackNamespace.Datapack.FileCreator.CreateWriter(PackNamespace.GetPath() + "tags/items/" + WritePath + ".json");
        }
    }
}
