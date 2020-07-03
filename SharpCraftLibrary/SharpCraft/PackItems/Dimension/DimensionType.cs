using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SharpCraft.DimensionObjects;

namespace SharpCraft
{
    /// <summary>
    /// Class for dimension type files
    /// </summary>
    public class DimensionType : BaseFile, IDimensionTypeFile
    {
        /// <summary>
        /// The vanilla overworld dimension
        /// </summary>
        public static readonly IDimensionTypeFile Overworld = new EmptyDimensionType(EmptyNamespace.GetMinecraftNamespace(), "overworld");

        /// <summary>
        /// The vanilla nether dimension
        /// </summary>
        public static readonly IDimensionTypeFile Nether = new EmptyDimensionType(EmptyNamespace.GetMinecraftNamespace(), "the_nether");

        /// <summary>
        /// The vanilla end dimension
        /// </summary>
        public static readonly IDimensionTypeFile End = new EmptyDimensionType(EmptyNamespace.GetMinecraftNamespace(), "the_end");

        private DimensionTypeObject dimensionTypeObject = null!;

        /// <summary>
        /// Intializes a new <see cref="DimensionType"/>. Inherite from this constructor.
        /// </summary>
        /// <param name="_">Unused parameter used for specifing you want to use this constructor</param>
        /// <param name="packNamespace">The namespace the type is in</param>
        /// <param name="fileName">The name of the dimension type file</param>
        /// <param name="dimensionTypeObject">The information about the type</param>
        /// <param name="writeSetting">The settings for how to write this file</param>
        protected DimensionType(bool _, BasePackNamespace packNamespace, string? fileName, DimensionTypeObject dimensionTypeObject, WriteSetting writeSetting = WriteSetting.LockedAuto) : base(packNamespace, fileName, writeSetting, "dimension_type")
        {
            DimensionTypeObject = dimensionTypeObject;
        }

        /// <summary>
        /// Intializes a new <see cref="DimensionType"/>
        /// </summary>
        /// <param name="packNamespace">The namespace the type is in</param>
        /// <param name="fileName">The name of the dimensiotn type file</param>
        /// <param name="dimensionTypeObject">The information about the type</param>
        /// <param name="writeSetting">The settings for how to write this file</param>
        public DimensionType(BasePackNamespace packNamespace, string? fileName, DimensionTypeObject dimensionTypeObject, WriteSetting writeSetting = WriteSetting.LockedAuto) : this(true, packNamespace, fileName, dimensionTypeObject, writeSetting)
        {
            FinishedConstructing();
        }

        /// <summary>
        /// The information about the type
        /// </summary>
        public DimensionTypeObject DimensionTypeObject { get => dimensionTypeObject; set => dimensionTypeObject = value ?? throw new ArgumentNullException(nameof(DimensionTypeObject), "DimensionTypeObject may not be null"); }

        /// <summary>
        /// Returns the stream this file is going to use for writing it's file
        /// </summary>
        /// <returns>The stream for this file</returns>
        protected override TextWriter GetStream()
        {
            string minecraftNamespacePath = new EmptyNamespace(PackNamespace.Datapack, "minecraft").GetPath();
            if (WritePath.Contains("/"))
            {
                PackNamespace.Datapack.FileCreator.CreateDirectory(minecraftNamespacePath + "dimension_type/" + PackNamespace.Name + "/" + WritePath.Substring(0, WritePath.LastIndexOf("/")) + "/");
            }
            else
            {
                PackNamespace.Datapack.FileCreator.CreateDirectory(minecraftNamespacePath + "dimension_type/" + PackNamespace.Name + "/" + WritePath);
            }

            return PackNamespace.Datapack.FileCreator.CreateWriter(minecraftNamespacePath + "dimension_type/" + PackNamespace.Name + "/" + WritePath + ".json");
        }

        /// <summary>
        /// Clears the things in the file.
        /// </summary>
        protected override void AfterDispose()
        {
            dimensionTypeObject = null!;
        }

        /// <summary>
        /// Writes the file
        /// </summary>
        /// <param name="stream">The stream used for writing the file</param>
        protected override void WriteFile(TextWriter stream)
        {
            stream.Write(DimensionTypeObject.GetDimensionTypeString());
        }

        /// <summary>
        /// Returns the string used for chosen this dimension type
        /// </summary>
        /// <returns>String for chosing this</returns>
        public string GetDimensionTypeString()
        {
            return "\"" + GetNamespacedName() + "\"";
        }
    }

    /// <summary>
    /// Used for calling dimension types outside this program
    /// </summary>
    public class EmptyDimensionType : IDimensionTypeFile
    {
        /// <summary>
        /// Intializes a new <see cref="EmptyDimension"/>
        /// </summary>
        /// <param name="packNamespace">The namespace the dimension is in</param>
        /// <param name="fileName">The name of the dimension</param>
        public EmptyDimensionType(BasePackNamespace packNamespace, string fileName)
        {
            PackNamespace = packNamespace;
            FileId = fileName;
        }

        /// <summary>
        /// The name of the dimension
        /// </summary>
        public string FileId { get; private set; }

        /// <summary>
        /// The namespace the dimension is in
        /// </summary>
        public BasePackNamespace PackNamespace { get; private set; }

        /// <summary>
        /// Returns the string used for chosen this dimension type
        /// </summary>
        /// <returns>String for chosing this</returns>
        public string GetDimensionTypeString()
        {
            return "\"" + GetNamespacedName() + "\"";
        }

        /// <summary>
        /// Returns the string used for selecting the dimension
        /// </summary>
        /// <returns>The string used for selecting the dimension</returns>
        public string GetNamespacedName()
        {
            return PackNamespace.Name + ":" + FileId;
        }

        /// <summary>
        /// Converts a string of the format NAMESPACE:DIMENSIONTYPE into an <see cref="EmptyDimensionType"/>
        /// </summary>
        /// <param name="dimensionType">The string to convert</param>
        public static implicit operator EmptyDimensionType(string dimensionType)
        {
            string[] parts = dimensionType.Split(':');
            if (parts.Length != 2)
            {
                throw new InvalidCastException("String for creating empty dimension has to contain a single :");
            }
            return new EmptyDimensionType(EmptyDatapack.GetPack().Namespace(parts[0]), parts[1]);
        }
    }
}
