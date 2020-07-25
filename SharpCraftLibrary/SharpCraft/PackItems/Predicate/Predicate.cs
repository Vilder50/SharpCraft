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
    public class Predicate : BaseFile<TextWriter>, IPredicate
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
        /// Returns the stream this file is going to use for writing it's file
        /// </summary>
        /// <returns>The stream for this file</returns>
        protected override TextWriter GetStream()
        {
            CreateDirectory("predicates");
            return PackNamespace.Datapack.FileCreator.CreateWriter(PackNamespace.GetPath() + "predicates/" + WritePath + ".json");
        }

        /// <summary>
        /// Writes the file
        /// </summary>
        /// <param name="stream">The stream used for writing the file</param>
        protected override void WriteFile(TextWriter stream)
        {
            if (condition is AllCondition andCondition)
            {
                List<string> parts = new List<string>();

                foreach (var innerCondition in andCondition.Conditions) {
                    parts.Add(innerCondition.GetDataString());
                }

                stream.Write("[" + string.Join(",", parts) + "]");
            } 
            else
            {
                stream.Write(condition.GetDataString());
            }
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
}
