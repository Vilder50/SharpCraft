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
        private PredicateCondition? thisCondition;
        private BaseCondition condition = null!;

        /// <summary>
        /// Intializes a new <see cref="Predicate"/>. Inherite from this constructor.
        /// </summary>
        /// <param name="packNamespace">The namespace the predicate is in</param>
        /// <param name="fileName">The name of the predicate file</param>
        /// <param name="writeSetting">The settings for how to write this file</param>
        /// <param name="condition">The predicate to test for</param>
        /// <param name="_">Unused parameter used for specifing you want to use this constructor</param>
        protected Predicate(bool _, BasePackNamespace packNamespace, string? fileName, BaseCondition condition, WriteSetting writeSetting = WriteSetting.LockedAuto) : base(packNamespace, fileName, writeSetting, "predicate")
        {
            Condition = condition;
        }

        /// <summary>
        /// Intializes a new <see cref="Predicate"/>
        /// </summary>
        /// <param name="packNamespace">The namespace the predicate is in</param>
        /// <param name="fileName">The name of the predicate file</param>
        /// <param name="writeSetting">The settings for how to write this file</param>
        /// <param name="condition">The predicate to test for</param>
        public Predicate(BasePackNamespace packNamespace, string? fileName, BaseCondition condition, WriteSetting writeSetting = WriteSetting.LockedAuto) : this(true, packNamespace, fileName, condition, writeSetting)
        {
            FinishedConstructing();
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
        public DataPartTag GetAsTag(ID.NBTTagType? asType, object?[]? extraConversionData)
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
            return PackNamespace.Datapack.FileCreator.CreateWriter(PackNamespace.GetPath() + "predicates\\" + WritePath + ".json");
        }

        /// <summary>
        /// Writes the file
        /// </summary>
        /// <param name="stream">The stream used for writing the file</param>
        protected override void WriteFile(TextWriter stream)
        {
            stream.Write(condition.GetDataString());
        }

        /// <summary>
        /// Clears the things in the file.
        /// </summary>
        protected override void AfterDispose()
        {
            condition = null!;
        }

        /// <summary>
        /// Returns a condition checking this predicate
        /// </summary>
        /// <returns>A condition checking this predicate</returns>
        public PredicateCondition GetCondition()
        {
            thisCondition ??= new PredicateCondition(this);
            return thisCondition;
        }
    }

    /// <summary>
    /// Used for calling predicates outside this program
    /// </summary>
    public class EmptyPredicate : IPredicate
    {
        private PredicateCondition? thisCondition;

        /// <summary>
        /// Intializes a new <see cref="EmptyPredicate"/>
        /// </summary>
        /// <param name="packNamespace">The namespace the predicate is in</param>
        /// <param name="fileName">The name of the predicate</param>
        public EmptyPredicate(BasePackNamespace packNamespace, string fileName)
        {
            PackNamespace = packNamespace;
            FileId = fileName;
        }

        /// <summary>
        /// The name of the predicate
        /// </summary>
        public string FileId { get; private set; }

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
            return PackNamespace.Name + ":" + FileId;
        }

        /// <summary>
        /// Converts this predicate into a <see cref="DataPartTag"/>
        /// </summary>
        /// <param name="asType">Unused</param>
        /// <param name="extraConversionData">Unused</param>
        /// <returns>This predicate into a <see cref="DataPartTag"/></returns>
        public DataPartTag GetAsTag(ID.NBTTagType? asType, object?[]? extraConversionData)
        {
            return new DataPartTag(GetNamespacedName());
        }

        /// <summary>
        /// Converts a string of the format NAMESPACE:PREDICATE into an <see cref="EmptyPredicate"/>
        /// </summary>
        /// <param name="predicate">The string to convert</param>
        public static implicit operator EmptyPredicate(string predicate)
        {
            string[] parts = predicate.Split(':');
            if (parts.Length != 2)
            {
                throw new InvalidCastException("String for creating empty predicate has to contain a single :");
            }
            return new EmptyPredicate(EmptyDatapack.GetPack().Namespace(parts[0]), parts[1]);
        }

        /// <summary>
        /// Returns a condition checking this predicate
        /// </summary>
        /// <returns>A condition checking this predicate</returns>
        public PredicateCondition GetCondition()
        {
            thisCondition ??= new PredicateCondition(this);
            return thisCondition;
        }
    }
}
