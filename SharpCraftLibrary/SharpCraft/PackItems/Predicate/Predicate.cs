using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Conditions;
using SharpCraft.Data;

namespace SharpCraft
{
    /// <summary>
    /// Class for predicate files
    /// </summary>
    public class Predicate : BaseFile, IPredicate
    {
        private BaseCondition condition;

        /// <summary>
        /// Intializes a new <see cref="Predicate"/>
        /// </summary>
        /// <param name="packNamespace">The namespace the predicate is in</param>
        /// <param name="fileName">The name of the predicate file</param>
        /// <param name="writeSetting">The settings for how to write this file</param>
        /// <param name="condition">The predicate to test for</param>
        public Predicate(BasePackNamespace packNamespace, string fileName, BaseCondition condition, WriteSetting writeSetting = WriteSetting.LockedAuto) : base(packNamespace, fileName, writeSetting, "predicate")
        {
            Condition = condition;
            if (IsAuto())
            {
                WriteFile(GetStream());
                Dispose();
            }
        }

        /// <summary>
        /// The condition to test for
        /// </summary>
        public BaseCondition Condition { get => condition; set => condition = value ?? throw new ArgumentNullException(nameof(Condition), "Condition may not be null"); }

        /// <summary>
        /// Converts this predicate into a <see cref="DataPartTag"/>
        /// </summary>
        /// <param name="asType">Unused</param>
        /// <param name="extraConversionData">Unused</param>
        /// <returns>This predicate into a <see cref="DataPartTag"/></returns>
        public DataPartTag GetAsTag(ID.NBTTagType? asType, object[] extraConversionData)
        {
            return new DataPartTag(GetNamespacedName());
        }

        /// <summary>
        /// Returns the stream this file is going to use for writing it's file
        /// </summary>
        /// <returns>The stream for this file</returns>
        protected override TextWriter GetStream()
        {
            CreateDirectory("predicates");
            return PackNamespace.Datapack.FileCreator.CreateWriter(PackNamespace.GetPath() + "predicates\\" + FileName + ".json");
        }

        /// <summary>
        /// Writes the file
        /// </summary>
        /// <param name="stream">The stream used for writing the file</param>
        protected override void WriteFile(TextWriter stream)
        {
            stream.Write(condition.GetDataString());
        }
    }

    /// <summary>
    /// Used for calling predicates outside this program
    /// </summary>
    public class EmptyPredicate : IPredicate
    {
        /// <summary>
        /// Intializes a new <see cref="EmptyPredicate"/>
        /// </summary>
        /// <param name="packNamespace">The namespace the predicate is in</param>
        /// <param name="fileName">The name of the predicate</param>
        public EmptyPredicate(BasePackNamespace packNamespace, string fileName)
        {
            PackNamespace = packNamespace;
            FileName = fileName;
        }

        /// <summary>
        /// The name of the predicate
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// The namespace the predicate is in
        /// </summary>
        public BasePackNamespace PackNamespace { get; private set; }

        /// <summary>
        /// Returns the string used for checking the predicate
        /// </summary>
        /// <returns>The string used for checking the predicate</returns>
        public string GetNamespacedName()
        {
            return PackNamespace.Name + ":" + FileName;
        }

        /// <summary>
        /// Converts this predicate into a <see cref="DataPartTag"/>
        /// </summary>
        /// <param name="asType">Unused</param>
        /// <param name="extraConversionData">Unused</param>
        /// <returns>This predicate into a <see cref="DataPartTag"/></returns>
        public DataPartTag GetAsTag(ID.NBTTagType? asType, object[] extraConversionData)
        {
            return new DataPartTag(GetNamespacedName());
        }
    }
}
