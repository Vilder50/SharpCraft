using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SharpCraft;

namespace SharpCraftBeginning
{
    /// <summary>
    /// The base class of a writer
    /// </summary>
    public abstract class BaseDatapackWriter
    {
        private Packspace pack;

        /// <summary>
        /// Gets or sets the pack this writer should write to
        /// </summary>
        public Packspace Pack
        {
            get
            {
                return pack;
            }
            set
            {
                if (pack is null)
                {
                    pack = value;
                }
            }
        }

        /// <summary>
        /// Method writing things to <see cref="Pack"/>
        /// </summary>
        public abstract void StartWrite();

        /// <summary>
        /// Gets a list of all required writers that has to finish before the given writer
        /// </summary>
        /// <param name="writer">the type of a sub class of <see cref="BaseDatapackWriter"/></param>
        /// <returns>A list of all required writers</returns>
        public static List<Type> RequiredClassesList(Type writer)
        {
            List<CustomAttributeData> attributes = writer.CustomAttributes.Where(a => a.AttributeType == typeof(RequiredFinishedWriters)).ToList();
            List<Type> returnList = new List<Type>();
            foreach (CustomAttributeData data in attributes)
            {
                returnList.Add((Type)data.ConstructorArguments[0].Value);
            }

            return returnList;
        }
    }

    /// <summary>
    /// An attribute used to specify what writers a writer requires to have finished before it
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class RequiredFinishedWriters : Attribute
    {
        /// <summary>
        /// The required writer
        /// </summary>
        public readonly Type RequiredWriter;
        
        /// <summary>
        /// Creates a new <see cref="RequiredFinishedWriters"/> attribute
        /// </summary>
        /// <param name="requiredWriter">The type of the writer which is required to be done before this writer</param>
        public RequiredFinishedWriters(Type requiredWriter)
        {
            if (requiredWriter is null)
            {
                throw new ArgumentNullException(nameof(requiredWriter) + " may not be null");
            }
            if (!requiredWriter.IsSubclassOf(typeof(BaseDatapackWriter)))
            {
                throw new ArgumentException(nameof(requiredWriter) + " has to be a subclass of " + nameof(BaseDatapackWriter));
            }
            RequiredWriter = requiredWriter;
        }
    }
}
