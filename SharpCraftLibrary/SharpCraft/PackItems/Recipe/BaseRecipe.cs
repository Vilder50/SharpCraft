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
    /// Base class for recipe files
    /// </summary>
    public abstract class BaseRecipe : BaseFile, IRecipe, IConvertableToDataTag
    {
        /// <summary>
        /// Intializes a new <see cref="BaseRecipe"/>
        /// </summary>
        /// <param name="packNamespace">The namespace the recipe is in</param>
        /// <param name="fileName">The name of the recipe file</param>
        /// <param name="writeSetting">The settings for how to write this file</param>
        /// <param name="group">The name of the recipe group the recipe is in. Leave null for no group.</param>
        /// <param name="recipeType">The type of recipe file</param>
        protected BaseRecipe(BasePackNamespace packNamespace, string fileName, string group, WriteSetting writeSetting, string recipeType) : base(packNamespace, fileName, writeSetting, "recipe")
        {
            Type = recipeType;
            Group = group;
        }

        /// <summary>
        /// The type of recipe
        /// </summary>
        public string Type { get; private set; }

        /// <summary>
        /// The name of the recipe group the recipe is in. Leave null for no group.
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// Returns the stream this file is going to use for writing it's file
        /// </summary>
        /// <returns>The stream for this file</returns>
        protected override TextWriter GetStream()
        {
            CreateDirectory("recipes");
            return PackNamespace.Datapack.FileCreator.CreateWriter(PackNamespace.GetPath() + "recipes\\" + FileName + ".json");
        }

        /// <summary>
        /// Writes the beginning of the file
        /// </summary>
        /// <param name="stream">The stream to write to</param>
        protected void WriteFileStart(TextWriter stream)
        {
            stream.Write("{\"type\":\"minecraft:"+Type+"\"");

            if (!string.IsNullOrWhiteSpace(Group))
            {
                stream.Write(",\"group\":\""+Group+"\"");
            }
        }

        /// <summary>
        /// Writes the end of the file
        /// </summary>
        /// <param name="stream">The stream to write to</param>
        protected void WriteFileEnd(TextWriter stream)
        {
            stream.Write("}");
        }

        /// <summary>
        /// Returns a string used for specifieng the item in the ingredient list
        /// </summary>
        /// <param name="item">The item to get the string for</param>
        /// <returns>A string used for specifieng the item in the ingredient list</returns>
        protected static string GetItemCompound(ItemType item)
        {
            if (item.Name.Contains("#"))
            {
                return "{\"tag\":\""+item.Name.Replace("#","")+"\"}";
            }
            else
            {
                return "{\"item\":\"" + item.Name + "\"}";
            }
        }

        /// <summary>
        /// Converts this recipe into a <see cref="DataPartTag"/>
        /// </summary>
        /// <param name="asType">Not in use</param>
        /// <param name="extraConversionData">Not in use</param>
        /// <returns>the made <see cref="DataPartTag"/></returns>
        public DataPartTag GetAsTag(ID.NBTTagType? asType, object[] extraConversionData)
        {
            return new DataPartTag(GetNamespacedName());
        }

        /// <summary>
        /// Clears the things in the file.
        /// </summary>
        protected override void AfterDispose()
        {
            Type = null;
            Group = null;
        }
    }

    /// <summary>
    /// Used for giving recipes outside this program
    /// </summary>
    public class EmptyRecipe : IRecipe, IConvertableToDataTag
    {
        /// <summary>
        /// Intializes a new <see cref="EmptyRecipe"/>
        /// </summary>
        /// <param name="packNamespace">The namespace the recipe is in</param>
        /// <param name="fileName">The name of the recipe</param>
        public EmptyRecipe(BasePackNamespace packNamespace, string fileName)
        {
            PackNamespace = packNamespace;
            FileName = fileName;
        }

        /// <summary>
        /// The name of the recipe
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// The namespace the recipe is in
        /// </summary>
        public BasePackNamespace PackNamespace { get; private set; }

        /// <summary>
        /// Returns the string used for evoking this recipe
        /// </summary>
        /// <returns>The string used for evoking this recipe</returns>
        public string GetNamespacedName()
        {
            return PackNamespace.Name + ":" + FileName;
        }

        /// <summary>
        /// Converts this recipe into a <see cref="DataPartTag"/>
        /// </summary>
        /// <param name="asType">Not in use</param>
        /// <param name="extraConversionData">Not in use</param>
        /// <returns>the made <see cref="DataPartTag"/></returns>
        public DataPartTag GetAsTag(ID.NBTTagType? asType, object[] extraConversionData)
        {
            return new DataPartTag(GetNamespacedName());
        }

        /// <summary>
        /// Converts a string of the format NAMESPACE:RECIPE into an <see cref="EmptyRecipe"/>
        /// </summary>
        /// <param name="recipe">The string to convert</param>
        public static implicit operator EmptyRecipe(string recipe)
        {
            string[] parts = recipe.Split(':');
            if (parts.Length != 2)
            {
                throw new InvalidCastException("String for creating empty recipe has to contain a single :");
            }
            return new EmptyRecipe(EmptyDatapack.GetPack().Namespace(parts[0]), parts[1]);
        }
    }
}
