using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCraft
{
    /// <summary>
    /// Used for calling advancements outside this program
    /// </summary>
    public class EmptyAdvancement : IAdvancement
    {
        /// <summary>
        /// Intializes a new <see cref="EmptyAdvancement"/>
        /// </summary>
        /// <param name="packNamespace">The namespace the advancement is in</param>
        /// <param name="fileName">The name of the advancement</param>
        public EmptyAdvancement(BasePackNamespace packNamespace, string fileName)
        {
            PackNamespace = packNamespace;
            FileName = fileName;
        }

        /// <summary>
        /// The name of the advancement
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// The namespace the advancement is in
        /// </summary>
        public BasePackNamespace PackNamespace { get; private set; }

        /// <summary>
        /// Returns the string used for evoking this advancement
        /// </summary>
        /// <returns>The string used for evoking this advancement</returns>
        public string GetNamespacedName()
        {
            return PackNamespace.Name + ":" + FileName;
        }

        /// <summary>
        /// Converts a string of the format NAMESPACE:ADVANCEMENT into an <see cref="EmptyAdvancement"/>
        /// </summary>
        /// <param name="advancement">The string to convert</param>
        public static implicit operator EmptyAdvancement(string advancement)
        {
            string[] parts = advancement.Split(':');
            if (parts.Length != 2)
            {
                throw new InvalidCastException("String for creating empty advancement has to contain a single :");
            }
            return new EmptyAdvancement(EmptyDatapack.GetPack().Namespace(parts[0]),parts[1]);
        }
    }
}
