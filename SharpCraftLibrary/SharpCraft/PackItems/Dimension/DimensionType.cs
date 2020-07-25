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
    public class DimensionType : BaseFile<TextWriter>, IDimensionTypeFile
    {
        /// <summary>
        /// The vanilla overworld dimension
        /// </summary>
        public static readonly IDimensionTypeFile Overworld = new FileMocks.MockDimensionType(MockNamespace.GetMinecraftNamespace(), "overworld");

        /// <summary>
        /// The vanilla nether dimension
        /// </summary>
        public static readonly IDimensionTypeFile Nether = new FileMocks.MockDimensionType(MockNamespace.GetMinecraftNamespace(), "the_nether");

        /// <summary>
        /// The vanilla end dimension
        /// </summary>
        public static readonly IDimensionTypeFile End = new FileMocks.MockDimensionType(MockNamespace.GetMinecraftNamespace(), "the_end");

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
            string minecraftNamespacePath = new MockNamespace(PackNamespace.Datapack, "minecraft").GetPath();
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
}
