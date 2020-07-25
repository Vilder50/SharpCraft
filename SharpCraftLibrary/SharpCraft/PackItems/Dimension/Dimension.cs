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
        public static readonly IDimension Overworld = new FileMocks.MockDimension(MockNamespace.GetMinecraftNamespace(), "overworld");

        /// <summary>
        /// The vanilla nether dimension
        /// </summary>
        public static readonly IDimension Nether = new FileMocks.MockDimension(MockNamespace.GetMinecraftNamespace(), "the_nether");

        /// <summary>
        /// The vanilla end dimension
        /// </summary>
        public static readonly IDimension End = new FileMocks.MockDimension(MockNamespace.GetMinecraftNamespace(), "the_end");
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
            string minecraftNamespacePath = new MockNamespace(PackNamespace.Datapack, "minecraft").GetPath();
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
    }
}
