using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SharpCraft.Data;
using SharpCraft.DimensionObjects;

namespace SharpCraft
{
    /// <summary>
    /// Class for dimension files
    /// </summary>
    public class Dimension : BaseFile<TextWriter>, IDimension
    {
        /// <summary>
        /// The vanilla overworld dimension
        /// </summary>
        public static readonly IDimension Overworld = new EmptyDimension(EmptyNamespace.GetMinecraftNamespace(), "overworld");

        /// <summary>
        /// The vanilla nether dimension
        /// </summary>
        public static readonly IDimension Nether = new EmptyDimension(EmptyNamespace.GetMinecraftNamespace(), "the_nether");

        /// <summary>
        /// The vanilla end dimension
        /// </summary>
        public static readonly IDimension End = new EmptyDimension(EmptyNamespace.GetMinecraftNamespace(), "the_end");
        private BaseGenerator generator = null!;
        private IDimensionType dimensionType = null!;

        /// <summary>
        /// Intializes a new <see cref="Dimension"/>. Inherite from this constructor.
        /// </summary>
        /// <param name="_">Unused parameter used for specifing you want to use this constructor</param>
        /// <param name="packNamespace">The namespace the dimension is in</param>
        /// <param name="fileName">The name of the dimension file</param>
        /// <param name="generator">The generator used for generating the terrain for the dimension</param>
        /// <param name="dimensionType">Data about the type of dimension</param>
        /// <param name="writeSetting">The settings for how to write this file</param>
        protected Dimension(bool _, BasePackNamespace packNamespace, string? fileName, BaseGenerator generator, IDimensionType dimensionType, WriteSetting writeSetting = WriteSetting.LockedAuto) : base(packNamespace, fileName, writeSetting, "dimension")
        {
            Generator = generator;
            DimensionType = dimensionType;
        }

        /// <summary>
        /// Intializes a new <see cref="Dimension"/>.
        /// </summary>
        /// <param name="packNamespace">The namespace the dimension is in</param>
        /// <param name="fileName">The name of the dimension file</param>
        /// <param name="generator">The generator used for generating the terrain for the dimension</param>
        /// <param name="dimensionType">Data about the type of dimension</param>
        /// <param name="writeSetting">The settings for how to write this file</param>
        public Dimension(BasePackNamespace packNamespace, string? fileName, BaseGenerator generator, IDimensionType dimensionType, WriteSetting writeSetting = WriteSetting.LockedAuto) : this(true, packNamespace, fileName, generator, dimensionType, writeSetting)
        {
            FinishedConstructing();
        }

        /// <summary>
        /// The generator used for generating the terrain for the dimension
        /// </summary>
        public BaseGenerator Generator { get => generator; set => generator = value ?? throw new ArgumentNullException(nameof(Generator), "Generator may not be null"); }

        /// <summary>
        /// Data about the type of dimension
        /// </summary>
        public IDimensionType DimensionType { get => dimensionType; set => dimensionType = value ?? throw new ArgumentNullException(nameof(DimensionType), "DimensionType may not be null"); }

        /// <summary>
        /// Returns the stream this file is going to use for writing it's file
        /// </summary>
        /// <returns>The stream for this file</returns>
        protected override TextWriter GetStream()
        {
            string minecraftNamespacePath = new EmptyNamespace(PackNamespace.Datapack, "minecraft").GetPath();
            if (WritePath.Contains("/"))
            {
                PackNamespace.Datapack.FileCreator.CreateDirectory(minecraftNamespacePath + "dimension/" + PackNamespace.Name + "/" + WritePath.Substring(0, WritePath.LastIndexOf("/")) + "/");
            }
            else
            {
                PackNamespace.Datapack.FileCreator.CreateDirectory(minecraftNamespacePath + "dimension/" + PackNamespace.Name + "/");
            }

            return PackNamespace.Datapack.FileCreator.CreateWriter(minecraftNamespacePath + "dimension/" + PackNamespace.Name + "/" + WritePath + ".json");
        }

        /// <summary>
        /// Clears the things in the file.
        /// </summary>
        protected override void AfterDispose()
        {
            generator = null!;
            dimensionType = null!;
        }

        /// <summary>
        /// Writes the file
        /// </summary>
        /// <param name="stream">The stream used for writing the file</param>
        protected override void WriteFile(TextWriter stream)
        {
            string type = dimensionType.GetDimensionTypeString();
            if (dimensionType is DimensionTypeObject asObject)
            {
                type = new DimensionType(PackNamespace, "sharpgen/" + FileId, asObject).GetDimensionTypeString();
            }

            stream.Write("{");

            stream.Write("\"generator\":" + generator.GetDataString());
            stream.Write(",\"type\":" + type);

            stream.Write("}");
        }

        /// <summary>
        /// Converts this dimension into a <see cref="DataPartTag"/>
        /// </summary>
        /// <param name="asType">Not in use</param>
        /// <param name="extraConversionData">Not in use</param>
        /// <returns>the made <see cref="DataPartTag"/></returns>
        public DataPartTag GetAsTag(ID.NBTTagType? asType, object?[] extraConversionData)
        {
            return new DataPartTag(GetNamespacedName());
        }
    }

    /// <summary>
    /// Used for calling dimensions outside this program
    /// </summary>
    public class EmptyDimension : IDimension
    {
        /// <summary>
        /// Intializes a new <see cref="EmptyDimension"/>
        /// </summary>
        /// <param name="packNamespace">The namespace the dimension is in</param>
        /// <param name="fileName">The name of the dimension</param>
        public EmptyDimension(BasePackNamespace packNamespace, string fileName)
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
        /// Converts this dimension into a <see cref="DataPartTag"/>
        /// </summary>
        /// <param name="asType">Not in use</param>
        /// <param name="extraConversionData">Not in use</param>
        /// <returns>the made <see cref="DataPartTag"/></returns>
        public DataPartTag GetAsTag(ID.NBTTagType? asType, object?[] extraConversionData)
        {
            return new DataPartTag(GetNamespacedName());
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
        /// Converts a string of the format NAMESPACE:DIMENSION into an <see cref="EmptyDimension"/>
        /// </summary>
        /// <param name="dimension">The string to convert</param>
        public static implicit operator EmptyDimension(string dimension)
        {
            string[] parts = dimension.Split(':');
            if (parts.Length != 2)
            {
                throw new InvalidCastException("String for creating empty dimension has to contain a single :");
            }
            return new EmptyDimension(EmptyDatapack.GetPack().Namespace(parts[0]), parts[1]);
        }
    }
}
