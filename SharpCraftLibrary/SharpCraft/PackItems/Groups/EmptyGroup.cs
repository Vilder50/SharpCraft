using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft
{ 
    /// <summary>
    /// Used for calling groups outside this program
    /// </summary>
    public class EmptyGroup<TItem> : IBlockType, IEntityType, IItemType, ILiquidType, IFunction, IGroup<TItem> where TItem : IGroupable
    {
        /// <summary>
        /// Intializes a new <see cref="EmptyGroup{TItem}"/>
        /// </summary>
        /// <param name="packNamespace">The namespace the group is in</param>
        /// <param name="fileName">The name of the group</param>
        public EmptyGroup(BasePackNamespace packNamespace, string fileName)
        {
            PackNamespace = packNamespace;
            FileName = fileName;
        }

        /// <summary>
        /// The name of the group
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// The namespace the group is in
        /// </summary>
        public BasePackNamespace PackNamespace { get; private set; }

        /// <summary>
        /// This
        /// </summary>
        public object Value => this;

        /// <summary>
        /// The name of this group
        /// </summary>
        public string Name => GetNamespacedName();

        /// <summary>
        /// The name of this group
        /// </summary>
        public string FileId => FileName;

        /// <summary>
        /// Converts this recipe into a <see cref="Data.DataPartTag"/>
        /// </summary>
        /// <param name="asType">Not in use</param>
        /// <param name="extraConversionData">Not in use</param>
        /// <returns>the made <see cref="Data.DataPartTag"/></returns>
        public DataPartTag GetAsTag(ID.NBTTagType? asType, object?[] extraConversionData)
        {
            return new DataPartTag(GetNamespacedName());
        }

        /// <summary>
        /// Returns the string used for refering this group
        /// </summary>
        /// <returns>The string used for refering this group</returns>
        public string GetNamespacedName()
        {
            return "#" + PackNamespace.Name + ":" + FileName;
        }
    }
}
