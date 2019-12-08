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
    /// Class for <see cref="IFunction"/> groups files
    /// </summary>
    public class ItemGroup : BaseGroup<ItemType>, IItemType, IConvertableToDataTag
    {
        /// <summary>
        /// Intializes a new Group with the given parameters
        /// </summary>
        /// <param name="packNamespace">The namespace the group is in</param>
        /// <param name="fileName">The name of the group file</param>
        /// <param name="writeSetting">The settings for how to write this file</param>
        /// <param name="items">The items in this group</param>
        /// <param name="appendGroup">If this group should append other groups of the same type and same name from other datapacks</param>
        public ItemGroup(BasePackNamespace packNamespace, string fileName, List<ItemType> items, bool appendGroup, WriteSetting writeSetting) : base(packNamespace, fileName, items, appendGroup, writeSetting)
        {
            
        }

        /// <summary>
        /// Returns <see cref="BaseFile.GetNamespacedName()"/>
        /// </summary>
        public string Name
        {
            get => GetNamespacedName();
        }

        /// <summary>
        /// Returns the stream this file is going to use for writing it's file
        /// </summary>
        /// <returns>The stream for this file</returns>
        protected override TextWriter GetStream()
        {
            CreateDirectory("tags\\items");
            return PackNamespace.Datapack.FileCreator.CreateWriter(PackNamespace.GetPath() + "tags\\items\\" + FileName + ".json");
        }

        /// <summary>
        /// implicit converts a <see cref="ItemGroup"/> into a <see cref="ItemType"/> object
        /// </summary>
        /// <param name="group">The group to convert</param>
        public static implicit operator ItemType(ItemGroup group)
        {
            return new ItemType(group);
        }

        /// <summary>
        /// Converts this <see cref="ItemType"/> into a <see cref="DataPartTag"/>
        /// </summary>
        /// <param name="asType">The type of tag to get. Set to null or <see cref="ID.NBTTagType.TagString"/></param>
        /// <param name="extraConversionData">0: If true returns the string without the #</param>
        /// <returns>This <see cref="ItemType"/> as a <see cref="DataPartTag"/></returns>
        public DataPartTag GetAsTag(ID.NBTTagType? asType, object[] extraConversionData)
        {
            if (extraConversionData.Length == 1)
            {
                bool? ignoreGroupChar = extraConversionData[0] as bool?;
                if (ignoreGroupChar is null)
                {
                    throw new ArgumentException("Param 0 has to be bool");
                }
                if (ignoreGroupChar.Value)
                {
                    return new DataPartTag(Name.Replace("#",""));
                }
            }
            if (!(asType is null) && asType != ID.NBTTagType.TagString)
            {
                throw new InvalidCastException("Cannot convert ItemType into the given type");
            }

            return new DataPartTag(Name);
        }
    }
}
