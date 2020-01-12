using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCraft
{
    /// <summary>
    /// Class for <see cref="BlockType"/> groups files
    /// </summary>
    public class BlockGroup : BaseGroup<BlockType>, IBlockType
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
        protected BlockGroup(bool _, BasePackNamespace packNamespace, string fileName, List<BlockType> items, bool appendGroup, WriteSetting writeSetting) : base(packNamespace, fileName, items, appendGroup, writeSetting, "block")
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
        public BlockGroup(BasePackNamespace packNamespace, string fileName, List<BlockType> items, bool appendGroup, WriteSetting writeSetting) : this(true, packNamespace, fileName, items, appendGroup, writeSetting)
        {
            FinishedConstructing();
        }

        /// <summary>
        /// Returns <see cref="BaseFile.GetNamespacedName()"/>
        /// </summary>
        public string Name
        {
            get => GetNamespacedName();
        }

        /// <summary>
        /// The value (Used for when converted into a block type)
        /// </summary>
        public object Value => this;

        /// <summary>
        /// Returns the stream this file is going to use for writing it's file
        /// </summary>
        /// <returns>The stream for this file</returns>
        protected override TextWriter GetStream()
        {
            CreateDirectory("tags\\blocks");
            return PackNamespace.Datapack.FileCreator.CreateWriter(PackNamespace.GetPath() + "tags\\blocks\\" + FileName + ".json");
        }

        /// <summary>
        /// implicit converts a <see cref="BlockGroup"/> into a <see cref="BlockType"/> object
        /// </summary>
        /// <param name="group">The group to convert</param>
        public static implicit operator BlockType(BlockGroup group)
        {
            return new BlockType(group);
        }
    }
}
